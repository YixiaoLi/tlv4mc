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
    public class CommonFormatTraceLog : IFileContextData
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
        public TraceLogList TraceLogList { get; private set; }

        public CommonFormatTraceLog()
        {
        }

        /// <summary>
        /// <c>CommonFormatTraceLog</c>のコンストラクタ
        /// </summary>
        /// <param name="resourceData">共通形式のリソースデータ</param>
        /// <param name="traceLogData">共通形式のトレースログデータ</param>
		public CommonFormatTraceLog(ResourceData resourceData, TraceLogList traceLogData)
        {
			ResourceData = resourceData;
			TraceLogList = traceLogData;
        }

        /// <summary>
        /// パスを指定してシリアライズ
        /// </summary>
        /// <param name="path">保存する先のパス</param>
        public void Serialize(string path)
        {
            CommonFormatTraceLogSerializer.Serialize(path, this);
        }

        /// <summary>
        /// パスを指定してデシリアライズ
        /// </summary>
        /// <param name="path">読み込むパス</param>
        public void Deserialize(string path)
        {
            CommonFormatTraceLog c = CommonFormatTraceLogSerializer.Deserialize(path);
			ResourceData = c.ResourceData;
            TraceLogList = c.TraceLogList;
        }
    }
}
