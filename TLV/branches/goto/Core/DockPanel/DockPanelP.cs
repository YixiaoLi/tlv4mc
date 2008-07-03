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
        public DockPanelP(string name)
        {
            this.Name = name;
            this.Dock = DockStyle.Fill;
            this.DocumentStyle = Docking.DocumentStyle.DockingWindow;
        }

        public void Add(IPresentation presentation)
        {
            ((Docking.DockContent)presentation).DockPanel = this;
        }

    }

}
