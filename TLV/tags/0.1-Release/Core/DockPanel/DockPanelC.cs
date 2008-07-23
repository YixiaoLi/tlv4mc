using NU.OJL.MPRTOS.TLV.Architecture.PAC;
using WeifenLuo.WinFormsUI.Docking;

namespace NU.OJL.MPRTOS.TLV.Core.DockPanel
{
    public class DockPanelC : Control<DockPanelP, DockPanelA>
    {
        public DockPanelC(string name, DockPanelP presentarion, DockPanelA abstraction)
            : base(name, presentarion, abstraction)
        {

        }

    }
}
