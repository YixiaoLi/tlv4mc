using System;
using System.Collections.Generic;
using NU.OJL.MPRTOS.TLV.Architecture.PAC.Bace;

namespace NU.OJL.MPRTOS.TLV.Architecture.PAC
{
    public interface IAgent : IElement
    {
        IAgent Parent { get; set; }
        Abstraction Abstraction { get; }
        IPresentation Presentation { get; }
        IControl Control { get; }
        AgentTable Children { get; }
        void Show();
        bool IsMain { get; }
        void Add(IAgent agent);
    }
}
