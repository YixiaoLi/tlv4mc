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
            BindPToA("TimeLineX", typeof(int), "TimeLineX", SearchAFlags.AncestorsWithSiblings);
            BindPToA("TimeLineMinimumX", typeof(int), "TimeLineMinimumX", SearchAFlags.AncestorsWithSiblings);
        }
    }
}
