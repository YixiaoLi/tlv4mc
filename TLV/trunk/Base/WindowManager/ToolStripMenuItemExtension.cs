using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Windows.Forms;

namespace NU.OJL.MPRTOS.TLV.Base
{
    public static class ToolStripMenuItemExtension
    {
        public static void SetWindowManager(this ToolStripMenuItem tsmi, WindowManager windowManager)
        {
            if (windowManager.SubWindowCount != 0)
            {
                foreach (SubWindow sw in windowManager.SubWindows)
                {
                    tsmi.AddMenuItem(sw);
                }
            }
            windowManager.SubWindowAdded += (o, e) =>
            {
                tsmi.AddMenuItem(e.SubWindow);
            };
        }

        public static void AddMenuItem(this ToolStripMenuItem tsmi, SubWindow sw)
        {
            ToolStripMenuItem item = new ToolStripMenuItem() { Text = sw.Text, Name = sw.Name };
            item.Checked = sw.Visible;
            item.CheckOnClick = true;
            item.CheckedChanged += (o, e) => { sw.Visible = ((ToolStripMenuItem)o).Checked; };
            sw.VisibleChanged += (o, e) => { item.Checked = ((SubWindow)sw).Visible; };
            tsmi.DropDownItems.Add(item);
        }
    }
}
