using System;
using System.Drawing;
using System.Windows.Forms;
using NU.OJL.MPRTOS.TLV.Architecture.PAC;
using Docking = WeifenLuo.WinFormsUI.Docking;
using System.Collections.Generic;


namespace NU.OJL.MPRTOS.TLV.Core.DockPanel
{
    public class DockPanelP : Docking.DockPanel, IPresentation
    {
        private Dictionary<string, DockContentDockStatePair> dockTable;

        public DockPanelP(string name)
        {
            this.Name = name;
            this.Dock = DockStyle.Fill;
            this.DocumentStyle = Docking.DocumentStyle.DockingWindow;
            this.dockTable = new Dictionary<string, DockContentDockStatePair>();
        }

        public void Add(IPresentation presentation, object args)
        {
            this.dockTable.Add(presentation.Name, new DockContentDockStatePair((Docking.DockContent)presentation, (Docking.DockState)args));
        }

        public new void Show()
        {
            base.Show();
            foreach(DockContentDockStatePair dockContentDockStatePair in dockTable.Values)
            {
                dockContentDockStatePair.Content.Show(this, dockContentDockStatePair.State);
            }
        }

        public class DockContentDockStatePair
        {
            public Docking.DockContent Content { get; set; }
            public Docking.DockState State { get; set; }

            public DockContentDockStatePair(Docking.DockContent content, Docking.DockState state)
            {
                this.Content = content;
                this.State = state;
            }
        }
    }

}
