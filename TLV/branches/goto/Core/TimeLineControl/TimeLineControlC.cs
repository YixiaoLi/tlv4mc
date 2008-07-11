using System;
using WeifenLuo.WinFormsUI.Docking;
using NU.OJL.MPRTOS.TLV.Core.TimeLineControl.TimeLineGrid;
using NU.OJL.MPRTOS.TLV.Architecture.PAC;
using NU.OJL.MPRTOS.TLV.Architecture.PAC.Bace;
using NU.OJL.MPRTOS.TLV.Core.Base;

namespace NU.OJL.MPRTOS.TLV.Core.TimeLineControl
{
    public class TimeLineControlC: Control<TimeLineControlP, TimeLineControlA>
    {
        public TimeLineControlC(string name, TimeLineControlP presentation, TimeLineControlA abstraction)
            : base(name, presentation, abstraction)
        {
            P.DockAreas = DockAreas.Document;
        }

        public override void InitChildrenFirst()
        {
            base.InitChildrenFirst();
            BindPToA("RowSizeMode", typeof(RowSizeMode), "RowSizeMode", SearchAFlags.Children);
            BindPToA("MaximumNsPerScaleMark", typeof(ulong), "MaximumNsPerScaleMark", SearchAFlags.Children);
            BindPToA("NsPerScaleMark", typeof(ulong), "NsPerScaleMark", SearchAFlags.Children);
            BindPToA("PixelPerScaleMark", typeof(int), "PixelPerScaleMark", SearchAFlags.Children);
            BindPToA("RowHeight", typeof(int), "NowRowHeight", SearchAFlags.Children);
            BindPToA("MaxRowHeight", typeof(int), "MaxRowHeight", SearchAFlags.Children);
            BindPToA("MinRowHeight", typeof(int), "MinRowHeight", SearchAFlags.Children);
        }

        public override void InitParentFirst()
        {
            BindPToA("ViewableObjectType", typeof(Type), "ViewableObjectType", SearchAFlags.Self);
            BindPToA("CursorMode", typeof(CursorMode), "CursorMode", SearchAFlags.Self);
            P.ViewableObjectDataSource = A.ViewableObjectDataSource;
            P.AddViewableObject += A.AddViewableObject;
            P.RemoveAtViewableObject += A.RemoveAtViewableObject;
            P.InsertViewableObject += A.InsertViewableObject;
            P.GetViewableObject += A.GetViewableObject;
            P.IndexOfViewableObject += A.IndexOfViewableObject;
        }
    }
}
