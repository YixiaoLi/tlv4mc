using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NU.OJL.MPRTOS.TLV.Core.FileContext.StatisticsData.Rules;

namespace NU.OJL.MPRTOS.TLV.Core
{
    public class StatisticsGenerationRule : INamed
    {
        /// <summary>
        /// 統計情報名　
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// グラフの設定
        /// </summary>
        public ChartSetting Setting { get; set; }
        /// <summary>
        /// 統計情報を生成するモード
        /// </summary>
        public string Mode { get; set; }
        /// <summary>
        /// DataPoint.XLabelの値がリソースファイルに定義されているリソース名の場合、そのColorを使用するか
        /// </summary>
        public bool UseResourceColor { get; set; }

        #region モード別のルール
        
        public RegexpRule RegexpRule { get; set; }
        public ScriptExtension ScriptExtension { get; set; }
        public InputRule InputRule { get; set; }

        #endregion モード別のルール

        public StatisticsGenerationRule()
        {
            Name = string.Empty;
            Setting = null;
            Mode = string.Empty;
            UseResourceColor = false;

            RegexpRule = null;
            ScriptExtension = null;
        }
    }
}
