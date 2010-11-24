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
using System.Diagnostics;

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
                        case "Regexp": applyRegexpRule(stats, rules[tgt].RegexpRule); break;
                        case "Script": applyScriptExtension(stats, rules[tgt].ScriptExtension);  break;
                        default: throw new StatisticsGenerateException(rules[tgt].Mode + "無効なスタイルです");
                    }

                    if (rules[tgt].UseResourceColor)
                    {
                        applyResourceColor(stats);
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
        private void applyRegexpRule(Statistics stats, RegexpRule rule)
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


        /// <summary>
        /// ScriptExtensionで生成
        /// </summary>
        /// <param name="stats">統計情報を格納するオブジェクト(注：スクリプトで生成したオブジェクトのSettingがNullでない場合、Settingは生成したオブジェクトに置き換えられます)</param>
        /// <param name="rule">"ScriptExtension"のオブジェクト</param>
        private void applyScriptExtension(Statistics stats, ScriptExtension rule)
        {
            List<string> data = getTargetData(rule.Target);

            #region StandardFrmatConverter.generateByNewRule　の一部をコピペして修正

            ProcessStartInfo psi;
            if (rule.Script != null)
            {
                string path = Path.GetTempFileName();
                StreamWriter sw = new StreamWriter(new FileStream(path, FileMode.Create));

                string script = rule.Script;
                sw.Write(script);
                sw.Close();
                psi = new ProcessStartInfo(rule.FileName,
                                           string.Format(rule.Arguments, path));  // Argumentsが"{0}"であれば一時ファイルを使用し、そうでなければ指定ファイルを使用する
            }
            else
            {
                psi = new ProcessStartInfo(rule.FileName, rule.Arguments);
            }
            psi.UseShellExecute = false;
            psi.RedirectStandardInput = true;
            psi.RedirectStandardOutput = true;
            psi.RedirectStandardError = true;

            Process p = new Process();
            p.StartInfo = psi;
            string AppPath = System.Windows.Forms.Application.StartupPath;
            p.StartInfo.WorkingDirectory = AppPath;

            string json = "";

            p.Start();
            p.StandardInput.WriteLine(_resourceData.ToJson());  // 修正：変数名をこのクラス用へ
            p.StandardInput.WriteLine("---");

            // 修正：TraceLog -> string, TraceLogData.TraceLogs -> data
            foreach (string d in data)
            {
                p.StandardInput.WriteLine(d);
            }


            p.StandardInput.Close();

            while (!(p.HasExited && p.StandardOutput.EndOfStream))
            {
                json += p.StandardOutput.ReadLine();
            }

            if (p.ExitCode != 0)
            {
                string error = "";
                while (!p.StandardError.EndOfStream)
                {
                    error += p.StandardError.ReadLine() + "\n";
                }
                throw new Exception(error);
            }
            p.Close();

            #endregion StandardFrmatConverter.generateByNewRule　の一部をコピペして修正

            Statistics newStats = ApplicationFactory.JsonSerializer.Deserialize<Statistics>(json);


            if (newStats.Setting != null)
            {
                stats.Setting = newStats.Setting;
            }
            stats.Series = newStats.Series;
        }


        /// <summary>
        /// DataPoint.XLabel値がリソースファイルで定義されたリソース名である場合、リソースファイルで定義されたカラーをグラフ設定で使用する
        /// </summary>
        /// <param name="stats"></param>
        private void applyResourceColor(Statistics stats)
        {
            foreach (DataPoint d in stats.Series.Points)
            {
                if (_resourceData.Resources.ContainsKey(d.XLabel))
                {
                    d.Color = _resourceData.Resources[d.XLabel].Color;
                }
            }

        }

        /// <summary>
        /// ルールファイルの"Target"で指定したファイルの内容を取得する
        /// <para></para>
        /// <para>standard: 標準形式トレースログ</para>
        /// <para>nonstandard: 標準形式へ変換する前のログ(TLVへ入力したログ)</para>
        /// <para>ファイルパス: 統計情報を取得するために用いるファイルのフルパス</para>
        /// </summary>
        /// <param name="target">"Target"に指定可能な文字列</param>
        /// <returns>ファイルの内容</returns>
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
