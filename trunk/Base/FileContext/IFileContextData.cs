
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NU.OJL.MPRTOS.TLV.Base
{
    /// <summary>
    /// IFileContextが管理できるデータのインターフェイス
    /// </summary>
    public interface IFileContextData : ISerializable
    {
        /// <summary>
        /// 最新の状態ではなくなったときに発生するイベント
        /// </summary>
        event EventHandler<GeneralEventArgs<bool>> IsDirtyChanged;
        
        /// <summary>
        /// ファイルが最新の状態ではないかどうか（最新のときfalse）
        /// </summary>
        bool IsDirty { get; set; }
    }
}
