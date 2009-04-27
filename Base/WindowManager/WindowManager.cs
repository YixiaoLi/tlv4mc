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
using System.Drawing;
using System.Windows.Forms;

namespace NU.OJL.MPRTOS.TLV.Base
{
    /// <summary>
    /// �ɥå��󥰲�ǽ�ʥ��֥�����ɥ���������륯�饹
    /// </summary>
    public class WindowManager : IWindowManager
    {
        private SubWindowCollection _subWindows = new SubWindowCollection();
		private Control _mainPanel = null;

        /// <summary>
        /// <c>SubWindow</c>���ɲä����Ȥ���ȯ�����륤�٥��
        /// </summary>
        public event EventHandler<GeneralEventArgs<SubWindow>> SubWindowAdded = null;
        /// <summary>
        /// ��������SubWindow��DockState���Ѥä��Ȥ���ȯ�����륤�٥��
        /// </summary>
        public event EventHandler<GeneralChangedEventArgs<DockState>> SubWindowDockStateChanged = null;
        /// <summary>
        /// ��������SubWindow��Visible���Ѥä��Ȥ���ȯ�����륤�٥��
        /// </summary>
        public event EventHandler<GeneralChangedEventArgs<bool>> SubWindowVisibleChanged = null;

        /// <summary>
        /// ����<c>WindowManager</c>���Ǽ����<c>Control</c>
        /// </summary>
		public virtual Control Parent { get; set; }
        /// <summary>
        /// <c>MainPanel</c>��Fill�����<c>Contorl</c>
        /// </summary>
        public virtual Control MainPanel
        {
            get { return _mainPanel; }
            set
            {
                _mainPanel = value;
                _mainPanel.Dock = DockStyle.Fill;
            }
        }
        /// <summary>
        /// ����<c>WindowManager</c>�Ǵ������Ƥ���SubWindow���֤����ƥ졼��
        /// </summary>
        public IEnumerable<SubWindow> SubWindows
        {
            get
            {
                foreach (SubWindow sw in _subWindows)
                {
                    yield return sw;
                }
            }
        }
        /// <summary>
        /// ���ꤷ��<c>SubWindow</c>���ɲä��ޤ�
        /// </summary>
        /// <remarks><paramref name="subWindows"/>�ѥ�᡼�����ɲä���<c>SubWindow</c>����ꤹ�롣<paramref name="subWindows"/>�ѥ�᡼���ϲ���Ĺ�����Ǥ��롣</remarks>
        /// <param name="subWindows">�ɲä���<c>SubWindow</c></param>
        public virtual void AddSubWindow(params SubWindow[] subWindows)
        {
            foreach(SubWindow sw in subWindows)
            {
                _subWindows.Add(sw);

                sw.DockStateChanged += OnSubWindowDockStateChanged;
                sw.VisibleChanged += OnSubWindowVisibleChanged;

                if (sw.Visible)
                {
                    ShowSubWindow(sw.Name);
                }
                else
                {
                    HideSubWindow(sw.Name);
                }

                if (SubWindowAdded != null)
                    SubWindowAdded(this, new GeneralEventArgs<SubWindow>(sw));

            }
        }
        /// <summary>
        /// �������ˤ���<c>SubWindow</c>�Τ��٤Ƥ�ɽ�����ޤ�
        /// </summary>
        public virtual void ShowAllSubWindows()
        {
            foreach (SubWindow sw in _subWindows)
            {
                ShowSubWindow(sw.Name);
            }
        }
        /// <summary>
        /// �������ˤ���<c>SubWindow</c>�Τ��٤Ƥ���ɽ���ˤ��ޤ�
        /// </summary>
        public virtual void AutoHideAllSubWindows()
        {
            foreach (SubWindow sw in _subWindows)
            {
                AutoHideSubWindow(sw.Name);
            }
        }
        /// <summary>
        /// �������ˤ���<c>SubWindow</c>�Τ��٤Ƥ򥪡��ȥϥ��ɾ��֤ˤ��ޤ�
        /// </summary>
        public virtual void HideAllSubWindows()
        {
            foreach (SubWindow sw in _subWindows)
            {
                HideSubWindow(sw.Name);
            }
        }
        /// <summary>
        /// <paramref name="name"/>�ǻ��ꤷ��<c>Name</c>�ץ�ѥƥ�����<c>SubWindow</c>��ɽ�����ޤ�
        /// </summary>
        /// <param name="name">ɽ������<c>SubWindow</c>��<c>Name</c>�ץ�ѥƥ�����</param>
        public virtual void ShowSubWindow(string name)
        {
            _subWindows[name].Visible = true;
        }
        /// <summary>
        /// <paramref name="name"/>�ǻ��ꤷ��<c>Name</c>�ץ�ѥƥ�����<c>SubWindow</c>����ɽ���ˤ��ޤ�
        /// </summary>
        /// <param name="name">��ɽ���ˤ���<c>SubWindow</c>��<c>Name</c>�ץ�ѥƥ�����</param>
        public virtual void HideSubWindow(string name)
        {
            _subWindows[name].Visible = false;
        }
        /// <summary>
        /// <paramref name="name"/>�ǻ��ꤷ��<c>Name</c>�ץ�ѥƥ�����<c>SubWindow</c>�򥪡��ȥϥ��ɾ��֤ˤ��ޤ�
        /// </summary>
        /// <param name="name">�����ȥϥ��ɾ��֤ˤ���<c>SubWindow</c>��<c>Name</c>�ץ�ѥƥ�����</param>
        public virtual void AutoHideSubWindow(string name)
        {
            switch (_subWindows[name].DockState)
            {
                case DockState.DockBottom:
                    _subWindows[name].DockState = DockState.DockBottomAutoHide;
                    break;
                case DockState.DockLeft:
                    _subWindows[name].DockState = DockState.DockLeftAutoHide;
                    break;
                case DockState.DockRight:
                    _subWindows[name].DockState = DockState.DockRightAutoHide;
                    break;
                case DockState.DockTop:
                    _subWindows[name].DockState = DockState.DockTopAutoHide;
                    break;
                default:
                    break;
            }
        }
        /// <summary>
        /// �������Ƥ���<c>SubWindow</c>�ο�
        /// </summary>
        public int SubWindowCount
        {
            get { return _subWindows.Count; }
        }
        /// <summary>
        /// <paramref name="name"/>�ǻ��ꤷ��<c>Name</c>�ץ�ѥƥ�����<c>SubWindow</c>���������
        /// </summary>
        /// <param name="name">����������<c>SubWindow</c>��<c>Name</c>�ץ�ѥƥ�����</param>
        /// <returns><paramref name="name"/>�ǻ��ꤷ���ͤ�<c>Name</c>�ץ�ѥƥ�����<c>SubWindow</c></returns>
        public SubWindow GetSubWindow(string name)
        {
            return _subWindows[name];
        }
        /// <summary>
        /// <paramref name="name"/>�ǻ��ꤷ��<c>Name</c>�ץ�ѥƥ�����<c>SubWindow</c>���������ˤ��뤫�ɤ���
        /// </summary>
        /// <param name="name">�������ˤ��뤫�ɤ���Ĵ�٤���<c>SubWindow</c>��<c>Name</c>�ץ�ѥƥ�</param>
        /// <returns>�������ˤ�����true</returns>
        /// <returns>�������ˤʤ����false</returns>
        public bool ContainSubWindow(string name)
        {
            return _subWindows.Contains(name);
        }
        /// <summary>
        /// ��������<c>SubWindow</c>�Τɤ줫��<c>DockState</c>�ץ�ѥƥ����ͤ��Ѳ������Ȥ��˸ƤӽФ����
        /// </summary>
        /// <param name="sender"><c>DockState</c>���Ѳ�����<c>SubWindow</c></param>
        /// <param name="e"><c>EventArgs.Empty</c></param>
        protected virtual void OnSubWindowDockStateChanged(object sender, GeneralChangedEventArgs<DockState> e)
        {
            if (SubWindowDockStateChanged != null)
                SubWindowDockStateChanged(sender, e);
        }
        /// <summary>
        /// ��������<c>SubWindow</c>�Τɤ줫��<c>Visible</c>�ץ�ѥƥ����ͤ��Ѳ������Ȥ��˸ƤӽФ����
        /// </summary>
        /// <param name="sender"><c>Visible</c>���Ѳ�����<c>SubWindow</c></param>
        /// <param name="e"><c>EventArgs.Empty</c></param>
        protected virtual void OnSubWindowVisibleChanged(object sender, GeneralChangedEventArgs<bool> e)
        {
            if (SubWindowVisibleChanged != null)
                SubWindowVisibleChanged(sender, e);
        }

		public virtual void Save()
		{

		}
		public virtual void Load()
		{

		}
		public virtual void Show()
		{

		}

    }
}
