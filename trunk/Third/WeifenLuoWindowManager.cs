/*
 *  TLV - Trace Log Visualizer
 *
 *  Copyright (C) 2008-2013 by Nagoya Univ., JAPAN
 *
 *  上記著作権者は，以下の(1)〜(4)の条件を満たす場合に限り，本ソフトウェ
 *  ア（本ソフトウェアを改変したものを含む．以下同じ）を使用・複製・改
 *  変・再配布（以下，利用と呼ぶ）することを無償で許諾する．
 *  (1) 本ソフトウェアをソースコードの形で利用する場合には，上記の著作
 *      権表示，この利用条件および下記の無保証規定が，そのままの形でソー
 *      スコード中に含まれていること．
 *  (2) 本ソフトウェアを，ライブラリ形式など，他のソフトウェア開発に使
 *      用できる形で再配布する場合には，再配布に伴うドキュメント（利用
 *      者マニュアルなど）に，上記の著作権表示，この利用条件および下記
 *      の無保証規定を掲載すること．
 *  (3) 本ソフトウェアを，機器に組み込むなど，他のソフトウェア開発に使
 *      用できない形で再配布する場合には，次のいずれかの条件を満たすこ
 *      と．
 *    (a) 再配布に伴うドキュメント（利用者マニュアルなど）に，上記の著
 *        作権表示，この利用条件および下記の無保証規定を掲載すること．
 *    (b) 再配布の形態を，別に定める方法によって，TOPPERSプロジェクトに
 *        報告すること．
 *  (4) 本ソフトウェアの利用により直接的または間接的に生じるいかなる損
 *      害からも，上記著作権者およびTOPPERSプロジェクトを免責すること．
 *      また，本ソフトウェアのユーザまたはエンドユーザからのいかなる理
 *      由に基づく請求からも，上記著作権者およびTOPPERSプロジェクトを
 *      免責すること．
 *
 *  本ソフトウェアは，無保証で提供されているものである．上記著作権者お
 *  よびTOPPERSプロジェクトは，本ソフトウェアに関して，特定の使用目的
 *  に対する適合性も含めて，いかなる保証も行わない．また，本ソフトウェ
 *  アの利用により直接的または間接的に生じたいかなる損害に関しても，そ
 *  の責任を負わない．
 *
 *  @(#) $Id$
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.Diagnostics;
using NU.OJL.MPRTOS.TLV.Base;
using WeifenLuo.WinFormsUI.Docking;
using System.IO;

namespace NU.OJL.MPRTOS.TLV.Third
{
    /// <summary>
    /// DockPanel Suiteのドッキングウィンドウを
    /// IWindowManagerで使うためのハンドラ
    /// WeifenLuo.WinFormsUI.Docking.dllを必要とする
    /// </summary>
    public class WeifenLuoWindowManager : WindowManager
    {
		private readonly string path = Path.Combine(Application.StartupPath, "windowManager.setting");
        private Dictionary<string, DockContent> _dockContents = new Dictionary<string, DockContent>();
        private DockPanel _dockPanel = null;
		private DockPanel _settingDockPanel;
		private DockContent _mainContent = new DockContent();

        public override Control Parent
        {
            get { return base.Parent; }
            set
            {
                base.Parent = value;
            }
        }

        public override Control MainPanel
        {
            get { return base.MainPanel; }
            set
            {
				base.MainPanel = value;
				_mainContent.Controls.Add(base.MainPanel);
				_mainContent.DockPanel = _dockPanel;
				_mainContent.Name = "___mainContent";
				_mainContent.DockState = WeifenLuo.WinFormsUI.Docking.DockState.Document;
				_mainContent.Pane.BorderStyle = BorderStyle.None;
            }
        }

        public override void AddSubWindow(params SubWindow[] subWindows)
        {
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
					DockContent docc = ((DockContent)o);
					SubWindow subwin = GetSubWindow(docc.Name);
					if (docc.DockState == WeifenLuo.WinFormsUI.Docking.DockState.Hidden)
					{
						subwin.Visible = false;
						if (docc.Pane != null)
							subwin.DockState = docc.Pane.DockState.Generalize();
					}
					else
					{
						subwin.Visible = true;
						subwin.DockState = docc.DockState.Generalize();
					}
				};
                _dockContents.Add(sw.Name, dc);

            }

            base.AddSubWindow(subWindows);

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

        protected override void OnSubWindowDockStateChanged(object o, GeneralChangedEventArgs<NU.OJL.MPRTOS.TLV.Base.DockState> e)
        {
            base.OnSubWindowDockStateChanged(o, e);
            _dockContents[((SubWindow)o).Name].DockState = ((SubWindow)o).DockState.Specialize();
        }

        protected override void OnSubWindowVisibleChanged(object o, GeneralChangedEventArgs<bool> e)
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

        public WeifenLuoWindowManager()
        {
            _dockPanel = new DockPanel();
			_dockPanel.DocumentStyle = DocumentStyle.DockingSdi;
			_dockPanel.Dock = DockStyle.Fill;
		}

		public override void Save()
		{
			base.Save();
			_dockPanel.SaveAsXml(path);
		}

		public override void Load()
		{
			base.Load();

			if (File.Exists(path) && _dockContents.Count != 0)
			{
				_settingDockPanel = new DockPanel();
				_settingDockPanel.DocumentStyle = DocumentStyle.DockingSdi;
				_settingDockPanel.Dock = DockStyle.Fill;
				_settingDockPanel.LoadFromXml(path, (s) =>
					{
						foreach (DockContent dc in _dockContents.Values)
						{
							if (s == dc.Name + ":" + dc.GetType().ToString())
							{
								dc.DockPanel = _settingDockPanel;
								return dc;
							}
						}
						if (s == _mainContent.Name + ":" + _mainContent.GetType().ToString())
							return _mainContent;
						else
							return null;
					});

				_dockPanel = _settingDockPanel;
				foreach(DockContent dc in _dockContents.Values)
				{
					if (dc.DockState == WeifenLuo.WinFormsUI.Docking.DockState.Hidden)
					{
						GetSubWindow(dc.Name).DockState = dc.Pane.DockState.Generalize();
						GetSubWindow(dc.Name).Visible = false;
					}
				}
			}
		}

		public override void Show()
		{
			base.Show();
			Parent.Controls.Add(_dockPanel);
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
