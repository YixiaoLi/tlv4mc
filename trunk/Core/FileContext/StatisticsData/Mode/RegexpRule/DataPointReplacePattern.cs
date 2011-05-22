using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NU.OJL.MPRTOS.TLV.Core.FileContext.StatisticsData.Mode
{
    /// <summary>
    /// マッチした場合のDataPointの各プロパティへ格納するデータ(または置換パターン(例：${置換}))を表すクラス
    /// </summary>
    public class DataPointReplacePattern
    {
        /// <summary>
        /// DataPoint.XLabelに格納する文字列または置換パターン
        /// </summary>
        public string XLabel { get; set; }
        /// <summary>
        /// DataPoint.XValueに格納する文字列または置換パターン
        /// </summary>
        public string XValue { get; set; }
        /// <summary>
        /// DataPoint.YValueに格納する文字列または置換パターン
        /// </summary>
        public string YValue { get; set; }
        /// <summary>
        /// DataPoint.Colorに格納する文字列または置換パターン
        /// </summary>
        public string Color { get; set; }
    }
}
