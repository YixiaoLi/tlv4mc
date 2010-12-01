using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NU.OJL.MPRTOS.TLV.Core.FileContext.StatisticsData.Rules
{
    public class FromToPair
    {
        public string From { get; set; }
        public string To { get; set; }

        public FromToPair(string from, string to)
        {
            From = from;
            To = to;
        }
    }
}
