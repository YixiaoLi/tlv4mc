using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.IO;
using NU.OJL.MPRTOS.TLV.Third;

namespace NU.OJL.MPRTOS.TLV.Core
{
    public class ConvertRule
    {
        public string Name { get; private set; }
        public string Path { get; private set; }
        public string Description { get; private set; }
        public string ResourceXsd { get; private set; }
        public string ResourceXslt { get; private set; }

        public static ConvertRule GetInstance(string convertFilePath)
        {
            ConvertRule c = new ConvertRule();
            c.Path = System.IO.Path.GetFullPath(convertFilePath);

            if (!File.Exists(convertFilePath) || System.IO.Path.GetExtension(convertFilePath) != ".zip")
                return null;

            SharpZipLibZip zip = new SharpZipLibZip();
            try
            {
                string tmpPath = System.IO.Path.GetTempPath() + "tlv_convertRuleTmp_" + DateTime.Now.Ticks.ToString() + @"\";
                Directory.CreateDirectory(tmpPath);
                zip.Extract(convertFilePath, tmpPath);
                string[] ruleFileLines = File.ReadAllLines(tmpPath + "rule.txt");
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
                            c.ResourceXsd = File.ReadAllText(tmpPath + m.Groups["value"]);
                            break;
                        case "resourceXslt":
                            c.ResourceXslt = File.ReadAllText(tmpPath + m.Groups["value"]);
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

        private ConvertRule()
        {

        }

        public override string ToString()
        {
            return Name;
        }
    }
}
