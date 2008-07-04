using System;
using System.Collections.Generic;
using NU.OJL.MPRTOS.TLV.Architecture.PAC.Bace;

namespace NU.OJL.MPRTOS.TLV.Architecture.PAC
{
    public interface IControl : IElement
    {
        Abstraction Abstraction { get; }
        IPresentation Presentation { get; }
        IControl Parent { get; set; }
        ControlTable Children { get; }
        void Add(IControl control);
        IAbstraction getAProviding(Type type, string name, SearchAFlags flags, IControl self);
        IAbstraction getAProviding(Type type, string name, SearchAFlags flags);
        void BindPToA(string pPropertyName, Type aType, string aPropertyName);
        void BindPToA(string pPropertyName, Type aType, string aPropertyName, SearchAFlags flags);
        void Init();
    }
}
