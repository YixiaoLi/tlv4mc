﻿using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Linq;
using System.Text;
using System.IO;
using NU.OJL.MPRTOS.TLV.Base;

namespace NU.OJL.MPRTOS.TLV.Core
{
	public class TraceLogGenerator
	{
		private readonly string[] _convertFunction = new string[] { "COUNT", "EXIST", "ATTR", "NAME", "DISPLAYNAME" };
		private string _traceLogFilePath;
		private ResourceData _resourceData;
		public Action<int, string> _constructProgressReport = null;
		private int _progressFrom = 0;
		private int _progressTo = 0;

		public TraceLogGenerator(string traceLogFilePath, ResourceData resourceData, Action<int, string> ConstructProgressReport, int progressFrom, int progressTo)
		{
			_constructProgressReport = ConstructProgressReport;
			_traceLogFilePath = traceLogFilePath;
			_resourceData = resourceData;
			_progressFrom = progressFrom;
			_progressTo = progressTo;
		}

		public TraceLogData Generate()
		{
			Dictionary<string, Json> dic = new Dictionary<string, Json>();

			string[] target = _resourceData.ConvertRules.ToArray();

			string[] convertRulePaths = Directory.GetFiles(ApplicationData.Setting.ConvertRulesDirectoryPath, "*." + Properties.Resources.ConvertRuleFileExtension);
			// トレースログ変換ファイルを開きJsonValueでデシリアライズ
			// ファイルが複数ある場合を想定している
			foreach (string s in convertRulePaths)
			{
				Json json = new Json().Parse(File.ReadAllText(s));
				foreach (KeyValuePair<string, Json> j in json.GetKeyValuePairEnumerator())
				{
					if (target.Contains(j.Key))
					{
						foreach (KeyValuePair<string, Json> _j in j.Value.GetKeyValuePairEnumerator())
						{
							dic.Add(_j.Key, _j.Value);
						}
					}
				}
			}

			TraceLogData t = new TraceLogData(_resourceData);

			// トレースログを一行ずつ調べTraceLogクラスに変換しTraceLogListに追加していく
			string[] logs = File.ReadAllLines(_traceLogFilePath);
			float i = 1;
			float max = logs.Length;
			foreach (string s in logs)
			{
				if (_constructProgressReport != null)
					_constructProgressReport((int)(((i / max) * (float)(_progressTo - _progressFrom)) + (float)_progressFrom), "トレースログを共通形式へ変換中 " + i + "/" + max + " 行目...");

				foreach (KeyValuePair<string, Json> kvp in dic)
				{
					if (Regex.IsMatch(s, kvp.Key))
					{
						addTraceLog(s, kvp.Key, kvp.Value, t);
					}
				}
				i++;
			}

			return t;
		}

		/// <summary>
		/// 読み込んだログがパターンにマッチした場合に変換してログを追加する
		/// </summary>
		/// <param name="log">読み込むログ</param>
		/// <param name="pattern">パターン</param>
		/// <param name="value">変換値がValue（Jsonでいうところの）</param>
		/// <param name="traceLogManager">追加先</param>
		private void addTraceLog(string log, string pattern, Json value, TraceLogData traceLogData)
		{
			if (value.IsArray)
			{
				addTraceLogAsArray(log, pattern, value, traceLogData);
			}
			else if (value.IsObject)
			{
				addTraceLogAsObject(log, pattern, value, traceLogData);
			}
			else
			{
				// valueがstringのときログを置換して追加
				string s = Regex.Replace(log, pattern, value);
				s = applyConvertFunc(traceLogData, s);

				traceLogData.Add(new TraceLog(s));
			}
		}

		/// <summary>
		/// 読み込んだログがパターンにマッチした場合に変換してログを追加する。変換値
		/// </summary>
		/// <param name="log">読み込むログ</param>
		/// <param name="pattern">パターン</param>
		/// <param name="value">変換値がArray（Jsonでいうところの）</param>
		/// <param name="traceLogManager">追加先</param>
		private void addTraceLogAsArray(string log, string pattern, List<Json> value, TraceLogData traceLogData)
		{
			foreach (Json j in value)
			{
				addTraceLog(log, pattern, j, traceLogData);
			}
		}

		/// <summary>
		/// 読み込んだログがパターンにマッチした場合に変換してログを追加する。変換値
		/// </summary>
		/// <param name="log">読み込むログ</param>
		/// <param name="condition">パターン</param>
		/// <param name="value">変換値がObject（Jsonでいうところの）</param>
		/// <param name="traceLogManager">追加先</param>
		private void addTraceLogAsObject(string log, string pattern, Dictionary<string, Json> value, TraceLogData traceLogData)
		{
			foreach (KeyValuePair<string, Json> kvp in value)
			{
				string condition = Regex.Replace(log, pattern, kvp.Key);

				condition = applyConvertFunc(traceLogData, condition);

				foreach (Match m in Regex.Matches(condition, @"\s*(\[\s*[^\]]+\s*\])?\s*[^\[\]\(\)\.\s]+\s*(\s*\([^\)]+\)\s*)?\s*\.\s*[^=!<>\(]+\s*"))
				{
					string val;
					try
					{
						val = traceLogData.GetAttributeValue(m.Value);
					}
					catch (Exception e)
					{
						throw new Exception("リソース条件式が異常です。\n" + "\"" + m.Value + "\"\n" + e.Message);
					}
					condition = Regex.Replace(condition, Regex.Escape(m.Value), val);
				}

				bool result;

				try
				{
					result = ConditionExpression.Result(condition);
				}
				catch (Exception e)
				{
					throw new Exception("ログ条件式が異常です。\n" + "\"" + kvp.Key + "\"\n" + e.Message);
				}

				if (result)
				{
					addTraceLog(log, pattern, kvp.Value, traceLogData);
				}
			}
		}

		private string applyConvertFunc(TraceLogData traceLogData, string condition)
		{
			foreach (string func in _convertFunction)
			{
				foreach (Match m in Regex.Matches(condition, func + @"{(?<condition>\s*(\[\s*[^\]]+\s*\])?\s*[^\[\]\(\)\.\s]+\s*(\s*\([^\)]+\)\s*)?\s*(\.\s*[^=!<>\(]+)?\s*)}"))
				{
					string val = calcConvertFunc(func, m.Groups["condition"].Value, traceLogData);
					condition = Regex.Replace(condition, Regex.Escape(m.Value), val);
				}
			}
			return condition;
		}

		private string calcConvertFunc(string func, string condition, TraceLogData traceLogData)
		{
			string result;
			switch (func)
			{
				case "COUNT":
					try
					{
						result = traceLogData.GetObject(condition).Count().ToString();
					}
					catch (Exception e)
					{
						throw new Exception("リソース条件式が異常です。\n" + "\"" + condition + "\"\n" + e.Message);
					}
					break;
				case "EXIST":
					try
					{
						result = traceLogData.GetObject(condition).Count() != 0 ? "True" : "False";
					}
					catch (Exception e)
					{
						throw new Exception("リソース条件式が異常です。\n" + "\"" + condition + "\"\n" + e.Message);
					}
					break;
				case "ATTR":
					try
					{
						result = traceLogData.GetAttributeValue(condition).ToString();
					}
					catch (Exception e)
					{
						throw new Exception("リソース条件式が異常です。\n" + "\"" + "ATTR{" + condition + "}" + "\"\n" + e.Message);
					}
					break;
				case "NAME":
					try
					{
						result = traceLogData.GetAttributeValue(condition + "._name").ToString();
					}
					catch (Exception e)
					{
						throw new Exception("リソース条件式が異常です。\n" + "\"" + "NAME{" + condition + "}" + "\"\n" + e.Message);
					}
					break;
				case "DISPLAYNAME":
					try
					{
						result = traceLogData.GetAttributeValue(condition + "._displayName").ToString();
					}
					catch (Exception e)
					{
						throw new Exception("リソース条件式が異常です。\n" + "\"" + "NAME{" + condition + "}" + "\"\n" + e.Message);
					}
					break;
				default:
					throw new Exception(func + "：未知の関数です。");
			}
			return result;
		}

	}
}