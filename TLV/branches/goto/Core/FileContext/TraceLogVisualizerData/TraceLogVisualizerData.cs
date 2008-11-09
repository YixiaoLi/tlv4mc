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
		public TraceLogVisualizerData(ResourceData resourceData, TraceLogList traceLogList, VisualizeData visualizeData)
        {
			ResourceData = resourceData;
			TraceLogData = new TraceLogData(traceLogList, resourceData);
			VisualizeData = visualizeData;
        }

        /// <summary>
        /// パスを指定してシリアライズ
        /// </summary>
        /// <param name="path">保存する先のパス</param>
        public void Serialize(string path)
        {
			// 一時ディレクトリ作成
			string tmpDirPath = Path.GetTempPath() + "tlv_convertRuleTmp_" + DateTime.Now.Ticks.ToString() + @"\";
			Directory.CreateDirectory(tmpDirPath);

			IZip zip = ApplicationFactory.Zip;

			string name = Path.GetFileNameWithoutExtension(path);

			File.WriteAllText(tmpDirPath + name + "." + Properties.Resources.ResourceFileExtension, ResourceData.ToJson());
			File.WriteAllText(tmpDirPath + name + "." + Properties.Resources.TraceLogFileExtension, TraceLogData.TraceLogList.ToJson());
			File.WriteAllText(tmpDirPath + name + "." + Properties.Resources.VisualizeRuleFileExtension, VisualizeData.ToJson());

			zip.Compress(path, tmpDirPath);

			Directory.Delete(tmpDirPath, true);
        }

        /// <summary>
        /// パスを指定してデシリアライズ
        /// </summary>
        /// <param name="path">読み込むパス</param>
        public void Deserialize(string path)
        {
			// 一時ディレクトリ作成
			string tmpDirPath = Path.GetTempPath() + "tlv_convertRuleTmp_" + DateTime.Now.Ticks.ToString() + @"\";
			Directory.CreateDirectory(tmpDirPath);

			IZip zip = ApplicationFactory.Zip;

			zip.Extract(path, tmpDirPath);

			string name = Path.GetFileNameWithoutExtension(path);

			ResourceData res = new ResourceData().Parse(File.ReadAllText(tmpDirPath + name + "." + Properties.Resources.ResourceFileExtension));
			TraceLogList log = new TraceLogList().Parse(File.ReadAllText(tmpDirPath + name + "." + Properties.Resources.TraceLogFileExtension));
			VisualizeData viz = new VisualizeData().Parse(File.ReadAllText(tmpDirPath + name + "." + Properties.Resources.VisualizeRuleFileExtension));

			TraceLogVisualizerData c = new TraceLogVisualizerData(res, log, viz);
			ResourceData = c.ResourceData;
			TraceLogData = c.TraceLogData;
			VisualizeData = c.VisualizeData;
        }
    }
}
