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
                if(_isDirty != value)
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
		public TraceLogVisualizerData(ResourceData resourceData, TraceLogData traceLogData, VisualizeData visualizeData, SettingData settingData)
        {
			ResourceData = resourceData;
			TraceLogData = traceLogData;
			VisualizeData = visualizeData;
			SettingData = settingData;

			setVisualizeRuleToEvent();
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
			if(!Directory.Exists(ApplicationData.Setting.TemporaryDirectoryPath))
				Directory.CreateDirectory(ApplicationData.Setting.TemporaryDirectoryPath);

			string targetTmpDirPath = Path.Combine(ApplicationData.Setting.TemporaryDirectoryPath, "tlv_convertRuleTmp_" + DateTime.Now.Ticks.ToString() + @"\");
			Directory.CreateDirectory(targetTmpDirPath);

			IZip zip = ApplicationFactory.Zip;

			string name = Path.GetFileNameWithoutExtension(path);

			File.WriteAllText(targetTmpDirPath + name + "." + Properties.Resources.ResourceFileExtension, ResourceData.ToJson());
			File.WriteAllText(targetTmpDirPath + name + "." + Properties.Resources.TraceLogFileExtension, TraceLogData.TraceLogs.ToJson());
			File.WriteAllText(targetTmpDirPath + name + "." + Properties.Resources.VisualizeRuleFileExtension, VisualizeData.ToJson());
			File.WriteAllText(targetTmpDirPath + name + "." + Properties.Resources.SettingFileExtension, SettingData.ToJson());

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

			IZip zip = ApplicationFactory.Zip;

			zip.Extract(path, targetTmpDirPath);

			string resFilePath = Directory.GetFiles(targetTmpDirPath, "*." + Properties.Resources.ResourceFileExtension)[0];
			string logFilePath = Directory.GetFiles(targetTmpDirPath, "*." + Properties.Resources.TraceLogFileExtension)[0];
			string vixFilePath = Directory.GetFiles(targetTmpDirPath, "*." + Properties.Resources.VisualizeRuleFileExtension)[0];
			string settingFilePath = Directory.GetFiles(targetTmpDirPath, "*." + Properties.Resources.SettingFileExtension)[0];

			ResourceData res = ApplicationFactory.JsonSerializer.Deserialize<ResourceData>(File.ReadAllText(resFilePath));
			TraceLogList log = ApplicationFactory.JsonSerializer.Deserialize<TraceLogList>(File.ReadAllText(logFilePath));
			VisualizeData viz = ApplicationFactory.JsonSerializer.Deserialize<VisualizeData>(File.ReadAllText(vixFilePath));
			SettingData setting = ApplicationFactory.JsonSerializer.Deserialize<SettingData>(File.ReadAllText(settingFilePath));
			
			ResourceData = res;
			TraceLogData = new TraceLogData(log, res);
			VisualizeData = viz;
			SettingData = setting;
			
			setVisualizeRuleToEvent();

			Directory.Delete(targetTmpDirPath, true);
        }
    }
}
