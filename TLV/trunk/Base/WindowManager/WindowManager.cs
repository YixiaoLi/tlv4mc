using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Windows.Forms;

namespace NU.OJL.MPRTOS.TLV.Base
{
    /// <summary>
    /// ドッキング可能なサブウィンドウを管理するクラス
    /// 実際の処理はIWindowManagerHandlerの実装クラスに委譲される
    /// </summary>
    public class WindowManager : IWindowManager
    {
        protected IWindowManagerHandler _handler = null;
        private ToolStripMenuItem _menu = null;

        public ToolStripMenuItem Menu
        {
            get { return _menu; }
            set
            {
                _menu = value;
                if (_handler.SubWindowCount != 0)
                {
                    foreach (SubWindow sw in SubWindows)
                    {
                        addMenuItem(sw);
                    }
                }
            }
        }

        public Control Parent
        {
            get { return _handler.Parent; }
            set { _handler.Parent = value; }
        }

        public Control MainPanel
        {
            get { return _handler.MainPanel; }
            set { _handler.MainPanel = value; }
        }

        public IEnumerable<SubWindow> SubWindows
        {
            get
            {
                return _handler.SubWindows;
            }
        }

        public virtual void AddSubWindow(params SubWindow[] subWindows)
        {
            foreach(SubWindow sw in subWindows)
            {
                _handler.AddSubWindow(sw);

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

                if (_menu != null)
                {
                    addMenuItem(sw);
                }

            }
        }

        public virtual void ShowAllSubWindows()
        {
            _handler.ShowAllSubWindows();
        }

        public virtual void AutoHideAllSubWindows()
        {
            _handler.AutoHideAllSubWindows();
        }

        public virtual void HideAllSubWindows()
        {
            _handler.HideAllSubWindows();
        }

        public virtual void ShowSubWindow(string name)
        {
            _handler.ShowSubWindow(name);
        }

        public virtual void HideSubWindow(string name)
        {
            _handler.HideSubWindow(name);
        }

        public virtual void AutoHideSubWindow(string name)
        {
            _handler.AutoHideSubWindow(name);
        }

        public int SubWindowCount
        {
            get { return _handler.SubWindowCount; }
        }

        public virtual SubWindow GetSubWindow(string name)
        {
            return _handler.GetSubWindow(name);
        }

        public virtual bool ContainSubWindow(string name)
        {
            return _handler.ContainSubWindow(name);
        }

        public virtual void OnSubWindowDockStateChanged(object sender, EventArgs e)
        {
        }

        public virtual void OnSubWindowVisibleChanged(object sender, EventArgs e)
        {
        }

        public WindowManager(IWindowManagerHandler handler)
        {
            _handler = handler;
        }

        private void addMenuItem(SubWindow sw)
        {
            ToolStripMenuItem item = new ToolStripMenuItem() { Text = sw.Name, Name = sw.Name };
            item.Checked = sw.Visible;
            item.CheckOnClick = true;
            item.CheckedChanged += (o, e) => { sw.Visible = ((ToolStripMenuItem)o).Checked; };
            sw.VisibleChanged += (o, e) => { item.Checked = ((SubWindow)sw).Visible; };
            _menu.DropDownItems.Add(item);
        }

    }
}
