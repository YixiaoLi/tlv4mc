using System;
using System.Drawing;
using System.ComponentModel;
using System.Windows.Forms;
using NU.OJL.MPRTOS.TLV.Architecture.PAC;
using Docking = WeifenLuo.WinFormsUI.Docking;
using System.Collections.Generic;
using WeifenLuo.WinFormsUI.Docking;

namespace NU.OJL.MPRTOS.TLV.Core.DockPanel
{
    public class DockPanelP : Docking.DockPanel, IPresentation
    {

        public event PropertyChangedEventHandler PropertyChanged;

        public DockPanelP(string name)
        {
            this.Name = name;
            this.Dock = DockStyle.Fill;
            this.DocumentStyle = DocumentStyle.DockingSdi;
        }

        public void Add(IPresentation presentation)
        {
            ((Docking.DockContent)presentation).DockPanel = this;
        }

        protected void NotifyPropertyChanged(string info)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(info));
            }
        }

    }

}
