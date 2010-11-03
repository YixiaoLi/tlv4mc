using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
        public string DefaultType { get; set; }
        /// <summary>
        /// 目盛の間隔
        /// </summary>
        public double? MajorTickMarkInterval { get; set; }
        /// <summary>
        /// 補助目盛の間隔
        /// </summary>
        public double? MinorTickMarkInterval { get; set; }
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
            MajorTickMarkInterval = null;
            MinorTickMarkInterval = null;
            MajorGridVisible = false;
            MinorGridVisible = false;
        }

        public void SetData(Json json)
        {
            // Key: 設定名("Title"等)
            // 各設定は必ずしもJsonに記述されているわけではないため、
            // KeyNotFoundExceptionチェックをしなくてもよいようにループで処理している
            foreach (KeyValuePair<string, Json> j in json.GetKeyValuePairEnumerator())
            {
                switch (j.Key)
                {
                    case "Title": Title = j.Value; break;
                    case "AxisXTitle": AxisXTitle = j.Value; break;
                    case "AxisYTitle": AxisYTitle = j.Value; break;
                    case "SeriesTitle": SeriesTitle = j.Value; break;
                    case "DefaultType": DefaultType = j.Value; break;
                    case "MajorTickMarkInterval": MajorTickMarkInterval = double.Parse(j.Value.ToString()); break;
                    case "MinorTickMarkInterval": MinorTickMarkInterval = double.Parse(j.Value.ToString()); break;
                    case "MajorGridVisible": MajorGridVisible = j.Value; break;
                    case "MinorGridVisible": MinorGridVisible = j.Value; break;
                    default: throw new Exception(j.Key + "は設定できない、または、存在しない設定です。");
                }
            }
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
}
