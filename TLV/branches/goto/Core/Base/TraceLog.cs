using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NU.OJL.MPRTOS.TLV.Core
{
    /// <summary>
    /// トレースログ
    /// </summary>
    public class TraceLog
    {
        public long Time { get; private set; }
        public string Subject { get; private set; }
        public string Object { get; private set; }
        public string Behavior { get; private set; }

        public TraceLog(long time, string subject, string _object, string behavior)
        {
            Time = time;
            Subject = subject;
            Object = _object;
            Behavior = behavior;
        }
    }
}
