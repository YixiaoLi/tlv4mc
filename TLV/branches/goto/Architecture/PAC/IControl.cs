using System;
using System.Collections.Generic;
using NU.OJL.MPRTOS.TLV.Architecture.PAC.Bace;

namespace NU.OJL.MPRTOS.TLV.Architecture.PAC
{
    public interface IControl : IElement, ITreeStructure<IControl>
    {
        Abstraction Abstraction { get; }
        IPresentation Presentation { get; }
        IAbstraction GetAProviding(Type type, string name, SearchAFlags flags, IControl self);
        Delegate GetPProviding(Type type, string name, SearchAFlags flags, IControl self);
        void BindPToA(string pPropertyName, Type aType, string aPropertyName, SearchAFlags flags);
        Delegate GetDelegate(Type type, string name, SearchAFlags flags);
        void Init();
    }

}
