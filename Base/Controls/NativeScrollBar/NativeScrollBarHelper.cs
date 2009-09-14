
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
