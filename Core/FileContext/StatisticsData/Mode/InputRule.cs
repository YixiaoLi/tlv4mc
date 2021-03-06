﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NU.OJL.MPRTOS.TLV.Core.FileContext.StatisticsData.Mode
{
    public class InputRule
    {
        /// <summary>
        /// 統計情報ファイルのフルパス
        /// </summary>
        public string FileName { get; set; }
        public GeneralNamedCollection<Statistics> Data { get; set; }
    }
}
