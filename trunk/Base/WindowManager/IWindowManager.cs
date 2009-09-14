
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
    public interface IWindowManager
    {
        event EventHandler<GeneralEventArgs<SubWindow>> SubWindowAdded;
        event EventHandler<GeneralChangedEventArgs<DockState>> SubWindowDockStateChanged;
        event EventHandler<GeneralChangedEventArgs<bool>> SubWindowVisibleChanged;
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
		void Show();

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

    public static class DockStateExtension
    {
        public static string ToText(this DockState dockState)
        {
            switch (dockState)
            {
                case DockState.DockBottom:
                    return "下";
                case DockState.DockBottomAutoHide:
                    return "下（オートハイド）";
                case DockState.DockLeft:
                    return "左";
                case DockState.DockLeftAutoHide:
                    return "左（オートハイド）";
                case DockState.DockRight:
                    return "右";
                case DockState.DockRightAutoHide:
                    return "右（オートハイド）";
                case DockState.DockTop:
                    return "上";
                case DockState.DockTopAutoHide:
                    return "上（オートハイド）";
                case DockState.Float:
                    return "フローティング";
                default:
                    return "状態不正";
            }
        }
    }
}
