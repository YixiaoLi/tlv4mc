using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NU.OJL.MPRTOS.TLV.Core
{
    /// <summary>
    /// 統計情報生成ルールファイルに誤りがあるときにスローされます
    /// </summary>
    class InvalidStatsGenRuleException : Exception
    {
        public InvalidStatsGenRuleException()
            : base()
        {
        }

        public InvalidStatsGenRuleException(string message)
            : base(message)
        {
        }

        public InvalidStatsGenRuleException(string message, Exception e)
            : base(message, e)
        {
        }
    }
}
