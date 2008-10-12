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
        /// メニューとWindowManagerを関連付ける拡張メソッド
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
            item.CheckOnClick = true;
            item.CheckedChanged += (o, e) => { sw.Visible = ((ToolStripMenuItem)o).Checked; };
            sw.VisibleChanged += (o, e) => { item.Checked = sw.Visible; };
            sw.DockStateChanged += (o, e) => { item.setSubWindowText(sw); };
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
        public static void SetUndoMenu(this ToolStripMenuItem tsmi, CommandManager commandManager)
        {
            tsmi.Enabled = false;

            commandManager.CommandDone += (o, e) =>
                {
                    tsmi.Text = "「" + commandManager.UndoText + "」を元に戻す";
                    tsmi.Click += (_o, _e) =>
                        {
                            commandManager.Undo();
                        };
                };
            commandManager.UndoBecameEnable += (o, e) =>
                {
                    tsmi.Enabled = true;
                };
            commandManager.UndoBecameDisEnable += (o, e) =>
            {
                tsmi.Text = "元に戻す";
                tsmi.Enabled = false;
            };
        }

        public static void SetRedoMenu(this ToolStripMenuItem tsmi, CommandManager commandManager)
        {
            tsmi.Enabled = false;

            commandManager.CommandUndone += (o, e) =>
            {
                tsmi.Text = "「" + commandManager.RedoText + "」をやり直す";
                tsmi.Click += (_o, _e) =>
                    {
                        commandManager.Redo();
                    };
            };
            commandManager.RedoBecameDisenable += (o, e) =>
            {
                tsmi.Text = "やり戻す";
                tsmi.Enabled = false;
            };
            commandManager.RedoBecameEnable += (o, e) =>
            {
                tsmi.Enabled = true;
            };
        }
    }
}
