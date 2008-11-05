using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.IO;
using NU.OJL.MPRTOS.TLV.Base;
using System;

namespace NU.OJL.MPRTOS.TLV.Core
{
    /// <summary>
    /// 共通形式トレースログへ変換するためのコンバータ
    /// コンストラクタはプライベートとなっているのでGetInstanceメソッドを使ってインスタンスを得る
    /// </summary>
    public class CommonFormatConverter
    {
		private ResourceData _resourceData;
		private VisualizeData _visualizeData;
		private TraceLog _traceLog;
		public Action<int> ConstructProgressReport;

        /// <summary>
        /// <c>CommonFormatConverter</c>のコンストラクタ
        /// </summary>
		public CommonFormatConverter(string resourceFilePath, string traceLogFilePath, Action<int,string> ConstructProgressReport)
		{
			if (ConstructProgressReport != null)
				ConstructProgressReport(0, "リソースデータを生成中");

			_resourceData = new ResourceData().Parse(File.ReadAllText(resourceFilePath));

			if (ConstructProgressReport != null)
				ConstructProgressReport(33, "トレースログデータを生成中");

			_traceLog = getTraceLog(traceLogFilePath, _resourceData);

			if (ConstructProgressReport != null)
				ConstructProgressReport(66, "可視化データを生成中");

			_visualizeData = getVisualizeData(_resourceData);

			if (ConstructProgressReport != null)
				ConstructProgressReport(100, "初期化中");
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
				foreach (KeyValuePair<string, Attribute> attr in resh.Value.Attributes)
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
				foreach (KeyValuePair<string, ShapeList> kvp in vd.Shapes)
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
			foreach (string s in File.ReadAllLines(traceLogFilePath))
			{
				foreach (KeyValuePair<string, Json> kvp in dic)
				{
					if (Regex.IsMatch(s, kvp.Key))
					{
						addTraceLog(s, kvp.Key, kvp.Value, t);
					}
				}
			}

			return t.TraceLog;
		}

		private void addTraceLog(string log, string key, Json value, TraceLogData traceLogManager)
		{
			if (value.IsArray)
			{
				addTraceLogAsArray(log, key, value, traceLogManager);
			}
			else if (value.IsObject)
			{
				addTraceLogAsObject(log, key, value, traceLogManager);
			}
			else
			{
				traceLogManager.Add(Regex.Replace(log, key, (string)value));
			}
		}

		private void addTraceLogAsArray(string log, string key, Json value, TraceLogData traceLogManager)
		{
			foreach (Json j in value)
			{
				addTraceLog(log, key, j, traceLogManager);
			}
		}

		private void addTraceLogAsObject(string log, string key, Json value, TraceLogData traceLogManager)
		{
			foreach (KeyValuePair<string, Json> kvp in value.GetKeyValuePairEnumerator())
			{
				if (checkCondition(log, key, kvp.Key, traceLogManager))
				{
					addTraceLog(log, key, kvp.Value, traceLogManager);
				}
			}
		}

		private bool checkCondition(string log, string key, string condition, TraceLogData traceLogManager)
		{
			condition = Regex.Replace(log, key, condition);

			foreach (Match m in Regex.Matches(condition, @"(?<name>\w+)\s*\(\s*(?<attrs>[^\(]+)\s*\)\s*\.\s*(?<attr>\w+)"))
			{
				condition = Regex.Replace(condition, Regex.Escape(m.Value), traceLogManager.GetAttributeValue(m.Value));
			}

			foreach (Match m in Regex.Matches(condition, @"(?<comparisonExpression>(?<left>[^\s]+)\s*(?<ope>(==|!=|<=|>=|>|<))\s*(?<right>[^\s]+))"))
			{
				ComparisonExpression ce = new ComparisonExpression(m.Groups["comparisonExpression"].Value);
				condition = Regex.Replace(condition, Regex.Escape(m.Groups["comparisonExpression"].Value), ce.Result("string").ToString());
			}
			foreach (Match m in Regex.Matches(condition, @"(?<conditionExpression>(?<attr>[^\s]+)\s*(?<ope>(&&|\|\|))\s*(?<val>[^\s]+))"))
			{
				ConditionExpression ce = new ConditionExpression(m.Groups["conditionExpression"].Value);
				condition = Regex.Replace(condition, Regex.Escape(m.Groups["conditionExpression"].Value), ce.Result().ToString());
			}


			return bool.Parse(condition);
		}

    }
}
