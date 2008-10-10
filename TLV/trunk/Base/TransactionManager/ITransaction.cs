using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NU.OJL.MPRTOS.TLV.Base
{
    public interface ITransaction
    {
        string Text { get; set; }
        void Do();
        void Undo();
    }
}
