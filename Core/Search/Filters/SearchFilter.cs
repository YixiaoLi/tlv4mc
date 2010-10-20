using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NU.OJL.MPRTOS.TLV.Core.FileContext.VisualizeData;

namespace NU.OJL.MPRTOS.TLV.Core.Search
{
    interface SearchFilter
    {
        Boolean checkSearchCondition(VisualizeLog visLog, SearchCondition condition, decimal normTime);
    }
}
