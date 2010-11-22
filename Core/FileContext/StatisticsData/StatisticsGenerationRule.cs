using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NU.OJL.MPRTOS.TLV.Core.FileContext.StatisticsData.Rules;

namespace NU.OJL.MPRTOS.TLV.Core
{
    public class StatisticsGenerationRule : INamed
    {
        public string Name { get; set; }
        public ChartSetting Setting { get; set; }
        public string Mode { get; set; }
        public bool UseResourceColor { get; set; }

        public RegexpRule RegexpRule 
        { 
            get
            {
                return (RegexpRule)_rule["RegexpRule"];
            }
            set
            {
                _rule["RegexpRule"] = value;
            }
        }
        public ScriptExtension ScriptExtension
        {
            get
            {
                return (ScriptExtension)_rule["ScriptExtension"];
            }
            set
            {
                _rule["ScriptExtension"] = value;
            }
        }

        public StatisticsGenerationRule()
        {
            Name = string.Empty;
            Setting = null;
            Mode = string.Empty;
            UseResourceColor = false;
            RegexpRule = null;
            ScriptExtension = null;
        }

        public void Apply(Statistics stats)
        {
            switch(Mode)
            {
                case "Regexp" : RegexpRule.Apply(stats); break;
                case "Script" : ScriptExtension.Apply(stats);break;
                default : throw new Exception(string.Format(@"統計生成ルール ""{0}"" に Mode が記述されていないか、無効なモードです。", Name));
            }
        }
    }
}
