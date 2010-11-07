using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NU.OJL.MPRTOS.TLV.Core.FileContext.VisualizeData;

namespace NU.OJL.MPRTOS.TLV.Core.Search
{
     abstract class TraceLogSearcher
    {
         abstract public void setSearchData(List<VisualizeLog> visLogs, SearchCondition condition, List<SearchCondition> refiningCondition);
         abstract public void setSearchData(List<VisualizeLog> visLogs, SearchCondition condition, List<SearchCondition> refiningCondition, Boolean isAnd);
         abstract public VisualizeLog searchForward(decimal time);
         abstract public VisualizeLog searchBackward(decimal time);
         abstract public List<VisualizeLog> searchWhole();
    }
}
