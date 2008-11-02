using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NU.OJL.MPRTOS.TLV.Core
{
    public class TraceLogFileValidationException : Exception
    {
        public TraceLogFileValidationException()
        {
        }
        public TraceLogFileValidationException(string message)
            : base(message)
        {
        }
        public TraceLogFileValidationException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
