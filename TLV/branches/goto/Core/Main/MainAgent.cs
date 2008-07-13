using NU.OJL.MPRTOS.TLV.Architecture.PAC;
using NU.OJL.MPRTOS.TLV.Core.DockPanel;
using NU.OJL.MPRTOS.TLV.Core.TimeLineControl;
using NU.OJL.MPRTOS.TLV.Core.ResourceSelectControl;
using NU.OJL.MPRTOS.TLV.Core.ResourcePropertyControl;
using WeifenLuo.WinFormsUI.Docking;
using NU.OJL.MPRTOS.TLV.Core.Base;
using NU.OJL.MPRTOS.TLV.Base;
using NU.OJL.MPRTOS.TLV.Core.ViewableObject.KernelObject.TaskInfo;
using System;

namespace NU.OJL.MPRTOS.TLV.Core.Main
{
    public class MainAgent : Agent<MainP, MainA, MainC>
    {
        private DockPanelAgent dockPanelAgent = new DockPanelAgent("DockPanel");
        private TimeLineControlAgent timeLineControlAgent = new TimeLineControlAgent("KernelObjectTimeLineControl");
        private ResourceSelectControlAgent resourceSelectControlAgent = new ResourceSelectControlAgent("KernelObjectResourceSelectControlAgent");
        private ResourcePropertyControlAgent resourcePropertyControlAgent = new ResourcePropertyControlAgent("KernelObjectResourcePropertyControlAgent");

        public MainAgent(string name)
            : base(name, new MainC(name, new MainP(name), new MainA(name)), true)
        {

            this.Add(dockPanelAgent);

            dockPanelAgent.Add(timeLineControlAgent);
            dockPanelAgent.Add(resourceSelectControlAgent);
            dockPanelAgent.Add(resourcePropertyControlAgent);

            this.Show();
        }

        public override void InitChildrenFirst()
        {
            timeLineControlAgent.P.DockState = DockState.Document;
            resourceSelectControlAgent.P.DockState = DockState.DockLeft;
            resourcePropertyControlAgent.P.DockState = DockState.DockBottom;

            resourceSelectControlAgent.P.Pane.DockPanel.SuspendLayout(true);
            resourcePropertyControlAgent.P.DockPanel = resourceSelectControlAgent.P.Pane.DockPanel;
            resourcePropertyControlAgent.P.DockPanel.DockPaneFactory.CreateDockPane(resourcePropertyControlAgent.P.DockHandler.Content, resourceSelectControlAgent.P.Pane, DockAlignment.Bottom, 0.5, true);
            resourceSelectControlAgent.P.Pane.DockPanel.ResumeLayout(true, true);
        }

        public override void InitParentFirst()
        {
            A.ViewableObjectType = typeof(TaskInfo);
        }
    }
}
