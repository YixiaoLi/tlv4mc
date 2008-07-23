using System;
using System.Collections.Generic;
using NU.OJL.MPRTOS.TLV.Architecture.PAC;
using NU.OJL.MPRTOS.TLV.Architecture.PAC.Bace;

namespace NU.OJL.MPRTOS.TLV.Core.TimeLineControl.TimeLineScrollBar
{
    public class TimeLineScrollBarC : Control<TimeLineScrollBarP, TimeLineScrollBarA>
    {
        public TimeLineScrollBarC(string name, TimeLineScrollBarP presentation, TimeLineScrollBarA abstraction)
            : base(name, presentation, abstraction)
        {
        }

        public override void InitChildrenFirst()
        {
            base.InitChildrenFirst();
        }

        public override void InitParentFirst()
        {
            base.InitParentFirst();
            BindPToA("X", typeof(int), "TimeLineX", SearchAFlags.AncestorsWithSiblings);
            BindPToA("MinimumTime", typeof(ulong), "MinimumTime", SearchAFlags.AncestorsWithSiblings);
            BindPToA("MaximumTime", typeof(ulong), "MaximumTime", SearchAFlags.AncestorsWithSiblings);
            BindPToA("BeginTime", typeof(ulong), "BeginTime", SearchAFlags.AncestorsWithSiblings);
            BindPToA("DisplayTimeLength", typeof(ulong), "DisplayTimeLength", SearchAFlags.AncestorsWithSiblings);
        }
    }
}
