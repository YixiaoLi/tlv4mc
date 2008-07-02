using NU.OJL.MPRTOS.TLV.Architecture.PAC;
using NU.OJL.MPRTOS.TLV.Core.DockPanel;
using NU.OJL.MPRTOS.TLV.Core.TimeLineControl;
using WeifenLuo.WinFormsUI.Docking;

namespace NU.OJL.MPRTOS.TLV.Core.Main
{
    public class MainAgent : Agent<MainP, MainA, MainC>
    {
        public MainAgent(string name)
            : base(name, new MainC(name, new MainP(name), new MainA(name)), true)
        {
            this.Parent = null;
            this.Children.Add(new DockPanelAgent("DockPanel"));
            this.Children["DockPanel"].Children.Add(new TimeLineControlAgent("TestTimeLineControl"));

            this.Show();
            ((DockPanelAgent)this.Children["DockPanel"]).Show("TestTimeLineControl", DockState.Document);
        }
    }
}
