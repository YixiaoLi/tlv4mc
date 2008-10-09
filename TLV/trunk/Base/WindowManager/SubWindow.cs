using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace NU.OJL.MPRTOS.TLV.Base
{
    /// <summary>
    /// WindowManagerで使用されるサブウィンドウの情報を管理するクラス
    /// </summary>
    public class SubWindow
    {
        public event EventHandler DockStateChanged = null;
        public event EventHandler VisibleChanged = null;

        private string _name = string.Empty;
        private DockState _dockState = DockState.Unknown;
        private bool _visible = true;

        public Control Control { get; private set; }
        public DockState DockState
        {
            get { return _dockState; }
            set
            {
                if(_dockState != value)
                {
                    _dockState = value;

                    if (DockStateChanged != null)
                        DockStateChanged(this, EventArgs.Empty);
                }
            }
        }
        public bool Visible
        {
            get { return _visible; }
            set
            {
                if (_visible != value)
                {
                    _visible = value;

                    if (VisibleChanged != null)
                        VisibleChanged(this, EventArgs.Empty);
                }
            }
        }
        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }
        public string Text { get; set; }

        public SubWindow(string name, Control control, DockState dockState)
        {
            Name = name;
            Text = name;
            Control = control;
            DockState = dockState;
            Visible = true;
        }
    }
}
