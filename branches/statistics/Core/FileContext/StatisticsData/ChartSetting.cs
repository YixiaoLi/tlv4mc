using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms.DataVisualization.Charting;
using NU.OJL.MPRTOS.TLV.Base;

namespace NU.OJL.MPRTOS.TLV.Core
{
    public class ChartSetting : IJsonable<ChartSetting>
    {
        /// <summary>
        /// グラフのタイトル
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// X軸のタイトル
        /// </summary>
        public string AxisXTitle { get; set; }
        /// <summary>
        /// Y軸のタイトル
        /// </summary>
        public string AxisYTitle { get; set; }
        /// <summary>
        /// 系列のタイトル
        /// </summary>
        public string SeriesTitle { get; set; }
        /// <summary>
        /// 最初に描画するグラフタイプ
        /// </summary>
        public SeriesChartType DefaultType { get; set; }
        /// <summary>
        /// 目盛の間隔
        /// </summary>
        public double MajorTickMarkInterval { get; set; }
        /// <summary>
        /// 補助目盛の間隔
        /// </summary>
        public double MinorTickMarkInterval { get; set; }
        /// <summary>
        /// グリッド線を表示するかどうか
        /// </summary>
        public bool MajorGridVisible { get; set; }
        /// <summary>
        /// 補助グリッド線を表示するかどうか
        /// </summary>
        public bool MinorGridVisible { get; set; }

        public ChartSetting()
        {
            Title = string.Empty;
            AxisXTitle = string.Empty;
            AxisYTitle = string.Empty;
            SeriesTitle = string.Empty;
            DefaultType = SeriesChartType.Pie;
            MajorTickMarkInterval = 0.0;//null;
            MinorTickMarkInterval = 0.0;//null;
            MajorGridVisible = false;
            MinorGridVisible = false;
        }

        public string ToJson()
        {
            return ApplicationFactory.JsonSerializer.Serialize(this);
        }

        public ChartSetting Parse(string chartSetting)
        {
            return ApplicationFactory.JsonSerializer.Deserialize<ChartSetting>(chartSetting);
        }
    }

    /// <summary>
    /// 利用可能なグラフタイプの列挙体<para></para>
    /// System.Windows.Forms.DataVisualization.Charting.SeriesChartTypeを参考にしている。
    /// </summary>
    public enum AvailableChartType
    {
        Bar = SeriesChartType.Bar,
        Column = SeriesChartType.Column,
        Line = SeriesChartType.Line,
        Pie = SeriesChartType.Pie,
        Histogram = SeriesChartType.Column
    }
}
