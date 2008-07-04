using System;
using System.Collections.Generic;
using NU.OJL.MPRTOS.TLV.Architecture.PAC.Bace;

namespace NU.OJL.MPRTOS.TLV.Architecture.PAC
{
    public interface IAgent : IElement
    {
        IAgent Parent { get; set; }
        AgentTable Children { get; }
        void Add(IAgent agent);
        Abstraction Abstraction { get; }
        IPresentation Presentation { get; }
        IControl Control { get; }
        void Show();
        void InitPAC();
        bool IsMain { get; }
    }
}
