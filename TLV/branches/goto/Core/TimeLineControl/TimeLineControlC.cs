using WeifenLuo.WinFormsUI.Docking;
using NU.OJL.MPRTOS.TLV.Core.TimeLineControl.TimeLineGrid;
using NU.OJL.MPRTOS.TLV.Architecture.PAC;
using NU.OJL.MPRTOS.TLV.Architecture.PAC.Bace;

namespace NU.OJL.MPRTOS.TLV.Core.TimeLineControl
{
    public class TimeLineControlC : Control<TimeLineControlP, TimeLineControlA>
    {
        public TimeLineControlC(string name, TimeLineControlP presentation, TimeLineControlA abstraction)
            : base(name, presentation, abstraction)
        {
            P.DockAreas = DockAreas.Document;
        }

        public override void Init()
        {
            base.Init();
            BindPToA("RowSizeMode", typeof(RowSizeMode), "RowSizeMode", SearchAFlags.Children);
            BindPToA("MaximumNsPerScaleMark", typeof(ulong), "MaximumNsPerScaleMark", SearchAFlags.Children);
            BindPToA("NsPerScaleMark", typeof(ulong), "NsPerScaleMark", SearchAFlags.Children);
            BindPToA("PixelPerScaleMark", typeof(int), "PixelPerScaleMark", SearchAFlags.Children);
        }
    }
}
