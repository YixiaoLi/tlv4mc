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

        /// <summary>
        /// この<c>WindowManager</c>に関連付けられる<c>ToolStripMenuItem</c>。
        /// <newpara>
        /// サブウィンドウ追加時に、追加するサブウィンドウの<c>Name</c>, <c>Text</c>プロパティを
        /// 継承した<c>ToolStripMenuItem</c>がここで指定した<c>ToolStripMenuItem</c>に追加される。
        /// </newpara>
        /// </summary>
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
        /// <summary>
        /// この<c>WindowManager</c>を格納する<c>Control</c>
        /// </summary>
        public Control Parent
        {
            get { return _handler.Parent; }
            set { _handler.Parent = value; }
        }
        /// <summary>
        /// <c>MainPanel</c>にFillされる<c>Contorl</c>
        /// </summary>
        public Control MainPanel
        {
            get { return _handler.MainPanel; }
            set { _handler.MainPanel = value; }
        }
        /// <summary>
        /// この<c>WindowManager</c>で管理しているSubWindowを返すイテレータ
        /// </summary>
        public IEnumerable<SubWindow> SubWindows
        {
            get
            {
                return _handler.SubWindows;
            }
        }
        /// <summary>
        /// 指定した<c>SubWindow</c>を追加します
        /// </summary>
        /// <remarks><paramref name="subWindows"/>パラメータで追加する<c>SubWindow</c>を指定する。<paramref name="subWindows"/>パラメータは可変長引数である。</remarks>
        /// <param name="subWindows">追加する<c>SubWindow</c></param>
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
        /// <summary>
        /// 管理下にある<c>SubWindow</c>のすべてを表示します
        /// </summary>
        public virtual void ShowAllSubWindows()
        {
            _handler.ShowAllSubWindows();
        }
        /// <summary>
        /// 管理下にある<c>SubWindow</c>のすべてを非表示にします
        /// </summary>
        public virtual void AutoHideAllSubWindows()
        {
            _handler.AutoHideAllSubWindows();
        }
        /// <summary>
        /// 管理下にある<c>SubWindow</c>のすべてをオートハイド状態にします
        /// </summary>
        public virtual void HideAllSubWindows()
        {
            _handler.HideAllSubWindows();
        }
        /// <summary>
        /// <paramref name="name"/>で指定した<c>Name</c>プロパティをもつ<c>SubWindow</c>を表示します
        /// </summary>
        /// <param name="name">表示する<c>SubWindow</c>の<c>Name</c>プロパティの値</param>
        public virtual void ShowSubWindow(string name)
        {
            _handler.ShowSubWindow(name);
        }
        /// <summary>
        /// <paramref name="name"/>で指定した<c>Name</c>プロパティをもつ<c>SubWindow</c>を非表示にします
        /// </summary>
        /// <param name="name">非表示にする<c>SubWindow</c>の<c>Name</c>プロパティの値</param>
        public virtual void HideSubWindow(string name)
        {
            _handler.HideSubWindow(name);
        }
        /// <summary>
        /// <paramref name="name"/>で指定した<c>Name</c>プロパティをもつ<c>SubWindow</c>をオートハイド状態にします
        /// </summary>
        /// <param name="name">オートハイド状態にする<c>SubWindow</c>の<c>Name</c>プロパティの値</param>
        public virtual void AutoHideSubWindow(string name)
        {
            _handler.AutoHideSubWindow(name);
        }
        /// <summary>
        /// 管理している<c>SubWindow</c>の数
        /// </summary>
        public int SubWindowCount
        {
            get { return _handler.SubWindowCount; }
        }
        /// <summary>
        /// <paramref name="name"/>で指定した<c>Name</c>プロパティをもつ<c>SubWindow</c>を取得する
        /// </summary>
        /// <param name="name">取得したい<c>SubWindow</c>の<c>Name</c>プロパティの値</param>
        /// <returns><paramref name="name"/>で指定した値の<c>Name</c>プロパティをもつ<c>SubWindow</c></returns>
        public virtual SubWindow GetSubWindow(string name)
        {
            return _handler.GetSubWindow(name);
        }
        /// <summary>
        /// <paramref name="name"/>で指定した<c>Name</c>プロパティをもつ<c>SubWindow</c>が管理下にあるかどうか
        /// </summary>
        /// <param name="name">管理下にあるかどうか調べたい<c>SubWindow</c>の<c>Name</c>プロパティ</param>
        /// <returns>管理下にある場合true</returns>
        /// <returns>管理下にない場合false</returns>
        public virtual bool ContainSubWindow(string name)
        {
            return _handler.ContainSubWindow(name);
        }
        /// <summary>
        /// 管理する<c>SubWindow</c>のどれかの<c>DockState</c>プロパティの値が変化したときに呼び出される
        /// </summary>
        /// <param name="sender"><c>DockState</c>が変化した<c>SubWindow</c></param>
        /// <param name="e"><c>EventArgs.Empty</c></param>
        public virtual void OnSubWindowDockStateChanged(object sender, EventArgs e)
        {
        }
        /// <summary>
        /// 管理する<c>SubWindow</c>のどれかの<c>Visible</c>プロパティの値が変化したときに呼び出される
        /// </summary>
        /// <param name="sender"><c>Visible</c>が変化した<c>SubWindow</c></param>
        /// <param name="e"><c>EventArgs.Empty</c></param>
        public virtual void OnSubWindowVisibleChanged(object sender, EventArgs e)
        {
        }
        /// <summary>
        /// WindowManagerのインスタンスを生成する
        /// </summary>
        /// <param name="handler">処理を委譲する<c>IWindowManagerHandler</c>の実装クラス</param>
        public WindowManager(IWindowManagerHandler handler)
        {
            _handler = handler;
        }

        private void addMenuItem(SubWindow sw)
        {
            ToolStripMenuItem item = new ToolStripMenuItem() { Text = sw.Text, Name = sw.Name };
            item.Checked = sw.Visible;
            item.CheckOnClick = true;
            item.CheckedChanged += (o, e) => { sw.Visible = ((ToolStripMenuItem)o).Checked; };
            sw.VisibleChanged += (o, e) => { item.Checked = ((SubWindow)sw).Visible; };
            _menu.DropDownItems.Add(item);
        }

    }
}
