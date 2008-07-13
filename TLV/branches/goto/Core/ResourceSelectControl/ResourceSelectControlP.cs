using System;
using System.ComponentModel;
using System.Windows.Forms;
using System.Collections.Generic;
using NU.OJL.MPRTOS.TLV.Architecture.PAC;
using NU.OJL.MPRTOS.TLV.Core.Base;
using WeifenLuo.WinFormsUI.Docking;

namespace NU.OJL.MPRTOS.TLV.Core.ResourceSelectControl
{
    public partial class ResourceSelectControlP : DockContent, IPresentation
    {
        private Type viewableObjectType = typeof(TimeLineViewableObject);

        public Type ViewableObjectType
        {
            get { return viewableObjectType; }
            set
            {
                if (value != null && !value.Equals(viewableObjectType))
                {
                    viewableObjectType = value;
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public ResourceSelectControlP(string name)
        {
            InitializeComponent();
            this.Name = name;
            this.TabText = "リソース一覧";
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
