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
    /// ɽ����˥塼��WindowManager���Ϣ�դ����ĥ�᥽�å�������饹
    /// </summary>
    public static class ToolStripMenuItemForWindowManagerExtensions
    {
        /// <summary>
        /// ɽ����˥塼��WindowManager���Ϣ�դ����ĥ�᥽�å�
        /// </summary>
        public static void SetWindowManager(this ToolStripMenuItem tsmi, IWindowManager windowManager)
        {
            if (windowManager.SubWindowCount != 0)
            {
                foreach (SubWindow sw in windowManager.SubWindows)
                {
                    tsmi.addSubWindowMenuItem(sw);
                }
            }
            windowManager.SubWindowAdded += (o, e) =>
            {
                tsmi.addSubWindowMenuItem(e.Arg);
            };
        }

        private static void addSubWindowMenuItem(this ToolStripMenuItem tsmi, SubWindow sw)
        {
            ToolStripMenuItem item = new ToolStripMenuItem() { Name = sw.Name };
            item.setSubWindowText(sw);
            item.Checked = sw.Visible;
            item.Enabled = sw.Enabled;
            item.CheckOnClick = true;
            item.CheckedChanged += (o, e) => { sw.Visible = ((ToolStripMenuItem)o).Checked; };
            sw.VisibleChanged += (o, e) => { item.Checked = sw.Visible; };
            sw.DockStateChanged += (o, e) => { item.setSubWindowText(sw); };
            sw.EnabledChanged += (o, e) => { item.Enabled = sw.Enabled; };
            tsmi.DropDownItems.Add(item);
        }

        private static void setSubWindowText(this ToolStripMenuItem tsmi, SubWindow sw)
        {
            tsmi.Text = sw.Text;
            tsmi.ShortcutKeyDisplayString = sw.DockState.ToText();
        }
    }

    /// <summary>
    /// ������롦���ľ����˥塼��WindowManager���Ϣ�դ����ĥ�᥽�å�������饹
    /// </summary>
    public static class ToolStripItemForTransactionManagerExtensions
    {
        /// <summary>
        /// ��������˥塼��WindowManager���Ϣ�դ����ĥ�᥽�å�
        /// </summary>
        public static void SetCommandManagerAsUndo(this ToolStripItem tsmi, ICommandManager commandManager)
        {
            tsmi.Enabled = false;

            tsmi.Click += (o, e) =>
                {
                    commandManager.Undo();
                };

            commandManager.CommandDone += (o, e) =>
                {
                    tsmi.Text = commandManager.UndoText + "�����᤹";
				};
			commandManager.CommandRedone += (o, e) =>
				{
					tsmi.Text = commandManager.UndoText + "�����᤹";
				};
			commandManager.CommandUndone += (o, e) =>
				{
					tsmi.Text = commandManager.UndoText + "�����᤹";
				};
            commandManager.UndoBecameEnable += (o, e) =>
                {
                    tsmi.Enabled = true;
                };
            commandManager.UndoBecameDisenable += (o, e) =>
                {
                    tsmi.Enabled = false;
                };
        }

        /// <summary>
        /// ���ľ����˥塼��WindowManager���Ϣ�դ����ĥ�᥽�å�
        /// </summary>
        public static void SetCommandManagerAsRedo(this ToolStripItem tsmi, ICommandManager commandManager)
        {
            tsmi.Enabled = false;

            tsmi.Click += (o, e) =>
                {
                    commandManager.Redo();
                };

			commandManager.CommandDone += (o, e) =>
				{
					tsmi.Text = commandManager.RedoText + "���ľ��";
				};
            commandManager.CommandUndone += (o, e) =>
                {
                    tsmi.Text = commandManager.RedoText + "���ľ��";
				};
			commandManager.CommandRedone += (o, e) =>
				{
					tsmi.Text = commandManager.RedoText + "���ľ��";
				};
            commandManager.RedoBecameDisenable += (o, e) =>
                {
                    tsmi.Enabled = false;
                };
            commandManager.RedoBecameEnable += (o, e) =>
                {
                    tsmi.Enabled = true;
                };
        }
    }
}
