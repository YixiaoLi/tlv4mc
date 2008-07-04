using NU.OJL.MPRTOS.TLV.Architecture.PAC;
using WeifenLuo.WinFormsUI.Docking;
using NU.OJL.MPRTOS.TLV.Core.TimeLineControl.TimeLineGrid;

namespace NU.OJL.MPRTOS.TLV.Core.TimeLineControl
{
    public class TimeLineControlC : Control<TimeLineControlP, TimeLineControlA>
    {
        public TimeLineControlC(string name, TimeLineControlP presentation, TimeLineControlA abstraction)
            : base(name, presentation, abstraction)
        {
            P.DockAreas = DockAreas.Document;
        }

        public override void InitC()
        {
            base.InitC();
            BindPToA("RowSizeMode", typeof(RowSizeMode), "RowSizeMode", SearchAFlags.Children);
        }
    }
}
