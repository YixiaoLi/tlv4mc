using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NU.OJL.MPRTOS.TLV.Core.FileContext.VisualizeData;
using NU.OJL.MPRTOS.TLV.Core.Search.SearchConditions;

namespace NU.OJL.MPRTOS.TLV.Core.Search.Filters
{
    public abstract class DetailFilter : SearchFilter
    {
        protected SearchFilter searchFilter;
        //public abstract Boolean checkSearchConditions(VisualizeLog targetLog);
    }
}
