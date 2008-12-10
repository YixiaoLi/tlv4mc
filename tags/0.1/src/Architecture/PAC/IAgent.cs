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
        void InitChildrenFirstPAC();
        void InitParentFirstPAC();
        void InitChildrenFirst();
        void InitParentFirst();
        bool IsMain { get; }
    }
}
