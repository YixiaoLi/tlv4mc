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

        public static CommonFormatConverter GetInstance(string convertFilePath)
        {
            CommonFormatConverter c = new CommonFormatConverter();
            c.Path = System.IO.Path.GetFullPath(convertFilePath);

            if (!File.Exists(convertFilePath) || System.IO.Path.GetExtension(convertFilePath) != ".zip")
                return null;

            IZip zip = ApplicationFactory.Zip;
            try
            {
                string tmpDirPath = System.IO.Path.GetTempPath() + "tlv_convertRuleTmp_" + DateTime.Now.Ticks.ToString() + @"\";
                Directory.CreateDirectory(tmpDirPath);
                zip.Extract(convertFilePath, tmpDirPath);
                string ruleFilePath = tmpDirPath + "rule.txt";
                string[] ruleFileLines = File.ReadAllLines(ruleFilePath);
                foreach (string line in ruleFileLines)
                {
                    Match m = new Regex(@"\s*(?<name>[^\s]+)\s*=\s*(?<value>[^\s]+)\s*").Match(line);
                    switch (m.Groups["name"].Value)
                    {
                        case "name":
                            c.Name = m.Groups["value"].Value;
                            break;
                        case "description":
                            c.Description = m.Groups["value"].Value;
                            c.Description = c.Description.Replace(@"\n", "\n");
                            c.Description = c.Description.Replace(@"\t", "\t");
                            break;
                        case "resourceXsd":
                            c.ResourceXsd = File.ReadAllText(tmpDirPath + m.Groups["value"]);
                            break;
                        case "resourceXslt":
                            c.ResourceXslt = File.ReadAllText(tmpDirPath + m.Groups["value"]);
                            break;
                    }
                }
                Directory.Delete(tmpDirPath, true);
            }
            catch
            {
                return null;
            }
            return c;
        }

        private CommonFormatConverter()
        {

        }

        public bool ConvertResourceFile(string resourceFilePath, TextWriter textWriter)
        {
            // resourceFilePathで読み込むXMLをResourceXsdのスキーマで検証
            if (!Xml.Validate(new XmlTextReader(new StringReader(ResourceXsd)), new XmlTextReader(resourceFilePath), textWriter))
            {
                return false;
            }

            // resourceFilePathで読み込むXMLをResourceXsltでXSLT変換
            StringBuilder sb = new StringBuilder();
            Xml.Transform(new XmlTextReader(resourceFilePath), new XmlTextReader(new StringReader(ResourceXslt)), new XmlTextWriter(new StringWriter(sb)));

            // 変換したxmlをDataSetで整形してtextWriterで記述
            Xml.AutoIndent(sb.ToString(), textWriter);

            return true;
        }

        public void ConvertTraceLogFile(string traceLogFilePath, string targetFilePath)
        {

        }

        public override string ToString()
        {
            return Name;
        }
    }
}
