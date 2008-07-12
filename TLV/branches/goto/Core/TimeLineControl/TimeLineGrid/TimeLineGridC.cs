using System;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Collections.Generic;
using NU.OJL.MPRTOS.TLV.Architecture.PAC;
using NU.OJL.MPRTOS.TLV.Architecture.PAC.Bace;
using NU.OJL.MPRTOS.TLV.Core.Base;

namespace NU.OJL.MPRTOS.TLV.Core.TimeLineControl.TimeLineGrid
{
    public class TimeLineGridC : Control<TimeLineGridP, TimeLineGridA>
    {
        public TimeLineGridC(string name, TimeLineGridP presentation, TimeLineGridA abstraction)
            : base(name, presentation, abstraction)
        {

        }

        public override void InitChildrenFirst()
        {
            base.InitChildrenFirst();
        }

        public override void InitParentFirst()
        {
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
            BindPToA("ViewableObjectType", typeof(Type), "ViewableObjectType", SearchAFlags.Ancestors);
            BindPToA("CursorMode", typeof(CursorMode), "CursorMode", SearchAFlags.Ancestors);
            A.ViewableObjectDataSource = GetPropertyAFrom(typeof(object), "ViewableObjectDataSource", SearchAFlags.Ancestors);
            P.ViewableObjectDataSource = A.ViewableObjectDataSource;
            P.AddViewableObject += (ViewableObjectAddHandler)GetDelegate(typeof(ViewableObjectAddHandler), "AddViewableObject", SearchAFlags.Ancestors);
            P.RemoveAtViewableObject += (ViewableObjectRemoveAtHandler)GetDelegate(typeof(ViewableObjectRemoveAtHandler), "RemoveAtViewableObject", SearchAFlags.Ancestors);
            P.InsertViewableObject += (ViewableObjectInsertHandler)GetDelegate(typeof(ViewableObjectInsertHandler), "InsertViewableObject", SearchAFlags.Ancestors);
            P.GetViewableObject += (ViewableObjectGetHandler)GetDelegate(typeof(ViewableObjectGetHandler), "GetViewableObject", SearchAFlags.Ancestors);
            P.IndexOfViewableObject += (ViewableObjectIndexOfHandler)GetDelegate(typeof(ViewableObjectIndexOfHandler), "IndexOfViewableObject", SearchAFlags.Ancestors);
        }
    }
}
