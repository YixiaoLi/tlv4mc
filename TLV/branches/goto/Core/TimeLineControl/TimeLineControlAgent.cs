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
using NU.OJL.MPRTOS.TLV.Core.ViewableObject;
using NU.OJL.MPRTOS.TLV.Core.ViewableObject.KernelObject;

namespace NU.OJL.MPRTOS.TLV.Core.TimeLineControl
{
    public class TimeLineControlAgent : Agent<TimeLineControlP, TimeLineControlA, TimeLineControlC>
    {
        private TimeLineGridAgent timeLineGridAgent = new TimeLineGridAgent("TimeLineGrid");
        private TimeLineAgent topTimeLineAgent = new TimeLineAgent("TopTimeLineAgent");
        private TimeLineAgent bottomTimeLineAgent = new TimeLineAgent("BottomTimeLineAgent");
        private TimeLineScrollBarAgent timeLineScrollBarAgent = new TimeLineScrollBarAgent("TimeLineScrollBarAgent");

        public TimeLineControlAgent(string name)
            : base(name, new TimeLineControlC(name, new TimeLineControlP(name), new TimeLineControlA(name)))
        {
            this.Add(timeLineGridAgent);
            this.Add(topTimeLineAgent);
            this.Add(bottomTimeLineAgent);
            this.Add(timeLineScrollBarAgent);

            topTimeLineAgent.P.Location = new Point(1, 1);
            topTimeLineAgent.P.Width = this.P.ClientSize.Width - (topTimeLineAgent.P.Location.X * 2);
            topTimeLineAgent.P.Anchor = AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top;
            topTimeLineAgent.P.ScaleMarkDirection = ScaleMarkDirection.Bottom;

            timeLineGridAgent.P.Margin = new Padding(1, topTimeLineAgent.P.Location.Y + topTimeLineAgent.P.Height, 1, bottomTimeLineAgent.P.Height + timeLineScrollBarAgent.P.Height);
            timeLineGridAgent.P.Location = new Point(1, topTimeLineAgent.P.Location.Y + topTimeLineAgent.P.Height);
            timeLineGridAgent.P.Anchor = AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top;
            timeLineGridAgent.P.HScrollBar = timeLineScrollBarAgent.P;

            bottomTimeLineAgent.P.Location = new Point(1, timeLineGridAgent.P.Location.Y + timeLineGridAgent.P.Height);
            bottomTimeLineAgent.P.Width = this.P.ClientSize.Width - (bottomTimeLineAgent.P.Location.X * 2);
            bottomTimeLineAgent.P.Anchor = AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top;
            bottomTimeLineAgent.P.ScaleMarkDirection = ScaleMarkDirection.Top;

            timeLineScrollBarAgent.P.Location = new Point(timeLineGridAgent.P.TimeLineX, bottomTimeLineAgent.P.Location.Y + bottomTimeLineAgent.P.Height);
            timeLineScrollBarAgent.P.Width = timeLineGridAgent.P.Width - timeLineGridAgent.P.TimeLineX;
            timeLineScrollBarAgent.P.Anchor = AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top;

            timeLineGridAgent.P.SizeChanged += timeLineGridPSizeChanged;
           
        }

        public override void InitChildrenFirst()
        {
            base.InitChildrenFirst();

            //timeLineGridAgent.P.ViewableObjectList = new TimeLineViewableObjectList<KernelObject>()
            //{
            //    new KernelObject(1, "task1", ObjectType.TSK, "", 1, new TimeLineEvents()
            //        {
            //            new TimeLineEvent(100239, (int)KernelObjectVerb.DORMANT),
            //            new TimeLineEvent(234532, (int)KernelObjectVerb.RUN),
            //            new TimeLineEvent(634633, (int)KernelObjectVerb.WAITING_SUSPENDED),
            //            new TimeLineEvent(745332, (int)KernelObjectVerb.RUNNABLE),
            //        }),
            //    new KernelObject(2, "task2", ObjectType.TSK, "", 1, new TimeLineEvents()
            //        {
            //            new TimeLineEvent(136511, (int)KernelObjectVerb.DORMANT),
            //            new TimeLineEvent(234532, (int)KernelObjectVerb.RUN),
            //            new TimeLineEvent(634633, (int)KernelObjectVerb.WAITING_SUSPENDED),
            //            new TimeLineEvent(823512, (int)KernelObjectVerb.RUNNABLE),
            //        }),
            //    new KernelObject(3, "task3", ObjectType.TSK, "", 1, new TimeLineEvents()
            //        {
            //            new TimeLineEvent(100239, (int)KernelObjectVerb.DORMANT),
            //            new TimeLineEvent(234532, (int)KernelObjectVerb.RUN),
            //            new TimeLineEvent(634633, (int)KernelObjectVerb.WAITING_SUSPENDED),
            //            new TimeLineEvent(713221, (int)KernelObjectVerb.RUNNABLE),
            //        }),
            //    new KernelObject(4, "task4", ObjectType.TSK, "", 1, new TimeLineEvents()
            //        {
            //            new TimeLineEvent(112231, (int)KernelObjectVerb.DORMANT),
            //            new TimeLineEvent(234532, (int)KernelObjectVerb.RUN),
            //            new TimeLineEvent(612612, (int)KernelObjectVerb.WAITING_SUSPENDED),
            //            new TimeLineEvent(621235, (int)KernelObjectVerb.RUNNABLE),
            //        }),
            //};

            timeLineGridAgent.P.MinRowHeight = 15;
            timeLineGridAgent.P.NowRowHeight = 25;
        }

        private void timeLineGridPSizeChanged(object sender, EventArgs e)
        {
            int timeLineWidth = timeLineGridAgent.P.Width - timeLineGridAgent.P.VerticalScrollBarWidth;
            topTimeLineAgent.P.Width = timeLineWidth;
            bottomTimeLineAgent.P.Width = timeLineWidth;
            timeLineScrollBarAgent.P.Width = timeLineWidth - (timeLineGridAgent.P.TimeLineX - timeLineGridAgent.P.Location.X);
            bottomTimeLineAgent.P.Location = new Point(1, timeLineGridAgent.P.Location.Y + timeLineGridAgent.P.Height);
            timeLineScrollBarAgent.P.Location = new Point(timeLineGridAgent.P.TimeLineX, bottomTimeLineAgent.P.Location.Y + bottomTimeLineAgent.P.Height);
        }

    }

}
