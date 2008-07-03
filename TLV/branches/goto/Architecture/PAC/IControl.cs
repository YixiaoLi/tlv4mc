using System;
using System.Collections.Generic;
using NU.OJL.MPRTOS.TLV.Architecture.PAC.Bace;

namespace NU.OJL.MPRTOS.TLV.Architecture.PAC
{
    public interface IControl : IElement
    {
        Abstraction Abstraction { get; }
        IPresentation Presentation { get; }
    }
}
