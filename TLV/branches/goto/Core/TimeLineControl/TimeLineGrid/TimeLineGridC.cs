using System;
using System.Windows.Forms;
using System.Collections.Generic;
using NU.OJL.MPRTOS.TLV.Architecture.PAC;
using NU.OJL.MPRTOS.TLV.Architecture.PAC.Bace;

namespace NU.OJL.MPRTOS.TLV.Core.TimeLineControl.TimeLineGrid
{
    public class TimeLineGridC : Control<TimeLineGridP, TimeLineGridA>
    {
        public TimeLineGridC(string name, TimeLineGridP presentation, TimeLineGridA abstraction)
            : base(name, presentation, abstraction)
        {

        }

        public override void Init()
        {
            base.Init();
            BindPToA("RowSizeMode", typeof(RowSizeMode), "RowSizeMode", SearchAFlags.Self);
            BindPToA("TimeLineX", typeof(int), "TimeLineX", SearchAFlags.Self);
            BindPToA("TimeLineMinimumX", typeof(int), "TimeLineMinimumX", SearchAFlags.Self);
        }

    }
}
