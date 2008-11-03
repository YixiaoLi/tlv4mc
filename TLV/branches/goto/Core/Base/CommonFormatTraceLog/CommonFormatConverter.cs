using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.IO;
using NU.OJL.MPRTOS.TLV.Base;

namespace NU.OJL.MPRTOS.TLV.Core
{
    /// <summary>
    /// 共通形式トレースログへ変換するためのコンバータ
    /// コンストラクタはプライベートとなっているのでGetInstanceメソッドを使ってインスタンスを得る
    /// </summary>
    public class CommonFormatConverter
    {
		private ResourceData _resourceData;
		private TraceLog _traceLog;

        /// <summary>
        /// <c>CommonFormatConverter</c>のコンストラクタ
        /// </summary>
		public CommonFormatConverter(string resourceFilePath, string traceLogFilePath)
        {
			_resourceData = new ResourceData().Parse(File.ReadAllText(resourceFilePath));
			_traceLog = getTraceLog(traceLogFilePath, _resourceData);
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
				foreach (KeyValuePair<string, Json> j in json.GetKeyValuePaierEnumerator())
				{
					if (j.Key == target)
					{
						foreach (KeyValuePair<string, Json> _j in j.Value.GetKeyValuePaierEnumerator())
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
			foreach (KeyValuePair<string, Json> kvp in value.GetKeyValuePaierEnumerator())
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
