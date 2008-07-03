using System;
using System.Collections.Generic;
using NU.OJL.MPRTOS.TLV.Architecture.PAC;

namespace NU.OJL.MPRTOS.TLV.Core.TimeLineControl.TimeLinePanel
{
    public class TimeLinePanelAgent : Agent<TimeLinePanelP, TimeLinePanelA, TimeLinePanelC>
    {
        public TimeLinePanelAgent(string name)
            : base(name, new TimeLinePanelC(name, new TimeLinePanelP(name), new TimeLinePanelA(name)))
        {

        }
    }
}
