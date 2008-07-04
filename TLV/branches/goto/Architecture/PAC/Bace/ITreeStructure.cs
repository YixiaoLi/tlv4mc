using System;
using System.Collections.Generic;

namespace NU.OJL.MPRTOS.TLV.Architecture.PAC.Bace
{
    public interface ITreeStructure<T>
        where T : ITreeStructure<T>, IElement
    {
        T Parent { get; set; }
        ChildrenTable<T> Children { get; }
        void Add(T control);
    }
}
