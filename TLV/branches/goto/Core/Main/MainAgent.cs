using NU.OJL.MPRTOS.TLV.Architecture.PAC;
using NU.OJL.MPRTOS.TLV.Core.DockPanel;
using NU.OJL.MPRTOS.TLV.Core.TimeLineControl;
using WeifenLuo.WinFormsUI.Docking;
using NU.OJL.MPRTOS.TLV.Core.ViewableObject.KernelObject;
using NU.OJL.MPRTOS.TLV.Core.Base;
using NU.OJL.MPRTOS.TLV.Base;
using System;

namespace NU.OJL.MPRTOS.TLV.Core.Main
{
    public class MainAgent : Agent<MainP, MainA, MainC>
    {
        private DockPanelAgent dockPanelAgent = new DockPanelAgent("DockPanel");
        private TimeLineControlAgent timeLineControlAgent = new TimeLineControlAgent("KernelObjectTimeLineControl");

        public MainAgent(string name)
            : base(name, new MainC(name, new MainP(name), new MainA(name)), true)
        {
            this.Add(dockPanelAgent);
            dockPanelAgent.Add(timeLineControlAgent);

            this.Show();
        }

        public override void InitChildrenFirst()
        {
            timeLineControlAgent.P.DockState = DockState.Document;
        }

        public override void InitParentFirst()
        {
            timeLineControlAgent.A.ViewableObjectType = typeof(KernelObject);
        }
    }
}
