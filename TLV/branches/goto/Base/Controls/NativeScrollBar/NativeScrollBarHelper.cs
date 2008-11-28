﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Text;
using System.ComponentModel;
using System.Runtime.InteropServices;

namespace NU.OJL.MPRTOS.TLV.Base
{

	public class NativeScrollBarHelper : Control
	{
		private NativeScrollBar _nsb;
		private Control _control;
		private ScrollBar[] _scrollBars;

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

		public NativeScrollBarHelper(Control control, ScrollBar[] scrollBars)
		{
			_control = control;
			_scrollBars = scrollBars;
			_nsb = new NativeScrollBar(this);
			_control.Controls.Add(this);

		}

		protected override void OnHandleDestroyed(EventArgs e)
		{
			if (_nsb != null)
			{
				_nsb.DestroyHandle();
			}
		}

		protected override void WndProc(ref Message m)
		{
			switch (m.Msg)
			{
				case WM_VSCROLL:
					foreach (ScrollBar vScrollBar in _scrollBars.Where(s => s.GetType() == typeof(VScrollBar)))
					{
						if (vScrollBar != null && m.LParam != vScrollBar.Handle && vScrollBar.Visible)
						{
							int sb = m.WParam.ToInt32();

							if (sb == SB_LINEDOWN)
							{
								SendMessage(_control.Handle.ToInt32(), WM_MOUSEWHEEL, 0x78, 0);
							}
							else
							{
								SendMessage(_control.Handle.ToInt32(), WM_MOUSEWHEEL, 0xffff88, 0);
							}

							if (vScrollBar.Value + vScrollBar.SmallChange > vScrollBar.Maximum && sb == SB_LINEDOWN)
							{
								vScrollBar.Value = vScrollBar.Maximum - vScrollBar.LargeChange;
							}
							else if (vScrollBar.Value + vScrollBar.LargeChange > vScrollBar.Maximum && sb == SB_PAGEDOWN)
							{
								vScrollBar.Value = vScrollBar.Maximum - vScrollBar.LargeChange;
							}
							else if (vScrollBar.Value - vScrollBar.SmallChange < vScrollBar.Minimum && sb == SB_LINEUP)
							{
								vScrollBar.Value = vScrollBar.Minimum;
							}
							else if (vScrollBar.Value - vScrollBar.LargeChange < vScrollBar.Minimum && sb == SB_PAGEUP)
							{
								vScrollBar.Value = vScrollBar.Minimum;
							}
							else if (!(vScrollBar.Value == vScrollBar.Minimum && (sb == SB_LINEUP || sb == SB_PAGEUP))
								&& !(vScrollBar.Value >= vScrollBar.Maximum - vScrollBar.LargeChange && (sb == SB_LINEDOWN || sb == SB_PAGEDOWN)))
							{
								Control.ReflectMessage(vScrollBar.Handle, ref m);
							}
						}
					}
					break;
				case WM_HSCROLL:
					foreach (ScrollBar hScrollBar in _scrollBars.Where(s => s.GetType() == typeof(HScrollBar)))
					{
						if (hScrollBar != null && m.LParam != hScrollBar.Handle && hScrollBar.Visible)
						{
							int sb = m.WParam.ToInt32();

							if (hScrollBar.Value + hScrollBar.SmallChange > hScrollBar.Maximum && sb == SB_LINERIGHT)
							{
								hScrollBar.Value = hScrollBar.Maximum - hScrollBar.LargeChange;
							}
							else if (hScrollBar.Value + hScrollBar.LargeChange > hScrollBar.Maximum && sb == SB_PAGERIGHT)
							{
								hScrollBar.Value = hScrollBar.Maximum - hScrollBar.LargeChange;
							}
							else if (hScrollBar.Value - hScrollBar.SmallChange < hScrollBar.Minimum && sb == SB_LINELEFT)
							{
								hScrollBar.Value = hScrollBar.Minimum;
							}
							else if (hScrollBar.Value - hScrollBar.LargeChange < hScrollBar.Minimum && sb == SB_PAGELEFT)
							{
								hScrollBar.Value = hScrollBar.Minimum;
							}
							else if (!(hScrollBar.Value == hScrollBar.Minimum && (sb == SB_PAGELEFT || sb == SB_LINELEFT))
								&& !(hScrollBar.Value >= hScrollBar.Maximum - hScrollBar.LargeChange && (sb == SB_PAGERIGHT || sb == SB_LINERIGHT)))
							{
								Control.ReflectMessage(hScrollBar.Handle, ref m);
							}
						}
					}
					break;
			}
			Control.ReflectMessage(_control.Handle, ref m);
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
}