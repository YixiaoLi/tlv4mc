using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NU.OJL.MPRTOS.TLV.Base
{
    public class TransactionManager : ITransactionManager
    {
        private Stack<ITransaction> _doneStack = new Stack<ITransaction>();
        private Stack<ITransaction> _undoStack = new Stack<ITransaction>();

        public void Do(params ITransaction[] transactions)
        {
            foreach (ITransaction t in transactions)
            {
                t.Do();
            }
            Done(transactions);
        }

        public void Done(params ITransaction[] transactions)
        {
            foreach (ITransaction t in transactions)
            {
                _doneStack.Push(t);
            }

            if(_undoStack.Count != 0)
            {
                _undoStack.Clear();
            }
        }

        public void Undo()
        {
            if (_doneStack.Count != 0)
            {
                ITransaction t = _doneStack.Pop();
                t.Undo();
                _undoStack.Push(t);
            }
        }

        public void Redo()
        {
            if (_undoStack.Count != 0)
            {
                ITransaction t = _undoStack.Pop();
                t.Do();
                _doneStack.Push(t);
            }
        }
    }
}
