using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace NU.OJL.MPRTOS.TLV.Core.Controls
{
    public partial class StatisticsViewer : Form
    {
        private Statistics _data = null;

        public StatisticsViewer()
        {
            InitializeComponent();
        }

        public StatisticsViewer(Statistics data)
            : this()
        {
            _data = data;
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            //chart1.Titles[0].Text = _data.Setting.Title;
            chart1.Series[0].Label = _data.Series.Label;
            chart1.Series[0].ChartType = _data.Setting.DefaultType;

            foreach (DataPoint p in _data.Series.Points)
            {
                var dp = new System.Windows.Forms.DataVisualization.Charting.DataPoint();
                dp.AxisLabel = p.XLabel;
                dp.XValue = p.XValue;
                dp.YValues[0] = p.YValue;
                if (p.Color != null)
                {
                    dp.Color = (Color)p.Color;
                }
                chart1.Series[0].Points.Add(dp);
            }
        }
    }
}
