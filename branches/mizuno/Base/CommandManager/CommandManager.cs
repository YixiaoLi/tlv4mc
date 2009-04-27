/*
 *  TLV - Trace Log Visualizer
 *
 *  Copyright (C) 2008,2009 by Embedded and Real-Time Systems Laboratory
 *              Graduate School of Information Science, Nagoya Univ., JAPAN
 *
 *  �嵭����Ԥϡ��ʲ���(1)��(4)�ξ������������˸¤ꡤ�ܥ��եȥ���
 *  �����ܥ��եȥ���������Ѥ�����Τ�ޤࡥ�ʲ�Ʊ���ˤ���ѡ�ʣ������
 *  �ѡ������ۡʰʲ������ѤȸƤ֡ˤ��뤳�Ȥ�̵���ǵ������롥
 *  (1) �ܥ��եȥ������򥽡��������ɤη������Ѥ�����ˤϡ��嵭������
 *      ��ɽ�����������Ѿ�浪��Ӳ�����̵�ݾڵ��꤬�����Τޤޤη��ǥ���
 *      ����������˴ޤޤ�Ƥ��뤳�ȡ�
 *  (2) �ܥ��եȥ������򡤥饤�֥������ʤɡ�¾�Υ��եȥ�������ȯ�˻�
 *      �ѤǤ�����Ǻ����ۤ�����ˤϡ������ۤ�ȼ���ɥ�����ȡ�����
 *      �ԥޥ˥奢��ʤɡˤˡ��嵭�����ɽ�����������Ѿ�浪��Ӳ���
 *      ��̵�ݾڵ����Ǻܤ��뤳�ȡ�
 *  (3) �ܥ��եȥ������򡤵�����Ȥ߹���ʤɡ�¾�Υ��եȥ�������ȯ�˻�
 *      �ѤǤ��ʤ����Ǻ����ۤ�����ˤϡ����Τ����줫�ξ�����������
 *      �ȡ�
 *    (a) �����ۤ�ȼ���ɥ�����ȡ����Ѽԥޥ˥奢��ʤɡˤˡ��嵭����
 *        �ɽ�����������Ѿ�浪��Ӳ�����̵�ݾڵ����Ǻܤ��뤳�ȡ�
 *    (b) �����ۤη��֤��̤�������ˡ�ˤ�äơ�TOPPERS�ץ������Ȥ�
 *        ��𤹤뤳�ȡ�
 *  (4) �ܥ��եȥ����������Ѥˤ��ľ��Ū�ޤ��ϴ���Ū�������뤤���ʤ�»
 *      ������⡤�嵭����Ԥ����TOPPERS�ץ������Ȥ����դ��뤳�ȡ�
 *      �ޤ����ܥ��եȥ������Υ桼���ޤ��ϥ���ɥ桼������Τ����ʤ���
 *      ͳ�˴�Ť����ᤫ��⡤�嵭����Ԥ����TOPPERS�ץ������Ȥ�
 *      ���դ��뤳�ȡ�
 *
 *  �ܥ��եȥ������ϡ�̵�ݾڤ��󶡤���Ƥ����ΤǤ��롥�嵭����Ԥ�
 *  ���TOPPERS�ץ������Ȥϡ��ܥ��եȥ������˴ؤ��ơ�����λ�����Ū
 *  ���Ф���Ŭ������ޤ�ơ������ʤ��ݾڤ�Ԥ�ʤ����ޤ����ܥ��եȥ���
 *  �������Ѥˤ��ľ��Ū�ޤ��ϴ���Ū�������������ʤ�»���˴ؤ��Ƥ⡤��
 *  ����Ǥ�����ʤ���
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
            get { return CanUndo ? "��" + _undoText() + "�פ�" : ""; }
        }
        public string RedoText
        {
            get { return CanRedo ? "��" + _redoText() + "�פ�" : ""; }
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
        /// <paramref name="command"/>��¹Ԥ���
        /// </summary>
        /// <param name="command">�¹Ԥ��륳�ޥ��</param>
        public void Do(ICommand command)
        {
            command.Do();
			Done(command);
        }
        /// <summary>
        /// <paramref name="command"/>�ǹԤ����������˹Ԥ��Ƥ��ޤä��Ȥ��˼¹Ԥ���
        /// �¹Ԥ�����Undo�����å����Ѥߤ����Ȥ��˻��Ѥ���
        /// </summary>
        /// <param name="command">�¹Ԥ������ޥ��</param>
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
        /// ������Υ��ޥ�ɤ򸵤��᤹
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
        /// ������˥���ɥ�������Τ���ľ��
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
        /// <c>CommandManager</c>���饹�Υ��󥹥��󥹤���������
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
