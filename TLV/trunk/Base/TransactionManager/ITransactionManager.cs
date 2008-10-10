using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NU.OJL.MPRTOS.TLV.Base
{
    public interface ITransactionManager
    {
        void Do(params ITransaction[] transactions);
        void Done(params ITransaction[] transactions);
        void Undo();
        void Redo();
    }
}
