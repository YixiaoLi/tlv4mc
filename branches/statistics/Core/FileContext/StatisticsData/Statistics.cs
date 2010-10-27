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
    public class Statistics : INamed
    {
        private string _name = string.Empty;

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
        public string DisplayName { get; set; }
        public string DefaultType { get; set; }
        public Series Series { get; set; }
    }
}
