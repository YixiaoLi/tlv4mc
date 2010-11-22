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

            // コンボボックスの設定
            comboBox1.Items.AddRange( Enum.GetNames(typeof(AvailableChartType)) );
            
            setChartSetting();
            setEachTypeSetting();

            foreach (DataPoint p in _data.Series.Points)
            {
                var dp = new System.Windows.Forms.DataVisualization.Charting.DataPoint();
                dp.LegendText = p.XLabel;
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

        /// <summary>
        /// グラフ設定をセットします
        /// </summary>
        /// <param name="setting">新たなグラフ設定</param>
        public void SetChartSetting(ChartSetting setting)
        {
            _data.Setting = setting;
            setChartSetting();
        }

        /// <summary>
        /// _data.Settingをもとにグラフ設定をセットします
        /// </summary>
        private void setChartSetting()
        {
            // ウィンドウタイトル
            if (!string.IsNullOrEmpty(_data.Setting.Title))
            {
                this.Text = _data.Setting.Title + " - 統計情報ビューア";
            }
            else
            {
                this.Text = _data.Name + " - 統計情報ビューア";
            }

            // 補助目盛、補助グリッドの色
            chart1.ChartAreas[0].AxisY.MinorTickMark.LineColor = Color.LightGray;
            chart1.ChartAreas[0].AxisY.MinorGrid.LineColor = Color.LightGray;

            // 棒グラフや折れ線で邪魔に感じたので消しました
            // 必要に応じて設定しなおしてください
            chart1.ChartAreas[0].AxisX.MajorGrid.Enabled = false;

            chart1.Series[0].Name = _data.Setting.SeriesTitle;
            chart1.Series[0].ChartType = _data.Setting.DefaultType;
            comboBox1.SelectedItem = Enum.GetName(typeof(AvailableChartType), _data.Setting.DefaultType);
            
            chart1.ChartAreas[0].AxisX.Title = _data.Setting.AxisXTitle;
            chart1.ChartAreas[0].AxisY.Title = _data.Setting.AxisYTitle;
            
            chart1.ChartAreas[0].AxisY.MajorTickMark.Interval = _data.Setting.MajorTickMarkInterval;
            chart1.ChartAreas[0].AxisY.MinorTickMark.Interval = _data.Setting.MinorTickMarkInterval;
            
            chart1.ChartAreas[0].AxisY.MajorGrid.Enabled = _data.Setting.MajorGridVisible;
            chart1.ChartAreas[0].AxisY.MajorGrid.Interval = _data.Setting.MajorTickMarkInterval;

            chart1.ChartAreas[0].AxisY.MinorGrid.Enabled = _data.Setting.MinorGridVisible;
            chart1.ChartAreas[0].AxisY.MinorGrid.Interval = _data.Setting.MinorTickMarkInterval;
        }

        private void setEachTypeSetting()
        {
            switch (chart1.Series[0].ChartType)
            {
                case SeriesChartType.Pie:
                    chart1.Series[0].Label = "#VALY\n" + "(#PERCENT)";
                    break;
                default:
                    chart1.Series[0].Label = "#VALY";
                    break;
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            chart1.Series[0].ChartType = (SeriesChartType)Enum.Parse(typeof(SeriesChartType), comboBox1.Text);
            setEachTypeSetting();
        }
    }
}
