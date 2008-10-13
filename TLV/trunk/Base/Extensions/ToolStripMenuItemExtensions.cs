﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Windows.Forms;

namespace NU.OJL.MPRTOS.TLV.Base
{
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

    public static class ToolStripMenuItemForTransactionManagerExtensions
    {
        /// <summary>
        /// 元に戻るメニューとWindowManagerを関連付ける拡張メソッド
        /// </summary>
        public static void SetCommandManagerAsUndo(this ToolStripMenuItem tsmi, ICommandManager commandManager)
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
            commandManager.UndoEnable += (o, e) =>
                {
                    tsmi.Enabled = true;
                };
            commandManager.UndoDisEnable += (o, e) =>
                {
                    tsmi.Enabled = false;
                };
        }

        /// <summary>
        /// やり直すメニューとWindowManagerを関連付ける拡張メソッド
        /// </summary>
        public static void SetCommandManagerAsRedo(this ToolStripMenuItem tsmi, ICommandManager commandManager)
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
            commandManager.RedoDisenable += (o, e) =>
                {
                    tsmi.Enabled = false;
                };
            commandManager.RedoEnable += (o, e) =>
                {
                    tsmi.Enabled = true;
                };
        }
    }
}