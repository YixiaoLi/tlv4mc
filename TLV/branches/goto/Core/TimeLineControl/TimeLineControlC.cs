using NU.OJL.MPRTOS.TLV.Architecture.PAC;
using WeifenLuo.WinFormsUI.Docking;

namespace NU.OJL.MPRTOS.TLV.Core.TimeLineControl
{
    public class TimeLineControlC : Control<TimeLineControlP, TimeLineControlA>
    {
        public TimeLineControlC(string name, TimeLineControlP presentation, TimeLineControlA abstraction)
            : base(name, presentation, abstraction)
        {
            P.DockAreas = DockAreas.Document;
        }
    }
}
