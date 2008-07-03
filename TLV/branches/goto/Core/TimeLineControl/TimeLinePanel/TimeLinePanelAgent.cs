using System;
using System.Collections.Generic;
using NU.OJL.MPRTOS.TLV.Architecture.PAC;

namespace NU.OJL.MPRTOS.TLV.Core.TimeLineControl.TimeLinePanel
{
    public class TimeLinePanelAgentAgent : Agent<TimeLinePanelP, TimeLinePanelA, TimeLinePanelC>
    {
        public TimeLinePanelAgentAgent(string name)
            : base(name, new TimeLinePanelC(name, new TimeLinePanelP(name), new TimeLinePanelA(name)))
        {

        }
    }
}
