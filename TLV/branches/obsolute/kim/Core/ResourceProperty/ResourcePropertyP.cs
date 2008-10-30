using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using NU.OJL.MPRTOS.TLV.Architecture.PAC;

namespace NU.OJL.MPRTOS.TLV.Core.ResourceProperty
{
    public partial class ResourcePropertyP : WeifenLuo.WinFormsUI.Docking.DockContent, IPresentation
    {
        public ResourcePropertyP(string name)
        {
            InitializeComponent();

            this.Name = name;
        }

        public void AddChild(Control control, object args)
        {

        }


        public void InitPropty(Object obj)
        {
            proptyGrid.SelectedObject = obj;
        }


    }
}
