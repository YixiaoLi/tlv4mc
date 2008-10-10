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
        /// <summary>
        /// DockStateが変わるときに発生するイベント
        /// </summary>
        public event EventHandler<SubWindowEventArgs> DockStateChanged = null;

        /// <summary>
        /// Visibleが変わるときに発生するイベント
        /// </summary>
        public event EventHandler<SubWindowEventArgs> VisibleChanged = null;

        private string _name = string.Empty;
        private DockState _dockState = DockState.Unknown;
        private bool _visible = true;

        /// <summary>
        /// サブウィンドウにFillされるControl
        /// </summary>
        public Control Control { get; private set; }
        /// <summary>
        /// ドッキング状態
        /// </summary>
        public DockState DockState
        {
            get { return _dockState; }
            set
            {
                if(_dockState != value)
                {
                    _dockState = value;

                    if (DockStateChanged != null)
                        DockStateChanged(this, new SubWindowEventArgs(this));
                }
            }
        }
        /// <summary>
        /// サブウィンドウが表示されているかどうかを示す値
        /// </summary>
        public bool Visible
        {
            get { return _visible; }
            set
            {
                if (_visible != value)
                {
                    _visible = value;

                    if (VisibleChanged != null)
                        VisibleChanged(this, new SubWindowEventArgs(this));
                }
            }
        }
        /// <summary>
        /// サブウィンドウの名前。識別子として使われる
        /// </summary>
        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }
        /// <summary>
        /// サブウィンドウのタイトルバーに表示されるテキスト
        /// </summary>
        public string Text { get; set; }

        /// <summary>
        /// SubWindowのインスタンスを生成
        /// </summary>
        /// <param name="name">サブウィンドウの名前</param>
        /// <param name="control">サブウィンドウにFillされるControl</param>
        /// <param name="dockState">サブウィンドウのドッキング状態</param>
        public SubWindow(string name, Control control, DockState dockState)
        {
            Name = name;
            Text = name;
            Control = control;
            DockState = dockState;
            Visible = true;
        }
    }

    public class SubWindowEventArgs : EventArgs
    {
        public SubWindow SubWindow { get; set; }
        public SubWindowEventArgs(SubWindow subWindow)
        {
            SubWindow = subWindow;
        }
    }
}
