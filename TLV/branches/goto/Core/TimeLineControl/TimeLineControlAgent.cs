using NU.OJL.MPRTOS.TLV.Architecture.PAC;
using WeifenLuo.WinFormsUI.Docking;

namespace NU.OJL.MPRTOS.TLV.Core.TimeLineControl
{
    public class TimeLineControlAgent : Agent<TimeLineControlP, TimeLineControlA, TimeLineControlC>
    {
        public TimeLineControlAgent(string name)
            : base(name, new TimeLineControlC(name, new TimeLineControlP(name), new TimeLineControlA(name)))
        {
            ((TimeLineControlP)this.Control.Presentation).DockAreas = DockAreas.Document;
        }
    }
}
