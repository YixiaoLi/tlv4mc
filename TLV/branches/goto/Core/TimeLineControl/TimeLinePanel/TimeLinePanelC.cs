using System;
using System.Collections.Generic;
using NU.OJL.MPRTOS.TLV.Architecture.PAC;

namespace NU.OJL.MPRTOS.TLV.Core.TimeLineControl.TimeLinePanel
{
    public class TimeLinePanelC : Control<TimeLinePanelP, TimeLinePanelA>
    {
        public TimeLinePanelC(string name, TimeLinePanelP presentation, TimeLinePanelA abstraction)
            : base(name, presentation, abstraction)
        {
        }
    }
}
