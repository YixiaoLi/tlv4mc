using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NU.OJL.MPRTOS.TLV.Base
{
    public class CommandManager : ICommandManager
    {
        private event Action<ICommand> _pushUndo;
        private event Func<ICommand> _popUndo;
		private event Action<ICommand> _pushRedo;
		private event Func<ICommand> _popRedo;
        private event Func<string> _undoText;
		private event Func<string> _redoText;
        private event Action _clearRedo;
        private bool _isUndoing = false;
        private bool _isRedoing = false;
        private bool _canUndo = false;
        private bool _canRedo = false;

        public event EventHandler<GeneralEventArgs<ICommand>> CommandDone;
        public event EventHandler<GeneralEventArgs<ICommand>> CommandUndone;
        public event EventHandler<GeneralEventArgs<ICommand>> CommandRedone;
        public event EventHandler UndoBecameDisenable;
        public event EventHandler UndoBecameEnable;
        public event EventHandler RedoBecameDisenable;
        public event EventHandler RedoBecameEnable;

        public string UndoText
        {
            get { return CanUndo ? "「" + _undoText() + "」を" : ""; }
        }
        public string RedoText
        {
            get { return CanRedo ? "「" + _redoText() + "」を" : ""; }
        }
        public bool CanUndo
        {
            get { return _canUndo; }
            set
            {
                if (_canUndo != value)
                {
                    _canUndo = value;

                    if (_canUndo && UndoBecameEnable != null)
                        UndoBecameEnable(this, EventArgs.Empty);
                    else if (!_canUndo && UndoBecameDisenable != null)
                        UndoBecameDisenable(this, EventArgs.Empty);
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

                    if (_canRedo && RedoBecameEnable != null)
                        RedoBecameEnable(this, EventArgs.Empty);
                    else if (!_canRedo && RedoBecameDisenable != null)
                        RedoBecameDisenable(this, EventArgs.Empty);
                }
            }
        }

        /// <summary>
        /// <paramref name="command"/>を実行する
        /// </summary>
        /// <param name="command">実行するコマンド</param>
        public void Do(ICommand command)
        {
            command.Do();
            Done(command);
        }
        /// <summary>
        /// <paramref name="command"/>で行う処理が既に行われてしまったときに実行する
        /// 実行せずにUndoスタックに積みたいときに使用する
        /// </summary>
        /// <param name="command">実行したコマンド</param>
        public void Done(ICommand command)
        {
            if (!_isUndoing && !_isRedoing)
            {
                _pushUndo(command);
                _clearRedo();
            }

            if (CommandDone != null)
                CommandDone(this, new GeneralEventArgs<ICommand>(command));
        }
        /// <summary>
        /// 一つ前のコマンドを元に戻す
        /// </summary>
        public void Undo()
        {
            if (CanUndo)
            {
                _isUndoing = true;

                ICommand c = _popUndo();

                _pushRedo(c);

                c.Undo();

                _isUndoing = false;

                if (CommandUndone != null)
                    CommandUndone(this, new GeneralEventArgs<ICommand>(c));
            }
        }
        /// <summary>
        /// 一つ前にアンドゥしたものをやり直す
        /// </summary>
        public void Redo()
        {
            if (CanRedo)
            {
                _isRedoing = true;

                ICommand c = _popRedo();

                _pushUndo(c);

                Do(c);

                _isRedoing = false;

                if (CommandRedone != null)
                    CommandRedone(this, new GeneralEventArgs<ICommand>(c));
            }
        }
        /// <summary>
        /// <c>CommandManager</c>クラスのインスタンスを生成する
        /// </summary>
        public CommandManager()
        {
            Stack<ICommand> undoStack = new Stack<ICommand>();
            Stack<ICommand> redoStack = new Stack<ICommand>();

            _pushUndo = (command) =>
                {
                    if (command.CanUndo)
                    {
                        if (undoStack.Count == 0)
                            CanUndo = true;

                        undoStack.Push(command);
                    }
                };
            _popUndo = () =>
                {
                    ICommand c = undoStack.Pop();

                    if (undoStack.Count == 0)
                        CanUndo = false;

                    return c;
                };
            _pushRedo = (command) =>
                {
                    if (redoStack.Count == 0)
                        CanRedo = true;

                    redoStack.Push(command);
                };
            _popRedo = () =>
                {
                    ICommand c = redoStack.Pop();

                    if (redoStack.Count == 0)
                        CanRedo = false;

                    return c;
                };
            _clearRedo = () =>
                {
                    if (redoStack.Count != 0)
                    {
                        CanRedo = false;
                        redoStack.Clear();
                    }
                };
            _undoText = () =>
                {
                    return undoStack.Peek().Text;
                };
            _redoText = () =>
                {
                    return redoStack.Peek().Text;
                };
        }
    }
}
