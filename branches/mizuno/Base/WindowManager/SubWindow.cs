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
using System.Drawing;

namespace NU.OJL.MPRTOS.TLV.Base
{
    /// <summary>
    /// WindowManager�ǻ��Ѥ���륵�֥�����ɥ��ξ����������륯�饹
    /// </summary>
    public class SubWindow
    {
        /// <summary>
        /// DockState���Ѥä��Ȥ���ȯ�����륤�٥��
        /// </summary>
        public event EventHandler<GeneralChangedEventArgs<DockState>> DockStateChanged = null;
        /// <summary>
        /// Visible���Ѥä��Ȥ���ȯ�����륤�٥��
        /// </summary>
        public event EventHandler<GeneralChangedEventArgs<bool>> VisibleChanged = null;
        /// <summary>
        /// Enabled���Ѥä��Ȥ���ȯ�����륤�٥��
        /// </summary>
        public event EventHandler<GeneralChangedEventArgs<bool>> EnabledChanged = null;

        private string _name = string.Empty;
        private DockState _dockState = DockState.Unknown;
        private bool _visible = true;
        private bool _enabled = true;

        /// <summary>
        /// ���֥�����ɥ���Fill�����Control
        /// </summary>
        public Control Control{ get; private set; }
        /// <summary>
        /// �ɥå��󥰾���
        /// </summary>
        public DockState DockState
        {
            get { return _dockState; }
            set
            {
                if(_dockState != value)
                {
                    DockState old = _dockState;

                    _dockState = value;

                    if (DockStateChanged != null)
                        DockStateChanged(this, new GeneralChangedEventArgs<DockState>(old, _dockState));
                }
            }
        }
        /// <summary>
        /// ���֥�����ɥ���ɽ������Ƥ��뤫�ɤ����򼨤���
        /// </summary>
        public bool Visible
        {
            get { return _visible; }
            set
            {
                if (_visible != value)
                {
                    bool old = _visible;

                    _visible = value;

                    if (VisibleChanged != null)
                        VisibleChanged(this, new GeneralChangedEventArgs<bool>(old, _visible));
                }
            }
        }
        /// <summary>
        /// ���֥�����ɥ���̾�������̻ҤȤ��ƻȤ���
        /// </summary>
        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }
        /// <summary>
        /// ���֥�����ɥ��Υ����ȥ�С���ɽ�������ƥ�����
        /// </summary>
        public string Text { get; set; }
        /// <summary>
        /// ���֥�����ɥ���ͭ�����ɤ���
        /// </summary>
        public bool Enabled
        {
            get { return _enabled; }
            set
            {
                if(_enabled != value)
                {
                    bool old = _enabled;

                    _enabled = value;

                    if (EnabledChanged != null)
                        EnabledChanged(this, new GeneralChangedEventArgs<bool>(old, _enabled));

                    if (!_enabled && Visible)
                    {
                        Visible = false;
                    }
                }
            }
        }

        /// <summary>
        /// SubWindow�Υ��󥹥��󥹤�����
        /// </summary>
        /// <param name="name">���֥�����ɥ���̾��</param>
        /// <param name="control">���֥�����ɥ���Fill�����Control</param>
        /// <param name="dockState">���֥�����ɥ��Υɥå��󥰾���</param>
        public SubWindow(string name, Control control, DockState dockState)
        {
            Name = name;
            Text = name;
            Control = control;
            Control.Dock = DockStyle.Fill;
            DockState = dockState;
            Visible = true;
        }

    }

}
