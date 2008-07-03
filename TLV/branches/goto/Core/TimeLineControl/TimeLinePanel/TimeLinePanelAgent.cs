using System;
using System.Drawing;
using System.Windows.Forms;
using System.Collections.Generic;
using NU.OJL.MPRTOS.TLV.Architecture.PAC;
using NU.OJL.MPRTOS.TLV.Core.TimeLineControl.TimeLineGrid;

namespace NU.OJL.MPRTOS.TLV.Core.TimeLineControl.TimeLinePanel
{
    public class TimeLinePanelAgent : Agent<TimeLinePanelP, TimeLinePanelA, TimeLinePanelC>
    {
        private TimeLineGridAgent timeLineGridAgent = new TimeLineGridAgent("TimeLineGrid");

        public TimeLinePanelAgent(string name)
            : base(name, new TimeLinePanelC(name, new TimeLinePanelP(name), new TimeLinePanelA(name)))
        {
            this.Add(timeLineGridAgent);

            timeLineGridAgent.P.Dock = DockStyle.Fill;

        }
    }
}
