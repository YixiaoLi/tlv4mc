using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NU.OJL.MPRTOS.TLV.Architecture.PAC.Bace
{
    [Flags]
    public enum SearchAFlags
    {
        None = 0x00,
        Self = 0x01,
        Parent = 0x02,
        Ancestors = 0x04,
        AncestorsWithSiblings = 0x08,
        Children = 0x10,
        Descendants = 0x20,
        All = Self | AncestorsWithSiblings | Descendants,
    }
}
