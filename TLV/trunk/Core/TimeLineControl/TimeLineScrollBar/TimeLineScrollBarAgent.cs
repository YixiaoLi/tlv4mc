using System;
using System.Collections.Generic;
using NU.OJL.MPRTOS.TLV.Architecture.PAC;

namespace NU.OJL.MPRTOS.TLV.Core.TimeLineControl.TimeLineScrollBar
{
    public class TimeLineScrollBarAgent : Agent<TimeLineScrollBarP, TimeLineScrollBarA, TimeLineScrollBarC>
    {
        public TimeLineScrollBarAgent(string name)
            : base(name, new TimeLineScrollBarC(name, new TimeLineScrollBarP(name), new TimeLineScrollBarA(name)))
        {

        }
    }
}
