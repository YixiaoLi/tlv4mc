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
        private string _name = string.Empty;
        /// <summary>
        /// 統計情報名。統計生成ルールファイルのルール名が入る。
        /// </summary>
        public string Name
        {
            get { return _name; }
            set
            {
                _name = value;
                if (DisplayName == null)
                    DisplayName = value;
            }
        }
        /// <summary>
        /// 表示用の名前
        /// </summary>
        public string DisplayName { get; set; }
        /// <summary>
        /// グラフの初期タイプ
        /// </summary>
        public string DefaultType { get; set; }
        /// <summary>
        /// データ系列
        /// </summary>
        public Series Series { get; set; }

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
