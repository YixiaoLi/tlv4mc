using System;
using System.Drawing;
using System.Windows.Forms;
using System.Collections.Generic;
using NU.OJL.MPRTOS.TLV.Base;
using NU.OJL.MPRTOS.TLV.Architecture.PAC;
using NU.OJL.MPRTOS.TLV.Core.Base;
using NU.OJL.MPRTOS.TLV.Core.TimeLineControl.TimeLineGrid;
using NU.OJL.MPRTOS.TLV.Core.TimeLineControl.TimeLine;
using NU.OJL.MPRTOS.TLV.Core.TimeLineControl.TimeLineScrollBar;
using NU.OJL.MPRTOS.TLV.Core.TimeLineControl.TimeLineMarkerManager;
using NU.OJL.MPRTOS.TLV.Core.ViewableObject;
using NU.OJL.MPRTOS.TLV.Core.ViewableObject.KernelObject.TaskInfo;

namespace NU.OJL.MPRTOS.TLV.Core.TimeLineControl
{
    public class TimeLineControlAgent : Agent<TimeLineControlP, TimeLineControlA, TimeLineControlC>
    {
        private TimeLineGridAgent<TaskInfo> timeLineGridAgent = new TimeLineGridAgent<TaskInfo>("TimeLineGrid");
        private TimeLineAgent topTimeLineAgent = new TimeLineAgent("TopTimeLineAgent");
        private TimeLineAgent bottomTimeLineAgent = new TimeLineAgent("BottomTimeLineAgent");
        private TimeLineMarkerManagerAgent timeLineMarkerManagerAgent = new TimeLineMarkerManagerAgent("TimeLineMarkerManagerAgent");
        private TimeLineScrollBarAgent timeLineScrollBarAgent = new TimeLineScrollBarAgent("TimeLineScrollBarAgent");

        public TimeLineControlAgent(string name)
            : base(name, new TimeLineControlC(name, new TimeLineControlP(name), new TimeLineControlA(name)))
        {
            this.Add(timeLineGridAgent);
            this.Add(topTimeLineAgent);
            this.Add(bottomTimeLineAgent);
            this.Add(timeLineMarkerManagerAgent);
            this.Add(timeLineScrollBarAgent);

            topTimeLineAgent.P.Location = new Point(1, 1);
            topTimeLineAgent.P.Width = this.P.ClientSize.Width - (topTimeLineAgent.P.Location.X * 2);
            topTimeLineAgent.P.Anchor = AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top;
            topTimeLineAgent.P.ScaleMarkDirection = ScaleMarkDirection.Bottom;

            timeLineGridAgent.P.Margin = new Padding(1, topTimeLineAgent.P.Location.Y + topTimeLineAgent.P.Height, 1, bottomTimeLineAgent.P.Height + timeLineMarkerManagerAgent.P.Height + timeLineScrollBarAgent.P.Height);
            timeLineGridAgent.P.Location = new Point(1, topTimeLineAgent.P.Location.Y + topTimeLineAgent.P.Height);
            timeLineGridAgent.P.Anchor = AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top;
            timeLineGridAgent.P.HScrollBar = timeLineScrollBarAgent.P;

            bottomTimeLineAgent.P.Location = new Point(1, timeLineGridAgent.P.Location.Y + timeLineGridAgent.P.Height);
            bottomTimeLineAgent.P.Width = this.P.ClientSize.Width - (bottomTimeLineAgent.P.Location.X * 2);
            bottomTimeLineAgent.P.Anchor = AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top;
            bottomTimeLineAgent.P.ScaleMarkDirection = ScaleMarkDirection.Top;

            timeLineMarkerManagerAgent.P.Location = new Point(timeLineGridAgent.P.TimeLineX, bottomTimeLineAgent.P.Location.Y + bottomTimeLineAgent.P.Height);
            timeLineMarkerManagerAgent.P.Width = timeLineGridAgent.P.Width - timeLineGridAgent.P.TimeLineX;
            timeLineMarkerManagerAgent.P.Anchor = AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top;

            timeLineScrollBarAgent.P.Location = new Point(timeLineGridAgent.P.TimeLineX, timeLineMarkerManagerAgent.P.Location.Y + timeLineMarkerManagerAgent.P.Height);
            timeLineScrollBarAgent.P.Width = timeLineGridAgent.P.Width - timeLineGridAgent.P.TimeLineX;
            timeLineScrollBarAgent.P.Anchor = AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top;

            timeLineGridAgent.P.SizeChanged += timeLineGridPSizeChanged;
           
        }

        public override void InitChildrenFirst()
        {
            base.InitChildrenFirst();

            timeLineGridAgent.P.MinRowHeight = 15;
            timeLineGridAgent.P.NowRowHeight = 25;
        }

        private void timeLineGridPSizeChanged(object sender, EventArgs e)
        {
            int timeLineWidth = timeLineGridAgent.P.Width - timeLineGridAgent.P.VerticalScrollBarWidth;
            topTimeLineAgent.P.Width = timeLineWidth;
            bottomTimeLineAgent.P.Location = new Point(1, timeLineGridAgent.P.Location.Y + timeLineGridAgent.P.Height);
            bottomTimeLineAgent.P.Width = timeLineWidth;
            timeLineMarkerManagerAgent.P.Width = timeLineWidth - (timeLineGridAgent.P.TimeLineX - timeLineGridAgent.P.Location.X);
            timeLineMarkerManagerAgent.P.Location = new Point(timeLineGridAgent.P.TimeLineX, bottomTimeLineAgent.P.Location.Y + bottomTimeLineAgent.P.Height);
            timeLineScrollBarAgent.P.Width = timeLineWidth - (timeLineGridAgent.P.TimeLineX - timeLineGridAgent.P.Location.X);
            timeLineScrollBarAgent.P.Location = new Point(timeLineGridAgent.P.TimeLineX, timeLineMarkerManagerAgent.P.Location.Y + timeLineMarkerManagerAgent.P.Height);
        }

    }

}
