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
        /// <summary>
        /// 変換ルールの名前
        /// </summary>
        public string Name { get; private set; }
        /// <summary>
        /// 変換ルールのパス
        /// </summary>
        public string Path { get; private set; }
        /// <summary>
        /// 変換ルールの説明
        /// </summary>
        public string Description { get; private set; }
        /// <summary>
        /// リソースヘッダファイル（.resh）
        /// </summary>
		public string[] ResourceHeaderPaths { get; private set; }
        /// <summary>
        /// トレースログの変換ルール（.cnv）
        /// </summary>
		public string[] TraceLogConvertRulePaths { get; private set; }

        /// <summary>
        /// <c>CommonFormatConverter</c>のインスタンスを生成する
        /// 指定したディレクトリが不正な場合はnullが返される
        /// </summary>
        /// <param name="convertDirPathPath">変換ルールが格納されているディレクトリのパス</param>
        /// <returns>共通形式トレースログへ変換するためのコンバータ</returns>
        public static CommonFormatConverter GetInstance(string convertDirPath)
        {
            CommonFormatConverter c = new CommonFormatConverter();

            if (! Directory.Exists(convertDirPath))
                return null;

            try
            {
                string ruleFilePath = convertDirPath + Properties.Resources.ConvertRuleInfoFileName;
                string ruleFileData = File.ReadAllText(ruleFilePath);
				IJsonSerializer json = ApplicationFactory.JsonSerializer;
				Json j = json.Deserialize<Json>(ruleFileData);
				
				c.Name = j["Name"];
				c.Description = j["Description"];
				c.ResourceHeaderPaths = j["ResourceHeader"];
				c.TraceLogConvertRulePaths = j["TraceLogConvertRule"];
				c.Path = System.IO.Path.GetFullPath(convertDirPath);

				for(int i=0; i < c.TraceLogConvertRulePaths.Length; i++ )
				{
					c.TraceLogConvertRulePaths[i] = System.IO.Path.GetFullPath(convertDirPath + c.TraceLogConvertRulePaths[i]);
				}
				for (int i = 0; i < c.ResourceHeaderPaths.Length; i++)
				{
					c.ResourceHeaderPaths[i] = System.IO.Path.GetFullPath(convertDirPath + c.ResourceHeaderPaths[i]);
				}
            }
            catch
            {
                return null;
            }
            
            return c;
        }

        /// <summary>
        /// <c>CommonFormatConverter</c>のコンストラクタ プライベートである
        /// </summary>
        private CommonFormatConverter()
        {
        }

        /// <summary>
        /// リソースデータを得る
        /// </summary>
        /// <param name="resourceFilePath">リソースファイルのパス</param>
        /// <returns>リソースデータ</returns>
        public ResourceData GetResourceData(string resourceFilePath)
        {
			return new ResourceData(ResourceHeaderPaths, resourceFilePath);
        }

        /// <summary>
        /// トレースログファイルを共通形式へ変換する
        /// </summary>
        /// <param name="traceLogFilePath">変換する前のトレースログファイルのパス</param>
        /// <returns>変換後のトレースログファイルの内容の文字列</returns>
		public TraceLogList ConvertTraceLogFile(string traceLogFilePath)
        {
			List<Json> list = new List<Json>();
			Dictionary<string, Json> dic = new Dictionary<string, Json>();
			
			// トレースログ変換ファイルを開きJsonValueでデシリアライズ
			// ファイルが複数ある場合を想定している
			foreach(string s in TraceLogConvertRulePaths)
			{
				Json json = ApplicationFactory.JsonSerializer.Deserialize<Json>(File.ReadAllText(s));

				foreach (KeyValuePair<string, Json> j in json.GetKeyValuePaierEnumerator())
				{
					dic.Add(j.Key, j.Value);
				}
			}

			List<TraceLog> tll = new List<TraceLog>();

			foreach(string s in File.ReadAllLines(traceLogFilePath))
			{
				foreach(KeyValuePair<string, Json> kvp in dic)
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

        public override string ToString()
        {
            return Name;
        }
    }
}
