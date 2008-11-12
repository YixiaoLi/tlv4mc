using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.IO;
using NU.OJL.MPRTOS.TLV.Base;
using System;
using System.Linq;
using System.Threading;

namespace NU.OJL.MPRTOS.TLV.Core
{
	/// <summary>
	/// 共通形式トレースログへ変換するためのコンバータ
	/// コンストラクタはプライベートとなっているのでGetInstanceメソッドを使ってインスタンスを得る
	/// </summary>
	public class StandartFormatConverter
	{
		private readonly string[] _convertFunction = new string[] { "COUNT","EXIST","ATTR" };
		private ResourceData _resourceData;
		private VisualizeData _visualizeData;
		private TraceLogData _traceLogData;
		public Action<int, string> _constructProgressReport = null;
		private int _from = 0;
		private int _to = 100;

		/// <summary>
		/// <c>CommonFormatConverter</c>のコンストラクタ
		/// </summary>
		public StandartFormatConverter(string resourceFilePath, string traceLogFilePath, Action<int, string> ConstructProgressReport)
		{
			_constructProgressReport = ConstructProgressReport;

			if (_constructProgressReport != null)
				_constructProgressReport(0, "リソースデータを生成中");
			_to = 10;
			try
			{
				_resourceData = ApplicationFactory.JsonSerializer.Deserialize<ResourceData>(File.ReadAllText(resourceFilePath));
			}
			catch (Exception _e)
			{
				throw new Exception("リソースデータの生成に失敗しました。\nリソースファイルの記述に誤りがある可能性があります。\n" + _e.Message);
			}

			if (_constructProgressReport != null)
				_constructProgressReport(_to, "可視化データを生成中");
			_from = _to;
			_to = 20;

			try
			{
				_visualizeData = getVisualizeData(_resourceData);
			}
			catch (Exception _e)
			{
				throw new Exception("可視化データの生成に失敗しました。\n可視化ルールファイルの記述に誤りがある可能性があります。\n" + _e.Message);
			}

			if (_constructProgressReport != null)
				_constructProgressReport(_to, "トレースログデータを生成中");
			_from = _to;
			_to = 99;
			try
			{
				_traceLogData = getTraceLogData(traceLogFilePath, _resourceData);
			}
			catch (Exception _e)
			{
				throw new Exception("トレースログデータの生成に失敗しました。\nトレースログ変換ルールファイルの記述に誤りがある可能性があります。\n" + _e.Message);
			}

			if (_constructProgressReport != null)
				_constructProgressReport(_to, "初期化中");
		}

		/// <summary>
		/// <c>CommonFormatConverter</c>のコンストラクタ
		/// </summary>
		public StandartFormatConverter(string resourceFilePath, string traceLogFilePath)
		{
			_resourceData = new ResourceData().Parse(File.ReadAllText(resourceFilePath));

			_visualizeData = getVisualizeData(_resourceData);

			_traceLogData = getTraceLogData(traceLogFilePath, _resourceData);

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
		public TraceLogData TraceLogData
		{
			get
			{
				return _traceLogData;
			}
		}

		public VisualizeData VisualizeData
		{
			get { return _visualizeData; }
		}

		private VisualizeData getVisualizeData(ResourceData resourceData)
		{
			VisualizeData visualizeData = new VisualizeData();
			visualizeData.ApplyRules = new ApplyRuleList();
			visualizeData.VisualizeRules = new VisualizeRuleList();
			visualizeData.Shapes = new ShapesList();

			foreach (ResourceType resh in resourceData.ResourceHeader)
			{
				Dictionary<string, string> attrs = new Dictionary<string, string>();
				foreach (AttributeType attr in resh.Attributes)
				{
					attrs.Add(attr.Name, attr.VisualizeRule);
				}
				Dictionary<string, string> bhvrs = new Dictionary<string, string>();
				foreach (Behavior bhvr in resh.Behaviors)
				{
					bhvrs.Add(bhvr.Name, bhvr.VisualizeRule);
				}

				visualizeData.ApplyRules.Add(resh.Name, new ApplyRule(attrs, bhvrs) { Name = resh.Name });
			}

			string[] visualizeRuleFilePaths = Directory.GetFiles(ApplicationData.Setting.Data["VisualizeRulesDirectoryPath"], "*." + Properties.Resources.VisualizeRuleFileExtension);

			foreach (string s in visualizeRuleFilePaths)
			{
				VisualizeData vd = ApplicationFactory.JsonSerializer.Deserialize<VisualizeData>(File.ReadAllText(s));

				foreach (VisualizeRule vr in vd.VisualizeRules)
				{
					bool flag = false;
					foreach (ApplyRule r in visualizeData.ApplyRules)
					{
						bool f = false;
						foreach (string a in r.Attribute.Values)
						{
							if (vr.Name == a)
							{
								f = true;
								break;
							}
						}
						foreach (string b in r.Behavior.Values)
						{
							if (vr.Name == b)
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
						visualizeData.VisualizeRules.Add(vr.Name, vr);
					}
				}
				foreach (Shapes sp in vd.Shapes)
				{
					bool flag = false;
					foreach (VisualizeRule vr in visualizeData.VisualizeRules)
					{
						bool f = false;
						if (vr.IsMapped)
						{
							foreach (KeyValuePair<string, string> v in vr)
							{
								if (sp.Name == v.Value)
								{
									f = true;
									break;
								}
							}
						}
						else
						{
							if (sp.Name == vr)
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
						visualizeData.Shapes.Add(sp.Name, sp);
					}
				}
			}
			return visualizeData;
		}

		private TraceLogData getTraceLogData(string traceLogFilePath, ResourceData resourceData)
		{
			Dictionary<string, Json> dic = new Dictionary<string, Json>();

			string target = resourceData.ConvertRule;

			string[] convertRulePaths = Directory.GetFiles(ApplicationData.Setting.Data["ConvertRulesDirectoryPath"], "*." + Properties.Resources.ConvertRuleFileExtension);
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
					_constructProgressReport((int)(((i / max) * (float)(_to - _from)) + (float)_from), "トレースログを共通形式へ変換中 " + i + "/" + max + " 行目...");

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

				traceLogData.Add(new TraceLog(Regex.Replace(log, pattern, applyConvertFunc(traceLogData, value))));
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

				condition = applyConvertFunc(traceLogData,condition);

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
				default:
					throw new Exception(func + "：未知の関数です。");
			}
			return result;
		}

	}

}