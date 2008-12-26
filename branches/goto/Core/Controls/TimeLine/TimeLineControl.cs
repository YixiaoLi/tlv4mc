using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Design;
using System.Drawing.Drawing2D;
using NU.OJL.MPRTOS.TLV.Base;

namespace NU.OJL.MPRTOS.TLV.Core.Controls
{
	public partial class TimeLineControl : UserControl, ITimeLineControl
	{
		protected int _timeRadix = 10;
		protected TraceLogVisualizerData _data;

		public virtual TimeLine TimeLine { get; set; }

		public TimeLineControl()
		{
			ResizeRedraw = true;
			DoubleBuffered = true;
			InitializeComponent();
		}

		protected override void OnLoad(EventArgs e)
		{
			base.OnLoad(e);

			this.ApplyNativeScroll();

			ApplicationData.FileContext.DataChanged += (o, _e) =>
			{
				Invoke((MethodInvoker)(() =>
				{
					if (ApplicationData.FileContext.Data == null)
					{
						ClearData();
					}
					else
					{
						SetData(ApplicationData.FileContext.Data);
					}
				}));
			};
		}

		public virtual void SetData(TraceLogVisualizerData data)
		{
			ClearData();

			_data = data;

			_timeRadix = _data.ResourceData.TimeRadix;
		}

		public virtual void Draw(PaintEventArgs e)
		{

		}

		public virtual void ClearData()
		{
			_timeRadix = 10;
			TimeLine = null;
		}

		protected override void OnPaint(PaintEventArgs e)
		{
			base.OnPaint(e);
			Draw(e);
		}
	}
}
