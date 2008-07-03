using System;
using System.Collections.Generic;
using NU.OJL.MPRTOS.TLV.Architecture.PAC;

namespace NU.OJL.MPRTOS.TLV.Core.TimeLineControl.TimeLine
{
    public class TimeLineC : Control<TimeLineP, TimeLineA>
    {
        public TimeLineC(string name, TimeLineP presentation, TimeLineA abstraction)
            : base(name, presentation, abstraction)
        {
        }
    }
}
