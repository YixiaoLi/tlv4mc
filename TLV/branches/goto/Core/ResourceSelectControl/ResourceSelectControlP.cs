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
        private Dictionary<TimeLineViewableObjectType, List<TimeLineViewableObject>> viewableObjectList = new Dictionary<TimeLineViewableObjectType, List<TimeLineViewableObject>>();

        public Dictionary<TimeLineViewableObjectType, List<TimeLineViewableObject>> ViewableObjectList
        {
            get { return viewableObjectList; }
            set
            {
                if (value != null && !value.Equals(viewableObjectList))
                {
                    viewableObjectList = value;
                    NotifyPropertyChanged("ViewableObjectList");

                    this.tabControl.
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
