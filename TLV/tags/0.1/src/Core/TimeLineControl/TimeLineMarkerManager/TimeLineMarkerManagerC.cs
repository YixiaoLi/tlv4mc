using System;
using System.Collections.Generic;
using System.Drawing;
using NU.OJL.MPRTOS.TLV.Architecture.PAC;
using NU.OJL.MPRTOS.TLV.Architecture.PAC.Bace;

namespace NU.OJL.MPRTOS.TLV.Core.TimeLineControl.TimeLineMarkerManager
{
    public class TimeLineMarkerManagerC : Control<TimeLineMarkerManagerP, TimeLineMarkerManagerA>
    {
        public TimeLineMarkerManagerC(string name, TimeLineMarkerManagerP presentation, TimeLineMarkerManagerA abstraction)
            : base(name, presentation, abstraction)
        {
        }

        public override void InitParentFirst()
        {
            base.InitParentFirst();
            BindPToA("X", typeof(int), "TimeLineX", SearchAFlags.AncestorsWithSiblings);
            BindPToA("MinimumTime", typeof(ulong), "MinimumTime", SearchAFlags.AncestorsWithSiblings);
            BindPToA("MaximumTime", typeof(ulong), "MaximumTime", SearchAFlags.AncestorsWithSiblings);
            BindPToA("TimeLineX", typeof(int), "TimeLineX", SearchAFlags.AncestorsWithSiblings);
            BindPToA("TimeLineMinimumX", typeof(int), "TimeLineMinimumX", SearchAFlags.AncestorsWithSiblings);
            BindPToA("BeginTime", typeof(ulong), "BeginTime", SearchAFlags.AncestorsWithSiblings);
            BindPToA("DisplayTimeLength", typeof(ulong), "DisplayTimeLength", SearchAFlags.AncestorsWithSiblings);
            BindPToA("NsPerScaleMark", typeof(ulong), "NsPerScaleMark", SearchAFlags.AncestorsWithSiblings);
            BindPToA("PixelPerScaleMark", typeof(int), "PixelPerScaleMark", SearchAFlags.AncestorsWithSiblings);
            BindPToA("NowMarkerTime", typeof(ulong), "NowMarkerTime", SearchAFlags.AncestorsWithSiblings);
            BindPToA("TmpMarkerTime", typeof(ulong), "TmpMarkerTime", SearchAFlags.AncestorsWithSiblings);
            BindPToA("SelectRectStartTime", typeof(ulong), "SelectRectStartTime", SearchAFlags.AncestorsWithSiblings);
            BindPToA("NowMarkerColor", typeof(Color), "NowMarkerColor", SearchAFlags.AncestorsWithSiblings);
            BindPToA("TmpMarkerColor", typeof(Color), "TmpMarkerColor", SearchAFlags.AncestorsWithSiblings);
            BindPToA("TimeLineMarkerList", typeof(TimeLineMarkers), "TimeLineMarkerList", SearchAFlags.Self);
        }
    }
}
