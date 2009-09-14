
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
