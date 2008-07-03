using System;
using System.Collections.Generic;
using NU.OJL.MPRTOS.TLV.Architecture.PAC;

namespace NU.OJL.MPRTOS.TLV.Core.TimeLineControl.TimeLineGrid
{
    public class TimeLineGridC : Control<TimeLineGridP, TimeLineGridA>
    {
        public TimeLineGridC(string name, TimeLineGridP presentation, TimeLineGridA abstraction)
            : base(name, presentation, abstraction)
        {
        }
    }
}
