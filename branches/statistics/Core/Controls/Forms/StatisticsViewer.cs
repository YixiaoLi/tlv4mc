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
    public partial class StatisticsViewer : Form
    {
        public Statistics Data { get; private set; }
        private bool _existYLabel = false;

        public StatisticsViewer()
        {
            InitializeComponent();
        }

        public StatisticsViewer(Statistics data)
            : this()
        {
            Data = data;
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            // 補助目盛、補助グリッドの色
            chart1.ChartAreas[0].AxisY.MinorTickMark.LineColor = Color.LightGray;
            chart1.ChartAreas[0].AxisY.MinorGrid.LineColor = Color.LightGray;

            // 棒グラフや折れ線で邪魔に感じたので消しました
            // 必要に応じて設定しなおしてください
            chart1.ChartAreas[0].AxisX.MajorGrid.Enabled = false;

            // コンボボックスの設定
            comboBox1.Items.AddRange( Enum.GetNames(typeof(AvailableChartType)) );

            foreach (DataPoint p in Data.Series.Points)
            {
                var dp = new System.Windows.Forms.DataVisualization.Charting.DataPoint();
                dp.LegendText = p.XLabel;
                dp.AxisLabel = p.XLabel;
                dp.XValue = p.XValue;
                if (!_existYLabel)
                {
                    _existYLabel = !string.IsNullOrEmpty(p.YLabel);
                }
                dp.YValues[0] = p.YValue;

                if (p.Color != null)
                {
                    dp.Color = (Color)p.Color;
                }
                chart1.Series[0].Points.Add(dp);
            }

            setChartSetting();
        }

        /// <summary>
        /// グラフ設定をセットします
        /// </summary>
        /// <param name="setting">新たなグラフ設定</param>
        public void SetChartSetting(ChartSetting setting)
        {
            Data.Setting = setting;
            setChartSetting();
        }

        /// <summary>
        /// Data.Settingをもとにグラフ設定をセットします
        /// </summary>
        private void setChartSetting()
        {
            // ウィンドウタイトル
            this.Text = string.IsNullOrEmpty(Data.Setting.Title) ? Data.Name : Data.Setting.Title;
            this.Text += " - 統計情報ビューア";
            
            chart1.Series[0].Name = Data.Setting.SeriesTitle;
            chart1.Series[0].ChartType = Data.Setting.DefaultType.GetChartType();
            comboBox1.SelectedItem = Enum.GetName(typeof(AvailableChartType), Data.Setting.DefaultType);
            
            chart1.ChartAreas[0].AxisX.Title = Data.Setting.AxisXTitle;
            chart1.ChartAreas[0].AxisY.Title = Data.Setting.AxisYTitle;
            
            chart1.ChartAreas[0].AxisY.MajorTickMark.Interval = Data.Setting.MajorTickMarkInterval;
            chart1.ChartAreas[0].AxisY.MinorTickMark.Interval = Data.Setting.MinorTickMarkInterval;
            
            chart1.ChartAreas[0].AxisY.MajorGrid.Enabled = Data.Setting.MajorGridVisible;
            chart1.ChartAreas[0].AxisY.MajorGrid.Interval = Data.Setting.MajorTickMarkInterval;

            chart1.ChartAreas[0].AxisY.MinorGrid.Enabled = Data.Setting.MinorGridVisible;
            chart1.ChartAreas[0].AxisY.MinorGrid.Interval = Data.Setting.MinorTickMarkInterval;
        }

        private void setEachTypeSetting()
        {
            
            chart1.ChartAreas[0].AxisX.IntervalOffset = 0;
            chart1.Series[0].IsVisibleInLegend = false;
            switch (comboBox1.Text)
            {
                case "Pie":
                    chart1.Series[0].IsVisibleInLegend = true;
                    chart1.Series[0].CustomProperties = "PieStartAngle=270";
                    if (_existYLabel)
                    {
                        foreach (var dp in chart1.Series[0].Points)
                        {
                            dp.Label = "#VALY\n(#PERCENT)";
                        }
                    }
                    else
                    {
                        chart1.Series[0].Label = "#VALY\n(#PERCENT)";
                    }
                    break;

                case "Bar":
                case "Column":
                    setDefaultYLabel();
                    chart1.Series[0].CustomProperties = "PointWidth=0.8";
                    break;

                case "Histogram":
                    chart1.Series[0].Label = "";
                    chart1.Series[0].CustomProperties = "PointWidth=1";
                    chart1.ChartAreas[0].AxisX.IntervalOffset = (chart1.Series[0].Points[1].XValue - chart1.Series[0].Points[0].XValue) / 2.0;
                    break;

                default:
                    setDefaultYLabel();
                    break;
            }
        }

        private void setDefaultYLabel()
        {
            if (_existYLabel)
            {
                int j = 0;
                for (int i = 0; i < Data.Series.Points.Count; i++)
                {
                    if (Data.Series.Points[i].Visible)
                    {
                        chart1.Series[0].Points[j].Label = Data.Series.Points[i].YLabel;
                        j++;
                    }
                }
            }
            else
            {
                chart1.Series[0].Label = "#VALY";
            }
        }


        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            AvailableChartType type = (AvailableChartType)Enum.Parse(typeof(AvailableChartType), comboBox1.Text);
            chart1.Series[0].ChartType = type.GetChartType();
            setEachTypeSetting();
        }      
    }
}
