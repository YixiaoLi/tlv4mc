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

        public override void Init()
        {
            base.Init();
            BindPToA("X", typeof(int), "TimeLineX", SearchAFlags.AncestorsWithSiblings);
        }
    }
}
