using System;
using System.Windows.Forms;
using System.Collections.Generic;
using NU.OJL.MPRTOS.TLV.Architecture.PAC;
using NU.OJL.MPRTOS.TLV.Architecture.PAC.Bace;

namespace NU.OJL.MPRTOS.TLV.Core.TimeLineControl.TimeLine
{
    public class TimeLineC : Control<TimeLineP, TimeLineA>
    {
        public TimeLineC(string name, TimeLineP presentation, TimeLineA abstraction)
            : base(name, presentation, abstraction)
        {

        }

        public override void Init()
        {
            base.Init();
            BindPToA("MinimumTime", typeof(ulong), "MinimumTime", SearchAFlags.AncestorsWithSiblings);
            BindPToA("MaximumTime", typeof(ulong), "MaximumTime", SearchAFlags.AncestorsWithSiblings);
            BindPToA("TimeLineX", typeof(int), "TimeLineX", SearchAFlags.AncestorsWithSiblings);
            BindPToA("TimeLineMinimumX", typeof(int), "TimeLineMinimumX", SearchAFlags.AncestorsWithSiblings);
            BindPToA("BeginTime", typeof(ulong), "BeginTime", SearchAFlags.AncestorsWithSiblings);
            BindPToA("DisplayTimeLength", typeof(ulong), "DisplayTimeLength", SearchAFlags.AncestorsWithSiblings);
            BindPToA("NsPerPixel", typeof(ulong), "NsPerPixel", SearchAFlags.AncestorsWithSiblings);

            this.P.TimeLineXResizing += (MouseEventHandler)this.GetDelegate(typeof(MouseEventHandler), "TimeLineXResizing", SearchAFlags.AncestorsWithSiblings);
            this.P.TimeLineXResized += (MouseEventHandler)this.GetDelegate(typeof(MouseEventHandler), "TimeLineXResized", SearchAFlags.AncestorsWithSiblings);
        }
    }
}
