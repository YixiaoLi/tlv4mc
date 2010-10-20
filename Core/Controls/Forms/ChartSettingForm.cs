using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace NU.OJL.MPRTOS.TLV.Core.Controls
{
    public partial class ChartSettingForm : Form
    {
        public ChartSettingForm()
        {
            InitializeComponent();


            //listView1.Items[0].SubItems[0].BackColor = new Color().FromHexString(listView1.Items[0].SubItems[1].Text);
            //listView1.Items[1].SubItems[0].BackColor = new Color().FromHexString(listView1.Items[1].SubItems[1].Text);
        }

        private class BindTest
        {
            public string Name { set; get; }
            public string Color { set; get; }
            public int Data { set; get; }

            public BindTest(string n, string c)
            {
                Name = n; Color = c; 
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }


        private void ChartSettingForm_Load(object sender, EventArgs e)
        {
           tabPage2.Visible = true; // データバインド前にこれをしないと、色が反映されない
            BindingList<BindTest> bl = new BindingList<BindTest>();

            bl.Add(new BindTest("task1", "1E90FF"));
            bl.Add(new BindTest("task2", "FFA500"));

            dataGridView1.Columns[0].DataPropertyName = "Name";
            dataGridView1.Columns[1].DataPropertyName = "Color";
            dataGridView1.DataSource = bl;

            var c1 = new Color().FromHexString("ff" + bl[0].Color);
             var c2 = new Color().FromHexString("ff" + bl[1].Color);
            dataGridView1["Column2", 0].Style.BackColor = c1;
            dataGridView1["Column2", 1].Style.BackColor = c2;
 
        }

        private void dataGridView1_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.ColumnIndex.Equals(1))
                e.CellStyle.BackColor = new Color().FromHexString("ff" + (string)e.Value);
        }


    }
}
