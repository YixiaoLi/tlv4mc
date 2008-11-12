using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Text;

namespace NU.OJL.MPRTOS.TLV.Base.Controls
{
	public class NativeScrollDataGridView : DataGridView
	{

		#region NativeScrollBar

		private NativeScrollBar nsb;

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

		protected override void OnHandleCreated(EventArgs e)
		{
			base.OnHandleCreated(e);
			if (Site == null)
			{
				nsb = new NativeScrollBar(this);
			}
		}
		protected override void OnHandleDestroyed(EventArgs e)
		{
			base.OnHandleDestroyed(e);
			if (nsb != null)
			{
				nsb.DestroyHandle();
			}
		}
		protected override void WndProc(ref Message m)
		{
			switch (m.Msg)
			{
				case WM_VSCROLL:
					if (m.LParam != VerticalScrollBar.Handle && VerticalScrollBar.Visible)
					{
						int sb = m.WParam.ToInt32();

						if (sb == SB_LINEDOWN)
						{
							Message msg = Message.Create(Handle, WM_MOUSEWHEEL, (IntPtr)0x78, (IntPtr)0);
							WndProc(ref msg);
						}
						else
						{
							Message msg = Message.Create(Handle, WM_MOUSEWHEEL, (IntPtr)0xffff88, (IntPtr)0);
							WndProc(ref msg);
						}

						if (VerticalScrollBar.Value + VerticalScrollBar.SmallChange > VerticalScrollBar.Maximum && sb == SB_LINEDOWN)
						{
							VerticalScrollBar.Value = VerticalScrollBar.Maximum - VerticalScrollBar.LargeChange;
						}
						else if (VerticalScrollBar.Value + VerticalScrollBar.LargeChange > VerticalScrollBar.Maximum && sb == SB_PAGEDOWN)
						{
							VerticalScrollBar.Value = VerticalScrollBar.Maximum - VerticalScrollBar.LargeChange;
						}
						else if (VerticalScrollBar.Value - VerticalScrollBar.SmallChange < VerticalScrollBar.Minimum && sb == SB_LINEUP)
						{
							VerticalScrollBar.Value = VerticalScrollBar.Minimum;
						}
						else if (VerticalScrollBar.Value - VerticalScrollBar.LargeChange < VerticalScrollBar.Minimum && sb == SB_PAGEUP)
						{
							VerticalScrollBar.Value = VerticalScrollBar.Minimum;
						}
						else if (!(VerticalScrollBar.Value == VerticalScrollBar.Minimum && (sb == SB_LINEUP || sb == SB_PAGEUP))
							&& !(VerticalScrollBar.Value >= VerticalScrollBar.Maximum - VerticalScrollBar.LargeChange && (sb == SB_LINEDOWN || sb == SB_PAGEDOWN)))
						{
							Control.ReflectMessage(this.VerticalScrollBar.Handle, ref m);
						}
					}
					break;
				case WM_HSCROLL:
					if (m.LParam != HorizontalScrollBar.Handle && HorizontalScrollBar.Visible)
					{
						int sb = m.WParam.ToInt32();

						if (HorizontalScrollBar.Value + HorizontalScrollBar.SmallChange > HorizontalScrollBar.Maximum && sb == SB_LINERIGHT)
						{
							HorizontalScrollBar.Value = HorizontalScrollBar.Maximum - HorizontalScrollBar.LargeChange;
						}
						else if (HorizontalScrollBar.Value + HorizontalScrollBar.LargeChange > HorizontalScrollBar.Maximum && sb == SB_PAGERIGHT)
						{
							HorizontalScrollBar.Value = HorizontalScrollBar.Maximum - HorizontalScrollBar.LargeChange;
						}
						else if (HorizontalScrollBar.Value - HorizontalScrollBar.SmallChange < HorizontalScrollBar.Minimum && sb == SB_LINELEFT)
						{
							HorizontalScrollBar.Value = HorizontalScrollBar.Minimum;
						}
						else if (HorizontalScrollBar.Value - HorizontalScrollBar.LargeChange < HorizontalScrollBar.Minimum && sb == SB_PAGELEFT)
						{
							HorizontalScrollBar.Value = HorizontalScrollBar.Minimum;
						}
						else if (!(HorizontalScrollBar.Value == HorizontalScrollBar.Minimum && (sb == SB_PAGELEFT || sb == SB_LINELEFT))
							&& !(HorizontalScrollBar.Value >= HorizontalScrollBar.Maximum - HorizontalScrollBar.LargeChange && (sb == SB_PAGERIGHT || sb == SB_LINERIGHT)))
						{
							Control.ReflectMessage(this.HorizontalScrollBar.Handle, ref m);
						}
					}
					break;
			}
			base.WndProc(ref m);
		}

		#endregion
	}
}
