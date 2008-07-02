using System.Drawing;
using System.Windows.Forms;
using NU.OJL.MPRTOS.TLV.Architecture.PAC;
using Docking = WeifenLuo.WinFormsUI.Docking;
using System.Collections.Generic;


namespace NU.OJL.MPRTOS.TLV.Core.DockPanel
{
    public class DockPanelP : Docking.DockPanel, IPresentation
    {
        private Dictionary<string, Docking.DockContent> dockTable;

        public DockPanelP(string name)
        {
            this.Name = name;
            this.Dock = DockStyle.Fill;
            this.DocumentStyle = Docking.DocumentStyle.DockingWindow;
            this.dockTable = new Dictionary<string, Docking.DockContent>();
        }

        public void AddChild(Control control, object args)
        {
            dockTable.Add(control.Name, (Docking.DockContent)control);
        }

        public void Show(string name, Docking.DockState state)
        {
            this.dockTable[name].Show(this, state);
        }
    }

}
