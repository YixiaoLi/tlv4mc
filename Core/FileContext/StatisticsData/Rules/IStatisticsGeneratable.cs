using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NU.OJL.MPRTOS.TLV.Core.FileContext.StatisticsData.Rules
{
    public interface IStatisticsGeneratable
    {
        void Apply(Statistics stats);
    }
}
