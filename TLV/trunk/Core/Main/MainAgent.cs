using NU.OJL.MPRTOS.TLV.Architecture.PAC;
using NU.OJL.MPRTOS.TLV.Core.DockPanel;
using NU.OJL.MPRTOS.TLV.Core.TimeLineControl;
using WeifenLuo.WinFormsUI.Docking;

namespace NU.OJL.MPRTOS.TLV.Core.Main
{
    public class MainAgent : Agent<MainP, MainA, MainC>
    {
        private DockPanelAgent dockPanelAgent = new DockPanelAgent("DockPanel");
        private TimeLineControlAgent timeLineControlAgent = new TimeLineControlAgent("TestTimeLineControl");

        public MainAgent(string name)
            : base(name, new MainC(name, new MainP(name), new MainA(name)), true)
        {
            this.Add(dockPanelAgent);
            dockPanelAgent.Add(timeLineControlAgent);

            timeLineControlAgent.P.DockState = DockState.Document;

            this.Show();
        }
    }
}
