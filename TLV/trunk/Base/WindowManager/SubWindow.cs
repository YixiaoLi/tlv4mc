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
        /// DockStateが変ったときに発生するイベント
        /// </summary>
        public event EventHandler<GeneralChangedEventArgs<DockState>> DockStateChanged = null;
        /// <summary>
        /// Visibleが変ったときに発生するイベント
        /// </summary>
        public event EventHandler<GeneralChangedEventArgs<bool>> VisibleChanged = null;
        /// <summary>
        /// Enabledが変ったときに発生するイベント
        /// </summary>
        public event EventHandler<GeneralChangedEventArgs<bool>> EnabledChanged = null;

        private string _name = string.Empty;
        private DockState _dockState = DockState.Unknown;
        private bool _visible = true;
        private bool _enabled = true;
        
        /// <summary>
        /// サブウィンドウにFillされるControl
        /// </summary>
        public Control Control{ get; private set; }
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
                    DockState old = _dockState;

                    _dockState = value;

                    if (DockStateChanged != null)
                        DockStateChanged(this, new GeneralChangedEventArgs<DockState>(old, _dockState));
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
                    bool old = _visible;

                    _visible = value;

                    if (VisibleChanged != null)
                        VisibleChanged(this, new GeneralChangedEventArgs<bool>(old, _visible));
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
        /// サブウィンドウが有効かどうか
        /// </summary>
        public bool Enabled
        {
            get { return _enabled; }
            set
            {
                if(_enabled != value)
                {
                    bool old = _enabled;

                    _enabled = value;

                    if (EnabledChanged != null)
                        EnabledChanged(this, new GeneralChangedEventArgs<bool>(old, _enabled));

                    if (!_enabled && Visible)
                    {
                        Visible = false;
                    }
                }
            }
        }

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
            Control.Dock = DockStyle.Fill;
            DockState = dockState;
            Visible = true;
        }

    }

}
