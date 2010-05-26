using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NU.OJL.MPRTOS.TLV.Core.Search
{
    interface TraceLogSearcher
    {
        decimal searchForward();
        decimal searchBackward();
        decimal[] searchWhole();
    }
}
