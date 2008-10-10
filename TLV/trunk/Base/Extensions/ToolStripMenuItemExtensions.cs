using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Windows.Forms;

namespace NU.OJL.MPRTOS.TLV.Base
{
    public static class ToolStripMenuItemExtensions
    {
        public static void SetWindowManager(this ToolStripMenuItem tsmi, WindowManager windowManager)
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
                tsmi.addSubWindowMenuItem(e.SubWindow);
            };
        }

        private static void addSubWindowMenuItem(this ToolStripMenuItem tsmi, SubWindow sw)
        {
            ToolStripMenuItem item = new ToolStripMenuItem() { Name = sw.Name };
            item.setSubWindowText(sw);
            item.Checked = sw.Visible;
            item.CheckOnClick = true;
            item.CheckedChanged += (o, e) => { sw.Visible = ((ToolStripMenuItem)o).Checked; };
            sw.VisibleChanged += (o, e) => { item.Checked = ((SubWindow)sw).Visible; };
            sw.DockStateChanged += (o, e) => { item.setSubWindowText(sw); };
            tsmi.DropDownItems.Add(item);
        }

        private static void setSubWindowText(this ToolStripMenuItem tsmi, SubWindow sw)
        {
            tsmi.Text = sw.Text;
            tsmi.ShortcutKeyDisplayString = sw.DockState.ToText();
        }
    }
}
