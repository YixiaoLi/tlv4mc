using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.IO;
using System.Xml;
using System.Xml.XPath;
using System.Xml.Xsl;
using System.Xml.Schema;
using NU.OJL.MPRTOS.TLV.Base;

namespace NU.OJL.MPRTOS.TLV.Core
{
    /// <summary>
    /// 共通形式トレースログへ変換するためのコンバータ
    /// コンストラクタはプライベートとなっているのでGetInstanceメソッドを使ってインスタンスを得る
    /// </summary>
    public class CommonFormatConverter
    {
		private string[] _traceLogConvertRulePaths;
		private string _resourceFilePath;
		private string _traceLogFilePath;

        /// <summary>
        /// <c>CommonFormatConverter</c>のコンストラクタ
        /// </summary>
		public CommonFormatConverter(string resourceFilePath, string traceLogFilePath)
        {
			_resourceFilePath = resourceFilePath;
			_traceLogFilePath = traceLogFilePath;

			Json j = ApplicationFactory.JsonSerializer.Deserialize<Json>(File.ReadAllText(_resourceFilePath));

			string convertDirPath = ApplicationDatas.ConvertRulesDirectoryPath + j["Type"];

			_traceLogConvertRulePaths = Directory.GetFiles(convertDirPath, @"*." + Properties.Resources.TraceLogConvertFileExtension);

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
				return ApplicationFactory.JsonSerializer.Deserialize<ResourceData>(File.ReadAllText(_resourceFilePath));
			}
        }

        /// <summary>
        /// トレースログファイルを共通形式へ変換する
        /// </summary>
        /// <param name="traceLogFilePath">変換する前のトレースログファイルのパス</param>
        /// <returns>変換後のトレースログファイルの内容の文字列</returns>
		public TraceLogList TraceLogList
        {
			get
			{
				List<Json> list = new List<Json>();
				Dictionary<string, Json> dic = new Dictionary<string, Json>();

				// トレースログ変換ファイルを開きJsonValueでデシリアライズ
				// ファイルが複数ある場合を想定している
				foreach (string s in _traceLogConvertRulePaths)
				{
					Json json = ApplicationFactory.JsonSerializer.Deserialize<Json>(File.ReadAllText(s));

					foreach (KeyValuePair<string, Json> j in json.GetKeyValuePaierEnumerator())
					{
						dic.Add(j.Key, j.Value);
					}
				}

				List<TraceLog> tll = new List<TraceLog>();

				// トレースログを一行ずつ調べTraceLogクラスに変換しTraceLogListに追加していく
				foreach (string s in File.ReadAllLines(_traceLogFilePath))
				{
					foreach (KeyValuePair<string, Json> kvp in dic)
					{
						if (Regex.IsMatch(s, kvp.Key))
						{
							foreach (Json j in kvp.Value)
							{
								string t = Regex.Replace(s, kvp.Key, (string)j["Time"]);
								string _s = j.ContainsKey("Subject") ? Regex.Replace(s, kvp.Key, (string)j["Subject"]) : "";
								string o = Regex.Replace(s, kvp.Key, (string)j["Object"]);
								string b = Regex.Replace(s, kvp.Key, (string)j["Behavior"]);
								tll.Add(new TraceLog(t, _s, o, b));
							}
						}
					}
				}

				return new TraceLogList(tll);
			}
        }
    }
}
