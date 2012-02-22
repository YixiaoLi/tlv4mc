using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NU.OJL.MPRTOS.TLV.Base;
using System.Drawing;

namespace NU.OJL.MPRTOS.TLV.Core
{
    /// <summary>
    /// 統計情報を保持するクラス。統計情報ファイル(*.sta)として出力される。
    /// </summary>
    public class Statistics : INamed, IJsonable<Statistics>
    {
        /// <summary>
        /// 統計情報名
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// グラフの設定
        /// </summary>
        public ChartSetting Setting { get; set; }
        /// <summary>
        /// データ系列
        /// </summary>
        public Series Series { get; set; }

        #region コンストラクタ
        public Statistics()
        {
            Name = string.Empty;
            Setting = new ChartSetting();
            Series = new Series();
        }

        public Statistics(string name)
            : this()
        {
            Name = name;
        }
        #endregion コンストラクタ

        public string ToJson()
        {
            return ApplicationFactory.JsonSerializer.Serialize(this);
        }

        public Statistics Parse(string statistics)
        {
            return ApplicationFactory.JsonSerializer.Deserialize<Statistics>(statistics);
        }
    }
}
