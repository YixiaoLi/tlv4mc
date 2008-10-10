using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NU.OJL.MPRTOS.TLV.Base
{
    public class TransactionManager : ITransactionManager
    {
        public event EventHandler<GeneralEventArgs<ITransaction>> TransactionDone;
        public event EventHandler<GeneralEventArgs<ITransaction>> Undone;
        public event EventHandler UndoBecameDisEnable;
        public event EventHandler UndoBecameEnable;
        public event EventHandler RedoBecameDisenable;
        public event EventHandler RedoBecameEnable;

        private Stack<ITransaction> _doneStack = new Stack<ITransaction>();
        private Stack<ITransaction> _undoStack = new Stack<ITransaction>();

        public void Do(ITransaction transaction)
        {
            transaction.Do();
            Done(transaction);
        }

        public void Done(ITransaction transaction)
        {
            pushDoneTransaction(transaction);

            if(_undoStack.Count != 0)
            {
                _undoStack.Clear();

                if (RedoBecameDisenable != null)
                    RedoBecameDisenable(this, EventArgs.Empty);
            }
        }

        public void Undo()
        {
            if (_doneStack.Count != 0)
            {
                ITransaction t = _doneStack.Pop();
                t.Undo();
                _undoStack.Push(t);

                Undone(this, new GeneralEventArgs<ITransaction>(t));

                if (_undoStack.Count == 1 && RedoBecameEnable != null)
                    RedoBecameEnable(this, EventArgs.Empty);

                if(_doneStack.Count == 0 && UndoBecameDisEnable != null)
                    UndoBecameDisEnable(this, EventArgs.Empty);
            }
        }

        public void Redo()
        {
            if (_undoStack.Count != 0)
            {
                ITransaction t = _undoStack.Pop();
                t.Do();
                pushDoneTransaction(t);

                if (_undoStack.Count == 0 && RedoBecameDisenable != null)
                    RedoBecameDisenable(this, EventArgs.Empty);
            }
        }

        private void pushDoneTransaction(ITransaction transaction)
        {
            _doneStack.Push(transaction);

            if (_doneStack.Count == 1 && UndoBecameEnable != null)
                UndoBecameEnable(this, EventArgs.Empty);

            if (TransactionDone != null)
                TransactionDone(this, new GeneralEventArgs<ITransaction>(transaction));

        }

        public string UndoText
        {
            get { return _doneStack.Peek().Text; }
        }

        public string RedoText
        {
            get { return _undoStack.Peek().Text; }
        }

    }
}
