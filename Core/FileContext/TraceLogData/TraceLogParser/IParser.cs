using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NU.OJL.MPRTOS.TLV.Core
{
    /// <summary>
    /// ParserとNullObjectを結ぶために用いています。
    /// </summary>
    public interface IParser
    {
        IParser End();
    }
}
