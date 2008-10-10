using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.Diagnostics;
using NU.OJL.MPRTOS.TLV.Base;
using WeifenLuo.WinFormsUI.Docking;

namespace NU.OJL.MPRTOS.TLV.Third
{
    /// <summary>
    /// DockPanel Suiteのドッキングウィンドウを
    /// IWindowManagerで使うためのハンドラ
    /// WeifenLuo.WinFormsUI.Docking.dllを必要とする
    /// </summary>
    public class WeifenLuoWindowManagerHandler : AbstractWindowManagerHandler
    {
        Dictionary<string, DockContent> _dockContents = new Dictionary<string, DockContent>();
        private DockPanel _dockPanel = null;

        public override Control Parent
        {
            get { return base.Parent; }
            set
            {
                base.Parent = value;
                base.Parent.Controls.Add(_dockPanel);
                _dockPanel.Dock = DockStyle.Fill;
            }
        }

        public override Control MainPanel
        {
            get { return base.MainPanel; }
            set
            {
                base.MainPanel = value;
                DockContent dc = new DockContent();
                dc.Controls.Add(base.MainPanel);
                dc.DockPanel = _dockPanel;
                dc.DockState = WeifenLuo.WinFormsUI.Docking.DockState.Document;
            }
        }

        public override void AddSubWindow(params SubWindow[] subWindows)
        {
            base.AddSubWindow(subWindows);

            foreach(SubWindow sw in subWindows)
            {
                DockContent dc = new DockContent();
                dc.Controls.Add(sw.Control);
                dc.DockPanel = _dockPanel;
                dc.Name = sw.Name;
                dc.Text = sw.Text;
                dc.DockAreas = DockAreas.DockBottom | DockAreas.DockLeft | DockAreas.DockRight | DockAreas.DockTop | DockAreas.Float;
                dc.DockState = sw.DockState.Specialize();
                dc.HideOnClose = true;
                dc.DockStateChanged += (o, e) =>
                {
                    SubWindow s = this.GetSubWindow(((DockContent)o).Name);
                    if (((DockContent)o).DockState == WeifenLuo.WinFormsUI.Docking.DockState.Hidden)
                    {
                        s.Visible = false;
                    }
                    else
                    {
                        s.DockState = ((DockContent)o).DockState.Generalize();
                    }
                };

                _dockContents.Add(sw.Name, dc);
            }
        }

        public override void ShowSubWindow(string name)
        {
            base.ShowSubWindow(name);
            _dockContents[name].Show();
        }

        public override void HideSubWindow(string name)
        {
            base.HideSubWindow(name);
            _dockContents[name].DockState = WeifenLuo.WinFormsUI.Docking.DockState.Hidden;
        }

        public override void AutoHideSubWindow(string name)
        {
            base.AutoHideSubWindow(name);

            switch (_dockContents[name].DockState)
            {
                case WeifenLuo.WinFormsUI.Docking.DockState.DockBottom:
                    _dockContents[name].DockState = WeifenLuo.WinFormsUI.Docking.DockState.DockBottomAutoHide;
                    break;
                case WeifenLuo.WinFormsUI.Docking.DockState.DockLeft:
                    _dockContents[name].DockState = WeifenLuo.WinFormsUI.Docking.DockState.DockLeftAutoHide;
                    break;
                case WeifenLuo.WinFormsUI.Docking.DockState.DockRight:
                    _dockContents[name].DockState = WeifenLuo.WinFormsUI.Docking.DockState.DockRightAutoHide;
                    break;
                case WeifenLuo.WinFormsUI.Docking.DockState.DockTop:
                    _dockContents[name].DockState = WeifenLuo.WinFormsUI.Docking.DockState.DockTopAutoHide;
                    break;
                default:
                    break;
            }
        }

        public override void OnSubWindowDockStateChanged(object o, SubWindowEventArgs e)
        {
            base.OnSubWindowDockStateChanged(o, e);
            _dockContents[((SubWindow)o).Name].DockState = ((SubWindow)o).DockState.Specialize();
        }

        public override void OnSubWindowVisibleChanged(object o, SubWindowEventArgs e)
        {
            base.OnSubWindowVisibleChanged(o, e);
            if (((SubWindow)o).Visible)
            {
                ShowSubWindow(((SubWindow)o).Name);
            }
            else
            {
                HideSubWindow(((SubWindow)o).Name);
            }
        }

        public WeifenLuoWindowManagerHandler()
        {
            _dockPanel = new DockPanel();
            _dockPanel.DocumentStyle = DocumentStyle.DockingSdi;
        }

    }

    static class DockStateExtensions
    {
        public static WeifenLuo.WinFormsUI.Docking.DockState Specialize(this NU.OJL.MPRTOS.TLV.Base.DockState dockState)
        {
            switch (dockState)
            {
                case NU.OJL.MPRTOS.TLV.Base.DockState.DockBottom:
                    return WeifenLuo.WinFormsUI.Docking.DockState.DockBottom;
                case NU.OJL.MPRTOS.TLV.Base.DockState.DockBottomAutoHide:
                    return WeifenLuo.WinFormsUI.Docking.DockState.DockBottomAutoHide;
                case NU.OJL.MPRTOS.TLV.Base.DockState.DockLeft:
                    return WeifenLuo.WinFormsUI.Docking.DockState.DockLeft;
                case NU.OJL.MPRTOS.TLV.Base.DockState.DockLeftAutoHide:
                    return WeifenLuo.WinFormsUI.Docking.DockState.DockLeftAutoHide;
                case NU.OJL.MPRTOS.TLV.Base.DockState.DockRight:
                    return WeifenLuo.WinFormsUI.Docking.DockState.DockRight;
                case NU.OJL.MPRTOS.TLV.Base.DockState.DockRightAutoHide:
                    return WeifenLuo.WinFormsUI.Docking.DockState.DockRightAutoHide;
                case NU.OJL.MPRTOS.TLV.Base.DockState.DockTop:
                    return WeifenLuo.WinFormsUI.Docking.DockState.DockTop;
                case NU.OJL.MPRTOS.TLV.Base.DockState.DockTopAutoHide:
                    return WeifenLuo.WinFormsUI.Docking.DockState.DockTopAutoHide;
                case NU.OJL.MPRTOS.TLV.Base.DockState.Float:
                    return WeifenLuo.WinFormsUI.Docking.DockState.Float;
                default:
                    return WeifenLuo.WinFormsUI.Docking.DockState.Unknown;
            }
        }

        public static NU.OJL.MPRTOS.TLV.Base.DockState Generalize(this WeifenLuo.WinFormsUI.Docking.DockState dockState)
        {
            switch (dockState)
            {
                case WeifenLuo.WinFormsUI.Docking.DockState.DockBottom:
                    return NU.OJL.MPRTOS.TLV.Base.DockState.DockBottom;
                case WeifenLuo.WinFormsUI.Docking.DockState.DockBottomAutoHide:
                    return NU.OJL.MPRTOS.TLV.Base.DockState.DockBottomAutoHide;
                case WeifenLuo.WinFormsUI.Docking.DockState.DockLeft:
                    return NU.OJL.MPRTOS.TLV.Base.DockState.DockLeft;
                case WeifenLuo.WinFormsUI.Docking.DockState.DockLeftAutoHide:
                    return NU.OJL.MPRTOS.TLV.Base.DockState.DockLeftAutoHide;
                case WeifenLuo.WinFormsUI.Docking.DockState.DockRight:
                    return NU.OJL.MPRTOS.TLV.Base.DockState.DockRight;
                case WeifenLuo.WinFormsUI.Docking.DockState.DockRightAutoHide:
                    return NU.OJL.MPRTOS.TLV.Base.DockState.DockRightAutoHide;
                case WeifenLuo.WinFormsUI.Docking.DockState.DockTop:
                    return NU.OJL.MPRTOS.TLV.Base.DockState.DockTop;
                case WeifenLuo.WinFormsUI.Docking.DockState.DockTopAutoHide:
                    return NU.OJL.MPRTOS.TLV.Base.DockState.DockTopAutoHide;
                case WeifenLuo.WinFormsUI.Docking.DockState.Float:
                    return NU.OJL.MPRTOS.TLV.Base.DockState.Float;
                default:
                    return NU.OJL.MPRTOS.TLV.Base.DockState.Unknown;
            }
        }
    }

}
