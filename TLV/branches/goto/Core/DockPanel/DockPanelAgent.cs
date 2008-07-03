using NU.OJL.MPRTOS.TLV.Architecture.PAC;
using WeifenLuo.WinFormsUI.Docking;

namespace NU.OJL.MPRTOS.TLV.Core.DockPanel
{
    public class DockPanelAgent : Agent<DockPanelP, DockPanelA, DockPanelC>
    {
        public DockPanelAgent(string name)
            : base(name, new DockPanelC(name, new DockPanelP(name), new DockPanelA(name)))
        {

        }
    }
}
