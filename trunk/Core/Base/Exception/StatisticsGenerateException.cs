using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NU.OJL.MPRTOS.TLV.Core
{
    public class StatisticsGenerateException : System.Exception
    {
        public StatisticsGenerateException()
            : base()
        {
        }

        public StatisticsGenerateException(string message)
            : base(message)
        {
        }

        public StatisticsGenerateException(string message, Exception e)
            : base(message, e)
        {
        }
    }
}
