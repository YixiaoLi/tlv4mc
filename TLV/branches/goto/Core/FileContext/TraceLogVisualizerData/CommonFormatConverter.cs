﻿using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.IO;
using NU.OJL.MPRTOS.TLV.Base;
using System;
using System.Threading;

namespace NU.OJL.MPRTOS.TLV.Core
{
	/// <summary>
	/// 共通形式トレースログへ変換するためのコンバータ
	/// コンストラクタはプライベートとなっているのでGetInstanceメソッドを使ってインスタンスを得る
	/// </summary>
	public class CommonFormatConverter
	{
		private readonly string[] _convertFunction = new string[] { "COUNT","EXIST","ATTR" };
		private ResourceData _resourceData;
		private VisualizeData _visualizeData;
		private TraceLog _traceLog;
		public Action<int, string> _constructProgressReport = null;
		private int _from = 0;
		private int _to = 100;

		/// <summary>
		/// <c>CommonFormatConverter</c>のコンストラクタ
		/// </summary>
		public CommonFormatConverter(string resourceFilePath, string traceLogFilePath, Action<int, string> ConstructProgressReport)
		{
			_constructProgressReport = ConstructProgressReport;

			if (_constructProgressReport != null)
				_constructProgressReport(0, "リソースデータを生成中");
			_to = 10;
			try
			{
				_resourceData = new ResourceData().Parse(File.ReadAllText(resourceFilePath));
			}
			catch (Exception _e)
			{
				throw new Exception("リソースデータの生成に失敗しました。\nリソースファイルの記述に誤りがあります。\n" + _e.Message);
			}

			if (_constructProgressReport != null)
				_constructProgressReport(_to, "トレースログデータを生成中");
			_from = _to;
			_to = 90;
			try
			{
				_traceLog = getTraceLog(traceLogFilePath, _resourceData);
			}
			catch (Exception _e)
			{
				throw new Exception("トレースログデータの生成に失敗しました。\nトレースログ変換ルールファイルの記述に誤りがあります。\n" + _e.Message);
			}

			if (_constructProgressReport != null)
				_constructProgressReport(_to, "可視化データを生成中");
			_from = _to;
			_to = 100;
			
			try
			{
				_visualizeData = getVisualizeData(_resourceData);
			}
			catch (Exception _e)
			{
				throw new Exception("可視化データの生成に失敗しました。\n可視化ルールファイルの記述に誤りがあります。\n" + _e.Message);
			}

			if (_constructProgressReport != null)
				_constructProgressReport(_to, "初期化中");
		}

		/// <summary>
		/// <c>CommonFormatConverter</c>のコンストラクタ
		/// </summary>
		public CommonFormatConverter(string resourceFilePath, string traceLogFilePath)
		{
			_resourceData = new ResourceData().Parse(File.ReadAllText(resourceFilePath));

			_traceLog = getTraceLog(traceLogFilePath, _resourceData);

			_visualizeData = getVisualizeData(_resourceData);

		}

		/// <summary>
		/// リソースリストを得る
		/// </summary>
		/// <param name="resourceFilePath">リソースリストのパス</param>
		/// <returns>リソースリスト</returns>
		public ResourceData ResourceData
		{
			get
			{
				return _resourceData;
			}
		}

		/// <summary>
		/// トレースログファイルを共通形式へ変換する
		/// </summary>
		/// <param name="traceLogFilePath">変換する前のトレースログファイルのパス</param>
		/// <returns>変換後のトレースログファイルの内容の文字列</returns>
		public TraceLog TraceLog
		{
			get
			{
				return _traceLog;
			}
		}

		public VisualizeData VisualizeData
		{
			get { return _visualizeData; }
		}

		private VisualizeData getVisualizeData(ResourceData resourceData)
		{
			VisualizeData visualizeData = new VisualizeData();

			foreach (KeyValuePair<string, ResourceType> resh in resourceData.ResourceHeader)
			{
				Dictionary<string, string> attrs = new Dictionary<string, string>();
				foreach (KeyValuePair<string, AttributeType> attr in resh.Value.Attributes)
				{
					attrs.Add(attr.Key, attr.Value.VisualizeRule);
				}
				Dictionary<string, string> bhvrs = new Dictionary<string, string>();
				foreach (KeyValuePair<string, Behavior> bhvr in resh.Value.Behaviors)
				{
					bhvrs.Add(bhvr.Key, bhvr.Value.VisualizeRule);
				}

				visualizeData.ApplyRules.Add(resh.Key, new ApplyRule(attrs, bhvrs));
			}

			string[] visualizeRuleFilePaths = Directory.GetFiles(ApplicationDatas.Setting["VisualizeRulesDirectoryPath"], "*." + Properties.Resources.VisualizeRuleFileExtension);

			foreach (string s in visualizeRuleFilePaths)
			{
				VisualizeData vd = ApplicationFactory.JsonSerializer.Deserialize<VisualizeData>(File.ReadAllText(s));

				foreach (KeyValuePair<string, VisualizeRule> kvp in vd.VisualizeRules)
				{
					bool flag = false;
					foreach (KeyValuePair<string, ApplyRule> _kvp in visualizeData.ApplyRules)
					{
						bool f = false;
						foreach (string a in _kvp.Value.Attribute.Values)
						{
							if (kvp.Key == a)
							{
								f = true;
								break;
							}
						}
						foreach (string b in _kvp.Value.Behavior.Values)
						{
							if (kvp.Key == b)
							{
								f = true;
								break;
							}
						}
						if (f)
						{
							flag = true;
							break;
						}
					}
					if (flag)
					{
						visualizeData.VisualizeRules.Add(kvp.Key, kvp.Value);
					}
				}
				foreach (KeyValuePair<string, Shapes> kvp in vd.Shapes)
				{
					bool flag = false;
					foreach (KeyValuePair<string, VisualizeRule> _kvp in visualizeData.VisualizeRules)
					{
						bool f = false;
						if (_kvp.Value.IsMapped)
						{
							foreach (KeyValuePair<string, string> v in _kvp.Value)
							{
								if (kvp.Key == v.Value)
								{
									f = true;
									break;
								}
							}
						}
						else
						{
							if (kvp.Key == _kvp.Value)
							{
								f = true;
							}
						}

						if (f)
						{
							flag = true;
							break;
						}
					}
					if (flag)
					{
						visualizeData.Shapes.Add(kvp.Key, kvp.Value);
					}
				}
			}
			return visualizeData;
		}

		private TraceLog getTraceLog(string traceLogFilePath, ResourceData resourceData)
		{
			Dictionary<string, Json> dic = new Dictionary<string, Json>();

			string target = resourceData.ConvertRule;

			string[] convertRulePaths = Directory.GetFiles(ApplicationDatas.Setting["ConvertRulesDirectoryPath"], "*." + Properties.Resources.ConvertRuleFileExtension);
			// トレースログ変換ファイルを開きJsonValueでデシリアライズ
			// ファイルが複数ある場合を想定している
			foreach (string s in convertRulePaths)
			{
				Json json = new Json().Parse(File.ReadAllText(s));
				foreach (KeyValuePair<string, Json> j in json.GetKeyValuePairEnumerator())
				{
					if (j.Key == target)
					{
						foreach (KeyValuePair<string, Json> _j in j.Value.GetKeyValuePairEnumerator())
						{
							dic.Add(_j.Key, _j.Value);
						}
					}
				}
			}

			TraceLogData t = new TraceLogData(resourceData);

			// トレースログを一行ずつ調べTraceLogクラスに変換しTraceLogListに追加していく
			string[] logs = File.ReadAllLines(traceLogFilePath);
			float i = 1;
			float max = logs.Length;
			foreach (string s in logs)
			{
				if (_constructProgressReport != null)
					_constructProgressReport((int)(((i / max) * (float)(_to - _from)) + (float)_from), "トレースログを共通形式へ変換中 " + i + " 行目...");

				foreach (KeyValuePair<string, Json> kvp in dic)
				{
					if (Regex.IsMatch(s, kvp.Key))
					{
						addTraceLog(s, kvp.Key, kvp.Value, t);
					}
				}
				i++;
			}

			return t.TraceLog;
		}

		/// <summary>
		/// 読み込んだログがパターンにマッチした場合に変換してログを追加する
		/// </summary>
		/// <param name="log">読み込むログ</param>
		/// <param name="pattern">パターン</param>
		/// <param name="value">変換値がValue（Jsonでいうところの）</param>
		/// <param name="traceLogManager">追加先</param>
		private void addTraceLog(string log, string pattern, Json value, TraceLogData traceLogManager)
		{
			if (value.IsArray)
			{
				addTraceLogAsArray(log, pattern, value, traceLogManager);
			}
			else if (value.IsObject)
			{
				addTraceLogAsObject(log, pattern, value, traceLogManager);
			}
			else
			{
				// valueがstringのときログを置換して追加
				traceLogManager.Add(Regex.Replace(log, pattern, applyConvertFunc(traceLogManager, value)));
			}
		}

		/// <summary>
		/// 読み込んだログがパターンにマッチした場合に変換してログを追加する。変換値
		/// </summary>
		/// <param name="log">読み込むログ</param>
		/// <param name="pattern">パターン</param>
		/// <param name="value">変換値がArray（Jsonでいうところの）</param>
		/// <param name="traceLogManager">追加先</param>
		private void addTraceLogAsArray(string log, string pattern, List<Json> value, TraceLogData traceLogManager)
		{
			foreach (Json j in value)
			{
				addTraceLog(log, pattern, j, traceLogManager);
			}
		}

		/// <summary>
		/// 読み込んだログがパターンにマッチした場合に変換してログを追加する。変換値
		/// </summary>
		/// <param name="log">読み込むログ</param>
		/// <param name="condition">パターン</param>
		/// <param name="value">変換値がObject（Jsonでいうところの）</param>
		/// <param name="traceLogManager">追加先</param>
		private void addTraceLogAsObject(string log, string pattern, Dictionary<string, Json> value, TraceLogData traceLogManager)
		{
			Dictionary<string, string> cache = new Dictionary<string, string>();

			foreach (KeyValuePair<string, Json> kvp in value)
			{
				string condition = Regex.Replace(log, pattern, kvp.Key);

				condition = applyConvertFunc(traceLogManager,condition);

				foreach (Match m in Regex.Matches(condition, @"^\s*(\[\s*[^\]]+\s*\])?\s*[^\[\]\(\)\.\s]+\s*(\s*\([^\)]+\)\s*)?\s*(\.\s*[^=!<>\(]+)?\s*$"))
				{
					if (cache.ContainsKey(m.Value))
					{
						condition = Regex.Replace(condition, Regex.Escape(m.Value), cache[m.Value]);
					}
					else
					{
						string val;
						try
						{
							val = traceLogManager.GetAttributeValue(m.Value);
						}
						catch (Exception e)
						{
							throw new Exception("リソース条件式が異常です。\n" + "\"" + m.Value + "\"\n" + e.Message);
						}
						condition = Regex.Replace(condition, Regex.Escape(m.Value), val);
						cache.Add(m.Value, val);
					}
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
					addTraceLog(log, pattern, kvp.Value, traceLogManager);
				}
			}
		}

		private string applyConvertFunc(TraceLogData traceLogManager, string condition)
		{
			foreach (string func in _convertFunction)
			{
				foreach (Match m in Regex.Matches(condition, func + @"^\s*(\[\s*[^\]]+\s*\])?\s*[^\[\]\(\)\.\s]+\s*(\s*\([^\)]+\)\s*)?\s*(\.\s*[^=!<>\(]+)?\s*$"))
				{
					string val = calcConvertFunc(func, m.Value, traceLogManager);
					condition = Regex.Replace(condition, Regex.Escape(m.Value), val);
				}
			}
			return condition;
		}

		private string calcConvertFunc(string func, string condition, TraceLogData traceLogManager)
		{
			string result;
			switch (func)
			{
				case "COUNT":
					try
					{
						result = traceLogManager.GetResources(condition).Count.ToString();
					}
					catch (Exception e)
					{
						throw new Exception("リソース条件式が異常です。\n" + "\"" + condition + "\"\n" + e.Message);
					}
					break;
				case "EXIST":
					try
					{
						result = traceLogManager.GetResources(condition).Count != 0 ? "True" : "False";
					}
					catch (Exception e)
					{
						throw new Exception("リソース条件式が異常です。\n" + "\"" + condition + "\"\n" + e.Message);
					}
					break;
				case "ATTR":
					try
					{
						result = traceLogManager.GetAttributeValue(condition).ToString();
					}
					catch (Exception e)
					{
						throw new Exception("リソース条件式が異常です。\n" + "\"" + "ATTR{" + condition + "}" + "\"\n" + e.Message);
					}
					break;
				default:
					throw new Exception(func + "：未知の関数です。");
			}
			return result;
		}

	}

}