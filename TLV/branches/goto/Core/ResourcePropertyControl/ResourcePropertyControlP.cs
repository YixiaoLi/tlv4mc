using System;
using System.ComponentModel;
using System.Windows.Forms;
using System.Collections.Generic;
using NU.OJL.MPRTOS.TLV.Architecture.PAC;
using NU.OJL.MPRTOS.TLV.Core.Base;
using WeifenLuo.WinFormsUI.Docking;

namespace NU.OJL.MPRTOS.TLV.Core.ResourcePropertyControl
{
    public partial class ResourcePropertyControlP : DockContent, IPresentation
    {

        public event PropertyChangedEventHandler PropertyChanged;

        public ResourcePropertyControlP(string name)
        {
            InitializeComponent();
            this.Name = name;
            this.TabText = "リソース情報";
        }

        public void Add(IPresentation presentation)
        {

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
