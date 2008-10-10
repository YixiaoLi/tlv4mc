using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace NU.OJL.MPRTOS.TLV.Base
{
    /// <summary>
    /// IWindowManagerのハンドラ
    /// IWindowManagerの実装クラスから処理を委譲されるクラスがもつべきインターフェイス
    /// サブウィンドウの管理に必須な機能を定義する
    /// </summary>
    public interface IWindowManagerHandler
    {
        event EventHandler<SubWindowEventArgs> SubWindowAdded;
        Control Parent { get; set; }
        Control MainPanel { get; set; }
        int SubWindowCount { get; }
        IEnumerable<SubWindow> SubWindows { get; }
        SubWindow GetSubWindow(string name);
        bool ContainSubWindow(string name);
        void AddSubWindow(params SubWindow[] subWindows);
        void ShowAllSubWindows();
        void AutoHideAllSubWindows();
        void HideAllSubWindows();
        void ShowSubWindow(string name);
        void HideSubWindow(string name);
        void AutoHideSubWindow(string name);
        void OnSubWindowDockStateChanged(object sender, SubWindowEventArgs e);
        void OnSubWindowVisibleChanged(object sender, SubWindowEventArgs e);
    }

    public enum DockState
    {
        Unknown,
        Float,
        DockTopAutoHide,
        DockLeftAutoHide,
        DockBottomAutoHide,
        DockRightAutoHide,
        DockTop,
        DockLeft,
        DockBottom,
        DockRight
    }
}
