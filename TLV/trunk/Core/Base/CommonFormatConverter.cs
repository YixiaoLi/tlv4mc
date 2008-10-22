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
    public class CommonFormatConverter
    {
        public string Name { get; private set; }
        public string Path { get; private set; }
        public string Description { get; private set; }
        public string ResourceXsd { get; private set; }
        public string ResourceXslt { get; private set; }
        public string TraceLogConvertRule { get; private set; }

        public static CommonFormatConverter GetInstance(string convertDirPathPath)
        {
            CommonFormatConverter c = new CommonFormatConverter();
            c.Path = System.IO.Path.GetFullPath(convertDirPathPath);

            if (! Directory.Exists(convertDirPathPath))
                return null;

            try
            {
                string ruleFilePath = convertDirPathPath + Properties.Resources.ConvertRuleInfoFileName;
                string[] ruleFileLines = File.ReadAllLines(ruleFilePath);
                foreach (string line in ruleFileLines)
                {
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
                            c.ResourceXsd = File.ReadAllText(convertDirPathPath + m.Groups["value"]);
                            break;
                        case "resourceXslt":
                            c.ResourceXslt = File.ReadAllText(convertDirPathPath + m.Groups["value"]);
                            break;
                        case "traceLogCnv":
                            c.TraceLogConvertRule = File.ReadAllText(convertDirPathPath + m.Groups["value"]);
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

        public CommonFormatConverter()
        {
        }

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

            if (!Xml.IsValid(File.ReadAllText(ApplicationDatas.ResourceSchemaFilePath), res, new StringWriter(sb)))
            {
                throw new ResourceFileValidationException("リソースファイルの共通形式への変換に失敗しました。\nリソースファイル共通形式変換ルールファイルの定義が誤っている可能性があります。\n" + sb.ToString());
            }

            // 変換したxmlをDataSetで整形してtextWriterで記述
            //res = Xml.AutoIndent(res);

            return res;
        }

        public string ConvertTraceLogFile(string traceLogFilePath)
        {
            string log = File.ReadAllText(traceLogFilePath);

            log = TraceLogConverter.Transform(log, TraceLogConvertRule);

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
