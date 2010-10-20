using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NU.OJL.MPRTOS.TLV.Core.FileContext.VisualizeData;

namespace NU.OJL.MPRTOS.TLV.Core.Search
{
    interface TraceLogSearcher
    {
        void setSearchData(List<VisualizeLog> visLogs, SearchCondition baseCondition, List<SearchCondition> refiningCondition);
        VisualizeLog searchForward();
        VisualizeLog searchBackward();
        List<VisualizeLog> searchWhole();
    }
}
