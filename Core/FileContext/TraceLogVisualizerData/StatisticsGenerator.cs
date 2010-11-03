using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using NU.OJL.MPRTOS.TLV.Base;
using System.Text.RegularExpressions;
using System.Drawing;

namespace NU.OJL.MPRTOS.TLV.Core
{
    public class StatisticsGenerator
    {
        /// <summary>
        /// 変換前のトレースログファイルのパス
        /// </summary>
        private string _traceLogFilePath;
        /// <summary>
        /// 標準形式のリソースデータ
        /// </summary>
        private ResourceData _resourceData;
        /// <summary>
        /// 標準形式トレースログデータ
        /// </summary>
        private TraceLogData _traceLogData;
        /// <summary>
        /// 進行状況を知らせるアクション
        /// </summary>
        public Action<int, string> _constructProgressReport = null;

        #region コンストラクタ
        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="traceLogFilePath">変換前のトレースログファイルのパス</param>
        /// <param name="resourceData">標準形式のリソースデータ</param>
        /// <param name="traceLogData">標準形式トレースログデータ</param>
        /// <param name="constructProgressReport">進行状況を知らせるアクション</param>
        public StatisticsGenerator(string traceLogFilePath, ResourceData resourceData, TraceLogData traceLogData, Action<int, string> constructProgressReport)
            : this(resourceData, traceLogData, constructProgressReport)
        {
            _traceLogFilePath = traceLogFilePath;
        }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="resourceData">標準形式のリソースデータ</param>
        /// <param name="traceLogData">標準形式トレースログデータ</param>
        /// <param name="constructProgressReport">進行状況を知らせるアクション</param>
        public StatisticsGenerator(ResourceData resourceData, TraceLogData traceLogData, Action<int, string> constructProgressReport)
        {
            _resourceData = resourceData;
            _traceLogData = traceLogData;
            _constructProgressReport = constructProgressReport;
        }
        #endregion

        /// <summary>
        /// リソースファイルで指定した統計生成ルールを使用して統計情報を生成する
        /// </summary>
        /// <returns>リソースファイルで指定した全ての統計生成ルールを使用して生成した、全ての統計情報を含むStatisticsData</returns>
        public StatisticsData GenerateData()
        {
            if (_constructProgressReport != null)
            {
                _constructProgressReport(0, "統計生成ルールを読み込み中");
            }

            string[] target = _resourceData.StatisticsGenerationRules.ToArray();

            string[] rulePaths = Directory.GetFiles(ApplicationData.Setting.StatisticsGenerationRulesDirectoryPath, "*." + Properties.Resources.StatisticsGenerationRuleFileExtension);

            Dictionary<string, Json> rules = new Dictionary<string, Json>(); // Key: 統計情報名、Value:生成ルールのJsonオブジェクト

            try
            {
                // 統計生成ルールファイルを開きJsonValueでデシリアライズ
                // ファイルが複数あり、複数のファイルに同じ統計情報名を指定してファイル分割を実現する(変換ルール、可視化ルールと同様)
                foreach (string path in rulePaths)
                {
                    Json json = new Json().Parse(File.ReadAllText(path));

                    // Key:統計情報名、Value:生成ルールのJsonオブジェクト
                    foreach (KeyValuePair<string, Json> j in json.GetKeyValuePairEnumerator())
                    {
                        if (target.Contains(j.Key))
                        {
                            if (!rules.ContainsKey(j.Key))
                            {
                                rules.Add(j.Key, j.Value);
                            }
                            else
                            {
                                // Key:生成ルールの各要素("Style"等)、Value:各要素の値またはオブジェクト
                                foreach (KeyValuePair<string, Json> jj in j.Value.GetKeyValuePairEnumerator())
                                {
                                    if (rules[j.Key].ContainsKey(jj.Key))
                                    {
                                        throw new StatisticsGenerateException(string.Format(@"統計情報""{0}""の生成ルールで""{1}""が複数設定されています。", j.Key, jj.Key));
                                    }
                                    rules[j.Key].Add(jj.Key, jj.Value);
                                }
                            }
                        }
                    }
                }

                // 統計情報の生成
                StatisticsData sd = new StatisticsData();
                int max = target.Length;
                double current = 0.0;
                foreach (string tgt in target)
                {
                    try
                    {
                        if (_constructProgressReport != null)
                        {
                            current += 1.0;
                            _constructProgressReport((int)(current / max) * 10, string.Format(@"統計情報""{0}""を生成中 {1}/{2} 個目...", tgt, current, max));
                        }

                        Statistics stats = null;
                        switch (rules[tgt]["Style"])
                        {
                            case "Regexp": stats = applyRegexRule(tgt, rules[tgt]["Style"]); break;
                            default: throw new StatisticsGenerateException(rules[tgt]["Style"] + "無効なスタイルです");
                        }
                        sd.Statisticses.Add(stats);
                    }
                    catch (Exception e)
                    {
                        // ある統計情報の生成に失敗した場合、処理は止めずにほかの統計情報の生成を試みる
                    }
                }

                return sd;
            }
            catch (Exception e)
            {
                throw new StatisticsGenerateException("統計情報の生成に失敗しました。", e);
            }
        }

        #region 複数のファイル対応のためのメソッドサンプル
        //private StatisticsGenerationRule deserializeRule(Json json)
        //{
        //                // 統計生成ルールファイルを開きJsonValueでデシリアライズ
        //    // ファイルが複数あり、複数のファイルに同じ統計情報名を指定してファイル分割を実現する(変換ルール、可視化ルールと同様)
        //    foreach (string path in rulePaths)
        //    {
        //        Json json = new Json().Parse(File.ReadAllText(path));
        //        foreach (KeyValuePair<string, Json> j in json.GetKeyValuePairEnumerator())
        //        {
        //            if (target.Contains(j.Key))
        //            {
        //                if (!rules.ContainsKey(j.Key))
        //                {
        //                    rules.Add(j.Key, new StatisticsGenerationRule());
        //                }
        //                foreach (KeyValuePair<string, Json> jj in j.Value.GetKeyValuePairEnumerator())
        //                {
        //                    setRule(jj, rules[j.Key]);
        //                    j.Value.
        //                }
        //            }
        //        }
        //    }
        //}
        //private void setRule(KeyValuePair<string, Json> data, StatisticsGenerationRule rule)
        //{
        //    switch (data.Key)
        //    {
        //        case "Name":
        //            if (!string.IsNullOrEmpty(rule.Name)) throw new Exception("複数の" + data.Key + "が存在します"); ;
        //            rule.Name = data.Value;
        //            break;

        //        case "Style":
        //            if (!string.IsNullOrEmpty(rule.Style)) throw new Exception("複数の" + data.Key + "が存在します"); ;
        //            rule.Style = data.Value;
        //            break;

        //        case "Target":
        //            if (!string.IsNullOrEmpty(rule.Target)) throw new Exception("複数の" + data.Key + "が存在します"); ;
        //            rule.Target = data.Value;
        //            break;

        //        case "Setting":
        //            setChartSetting(data, rule);
        //    }
        //}

        //private void setChartSetting(KeyValuePair<string, Json> data, StatisticsGenerationRule rule)
        //{
        //    switch (data.Key)
        //    {
        //    }
        //}
        #endregion



        private Statistics applyRegexRule(string name, Json rule)
        {
            if (!rule.ContainsKey("RegexRule"))
            {
                throw new StatisticsGenerateException(@"""RegexpRule""が定義されて言いません。");
            }

            Statistics stats = new Statistics(name);

            stats.Setting.SetData(rule["Setting"]);

            List<string> data = getTargetData(rule["Target"]);

            // Key: 正規表現、Value:正規表現にマッチした場合の統計情報設定方法を記述したJsonオブジェクト
            // 複数の正規表現によってパースされることを想定している
            foreach (KeyValuePair<string, Json> j in rule["RegexpRule"].GetKeyValuePairEnumerator())
            {
                foreach (string l in data)
                {
                    if (l.Equals("Target")) continue;

                    if (Regex.IsMatch(l, j.Key))
                    {
                        DataPoint dp = new DataPoint();
                        
                        // Key:各種設定("XValue"等)
                        foreach (KeyValuePair<string, Json> jj in j.Value.GetKeyValuePairEnumerator())
                        {
                            switch (jj.Key)
                            {
                                case "XLabel": dp.XLabel = Regex.Replace(l, j.Key, jj.Value); break;
                                case "XValue": dp.XValue = double.Parse(Regex.Replace(l, j.Key, jj.Value)); break;
                                case "YValue": dp.YValue = double.Parse(Regex.Replace(l, j.Key, jj.Value)); break;
                                case "Color": dp.Color = new Color().FromHexString(jj.Value); break;
                                default: throw new StatisticsGenerateException(jj.Key + "という設定はありません。");
                            }
                        }

                        stats.Series.Points.Add(dp);
                    }
                }
            }

            return stats;
        }

        private List<string> getTargetData(string target)
        {
            List<string> data = new List<string>();
            switch (target)
            {
                 case "standard":
                    foreach (TraceLog t in _traceLogData.TraceLogs)
                    {
                        data.Add(t.ToString());
                    }
                    break;

                case "nonstandard":
                    data.AddRange(File.ReadAllLines(_traceLogFilePath));
                    break;

                default:
                    if (!File.Exists(target))
                    {
                        throw new StatisticsGenerateException("ファイル：" + target + "が見つかりません。");
                    }
                    data.AddRange(File.ReadAllLines(target));
                    break;
            }
            return data;
        }
    }
}
