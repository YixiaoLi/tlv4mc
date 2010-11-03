using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NU.OJL.MPRTOS.TLV.Core
{
    public class Series
    {
        public string Label { get; set; }
        public DataPointList Points { get; set; }

        public Series()
        {
            Label = null;
            Points = new DataPointList();
        }
    }
}
