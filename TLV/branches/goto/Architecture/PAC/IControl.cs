using System;
using System.Collections.Generic;
using NU.OJL.MPRTOS.TLV.Architecture.PAC.Bace;

namespace NU.OJL.MPRTOS.TLV.Architecture.PAC
{
    public interface IControl : IElement, ITreeStructure<IControl>
    {
        Abstraction Abstraction { get; }
        IPresentation Presentation { get; }
        IAbstraction getAProviding(Type type, string name, SearchAFlags flags, IControl self);
        void BindPToA(string pPropertyName, Type aType, string aPropertyName, SearchAFlags flags);
        void Init();
    }

    static class IControlExtension
    {
        public static IAbstraction getAProviding(this IControl ctrl, Type type, string name, SearchAFlags flags)
        {
            return ctrl.getAProviding(type, name, flags, null);
        }

        public static void BindPToA(this IControl ctrl, string pPropertyName, Type aType, string aPropertyName)
        {
            ctrl.BindPToA(pPropertyName, aType, aPropertyName, SearchAFlags.All);
        }
    }

}
