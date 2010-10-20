using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace NU.OJL.MPRTOS.TLV.Core.Controls
{
    public partial class StatsVewier : UserControl
    {
        public StatsVewier()
        {
            InitializeComponent();
            comboBox1.Items.AddRange(Enum.GetNames(typeof(SeriesChartType)));
            //chart1.ChartAreas[0].AxisX.Name = "x軸";
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            chart1.Series[0].ChartType = (SeriesChartType)Enum.Parse(typeof(SeriesChartType), comboBox1.Text);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var form = new ChartSettingForm();
            form.ShowDialog();
        }

    }
}
