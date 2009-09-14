/*
 *  TLV - Trace Log Visualizer
 *
 *  Copyright (C) 2008,2009 by Nagoya Univ., JAPAN
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
using System.Windows.Forms;
using System.Text;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Reflection;

namespace NU.OJL.MPRTOS.TLV.Base
{

	public class NativeScrollBarHelper : Control
	{
		private NativeScrollBar _nsbV;
		private NativeScrollBar _nsbH;
		private Control _control;
		private ScrollBar[] _scrollBars;

		private const int WM_KEYDOWN = 0x100;
		private const int WM_HSCROLL = 0x0114;
		private const int WM_VSCROLL = 0x0115;
		private const int WM_MOUSEWHEEL = 0x20A;
		private const int SB_LINEUP = 0;
		private const int SB_LINEDOWN = 1;
		private const int SB_LINELEFT = 0;
		private const int SB_LINERIGHT = 1;
		private const int SB_PAGELEFT = 2;
		private const int SB_PAGERIGHT = 3;
		private const int SB_PAGEUP = 2;
		private const int SB_PAGEDOWN = 3;

		public NativeScrollBarHelper(Control control, params ScrollBar[] scrollBars)
		{
			_control = control;
			_scrollBars = scrollBars;
			_nsbV = new NativeScrollBar(this, ScrollBarType.Vertical);
			_nsbH = new NativeScrollBar(this, ScrollBarType.Horizontal);
			_control.Controls.Add(this);

		}

		protected override void OnHandleDestroyed(EventArgs e)
		{
			if (_nsbV != null)
			{
				_nsbV.DestroyHandle();
			}
			if (_nsbH != null)
			{
				_nsbH.DestroyHandle();
			}
		}

		protected override void WndProc(ref Message m)
		{

			if (_scrollBars == null)
			{
				switch (m.Msg)
				{
					case WM_VSCROLL:
					case WM_HSCROLL:
						int sb = m.WParam.ToInt32();

						int d = 0;

						if (sb == SB_LINEDOWN || sb == SB_PAGEDOWN)
						{
							d = -120;
							//SendMessage(_control.Handle.ToInt32(), WM_MOUSEWHEEL, 0x78, 0);
						}
						else if (sb == SB_LINEUP || sb == SB_PAGEUP)
						{
							d = 120;
							//SendMessage(_control.Handle.ToInt32(), WM_MOUSEWHEEL, 0xffff88, 0);
						}

						MethodInfo method = _control.GetType().GetMethod("OnMouseWheel", BindingFlags.Instance | BindingFlags.NonPublic);
						ExMouseEventArgs e = new ExMouseEventArgs(MouseButtons.None, 0, 0, 0, d);
						method.Invoke(_control, new object[] { e });

						if (e.Handled)
							return;

						break;
				}
			}
			else
			{
				switch (m.Msg)
				{
					case WM_VSCROLL:
						foreach (ScrollBar vScrollBar in _scrollBars.Where(s => s.GetType() == typeof(VScrollBar)))
						{
							if (vScrollBar != null && m.LParam != vScrollBar.Handle)
							{
								int sb = m.WParam.ToInt32();

								int d = 0;

								if (sb == SB_LINEDOWN || sb == SB_PAGEDOWN)
								{
									d = -120;
									//SendMessage(_control.Handle.ToInt32(), WM_MOUSEWHEEL, 0x78, 0);
								}
								else if (sb == SB_LINEUP || sb == SB_PAGEUP)
								{
									d = 120;
									//SendMessage(_control.Handle.ToInt32(), WM_MOUSEWHEEL, 0xffff88, 0);
								}

								MethodInfo method = _control.GetType().GetMethod("OnMouseWheel", BindingFlags.Instance | BindingFlags.NonPublic);
								ExMouseEventArgs e = new ExMouseEventArgs(MouseButtons.None, 0, 0, 0, d);
								method.Invoke(_control, new object[] { e });

								if (e.Handled)
								    return;

								if (vScrollBar.Value + vScrollBar.SmallChange >= vScrollBar.Maximum && sb == SB_LINEDOWN)
								{
									vScrollBar.Value = vScrollBar.Maximum - vScrollBar.LargeChange + 1;
								}
								else if (vScrollBar.Value + vScrollBar.LargeChange >= vScrollBar.Maximum && sb == SB_PAGEDOWN)
								{
									vScrollBar.Value = vScrollBar.Maximum - vScrollBar.LargeChange + 1;
								}
								else if (vScrollBar.Value - vScrollBar.SmallChange <= vScrollBar.Minimum && sb == SB_LINEUP)
								{
									vScrollBar.Value = vScrollBar.Minimum;
								}
								else if (vScrollBar.Value - vScrollBar.LargeChange <= vScrollBar.Minimum && sb == SB_PAGEUP)
								{
									vScrollBar.Value = vScrollBar.Minimum;
								}
								else if (!(vScrollBar.Value == vScrollBar.Minimum && (sb == SB_LINEUP || sb == SB_PAGEUP))
									&& !(vScrollBar.Value == vScrollBar.Maximum - vScrollBar.LargeChange + 1 && (sb == SB_LINEDOWN || sb == SB_PAGEDOWN)))
								{
									Control.ReflectMessage(vScrollBar.Handle, ref m);
								}
							}
						}
						break;
					case WM_HSCROLL:
						foreach (ScrollBar hScrollBar in _scrollBars.Where(s => s.GetType() == typeof(HScrollBar)))
						{
							if (hScrollBar != null && m.LParam != hScrollBar.Handle)
							{
								int sb = m.WParam.ToInt32();

								if (hScrollBar.Value + hScrollBar.SmallChange >= hScrollBar.Maximum && sb == SB_LINERIGHT)
								{
									hScrollBar.Value = hScrollBar.Maximum - hScrollBar.LargeChange + 1;
								}
								else if (hScrollBar.Value + hScrollBar.LargeChange >= hScrollBar.Maximum && sb == SB_PAGERIGHT)
								{
									hScrollBar.Value = hScrollBar.Maximum - hScrollBar.LargeChange + 1;
								}
								else if (hScrollBar.Value - hScrollBar.SmallChange <= hScrollBar.Minimum && sb == SB_LINELEFT)
								{
									hScrollBar.Value = hScrollBar.Minimum;
								}
								else if (hScrollBar.Value - hScrollBar.LargeChange <= hScrollBar.Minimum && sb == SB_PAGELEFT)
								{
									hScrollBar.Value = hScrollBar.Minimum;
								}
								else if (!(hScrollBar.Value == hScrollBar.Minimum && (sb == SB_PAGELEFT || sb == SB_LINELEFT))
									&& !(hScrollBar.Value == hScrollBar.Maximum - hScrollBar.LargeChange + 1 && (sb == SB_PAGERIGHT || sb == SB_LINERIGHT)))
								{
									Control.ReflectMessage(hScrollBar.Handle, ref m);
								}
							}
						}
						break;
				}
			}

			Control.ReflectMessage(_control.Handle, ref m);

			//SendMessage(_control.Handle.ToInt32(), (uint)m.Msg, m.WParam.ToInt64(), m.LParam.ToInt64());
			
			base.WndProc(ref m);
		}

		[DllImport("user32.dll")]
		public static extern int SendMessage(
			  int hWnd,
			  uint Msg,
			  long wParam,
			  long lParam
			  );
	}

	public class ExMouseEventArgs : MouseEventArgs
	{
		public bool Handled { get; set; }

		public ExMouseEventArgs(MouseButtons mb, int click, int x, int y, int delta):base(mb, click, x, y, delta)  { }
	}
}
