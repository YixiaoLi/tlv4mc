using System;
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
        public static void SetUndoMenu(this ToolStripMenuItem tsmi, TransactionManager transactionManager)
        {
            tsmi.Enabled = false;

            transactionManager.TransactionDone += (o, e) =>
                {
                    tsmi.Text = "「" + transactionManager.UndoText + "」を元に戻す";
                    tsmi.Click += (_o, _e) =>
                        {
                            transactionManager.Undo();
                        };
                };
            transactionManager.UndoBecameEnable += (o, e) =>
                {
                    tsmi.Enabled = true;
                };
            transactionManager.UndoBecameDisEnable += (o, e) =>
            {
                tsmi.Text = "元に戻す";
                tsmi.Enabled = false;
            };
        }

        public static void SetRedoMenu(this ToolStripMenuItem tsmi, TransactionManager transactionManager)
        {
            tsmi.Enabled = false;

            transactionManager.Undone += (o, e) =>
            {
                tsmi.Text = "「" + transactionManager.RedoText + "」をやり直す";
                tsmi.Click += (_o, _e) =>
                    {
                        transactionManager.Redo();
                    };
            };
            transactionManager.RedoBecameDisenable += (o, e) =>
            {
                tsmi.Text = "やり戻す";
                tsmi.Enabled = false;
            };
            transactionManager.RedoBecameEnable += (o, e) =>
            {
                tsmi.Enabled = true;
            };
        }
    }
}
