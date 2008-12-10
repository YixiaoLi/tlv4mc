﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Windows.Forms;

namespace NU.OJL.MPRTOS.TLV.Base
{
    /// <summary>
    /// ドッキング可能なサブウィンドウを管理するクラス
    /// </summary>
    public class WindowManager : IWindowManager
    {
        private SubWindowCollection _subWindows = new SubWindowCollection();
        private Control _mainPanel = null;

        /// <summary>
        /// <c>SubWindow</c>を追加したときに発生するイベント
        /// </summary>
        public event EventHandler<GeneralEventArgs<SubWindow>> SubWindowAdded = null;
        /// <summary>
        /// 管理下のSubWindowのDockStateが変ったときに発生するイベント
        /// </summary>
        public event EventHandler<GeneralChangedEventArgs<DockState>> SubWindowDockStateChanged = null;
        /// <summary>
        /// 管理下のSubWindowのVisibleが変ったときに発生するイベント
        /// </summary>
        public event EventHandler<GeneralChangedEventArgs<bool>> SubWindowVisibleChanged = null;

        /// <summary>
        /// この<c>WindowManager</c>を格納する<c>Control</c>
        /// </summary>
        public virtual Control Parent { get; set; }
        /// <summary>
        /// <c>MainPanel</c>にFillされる<c>Contorl</c>
        /// </summary>
        public virtual Control MainPanel
        {
            get { return _mainPanel; }
            set
            {
                _mainPanel = value;
                _mainPanel.Dock = DockStyle.Fill;
            }
        }
        /// <summary>
        /// この<c>WindowManager</c>で管理しているSubWindowを返すイテレータ
        /// </summary>
        public IEnumerable<SubWindow> SubWindows
        {
            get
            {
                foreach (SubWindow sw in _subWindows)
                {
                    yield return sw;
                }
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
                _subWindows.Add(sw);

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

                if (SubWindowAdded != null)
                    SubWindowAdded(this, new GeneralEventArgs<SubWindow>(sw));

            }
        }
        /// <summary>
        /// 管理下にある<c>SubWindow</c>のすべてを表示します
        /// </summary>
        public virtual void ShowAllSubWindows()
        {
            foreach (SubWindow sw in _subWindows)
            {
                ShowSubWindow(sw.Name);
            }
        }
        /// <summary>
        /// 管理下にある<c>SubWindow</c>のすべてを非表示にします
        /// </summary>
        public virtual void AutoHideAllSubWindows()
        {
            foreach (SubWindow sw in _subWindows)
            {
                AutoHideSubWindow(sw.Name);
            }
        }
        /// <summary>
        /// 管理下にある<c>SubWindow</c>のすべてをオートハイド状態にします
        /// </summary>
        public virtual void HideAllSubWindows()
        {
            foreach (SubWindow sw in _subWindows)
            {
                HideSubWindow(sw.Name);
            }
        }
        /// <summary>
        /// <paramref name="name"/>で指定した<c>Name</c>プロパティをもつ<c>SubWindow</c>を表示します
        /// </summary>
        /// <param name="name">表示する<c>SubWindow</c>の<c>Name</c>プロパティの値</param>
        public virtual void ShowSubWindow(string name)
        {
            _subWindows[name].Visible = true;
        }
        /// <summary>
        /// <paramref name="name"/>で指定した<c>Name</c>プロパティをもつ<c>SubWindow</c>を非表示にします
        /// </summary>
        /// <param name="name">非表示にする<c>SubWindow</c>の<c>Name</c>プロパティの値</param>
        public virtual void HideSubWindow(string name)
        {
            _subWindows[name].Visible = false;
        }
        /// <summary>
        /// <paramref name="name"/>で指定した<c>Name</c>プロパティをもつ<c>SubWindow</c>をオートハイド状態にします
        /// </summary>
        /// <param name="name">オートハイド状態にする<c>SubWindow</c>の<c>Name</c>プロパティの値</param>
        public virtual void AutoHideSubWindow(string name)
        {
            switch (_subWindows[name].DockState)
            {
                case DockState.DockBottom:
                    _subWindows[name].DockState = DockState.DockBottomAutoHide;
                    break;
                case DockState.DockLeft:
                    _subWindows[name].DockState = DockState.DockLeftAutoHide;
                    break;
                case DockState.DockRight:
                    _subWindows[name].DockState = DockState.DockRightAutoHide;
                    break;
                case DockState.DockTop:
                    _subWindows[name].DockState = DockState.DockTopAutoHide;
                    break;
                default:
                    break;
            }
        }
        /// <summary>
        /// 管理している<c>SubWindow</c>の数
        /// </summary>
        public int SubWindowCount
        {
            get { return _subWindows.Count; }
        }
        /// <summary>
        /// <paramref name="name"/>で指定した<c>Name</c>プロパティをもつ<c>SubWindow</c>を取得する
        /// </summary>
        /// <param name="name">取得したい<c>SubWindow</c>の<c>Name</c>プロパティの値</param>
        /// <returns><paramref name="name"/>で指定した値の<c>Name</c>プロパティをもつ<c>SubWindow</c></returns>
        public SubWindow GetSubWindow(string name)
        {
            return _subWindows[name];
        }
        /// <summary>
        /// <paramref name="name"/>で指定した<c>Name</c>プロパティをもつ<c>SubWindow</c>が管理下にあるかどうか
        /// </summary>
        /// <param name="name">管理下にあるかどうか調べたい<c>SubWindow</c>の<c>Name</c>プロパティ</param>
        /// <returns>管理下にある場合true</returns>
        /// <returns>管理下にない場合false</returns>
        public bool ContainSubWindow(string name)
        {
            return _subWindows.Contains(name);
        }
        /// <summary>
        /// 管理する<c>SubWindow</c>のどれかの<c>DockState</c>プロパティの値が変化したときに呼び出される
        /// </summary>
        /// <param name="sender"><c>DockState</c>が変化した<c>SubWindow</c></param>
        /// <param name="e"><c>EventArgs.Empty</c></param>
        protected virtual void OnSubWindowDockStateChanged(object sender, GeneralChangedEventArgs<DockState> e)
        {
            if (SubWindowDockStateChanged != null)
                SubWindowDockStateChanged(sender, e);
        }
        /// <summary>
        /// 管理する<c>SubWindow</c>のどれかの<c>Visible</c>プロパティの値が変化したときに呼び出される
        /// </summary>
        /// <param name="sender"><c>Visible</c>が変化した<c>SubWindow</c></param>
        /// <param name="e"><c>EventArgs.Empty</c></param>
        protected virtual void OnSubWindowVisibleChanged(object sender, GeneralChangedEventArgs<bool> e)
        {
            if (SubWindowVisibleChanged != null)
                SubWindowVisibleChanged(sender, e);
        }
    }
}
