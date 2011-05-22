using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NU.OJL.MPRTOS.TLV.Base;

namespace NU.OJL.MPRTOS.TLV.Core.FileContext.StatisticsData.Mode
{
    public enum BasicRuleMethod
    {
        Count,
        Measure
    }

    public class BasicRule
    {
        /// <summary>
        /// 統計の取り方
        /// </summary>
        public BasicRuleMethod Method { get; set; }
        /// <summary>
        /// 時間間隔
        /// </summary>
        public decimal Interval { get; set; }
        /// <summary>
        /// 始点となるイベント
        /// </summary>
        public BaseEvent From { get; set; }
        /// <summary>
        /// 終点となるイベント
        /// </summary>
        public BaseEvent To { get; set; }
        /// <summary>
        /// 始点と終点が同じイベント
        /// </summary>
        public BaseEvent When { get; set; }
    }

}
