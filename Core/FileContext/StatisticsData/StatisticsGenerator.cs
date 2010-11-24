using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using NU.OJL.MPRTOS.TLV.Base;
using System.Text.RegularExpressions;
using System.Drawing;
using NU.OJL.MPRTOS.TLV.Core.Controls.Forms;
using NU.OJL.MPRTOS.TLV.Core.FileContext.StatisticsData.Rules;

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

            Json jrules = Json.Object;


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
                        if (!jrules.ContainsKey(j.Key))
                        {
                            jrules.Add(j.Key, j.Value);
                        }
                        else
                        {
                            // Key:生成ルールの各要素("Style"等)、Value:各要素の値またはオブジェクト
                            foreach (KeyValuePair<string, Json> jj in j.Value.GetKeyValuePairEnumerator())
                            {
                                if (jrules[j.Key].ContainsKey(jj.Key))
                                {
                                    throw new StatisticsGenerateException(string.Format(@"統計情報""{0}""の生成ルールで""{1}""が複数設定されています。", j.Key, jj.Key));
                                }
                                jrules[j.Key].Add(jj.Key, jj.Value);
                            }
                        }
                    }
                }
            }

            string d = ApplicationFactory.JsonSerializer.Serialize(jrules.Value);
            StatisticsGenerationRuleList rules = ApplicationFactory.JsonSerializer.Deserialize<StatisticsGenerationRuleList>(d);

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

                    Statistics stats = new Statistics(tgt);
                    stats.Setting = rules[tgt].Setting;

                    switch (rules[tgt].Mode)
                    {
                        case "Regexp": applyRegexRule(stats, rules[tgt].RegexpRule); break;
                        default: throw new StatisticsGenerateException(rules[tgt].Mode + "無効なスタイルです");
                    }

                    sd.Statisticses.Add(stats);
                }
                catch (Exception e)
                {
                    MessageForm mbox = new MessageForm(string.Format(@"統計情報 ""{0}"": {1}", tgt, e.ToString()), "統計情報の生成に失敗しました");
                    mbox.ShowDialog();
                }
            }

            return sd;
        }

        
        /// <summary>
        /// RegexpRuleで生成
        /// </summary>
        /// <param name="stats">統計情報を格納するオブジェクト</param>
        /// <param name="rule">"RegexpRule"のオブジェクト</param>
        /// <returns></returns>
        private void applyRegexRule(Statistics stats, RegexpRule rule)
        {
            List<string> data = getTargetData(rule.Target);
            
            // Key: 正規表現、Value:正規表現にマッチした場合の統計情報設定方法を記述したJsonオブジェクト
            // 複数の正規表現によってパースされることを想定している
            foreach (KeyValuePair<string, DataPointReplacePattern> pattern in rule.Regexps)
            {
                // l:一行のログ
                foreach (string l in data)
                {
                    if (Regex.IsMatch(l, pattern.Key))
                    {
                        DataPoint dp = new DataPoint();

                        if (!string.IsNullOrEmpty(pattern.Value.XLabel)) dp.XLabel = Regex.Replace(l, pattern.Key, pattern.Value.XLabel);
                        if (!string.IsNullOrEmpty(pattern.Value.XValue)) dp.XValue = double.Parse(Regex.Replace(l, pattern.Key, pattern.Value.XValue));
                        if (!string.IsNullOrEmpty(pattern.Value.YValue)) dp.YValue = double.Parse(Regex.Replace(l, pattern.Key, pattern.Value.YValue));
                        if (!string.IsNullOrEmpty(pattern.Value.Color)) dp.Color = new Color().FromHexString(Regex.Replace(l, pattern.Key, pattern.Value.Color));

                        stats.Series.Points.Add(dp);
                    }
                }
            }
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
                        throw new FileNotFoundException("ファイル：" + target + "が見つかりません。");
                    }
                    data.AddRange(File.ReadAllLines(target));
                    break;
            }
            return data;
        }
    }
}
