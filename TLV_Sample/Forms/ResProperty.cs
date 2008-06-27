using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace TLV.Forms
{
    public partial class ResProperty : WeifenLuo.WinFormsUI.Docking.DockContent
    {
        public ResProperty()
        {
            InitializeComponent();
        }

        public void InitPropty(Object obj)
        {
            proptyGrid.SelectedObject = obj;
        }


    }
}
