using NU.OJL.MPRTOS.TLV.Architecture.PAC;
using NU.OJL.MPRTOS.TLV.Core.DockPanel;
using NU.OJL.MPRTOS.TLV.Core.TimeLineControl;
using NU.OJL.MPRTOS.TLV.Core.ResourceSelectControl;
using NU.OJL.MPRTOS.TLV.Core.ResourcePropertyControl;
using NU.OJL.MPRTOS.TLV.Core.TraceLogListControl;
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
        private TraceLogListControlAgent traceLogListControlAgent = new TraceLogListControlAgent("KernelObjectTraceLogListControlAgent");

        public MainAgent(string name)
            : base(name, new MainC(name, new MainP(name), new MainA(name)), true)
        {

            this.Add(dockPanelAgent);

            dockPanelAgent.Add(timeLineControlAgent);
            dockPanelAgent.Add(traceLogListControlAgent);
            dockPanelAgent.Add(resourceSelectControlAgent);
            dockPanelAgent.Add(resourcePropertyControlAgent);

            P.ResourcePropertyControlShow += delegate
            {
                resourcePropertyControlAgent.P.Show();
            };
            P.ResourceSelectControlShow += delegate
            {
                resourceSelectControlAgent.P.Show();
            };

            this.Show();
        }

        public override void InitChildrenFirst()
        {
            timeLineControlAgent.P.DockState = DockState.Document;
            resourceSelectControlAgent.P.DockState = DockState.DockTop;
            resourcePropertyControlAgent.P.DockState = DockState.DockTop;
            traceLogListControlAgent.P.DockState = DockState.DockTop;

            resourcePropertyControlAgent.P.DockPanel = resourceSelectControlAgent.P.Pane.DockPanel;
            resourcePropertyControlAgent.P.DockPanel.DockPaneFactory.CreateDockPane(resourcePropertyControlAgent.P.DockHandler.Content, resourceSelectControlAgent.P.Pane, DockAlignment.Right,2.0D/3.0D , true);

            traceLogListControlAgent.P.DockPanel = resourcePropertyControlAgent.P.Pane.DockPanel;
            traceLogListControlAgent.P.DockPanel.DockPaneFactory.CreateDockPane(traceLogListControlAgent.P.DockHandler.Content, resourcePropertyControlAgent.P.Pane, DockAlignment.Right, 0.5D, true);

        }

        public override void InitParentFirst()
        {
            //A.ViewableObjectType = typeof(TaskInfo);
        }
    }
}
