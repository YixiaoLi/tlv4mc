using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NU.OJL.MPRTOS.TLV.Base
{
    public class CommandManager : ICommandManager
    {
        public event EventHandler<GeneralEventArgs<ICommand>> CommandDone;
        public event EventHandler<GeneralEventArgs<ICommand>> CommandUndone;
        public event EventHandler<GeneralEventArgs<ICommand>> CommandRedone;
        public event EventHandler UndoBecameDisEnable;
        public event EventHandler UndoBecameEnable;
        public event EventHandler RedoBecameDisenable;
        public event EventHandler RedoBecameEnable;
        private bool _isUndoing = false;
        private bool _canUndo = false;
        private bool _canRedo = false;
        private Stack<ICommand> _undoStack = new Stack<ICommand>();
        private Stack<ICommand> _redoStack = new Stack<ICommand>();

        public string UndoText
        {
            get { return _undoStack.Peek().Text; }
        }
        public string RedoText
        {
            get { return _redoStack.Peek().Text; }
        }

        public bool CanUndo
        {
            get { return _canUndo; }
            set
            {
                if (_canUndo != value)
                {
                    _canUndo = value;
                }
            }
        }

        public void Do(ICommand command)
        {
            command.Do();
            Done(command);
        }

        public void Done(ICommand command)
        {
            if (!_isUndoing)
            {
                _undoStack.Push(command);

                if (_undoStack.Count != 0 && UndoBecameEnable != null)
                    UndoBecameEnable(this, EventArgs.Empty);
            }

            if(_redoStack.Count != 0)
            {
                _redoStack.Clear();

                if (RedoBecameDisenable != null)
                    RedoBecameDisenable(this, EventArgs.Empty);
            }

            if (CommandDone != null)
                CommandDone(this, new GeneralEventArgs<ICommand>(command));
        }

        public void Undo()
        {
            if (_undoStack.Count != 0)
            {
                _isUndoing = true;
                ICommand t = _undoStack.Pop();
                t.Undo();
                _redoStack.Push(t);

                if (_redoStack.Count == 1 && RedoBecameEnable != null)
                    RedoBecameEnable(this, EventArgs.Empty);

                if(_undoStack.Count == 0 && UndoBecameDisEnable != null)
                    UndoBecameDisEnable(this, EventArgs.Empty);

                _isUndoing = false;

                if (CommandUndone != null)
                    CommandUndone(this, new GeneralEventArgs<ICommand>(t));
            }
        }

        public void Redo()
        {
            if (_redoStack.Count != 0)
            {
                ICommand t = _redoStack.Pop();

                t.Do();

                _undoStack.Push(t);

                if (_undoStack.Count == 1 && UndoBecameEnable != null)
                    UndoBecameEnable(this, EventArgs.Empty);

                if (_redoStack.Count == 0 && RedoBecameDisenable != null)
                    RedoBecameDisenable(this, EventArgs.Empty);

                if (CommandRedone != null)
                    CommandRedone(this, new GeneralEventArgs<ICommand>(t));
            }
        }


    }
}
