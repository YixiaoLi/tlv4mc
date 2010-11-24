using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NU.OJL.MPRTOS.TLV.Core.FileContext.StatisticsData.Rules
{
    public enum BasicRuleMethod
    {
        Count
    }

    public class BasicRule
    {
        public BasicRuleMethod Method { get; set; }
        public double Interval { get; set; }
        public BaseEvent From { get; set; }
        public BaseEvent To { get; set; }
        public BaseEvent When { get; set; }
    }

    public class BaseEvent
    {
        public string ResourceName { get; set; }
        public string ResourceType { get; set; }
        public string AttributeName { get; set; }
        public string AttributeValue { get; set; }   // 必ずしも文字列とは限らないので要修正
        public string BehaviorName { get; set; }
        public string BehaviorArg { get; set; }
    }
}
