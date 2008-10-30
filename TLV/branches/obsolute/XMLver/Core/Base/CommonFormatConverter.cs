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
        /// リソースファイルのスキーマ（.xsd）
        /// </summary>
        public string ResourceXsd { get; private set; }
        /// <summary>
        /// リソースファイルの変換ルール（.xslt）
        /// </summary>
        public string ResourceXslt { get; private set; }
        /// <summary>
        /// トレースログの変換ルール（.lcnv）
        /// </summary>
        public string TraceLogConvertRule { get; private set; }

        /// <summary>
        /// <c>CommonFormatConverter</c>のインスタンスを生成する
        /// 指定したディレクトリが不正な場合はnullが返される
        /// </summary>
        /// <param name="convertDirPathPath">変換ルールが格納されているディレクトリのパス</param>
        /// <returns>共通形式トレースログへ変換するためのコンバータ</returns>
        public static CommonFormatConverter GetInstance(string convertDirPath)
        {
            CommonFormatConverter c = new CommonFormatConverter();
            c.Path = System.IO.Path.GetFullPath(convertDirPath);

            if (! Directory.Exists(convertDirPath))
                return null;

            try
            {
                string ruleFilePath = convertDirPath + Properties.Resources.ConvertRuleInfoFileName;
                string[] ruleFileLines = File.ReadAllLines(ruleFilePath);
                foreach (string line in ruleFileLines)
                {
                    // タブ区切りで設定パラメータ名と値を得る
                    Match m = new Regex(@"^(?<name>[^\t]+)\t+(?<value>.+)$").Match(line);
                    switch (m.Groups["name"].Value)
                    {
                        case "name":
                            c.Name = m.Groups["value"].Value;
                            break;
                        case "description":
                            c.Description = m.Groups["value"].Value;
                            break;
                        case "resourceXsd":
                            c.ResourceXsd = File.ReadAllText(convertDirPath + m.Groups["value"]);
                            break;
                        case "resourceXslt":
                            c.ResourceXslt = File.ReadAllText(convertDirPath + m.Groups["value"]);
                            break;
                        case "traceLogLcnv":
                            c.TraceLogConvertRule = File.ReadAllText(convertDirPath + m.Groups["value"]);
                            break;
                    }
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
        /// リソースファイルを共通形式へ変換する
        /// </summary>
        /// <param name="resourceFilePath">変換する前のリソースファイルのパス</param>
        /// <returns>変換後のリソースファイルの内容の文字列</returns>
        public string ConvertResourceFile(string resourceFilePath)
        {
            string res = File.ReadAllText(resourceFilePath);
            StringBuilder sb = new StringBuilder();
            // resourceFilePathで読み込むXMLをResourceXsdのスキーマで検証
            if (!Xml.IsValid(ResourceXsd, res, new StringWriter(sb)))
            {
                throw new ResourceFileValidationException("リソースファイルの共通形式への変換に失敗しました。\n" + resourceFilePath + "は定義されたスキーマに準拠しません。\n" + sb.ToString());
            }

            // resourceFilePathで読み込むXMLをResourceXsltでXSLT変換
            res = Xml.Transform(res, ResourceXslt);

            if(File.Exists(ApplicationDatas.ResourceSchemaFilePath))
                {
                // 変換後のリソースファイルが有効か検証
                if (!Xml.IsValid(File.ReadAllText(ApplicationDatas.ResourceSchemaFilePath), res, new StringWriter(sb)))
                {
                    throw new ResourceFileValidationException("リソースファイルの共通形式への変換に失敗しました。\nリソースファイル共通形式変換ルールファイルの定義が誤っている可能性があります。\n" + sb.ToString());
                }
            }
            // 変換したxmlをDataSetで整形してtextWriterで記述
            //res = Xml.AutoIndent(res);

            return res;
        }

        /// <summary>
        /// トレースログファイルを共通形式へ変換する
        /// </summary>
        /// <param name="traceLogFilePath">変換する前のトレースログファイルのパス</param>
        /// <returns>変換後のトレースログファイルの内容の文字列</returns>
        public string ConvertTraceLogFile(string traceLogFilePath)
        {
            string log = File.ReadAllText(traceLogFilePath);

            // トレースログを共通形式に変換
            log = TraceLogConverter.Transform(log, TraceLogConvertRule);

            // 無効なトレースログを除去
            log = TraceLogConverter.Validate(log, ApplicationDatas.CommonFormatTraceLogRegex);

            // 有効なトレースログか検証
            if (!TraceLogConverter.IsValid(log, ApplicationDatas.CommonFormatTraceLogRegex))
            {
                throw new ResourceFileValidationException("トレースログファイルの共通形式への変換に失敗しました。\n" + traceLogFilePath + "は定義された正規表現に準拠しません。");
            }

            return log;
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
