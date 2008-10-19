using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml;
using System.Xml.Serialization;

namespace NU.OJL.MPRTOS.TLV.Core
{
    public class TraceLogConverter
    {
        public static string Transform(string log, string traceLogConvertRule)
        {
            string result = log;

            List<string[]> aliasRules = new List<string[]>();
            List<string[]> behaviorRules = new List<string[]>();
            List<string[]> replaceRules = new List<string[]>();

            // aliasルールを抜き出す
            foreach (Match m in Regex.Matches(traceLogConvertRule, @"alias\t+(?<from>[^\t]+)\t+(?<to>[^\r\n]+)"))
            {
                aliasRules.Add(new string[] { m.Groups["from"].Value, m.Groups["to"].Value });
            }

            // behaviorルールを抜き出す
            foreach (Match m in Regex.Matches(traceLogConvertRule, @"behavior\t+(?<from>[^\t]+)\t+(?<to>[^\r\n]+)"))
            {
                string from = m.Groups["from"].Value;
                string to = m.Groups["to"].Value;

                Match match = Regex.Match(from, @"\((?<name>\w+)\)\s*\.");
                if (match.Success)
                {
                    string name = match.Groups["name"].Value;
                    if (aliasRules.Exists((s) => { return s[0] == name; }))
                    {
                        string value = aliasRules.Single<string[]>((s) => { return s[0] == name; })[1];
                        from = Regex.Replace(from, @"\((?<name>\w+)\)\s*\.", value + ".");
                    }
                }

                behaviorRules.Add(new string[] { from, to });
            }

            // replaceルールを抜き出す
            foreach (Match m in Regex.Matches(traceLogConvertRule, @"replace\t+(?<from>[^\t]+)\t+(?<to>[^\r\n]+)"))
            {
                string from = m.Groups["from"].Value;
                string to = m.Groups["to"].Value;

                foreach(Match match in Regex.Matches(to, @"\((?<name>\w+)\)"))
                {
                    string name = match.Groups["name"].Value;
                    if (aliasRules.Exists((s) => { return s[0] == name; }))
                    {
                        string value = aliasRules.Single<string[]>((s) => { return s[0] == name; })[1];
                        to = Regex.Replace(to, @"\(" + name + @"\)", value);
                    }
                }

                replaceRules.Add(new string[] { from, to });
            }

            // replaceを実行する
            foreach (string[] strs in replaceRules)
            {
                result = Regex.Replace(result, strs[0], strs[1], RegexOptions.Multiline);
            }

            // ログを整形する
            result = Regex.Replace(result, @"[\r\n\s]", "");
            result = Regex.Replace(result, @"\[", "\n[");
            result = Regex.Replace(result, @"^[\r\n]", "");
            result = Regex.Replace(result, @"\](?<name>[^\.]+)\.", "]${name}:${name}.");

            return result;
        }

        public static bool IsValid(string log, string pattern)
        {
            return Regex.IsMatch(log, pattern);
        }
    }
}
