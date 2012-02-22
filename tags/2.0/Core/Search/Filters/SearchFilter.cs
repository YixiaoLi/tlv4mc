using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NU.OJL.MPRTOS.TLV.Core.Search;
using NU.OJL.MPRTOS.TLV.Core.Search.SearchConditions;
using NU.OJL.MPRTOS.TLV.Core.FileContext.VisualizeData;

namespace NU.OJL.MPRTOS.TLV.Core.Search.Filters
{
    public abstract class SearchFilter
    {
        public abstract Boolean doMatching(VisualizeLog targetLog);
    }
}
