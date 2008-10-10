using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NU.OJL.MPRTOS.TLV.Base
{
    public interface ITransactionManager
    {
        event EventHandler<GeneralEventArgs<ITransaction>> TransactionDone;
        event EventHandler<GeneralEventArgs<ITransaction>> Undone;
        event EventHandler UndoBecameEnable;
        event EventHandler UndoBecameDisEnable;
        event EventHandler RedoBecameDisenable;
        event EventHandler RedoBecameEnable;
        void Do(ITransaction transaction);
        void Done(ITransaction transaction);
        void Undo();
        void Redo();
        string UndoText { get; }
        string RedoText { get; }
    }


}
