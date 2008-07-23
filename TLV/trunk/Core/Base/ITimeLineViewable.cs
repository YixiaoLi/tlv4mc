using System.Collections.Generic;
using System;

namespace NU.OJL.MPRTOS.TLV.Core.Base
{
    public interface ITimeLineViewable
    {
        int MetaId { get; }
        TimeLineViewableObjectType ObjectType { get; }
        TimeLineEvents TimeLineEvents { get; set; }
        string ResourceFormat { get; }
    }
}
