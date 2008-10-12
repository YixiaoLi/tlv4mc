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
        public event EventHandler UndoDisEnable;
        public event EventHandler UndoEnable;
        public event EventHandler RedoDisenable;
        public event EventHandler RedoEnable;
        private bool _isUndoing = false;
        private bool _isRedoing = false;
        private bool _canUndo = false;
        private bool _canRedo = false;
        private Stack<ICommand> _undoStack = new Stack<ICommand>();
        private Stack<ICommand> _redoStack = new Stack<ICommand>();

        public string UndoText
        {
            get { return CanUndo ? "「" + _undoStack.Peek().Text + "」を" : ""; }
        }
        public string RedoText
        {
            get { return CanRedo ? "「" + _redoStack.Peek().Text + "」を" : ""; }
        }
        public bool CanUndo
        {
            get { return _canUndo; }
            set
            {
                if (_canUndo != value)
                {
                    _canUndo = value;

                    if (_canUndo && UndoEnable != null)
                        UndoEnable(this, EventArgs.Empty);
                    else if (!_canUndo && UndoDisEnable != null)
                        UndoDisEnable(this, EventArgs.Empty);
                }
            }
        }
        public bool CanRedo
        {
            get { return _canRedo; }
            set
            {
                if (_canRedo != value)
                {
                    _canRedo = value;

                    if (_canRedo && RedoEnable != null)
                        RedoEnable(this, EventArgs.Empty);
                    else if (!_canRedo && RedoDisenable != null)
                        RedoDisenable(this, EventArgs.Empty);
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
            if (!_isUndoing && !_isRedoing)
            {
                if (_undoStack.Count == 0)
                    CanUndo = true;

                if (_redoStack.Count != 0)
                {
                    CanRedo = false;
                    _redoStack.Clear();
                }

                _undoStack.Push(command);
            }

            if (CommandDone != null)
                CommandDone(this, new GeneralEventArgs<ICommand>(command));
        }

        public void Undo()
        {
            if (CanUndo)
            {
                _isUndoing = true;

                ICommand c = _undoStack.Pop();

                if (_undoStack.Count == 0)
                    CanUndo = false;

                if (_redoStack.Count == 0)
                    CanRedo = true;

                _redoStack.Push(c);

                c.Undo();

                _isUndoing = false;

                if (CommandUndone != null)
                    CommandUndone(this, new GeneralEventArgs<ICommand>(c));
            }
        }

        public void Redo()
        {
            if (CanRedo)
            {
                _isRedoing = true;

                ICommand c = _redoStack.Pop();

                if (_redoStack.Count == 0)
                    CanRedo = false;

                if (_undoStack.Count == 0)
                    CanUndo = true;

                _undoStack.Push(c);

                Do(c);

                _isRedoing = false;

                if (CommandRedone != null)
                    CommandRedone(this, new GeneralEventArgs<ICommand>(c));
            }
        }


    }
}
