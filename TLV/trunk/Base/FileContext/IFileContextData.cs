using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NU.OJL.MPRTOS.TLV.Base
{
    public interface IFileContextData : ISerializable
    {
        event EventHandler BecameDirty;
        bool IsDirty{ get; }
    }
}
