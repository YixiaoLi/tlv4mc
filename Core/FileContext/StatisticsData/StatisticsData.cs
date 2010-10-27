using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NU.OJL.MPRTOS.TLV.Base;

namespace NU.OJL.MPRTOS.TLV.Core
{
    /// <summary>
    /// 統計情報に関するデータ
    /// </summary>
    public class StatisticsData : IJsonable<StatisticsData>
    {
        public GeneralNamedCollection<Statistics> Statisticses { get; private set; }

        public StatisticsData()
        {
            Statisticses = new GeneralNamedCollection<Statistics>();
        }

        public string ToJson()
        {
            return ApplicationFactory.JsonSerializer.Serialize(this);
        }

        public StatisticsData Parse(string statisticsData)
        {
            return ApplicationFactory.JsonSerializer.Deserialize<StatisticsData>(statisticsData);
        }
    }
}
