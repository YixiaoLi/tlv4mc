using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NU.OJL.MPRTOS.TLV.Core.FileContext.VisualizeData
{
    class VisualizeLog
    {
        public string resourceName = null;
        public string ruleName = null;
        public string evntName = null;
        public string evntDetail = null;
        public decimal fromTime = -1;
        public EventShape eventShape = null;

        public VisualizeLog(string resource, string rule, string evnt, string detail, decimal from)
        {
            resourceName = resource;
            ruleName = rule;
            evntName = evnt;
            evntDetail = detail;
            fromTime = from;
        }
    }
}
