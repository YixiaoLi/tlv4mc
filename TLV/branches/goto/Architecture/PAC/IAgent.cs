using System;
using System.Collections.Generic;
using NU.OJL.MPRTOS.TLV.Architecture.PAC.Bace;

namespace NU.OJL.MPRTOS.TLV.Architecture.PAC
{
    public interface IAgent : IElement, ITreeStructure<IAgent>
    {
        Abstraction Abstraction { get; }
        IPresentation Presentation { get; }
        IControl Control { get; }
        void Show();
        void InitPAC();
        void Init();
        bool IsMain { get; }
    }
}
