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
    /// <summary>
    /// トレースログを共通形式に変換するコンバータ
    /// </summary>
    public class TraceLogConverter
    {
        /// <summary>
        /// トレースログを共通形式に変換する
        /// </summary>
        /// <param name="log">変換するトレースログ</param>
        /// <param name="traceLogConvertRule">共通形式変換ルール</param>
        /// <returns>共通形式トレースログ</returns>
        public static string Transform(string log, string traceLogConvertRule)
        {
            string result = log;

            List<string[]> aliasRules = new List<string[]>();
            List<string[]> behaviorRules = new List<string[]>();
            List<string[]> replaceRules = new List<string[]>();

            // aliasルールを抜き出す
            getAliasRules(traceLogConvertRule, aliasRules);

            // behaviorルールを抜き出す
            getBehaviorRules(traceLogConvertRule, aliasRules, behaviorRules);

            // replaceルールを抜き出す
            getReplaceRules(traceLogConvertRule, aliasRules, replaceRules);

            // replaceを実行する
            result = doReplace(result, replaceRules);

            // Subjectが未定義の場合
            result = Regex.Replace(result, @"\](?<name>[^\.:]+)\.", "]:${name}.");

            return result;
        }

        private static string doReplace(string result, List<string[]> replaceRules)
        {
            foreach (string[] strs in replaceRules)
            {
                result = Regex.Replace(result, strs[0], strs[1], RegexOptions.Multiline);
            }
            return result;
        }

        private static void getAliasRules(string traceLogConvertRule, List<string[]> aliasRules)
        {
            foreach (Match m in Regex.Matches(traceLogConvertRule, @"alias\t+(?<from>[^\t]+)\t+(?<to>[^\r\n]+)"))
            {
                aliasRules.Add(new string[] { m.Groups["from"].Value, m.Groups["to"].Value });
            }
        }

        private static void getBehaviorRules(string traceLogConvertRule, List<string[]> aliasRules, List<string[]> behaviorRules)
        {
            foreach (Match m in Regex.Matches(traceLogConvertRule, @"behavior\t+(?<from>[^\t]+)\t+(?<to>[^\r\n]+)"))
            {
                string from = m.Groups["from"].Value;
                string to = m.Groups["to"].Value;

                foreach (Match match in Regex.Matches(from, @"\((?<name>\w+)\)"))
                {
                    string name = match.Groups["name"].Value;

                    if (aliasRules.Exists((s) => { return s[0] == name; }))
                    {
                        string value = aliasRules.Single<string[]>((s) => { return s[0] == name; })[1];
                        from = Regex.Replace(from, @"\((?<name>\w+)\)", value);
                    }
                }

                behaviorRules.Add(new string[] { from, to });
            }
        }

        private static void getReplaceRules(string traceLogConvertRule, List<string[]> aliasRules, List<string[]> replaceRules)
        {
            foreach (Match m in Regex.Matches(traceLogConvertRule, @"replace\t+(?<from>[^\t]+)\t+(?<to>[^\r\n]+)"))
            {
                string from = m.Groups["from"].Value;
                string to = m.Groups["to"].Value;

                foreach (Match match in Regex.Matches(to, @"\((?<name>\w+)\)"))
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
        }

        /// <summary>
        /// トレースログを定義されたとおりか検証する
        /// </summary>
        /// <param name="log">検証するトレースログ</param>
        /// <param name="pattern">検証する正規表現</param>
        /// <returns>有効ならtrue, 無効ならfalse</returns>
        public static bool IsValid(string log, string pattern)
        {
            return Regex.IsMatch(log, pattern, RegexOptions.Multiline);
        }

        /// <summary>
        /// トレースログを有効化する。無効なログを消去しログを整える
        /// </summary>
        /// <param name="log">有効化する共通形式トレースログ</param>
        /// <param name="pattern">検証する正規表現</param>
        /// <returns>共通形式トレースログ</returns>
        public static string Validate(string log, string pattern)
        {
            string result = log;
            result = Regex.Replace(result, @"[\r\n\s]", "");
            result = Regex.Replace(result, @"\[", "\n[");
            result = Regex.Replace(result, @"^\n", "", RegexOptions.Multiline);

            StringBuilder sb = new StringBuilder();

            foreach (Match m in Regex.Matches(result, pattern, RegexOptions.Multiline))
            {
                sb.Append(m.Value + "\n");
            }

            return sb.ToString();
        }
    }
}
