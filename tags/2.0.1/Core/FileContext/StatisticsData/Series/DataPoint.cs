using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NU.OJL.MPRTOS.TLV.Base;
using System.Drawing;

namespace NU.OJL.MPRTOS.TLV.Core
{
    /// <summary>
    /// 統計情報の1つのデータポイントを表すクラス
    /// </summary>
    public class DataPoint
    {
        /// <summary>
        /// X値のラベル　X値を文字列で表現する際に使用
        /// </summary>
        public string XLabel { get; set; }
        /// <summary>
        /// Xの値
        /// </summary>
        public double XValue { get; set; }
        /// <summary>
        /// Y値のラベル
        /// </summary>
        public string YLabel { get; set; }
        /// <summary>
        /// Yの値
        /// </summary>
        public double YValue { get; set; }
        /// <summary>
        /// グラフ上でこのデータを表す色
        /// </summary>
        public Color? Color { get; set; }
        /// <summary>
        /// グラフに表示するかどうか
        /// </summary>
        public bool Visible { get; set; }

        public DataPoint()
        {
            XLabel = string.Empty;
            XValue = 0;
            YLabel = string.Empty;
            YValue = 0;
            Color = null;
            Visible = true;
        }
    }
}
