using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NU.OJL.MPRTOS.TLV.Base;

namespace NU.OJL.MPRTOS.TLV.Core
{
    /// <summary>
    /// 使わない
    /// </summary>
    public class StatisticsGenerationRule
    {
        /// <summary>
        /// 統計情報名
        /// </summary>
        public string Name { get; set; }

        public string Style { get; set; }

        public string Target { get; set; }

        public ChartSetting Setting { get; set; }

        public StatisticsGenerationRule()
        {
            Name = string.Empty;
            Style = string.Empty;
            Target = string.Empty;
            Setting = null;
        }
    }

}
