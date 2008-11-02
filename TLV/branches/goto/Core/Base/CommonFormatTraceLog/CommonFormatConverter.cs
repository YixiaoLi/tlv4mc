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
		private string _target;
		private string _resourceFilePath;
		private string _traceLogFilePath;

        /// <summary>
        /// <c>CommonFormatConverter</c>のコンストラクタ
        /// </summary>
		public CommonFormatConverter(string resourceFilePath, string traceLogFilePath)
        {
			_resourceFilePath = resourceFilePath;
			_traceLogFilePath = traceLogFilePath;

			_target = new Json().Parse(File.ReadAllText(_resourceFilePath))["Target"];

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
				return new ResourceData().Parse(File.ReadAllText(_resourceFilePath));
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

				string[] convertRulePaths =  Directory.GetFiles(ApplicationDatas.Setting["ConvertRulesDirectoryPath"],"*." + Properties.Resources.ConvertRuleFileExtension);
				// トレースログ変換ファイルを開きJsonValueでデシリアライズ
				// ファイルが複数ある場合を想定している
				foreach (string s in convertRulePaths)
				{
					Json json = new Json().Parse(File.ReadAllText(s));
					if(json["Target"] == _target)
					{
						foreach (KeyValuePair<string, Json> j in json["ConvertRules"].GetKeyValuePaierEnumerator())
						{
							dic.Add(j.Key, j.Value);
						}
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
