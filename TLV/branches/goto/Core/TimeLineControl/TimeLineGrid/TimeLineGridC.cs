using System;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Drawing2D;
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
            BindPToA("MinimumTime", typeof(ulong), "MinimumTime", SearchAFlags.Self);
            BindPToA("MaximumTime", typeof(ulong), "MaximumTime", SearchAFlags.Self);
            BindPToA("BeginTime", typeof(ulong), "BeginTime", SearchAFlags.Self);
            BindPToA("DisplayTimeLength", typeof(ulong), "DisplayTimeLength", SearchAFlags.Self);
            BindPToA("NsPerScaleMark", typeof(ulong), "NsPerScaleMark", SearchAFlags.Self);
            BindPToA("MaximumNsPerScaleMark", typeof(ulong), "MaximumNsPerScaleMark", SearchAFlags.Self);
            BindPToA("PixelPerScaleMark", typeof(int), "PixelPerScaleMark", SearchAFlags.Self);
            BindPToA("NowMarkerTime", typeof(ulong), "NowMarkerTime", SearchAFlags.Self);
            BindPToA("NowMarkerColor", typeof(Color), "NowMarkerColor", SearchAFlags.Self);
        }

    }
}
