using System;
using System.Collections.Generic;
using NU.OJL.MPRTOS.TLV.Architecture.PAC;

namespace NU.OJL.MPRTOS.TLV.Core.TimeLineControl.TimeLineMarkerManager
{
    public class TimeLineMarkerManagerAgent : Agent<TimeLineMarkerManagerP, TimeLineMarkerManagerA, TimeLineMarkerManagerC>
    {
        public TimeLineMarkerManagerAgent(string name)
            : base(name, new TimeLineMarkerManagerC(name, new TimeLineMarkerManagerP(name), new TimeLineMarkerManagerA(name)))
        {

        }
    }
}
