using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Drawing;
using System.Text;

namespace NU.OJL.MPRTOS.TLV.Base
{
    /// <summary>
    /// IWindowManagerHandlerの抽象実装クラス
    /// これをベースにThirdパーティ製のドッキングウィンドウを実装する
    /// </summary>
    public abstract class AbstractWindowManagerHandler : IWindowManagerHandler
    {
        private SubWindowCollection _subWindows = new SubWindowCollection();
        private Control _mainPanel = null;

        public virtual Control Parent { get; set; }

        public virtual Control MainPanel
        {
            get { return _mainPanel; }
            set
            {
                _mainPanel = value;
                _mainPanel.Dock = DockStyle.Fill;
            }
        }

        public IEnumerable<SubWindow> SubWindows
        {
            get
            {
                foreach(SubWindow sw in _subWindows)
                {
                    yield return sw;
                }
            }
        }

        public virtual void AddSubWindow(params SubWindow[] subWindows)
        {
            foreach (SubWindow sw in subWindows)
            {
                _subWindows.Add(sw);

                sw.Control.Dock = DockStyle.Fill;
                sw.DockStateChanged += OnSubWindowDockStateChanged;
                sw.VisibleChanged += OnSubWindowVisibleChanged;
            }
        }

        public virtual void ShowAllSubWindows()
        {
            foreach (SubWindow sw in _subWindows)
            {
                ShowSubWindow(sw.Name);
            }
        }

        public virtual void AutoHideAllSubWindows()
        {
            foreach (SubWindow sw in _subWindows)
            {
                AutoHideSubWindow(sw.Name);
            }
        }

        public virtual void HideAllSubWindows()
        {
            foreach (SubWindow sw in _subWindows)
            {
                HideSubWindow(sw.Name);
            }
        }

        public virtual void ShowSubWindow(string name)
        {
            _subWindows[name].Visible = true;
        }

        public virtual void HideSubWindow(string name)
        {
            _subWindows[name].Visible = false;
        }

        public virtual void AutoHideSubWindow(string name)
        {

        }

        public int SubWindowCount
        {
            get { return _subWindows.Count; }
        }

        public SubWindow GetSubWindow(string name)
        {
            return _subWindows[name];
        }

        public virtual void OnSubWindowDockStateChanged(object sender, EventArgs e)
        {
        }

        public virtual void OnSubWindowVisibleChanged(object sender, EventArgs e)
        {
        }

        public bool ContainSubWindow(string name)
        {
            return _subWindows.Contains(name);
        }

    }
}
