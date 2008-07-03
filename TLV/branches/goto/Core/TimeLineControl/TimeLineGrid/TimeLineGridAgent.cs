using System;
using System.Collections.Generic;
using NU.OJL.MPRTOS.TLV.Architecture.PAC;

namespace NU.OJL.MPRTOS.TLV.Core.TimeLineControl.TimeLineGrid
{
    public class TimeLineGridAgent : Agent<TimeLineGridP, TimeLineGridA, TimeLineGridC>
    {
        public TimeLineGridAgent(string name)
            : base(name, new TimeLineGridC(name, new TimeLineGridP(name), new TimeLineGridA(name)))
        {

        }
    }
}
