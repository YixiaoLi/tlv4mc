using System;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Collections.Generic;
using NU.OJL.MPRTOS.TLV.Architecture.PAC;
using NU.OJL.MPRTOS.TLV.Architecture.PAC.Bace;
using NU.OJL.MPRTOS.TLV.Core.Base;
using NU.OJL.MPRTOS.TLV.Base;

namespace NU.OJL.MPRTOS.TLV.Core.TimeLineControl.TimeLineGrid
{
    public class TimeLineGridC<T> : Control<TimeLineGridP<T>, TimeLineGridA<T>>
        where T : TimeLineViewableObject
    {
        public TimeLineGridC(string name, TimeLineGridP<T> presentation, TimeLineGridA<T> abstraction)
            : base(name, presentation, abstraction)
        {

        }

        public override void InitChildrenFirst()
        {
            base.InitChildrenFirst();
        }

        public override void InitParentFirst()
        {
            BindPToA("SelectedObject", typeof(object), "SelectedObject", SearchAFlags.AncestorsWithSiblings);
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
            BindPToA("SelectRectStartTime", typeof(ulong), "SelectRectStartTime", SearchAFlags.Self);
            BindPToA("NowMarkerColor", typeof(Color), "NowMarkerColor", SearchAFlags.Self);
            BindPToA("MaxRowHeight", typeof(int), "MaxRowHeight", SearchAFlags.Self);
            BindPToA("MinRowHeight", typeof(int), "MinRowHeight", SearchAFlags.Self);
            BindPToA("NowRowHeight", typeof(int), "NowRowHeight", SearchAFlags.Self);
            BindPToA("CursorMode", typeof(CursorMode), "CursorMode", SearchAFlags.Ancestors);
            BindPToA("ViewableObjectList", typeof(Dictionary<Type, List<T>>), "ViewableObjectList", SearchAFlags.AncestorsWithSiblings);
            P.ViewableObjectDataSource = A.ViewableObjectDataSource;

        }
    }
}
