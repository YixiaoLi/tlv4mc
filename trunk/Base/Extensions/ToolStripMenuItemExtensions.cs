/*
 *  TLV - Trace Log Visualizer
 *
 *  Copyright (C) 2008,2009 by Embedded and Real-Time Systems Laboratory
 *              Graduate School of Information Science, Nagoya Univ., JAPAN
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
using System.Drawing;
using System.Windows.Forms;

namespace NU.OJL.MPRTOS.TLV.Base
{
    /// <summary>
    /// 表示メニューとWindowManagerを関連付ける拡張メソッド定義クラス
    /// </summary>
    public static class ToolStripMenuItemForWindowManagerExtensions
    {
        /// <summary>
        /// 表示メニューとWindowManagerを関連付ける拡張メソッド
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
    /// 元に戻る・やり直すメニューとWindowManagerを関連付ける拡張メソッド定義クラス
    /// </summary>
    public static class ToolStripItemForTransactionManagerExtensions
    {
        /// <summary>
        /// 元に戻るメニューとWindowManagerを関連付ける拡張メソッド
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
                    tsmi.Text = commandManager.UndoText + "元に戻す";
				};
			commandManager.CommandRedone += (o, e) =>
				{
					tsmi.Text = commandManager.UndoText + "元に戻す";
				};
			commandManager.CommandUndone += (o, e) =>
				{
					tsmi.Text = commandManager.UndoText + "元に戻す";
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
        /// やり直すメニューとWindowManagerを関連付ける拡張メソッド
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
					tsmi.Text = commandManager.RedoText + "やり直す";
				};
            commandManager.CommandUndone += (o, e) =>
                {
                    tsmi.Text = commandManager.RedoText + "やり直す";
				};
			commandManager.CommandRedone += (o, e) =>
				{
					tsmi.Text = commandManager.RedoText + "やり直す";
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
