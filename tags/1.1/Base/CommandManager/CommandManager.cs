/*
 *  TLV - Trace Log Visualizer
 *
 *  Copyright (C) 2008,2009 by Nagoya Univ., JAPAN
 *
 *  上記著作権者は，以下の(1)〜(4)の条件を満たす場合に限り，本ソフトウェ
 *  ア（本ソフトウェアを改変したものを含む．以下同じ）を使用・複製・改
 *  変・再配布（以下，利用と呼ぶ）することを無償で許諾する．
 *  (1) 本ソフトウェアをソースコードの形で利用する場合には，上記の著作
 *      権表示，この利用条件および下記の無保証規定が，そのままの形でソー
 *      スコード中に含まれていること．
 *  (2) 本ソフトウェアを，ライブラリ形式など，他のソフトウェア開発に使
 *      用できる形で再配布する場合には，再配布に伴うドキュメント（利用
 *      者マニュアルなど）に，上記の著作権表示，この利用条件および下記
 *      の無保証規定を掲載すること．
 *  (3) 本ソフトウェアを，機器に組み込むなど，他のソフトウェア開発に使
 *      用できない形で再配布する場合には，次のいずれかの条件を満たすこ
 *      と．
 *    (a) 再配布に伴うドキュメント（利用者マニュアルなど）に，上記の著
 *        作権表示，この利用条件および下記の無保証規定を掲載すること．
 *    (b) 再配布の形態を，別に定める方法によって，TOPPERSプロジェクトに
 *        報告すること．
 *  (4) 本ソフトウェアの利用により直接的または間接的に生じるいかなる損
 *      害からも，上記著作権者およびTOPPERSプロジェクトを免責すること．
 *      また，本ソフトウェアのユーザまたはエンドユーザからのいかなる理
 *      由に基づく請求からも，上記著作権者およびTOPPERSプロジェクトを
 *      免責すること．
 *
 *  本ソフトウェアは，無保証で提供されているものである．上記著作権者お
 *  よびTOPPERSプロジェクトは，本ソフトウェアに関して，特定の使用目的
 *  に対する適合性も含めて，いかなる保証も行わない．また，本ソフトウェ
 *  アの利用により直接的または間接的に生じたいかなる損害に関しても，そ
 *  の責任を負わない．
 *
 *  @(#) $Id$
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

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
