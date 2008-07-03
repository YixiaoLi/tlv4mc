using System;
using System.Collections.Generic;
using NU.OJL.MPRTOS.TLV.Architecture.PAC;

namespace NU.OJL.MPRTOS.TLV.Core.TimeLineControl.TimeLine
{
    public class TimeLineAgent : Agent<TimeLineP, TimeLineA, TimeLineC>
    {
        public TimeLineAgent(string name)
            : base(name, new TimeLineC(name, new TimeLineP(name), new TimeLineA(name)))
        {

        }
    }
}
