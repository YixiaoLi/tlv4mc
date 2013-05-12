/*
 *  TLV - Trace Log Visualizer
 *
 *  Copyright (C) 2008-2010 by Nagoya Univ., JAPAN
 *
 *  上記著作権者は，以下の(1)〜(4)の条件を満たす場合に限り，本ソフトウェ
 *  ア（本ソフトウェアを改変したものを含む．以下同じ）を使用・複製・改
 *  変・再配布（以下，利用と呼ぶ）することを無償で許諾する．
 *  (1) 本ソフトウェアをソースコードの形で利用する場合には，上記の著作
 *      権表示，この利用条件および下記の無保証規定が，そのままの形でソー
 *      スコード中に含まれていること．
 *  (2) 本ソフトウェアを，ライブラリ形式など，他のソフトウェア開発に使
 *      用できる形で再配布する場合には，再配布に伴うドキュメント（利用
 *      者マニュアルなど）に，上記の著作権表示，この利用条件および下記
 *      の無保証規定を掲載すること．
 *  (3) 本ソフトウェアを，機器に組み込むなど，他のソフトウェア開発に使
 *      用できない形で再配布する場合には，次のいずれかの条件を満たすこ
 *      と．
 *    (a) 再配布に伴うドキュメント（利用者マニュアルなど）に，上記の著
 *        作権表示，この利用条件および下記の無保証規定を掲載すること．
 *    (b) 再配布の形態を，別に定める方法によって，TOPPERSプロジェクトに
 *        報告すること．
 *  (4) 本ソフトウェアの利用により直接的または間接的に生じるいかなる損
 *      害からも，上記著作権者およびTOPPERSプロジェクトを免責すること．
 *      また，本ソフトウェアのユーザまたはエンドユーザからのいかなる理
 *      由に基づく請求からも，上記著作権者およびTOPPERSプロジェクトを
 *      免責すること．
 *
 *  本ソフトウェアは，無保証で提供されているものである．上記著作権者お
 *  よびTOPPERSプロジェクトは，本ソフトウェアに関して，特定の使用目的
 *  に対する適合性も含めて，いかなる保証も行わない．また，本ソフトウェ
 *  アの利用により直接的または間接的に生じたいかなる損害に関しても，そ
 *  の責任を負わない．
 *
 *  @(#) $Id$
 */
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;
using NU.OJL.MPRTOS.TLV.Base;

namespace NU.OJL.MPRTOS.TLV.Core
{
    /// <summary>
    /// 共通形式トレースログおよびマーカー等の情報を表すクラス
    /// </summary>
    public class TraceLogVisualizerData : IFileContextData
    {
        private bool _isDirty = false;

        /// <summary>
        /// データが更新されたときに発生するイベント
        /// </summary>
        public event EventHandler<GeneralEventArgs<bool>> IsDirtyChanged = null;

        /// <summary>
        /// データが更新されているかどうか
        /// </summary>
        public bool IsDirty
        {
            get { return _isDirty; }
            set
            {
                if (_isDirty != value)
                {
                    _isDirty = value;

                    if (IsDirtyChanged != null)
                        IsDirtyChanged(this, new GeneralEventArgs<bool>(_isDirty));
                }
            }
        }
        /// <summary>
        /// リソースデータ
        /// </summary>
        public ResourceData ResourceData { get; private set; }
        /// <summary>
        /// トレースログのリスト
        /// </summary>
        public TraceLogData TraceLogData { get; private set; }
        /// <summary>
        /// 可視化データ
        /// </summary>
        public VisualizeData VisualizeData { get; set; }
        /// <summary>
        /// 設定データ
        /// </summary>
        public SettingData SettingData { get; set; }

        /// <summary>
        /// 図形データ
        /// </summary>
        public VisualizeShapeData VisualizeShapeData { get; set; }

        /// <summary>
        /// 統計データ
        /// </summary>
        public StatisticsData StatisticsData { get; set; }

        /// <summary>
        /// <c>CommonFormatTraceLog</c>のインスタンスを生成する
        /// </summary>
        public TraceLogVisualizerData()
        {
        }

        /// <summary>
        /// <c>CommonFormatTraceLog</c>のインスタンスを生成する
        /// </summary>
        /// <param name="resourceData">共通形式のリソースデータ</param>
        /// <param name="traceLogData">共通形式のトレースログデータ</param>
        public TraceLogVisualizerData(ResourceData resourceData, TraceLogData traceLogData, VisualizeData visualizeData, SettingData settingData, VisualizeShapeData shapesData)
        {
            ResourceData = resourceData;
            TraceLogData = traceLogData;
            VisualizeData = visualizeData;
            SettingData = settingData;
            VisualizeShapeData = shapesData;

            setVisualizeRuleToEvent();
        }

        /// <summary>
        /// <c>CommonFormatTraceLog</c>のインスタンスを生成する
        /// </summary>
        /// <param name="resourceData">共通形式のリソースデータ</param>
        /// <param name="traceLogData">共通形式のトレースログデータ</param>
        /// <param name="visualizeData">可視化データ</param>
        /// <param name="settingData">設定データ</param>
        /// <param name="shapesData">図形データ</param>
        /// <param name="statisticsData">統計データ</param>
        public TraceLogVisualizerData(ResourceData resourceData, TraceLogData traceLogData, VisualizeData visualizeData, SettingData settingData, VisualizeShapeData shapesData, StatisticsData statisticsData)
            : this(resourceData, traceLogData, visualizeData, settingData, shapesData)
        {
            StatisticsData = statisticsData;
        }

        private void setVisualizeRuleToEvent()
        {
            foreach (VisualizeRule rule in VisualizeData.VisualizeRules)
            {
                foreach (Event evnt in rule.Shapes)
                {
                    evnt.SetVisualizeRuleName(rule.Name);
                }
            }
        }

        /// <summary>
        /// パスを指定してシリアライズ
        /// </summary>
        /// <param name="path">保存する先のパス</param>
        public void Serialize(string path)
        {
            // 一時ディレクトリ作成
            if (!Directory.Exists(ApplicationData.Setting.TemporaryDirectoryPath))
                Directory.CreateDirectory(ApplicationData.Setting.TemporaryDirectoryPath);

            string targetTmpDirPath = Path.Combine(ApplicationData.Setting.TemporaryDirectoryPath, "tlv_convertRuleTmp_" + DateTime.Now.Ticks.ToString() + @"\");
            Directory.CreateDirectory(targetTmpDirPath);

            string targetTmpStatisticsDirPath = Path.Combine(targetTmpDirPath, Properties.Resources.DefaultStatisticsDirectoryName + @"\");
            Directory.CreateDirectory(targetTmpStatisticsDirPath);

            IZip zip = ApplicationFactory.Zip;

            string name = Path.GetFileNameWithoutExtension(path);

            File.WriteAllText(targetTmpDirPath + name + "." + Properties.Resources.ResourceFileExtension, ResourceData.ToJson());
            File.WriteAllText(targetTmpDirPath + name + "." + Properties.Resources.TraceLogFileExtension, TraceLogData.TraceLogs.ToJson());
            File.WriteAllText(targetTmpDirPath + name + "." + Properties.Resources.VisualizeRuleFileExtension, VisualizeData.ToJson());
            File.WriteAllText(targetTmpDirPath + name + "." + Properties.Resources.SettingFileExtension, SettingData.ToJson());
            File.WriteAllText(targetTmpDirPath + name + "." + Properties.Resources.VisualizeShapesFileExtension, VisualizeShapeData.ToJson());
            foreach (Statistics s in StatisticsData.Statisticses)
            {
                StatisticsData sd = new StatisticsData();
                sd.Statisticses.Add(s);
                File.WriteAllText(targetTmpStatisticsDirPath + name + "-" + s.Name + "." + Properties.Resources.StatisticsFileExtension, sd.Statisticses.ToJson());
            }
            zip.Compress(path, targetTmpDirPath);

            Directory.Delete(targetTmpDirPath, true);
        }

        /// <summary>
        /// パスを指定してデシリアライズ
        /// </summary>
        /// <param name="path">読み込むパス</param>
        public void Deserialize(string path)
        {
            // 一時ディレクトリ作成
            if (!Directory.Exists(ApplicationData.Setting.TemporaryDirectoryPath))
                Directory.CreateDirectory(ApplicationData.Setting.TemporaryDirectoryPath);

            string targetTmpDirPath = Path.Combine(ApplicationData.Setting.TemporaryDirectoryPath, "tlv_convertRuleTmp_" + DateTime.Now.Ticks.ToString() + @"\");
            Directory.CreateDirectory(targetTmpDirPath);

            string targetTmpStatisticsDirPath = Path.Combine(targetTmpDirPath, Properties.Resources.DefaultStatisticsDirectoryName + @"\");

            IZip zip = ApplicationFactory.Zip;

            zip.Extract(path, targetTmpDirPath);

            string resFilePath = Directory.GetFiles(targetTmpDirPath, "*." + Properties.Resources.ResourceFileExtension)[0];
            string logFilePath = Directory.GetFiles(targetTmpDirPath, "*." + Properties.Resources.TraceLogFileExtension)[0];
            string vixFilePath = Directory.GetFiles(targetTmpDirPath, "*." + Properties.Resources.VisualizeRuleFileExtension)[0];
            string settingFilePath = Directory.GetFiles(targetTmpDirPath, "*." + Properties.Resources.SettingFileExtension)[0];
            string visualizeShapesPath = Directory.GetFiles(targetTmpDirPath, "*." + Properties.Resources.VisualizeShapesFileExtension)[0];
 
            ResourceData res = ApplicationFactory.JsonSerializer.Deserialize<ResourceData>(File.ReadAllText(resFilePath));
            TraceLogList log = ApplicationFactory.JsonSerializer.Deserialize<TraceLogList>(File.ReadAllText(logFilePath));
            VisualizeData viz = ApplicationFactory.JsonSerializer.Deserialize<VisualizeData>(File.ReadAllText(vixFilePath));
            SettingData setting = ApplicationFactory.JsonSerializer.Deserialize<SettingData>(File.ReadAllText(settingFilePath));
            VisualizeShapeData shapes = ApplicationFactory.JsonSerializer.Deserialize<VisualizeShapeData>(File.ReadAllText(visualizeShapesPath));
            StatisticsData stad = new StatisticsData();
            
            if (Directory.Exists(targetTmpStatisticsDirPath))
            {
                string[] statisticsFilePathes = Directory.GetFiles(targetTmpStatisticsDirPath, "*." + Properties.Resources.StatisticsFileExtension);
                foreach (string staPath in statisticsFilePathes)
                {
                    GeneralNamedCollection<Statistics> sd = ApplicationFactory.JsonSerializer.Deserialize<GeneralNamedCollection<Statistics>>(File.ReadAllText(staPath));
                    stad.Statisticses.Add(sd.Single<Statistics>());
                }
            }

            ResourceData = res;
            TraceLogData = new TraceLogData(log, res);
            VisualizeData = viz;
            SettingData = setting;
            VisualizeShapeData = shapes;
            StatisticsData = stad;

            setVisualizeRuleToEvent();

            foreach (KeyValuePair<string, TimeLineMarker> tlm in (ObservableDictionary<string, TimeLineMarker>)(SettingData.LocalSetting.TimeLineMarkerManager.Markers))
            {
                tlm.Value.Name = tlm.Key;
            }

            Directory.Delete(targetTmpDirPath, true);
        }
    }
}
