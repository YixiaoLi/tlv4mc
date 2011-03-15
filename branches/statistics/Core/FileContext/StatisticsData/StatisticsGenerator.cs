using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using NU.OJL.MPRTOS.TLV.Base;
using System.Text.RegularExpressions;
using System.Drawing;
using NU.OJL.MPRTOS.TLV.Core.Controls.Forms;
using NU.OJL.MPRTOS.TLV.Core.FileContext.StatisticsData.Mode;
using System.Diagnostics;
using System.Windows.Forms.DataVisualization.Charting;

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
        /// リソースファイルで指定した統計生成ルールを使用してStatisticsDataを生成する
        /// </summary>
        /// <returns>リソースファイルで指定した全ての統計生成ルールを使用して生成した、全ての統計情報を含むStatisticsData</returns>
        public StatisticsData GenerateData()
        {
            if (_constructProgressReport != null)
            {
                _constructProgressReport(0, "統計生成ルールを読み込み中");
            }
            if (_resourceData.StatisticsGenerationRules == null
                || _resourceData.StatisticsGenerationRules.Count == 0)
            {
                return new StatisticsData();
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
                            // Key:生成ルールの各要素("Mode"等)、Value:各要素の値またはオブジェクト
                            foreach (KeyValuePair<string, Json> jj in j.Value.GetKeyValuePairEnumerator())
                            {
                                if (jrules[j.Key].ContainsKey(jj.Key))
                                {
                                    throw new InvalidStatsGenRuleException(string.Format(@"統計情報""{0}""の生成ルールで""{1}""が複数設定されています。", j.Key, jj.Key));
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

                    Statistics stats = Generate(rules[tgt]);                                       

                    if(stats.Series.Points.Count == 0)
                    {
                        throw new StatisticsGenerateException("データポイントを取得できませんでした。" +
                                            "リソースヘッダ、リソースファイルに定義された名前を使用しているか確認してください。");
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
        /// 統計生成ルールを用いて統計情報ファイルを生成する
        /// </summary>
        /// <param name="rule">統計生成ルール</param>
        /// <returns>統計情報ファイル</returns>
        public Statistics Generate(StatisticsGenerationRule rule)
        {
            Statistics stats = new Statistics(rule.Name);
            stats.Setting = rule.Setting;
            if (string.IsNullOrEmpty(rule.Setting.SeriesTitle))
            {
                stats.Setting.SeriesTitle = Path.GetFileName(_traceLogFilePath);
            }
                

            switch (rule.Mode)
            {
                case "Regexp": applyRegexpRule(stats, rule.RegexpRule); break;
                case "Script": applyScriptExtension(stats, rule.ScriptExtension); break;
                case "Basic": applyBasicRule(stats, rule.BasicRule); break;
                case "Input": applyInputRule(stats, rule.InputRule); break;
                default: throw new InvalidStatsGenRuleException(rule.Mode + "無効なスタイルです");
            }

            if (rule.UseResourceColor)
            {
                applyResourceColor(stats);
            }

            return stats;
        }

        #region 統計生成メソッド

        /// <summary>
        /// データ読み取りモードで生成
        /// </summary>
        /// <param name="stats">統計情報を格納するオブジェクト</param>
        /// <param name="rule">"RegexpRule"のオブジェクト</param>
        /// <returns></returns>
        private void applyRegexpRule(Statistics stats, RegexpRule rule)
        {
            if (rule == null
                || rule.Target == null
                || rule.Regexps == null)
            {
                throw new InvalidStatsGenRuleException("RegexpRuleに必要な項目が記述されていません");
            }

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
        /// スクリプト拡張モードで生成
        /// </summary>
        /// <param name="stats">統計情報を格納するオブジェクト(注：スクリプトで生成したオブジェクトのSettingがNullでない場合、Settingは生成したオブジェクトに置き換えられます)</param>
        /// <param name="rule">"ScriptExtension"のオブジェクト</param>
        private void applyScriptExtension(Statistics stats, ScriptExtension rule)
        {
            if (rule == null
                || rule.Target == null
                || rule.FileName == null
                || rule.Arguments == null)
            {
                throw new InvalidStatsGenRuleException("ScriptExtensionに必要な項目が記述されていません");
            }
            

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

            string sf = "";

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
                sf += p.StandardOutput.ReadLine();
            }

            if (p.ExitCode != 0)
            {
                string error = "";
                while (!p.StandardError.EndOfStream)
                {
                    error += p.StandardError.ReadLine() + "\n";
                }
                throw new StatisticsGenerateException(error);
            }
            p.Close();

            #endregion StandardFrmatConverter.generateByNewRule　の一部をコピペして修正

            // 統計情報ファイルは、ハッシュの中に統計情報を格納しているため、次の型でデシアライズが必要
            var statsList = ApplicationFactory.JsonSerializer.Deserialize<GeneralNamedCollection<Statistics>>(sf);
            if(statsList.Count > 1)
            {
                throw new StatisticsGenerateException("出力された統計情報ファイルに複数の統計情報が含まれています。");
            }

            Statistics newStats = statsList.First<Statistics>();

            // 外部プロセスで得たSettingの内容に上書き
            Json json = new Json().Parse(sf);
            foreach (KeyValuePair<string, Json> j in json.GetKeyValuePairEnumerator())
            {
                if (j.Key == "Setting")
                {
                    foreach(KeyValuePair<string, Json> jj in j.Value.GetKeyValuePairEnumerator())
                    switch (jj.Key) 
                    {
                        case "Title": stats.Setting.Title = jj.Value; break;
                        case "AxisXTitle": stats.Setting.AxisXTitle = jj.Value; break;
                        case "AxisYTitle": stats.Setting.AxisYTitle = jj.Value; break;
                        case "SeriesTitle": stats.Setting.SeriesTitle = jj.Value; break;
                        case "DefaultType": stats.Setting.DefaultType = (AvailableChartType)Enum.Parse(typeof(AvailableChartType), jj.Value); break;
                        case "MajorTickMarkInterval": stats.Setting.MajorTickMarkInterval = double.Parse(jj.Value.ToString()); break;
                        case "MinorTickMarkInterval": stats.Setting.MinorTickMarkInterval = double.Parse(jj.Value.ToString()); break;
                        case "MajorGridVisible": stats.Setting.MajorGridVisible = jj.Value; break;
                        case "MinorGridVisible": stats.Setting.MinorGridVisible = jj.Value; break;
                    }
                }
            }
            
            stats.Series = newStats.Series;
        }

        /// <summary>
        /// 基本解析モードで生成
        /// </summary>
        /// <param name="stats">統計情報を格納するオブジェクト</param>
        /// <param name="rule">"BasicRule"のオブジェクト</param>
        private void applyBasicRule(Statistics stats, BasicRule rule)
        {
            if (rule.When != null && rule.From != null)
            {
                throw new InvalidStatsGenRuleException("BasicRuleにはWhen、または、From-Toの組、のどちらかしか設定できません");
            }
            if (rule.When == null && (rule.From == null || rule.To == null))
            {
                throw new InvalidStatsGenRuleException("BasicRuleに必要な項目が記述されていません");
            }


            if (rule.When != null)
            {
                List<string> ress = rule.When.GetResourceNameList(_resourceData); // 対象とするリソース名のリスト
                
                switch (rule.Method)
                {
                    case BasicRuleMethod.Count:
                        if (rule.Interval == 0)
                        {
                            foreach (string name in ress)
                            {
                                Func<TraceLog, bool> logFilter = makeEventFilter(name, rule.When);
                                DataPoint dp = new DataPoint();

                                dp.XLabel = name;
                                dp.YValue = _traceLogData.TraceLogs.Count(logFilter);

                                stats.Series.Points.Add(dp);
                            }
                        }
                        else
                        {
                            // 系がリソースにあたるため、複数のリソースに対応するには、複数の系を一つのグラフに乗せる必要がある
                            // 現在の仕様では、系は一つしか認めていないので一つのリソースしか乗せることができないので保留
                            Func<TraceLog, bool> logFilter = makeEventFilter(ress[0], rule.When);
                            
                            decimal from = _traceLogData.MinTime.Value;
                            decimal to;

                            while(from < _traceLogData.MaxTime.Value)
                            {
                                to = from + rule.Interval;

                                DataPoint dp = new DataPoint();
                                dp.XLabel = string.Format("[{0},\n{1})", from, to);
                                dp.YValue = _traceLogData.TraceLogs
                                    .Where(
                                    (l) =>
                                    {
                                        decimal lt = l.Time.ToDecimal(_resourceData.TimeRadix);
                                        return lt >= from && lt < to;
                                    })
                                    .Where(logFilter)
                                    .Count();

                                stats.Series.Points.Add(dp);

                                from = to;
                            }
                        }
                        break;
                        
                    case BasicRuleMethod.Measure:
                        foreach (string name in ress)
                        {
                            Func<TraceLog, bool> logFilter = makeEventFilter(name, rule.When);
                            var enumarator = _traceLogData.TraceLogs.Where(logFilter);

                            DataPoint dp = new DataPoint();

                            dp.XLabel = makeXLabelForFromTo(name, rule.When, name, rule.When);

                            if (enumarator.Any())
                            {
                                TraceLog pre = enumarator.First();
                                Time pTime = new Time(pre.Time, _resourceData.TimeRadix);

                                double max = -1.0;             // 1組の中での最大値
                                double min = double.MaxValue;  // 1組の中での最小値
                                double num = 0.0;             // pre-nextの出現回数

                                foreach (TraceLog next in enumarator.Skip(1))
                                {
                                    Time nTime = new Time(next.Time, _resourceData.TimeRadix);

                                    double dis = double.Parse((nTime - pTime).Value.ToString());  // decimal -> double を無理やり行っている
                                    max = dis > max ? dis : max;
                                    min = dis < min ? dis : min;
                                    num++;

                                    dp.YValue += dis;

                                    pTime = nTime;
                                }

                                dp.YValue = dp.YValue / num; // 平均

                                if (num > 0)
                                {
                                    dp.YLabel = string.Format("ave: {0:#,#.######}\n\nmax: {1:#,#.######}\nmin: {2:#,#.######}", dp.YValue, max, min);
                                }
                            }

                            stats.Series.Points.Add(dp);
                        }
                        break;
                }
            }
            else if (rule.From != null && rule.To != null)
            {
                if (rule.From.GetResourceNameList(_resourceData).Count != rule.To.GetResourceNameList(_resourceData).Count)
                {
                    throw new InvalidStatsGenRuleException("FromとToのリソース数が違います");
                }

                // 対象とするFrom,Toの各リソース名の組のリスト
                List<FromToPair> toFromPairs = new List<FromToPair>();

                List<string> fs = rule.From.GetResourceNameList(_resourceData);
                List<string> ts = rule.To.GetResourceNameList(_resourceData);

                for(int i = 0; i < fs.Count; i++)
                {
                    toFromPairs.Add(new FromToPair(fs[i], ts[i]));
                }

                
                switch (rule.Method)
                {
                    case BasicRuleMethod.Measure:
                        
                        foreach (FromToPair p in toFromPairs)
                        {
                            Func<TraceLog, bool> fromLogFilter = makeEventFilter(p.From, rule.From);
                            Func<TraceLog, bool> toLogFilter = makeEventFilter(p.To, rule.To);

                            DataPoint dp = new DataPoint();
                            dp.XLabel = makeXLabelForFromTo(p.From, rule.From, p.To, rule.To);

                            double max = -1.0;             // 1組の中での最大値
                            double min = double.MaxValue;  // 1組の中での最小値
                            double num = 0.0;              // イベント出現回数

                            foreach (TraceLog to in _traceLogData.TraceLogs.Where(toLogFilter))
                            {
                                var d = _traceLogData.TraceLogs
                                    .Where(fromLogFilter)
                                    .Where(t => new Time(t.Time, _resourceData.TimeRadix) < new Time(to.Time, _resourceData.TimeRadix));

                                if (d.Any())
                                {
                                    TraceLog from = d.Last(); // 対象のtoから一番近いfromイベントを取り出す

                                    Time fTime = new Time(from.Time, _resourceData.TimeRadix);
                                    Time tTime = new Time(to.Time, _resourceData.TimeRadix);
                                    
                                    double dis = double.Parse((tTime - fTime).Value.ToString());  // decimal -> double を無理やり行っている
                                    max = dis > max ? dis : max;
                                    min = dis < min ? dis : min;
                                    num++;

                                    dp.YValue += dis;
                                }
                            }

                            dp.YValue = dp.YValue / num; // 平均

                            if (num > 0)
                            {
                                dp.YLabel = string.Format("ave: {0:#,#.######}\n\nmax: {1:#,#.######}\nmin: {2:#,#.######}", dp.YValue, max, min);
                            }

                            stats.Series.Points.Add(dp);
                        }
                        break;

                    case BasicRuleMethod.Count:
                    default:
                        throw new InvalidStatsGenRuleException("From-Toの組に対して無効なMethodです");
                }
            }
            
        }
        
        /// <summary>
        /// 統計情報ファイル入力モードで生成
        /// </summary>
        /// <param name="stats">統計情報を格納するオブジェクト(注：Name以外上書きされます)</param>
        /// <param name="rule"></param>
        private void applyInputRule(Statistics stats, InputRule rule)
        {
            if (rule == null || (rule.FileName == null && rule.Data == null))
            {
                throw new InvalidStatsGenRuleException("InputRuleに必要な項目が記述されていません");
            }

            Statistics newStats;
            if (rule.FileName != null)
            {
                newStats = ApplicationFactory.JsonSerializer.Deserialize<GeneralNamedCollection<Statistics>>(File.ReadAllText(rule.FileName)).Single<Statistics>();
            }
            else
            {
                newStats = rule.Data.Single<Statistics>();
            }
            stats.Setting = newStats.Setting;
            stats.Series = newStats.Series;
        }

        #endregion 統計生成メソッド

        /// <summary>
        /// 標準形式トレースログから目的のイベントをフィルタリングする匿名メソッドを取得する<para></para>
        /// LINQに属するメソッド(Whereなど)の引数として渡す匿名メソッドを想定している
        /// </summary>
        /// <param name="name">標準形式トレースログにおけるリソース名</param>
        /// <param name="bEvent">目的のイベント</param>
        /// <returns>目的のイベントをフィルタリングする匿名メソッド</returns>
        private Func<TraceLog, bool> makeEventFilter(string name, BaseEvent bEvent)
        {
            if (!string.IsNullOrEmpty(bEvent.AttributeName) && !string.IsNullOrEmpty(bEvent.AttributeValue)
                && string.IsNullOrEmpty(bEvent.BehaviorName))
            {
                return (t) =>
                {
                    return t.ObjectName == name
                        && t.Attribute == bEvent.AttributeName
                        && t.Value == bEvent.AttributeValue;
                };
            }
            else if (!string.IsNullOrEmpty(bEvent.BehaviorName))
            {
                if (string.IsNullOrEmpty(bEvent.BehaviorArg))
                {
                    return (t) =>
                    {
                        return t.ObjectName == name
                            && t.Behavior == bEvent.BehaviorName;
                    };
                }
                else
                {
                    return (t) =>
                    {
                        return t.ObjectName == name
                            && t.Behavior == bEvent.BehaviorName
                            && t.Arguments == bEvent.BehaviorArg;  // 引数の
                    };
                }
            }
            else
            {
                throw new InvalidStatsGenRuleException("イベント指定に誤りがあります");
            }
        }


        /// <summary>
        /// FromとToを設定したときの基本解析モードにおけるデータポイントのXLabel用の文字列を生成する
        /// </summary>
        /// <param name="fname">Fromのイベントにおけるリソース名</param>
        /// <param name="fevent">Fromのイベント</param>
        /// <param name="tname">Toのイベントにおけるリソース名</param>
        /// <param name="tevent">Toのイベント</param>
        /// <returns></returns>
        private string makeXLabelForFromTo(string fname, BaseEvent fevent, string tname, BaseEvent tevent)
        {
            StringBuilder sb = new StringBuilder();

            sb.Append("From: ");

            if (!string.IsNullOrEmpty(fevent.AttributeName) && !string.IsNullOrEmpty(fevent.AttributeValue)
                && string.IsNullOrEmpty(fevent.BehaviorName))
            {
                sb.Append(string.Format("{0}.{1}={2}\n", fname, fevent.AttributeName, fevent.AttributeValue));
            }
            else if (!string.IsNullOrEmpty(fevent.BehaviorName))
            {
                sb.Append(string.Format("{0}.{1}({2})\n", fname, fevent.BehaviorName, fevent.BehaviorArg));
            }
            else
            {
                throw new Exception("不正なログです");
            }


            sb.Append("To: ");

            if (!string.IsNullOrEmpty(fevent.AttributeName) && !string.IsNullOrEmpty(fevent.AttributeValue)
                 && string.IsNullOrEmpty(fevent.BehaviorName))
            {
                sb.Append(string.Format("{0}.{1}={2}", tname, tevent.AttributeName, tevent.AttributeValue));
            }
            else if (!string.IsNullOrEmpty(fevent.BehaviorName))
            {
                sb.Append(string.Format("{0}.{1}({2})", tname, tevent.BehaviorName, tevent.BehaviorArg));
            }
            else
            {
                throw new Exception("不正なログです");
            }

            return sb.ToString();
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
                    data.AddRange(File.ReadAllLines(target));
                    break;
            }
            return data;
        }
    }
}
