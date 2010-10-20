using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace NU.OJL.MPRTOS.TLV.Core.Controls
{
    public partial class StatsExplorer : UserControl
    {
        public StatsExplorer()
        {
            InitializeComponent();
        }

        private void listView1_ItemChecked(object sender, ItemCheckedEventArgs e)
        {
            var form = new StatsVewier();
            form.Show();
        }

        private void listView1_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            var form = new StatsViewerTmp();
            form.Show();
        }


    }
}
