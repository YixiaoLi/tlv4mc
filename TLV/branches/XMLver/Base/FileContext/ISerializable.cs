using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NU.OJL.MPRTOS.TLV.Base
{
    public interface ISerializable
    {
        void Serialize(string path);
        void Deserialize(string path);
    }
}
