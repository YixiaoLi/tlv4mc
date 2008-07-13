using System.Collections.Generic;

namespace NU.OJL.MPRTOS.TLV.Core.Base
{
    public interface ITimeLineViewable
    {
        string Name { get; }
        TimeLineEvents TimeLineEvents { get; }
        List<string> ResourceFileLineFormat { get; }
    }
}
