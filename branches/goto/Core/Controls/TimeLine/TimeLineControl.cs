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

		public virtual bool CursorTimeTracked { get; set; }
		public virtual bool CursorTimeDrawed { get; set; }
		public virtual bool SelectedTimeRangeTracked { get; set; }
		public virtual TimeLine TimeLine { get; set; }
		public virtual GeneralNamedCollection<TimeLineMarker> TimeLineMarkers { get; private set; }

		public TimeLineControl()
		{
			ResizeRedraw = true;
			DoubleBuffered = true;
			CursorTimeTracked = true;
			CursorTimeDrawed = true;
			SelectedTimeRangeTracked = true;
			TimeLineMarkers = new GeneralNamedCollection<TimeLineMarker>();
			InitializeComponent();
		}

		protected override void OnLoad(EventArgs e)
		{
			base.OnLoad(e);

			this.ApplyNativeScroll();

			ApplicationFactory.BlackBoard.CursorTimeChanged += (o, _e) => { Refresh(); };

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

		public virtual void Draw(Graphics g, Rectangle rect)
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
			Draw(e.Graphics, e.ClipRectangle);

			if (_data != null)
			{
				foreach (TimeLineMarker tlm in TimeLineMarkers)
				{
					DrawCursor(e.Graphics, tlm.Color, tlm.Time);
				}

				DrawCursor(e.Graphics, _data.SettingData.TraceLogDisplayPanelSetting.CursorColor, ApplicationFactory.BlackBoard.CursorTime);
			}
		}

		public virtual void DrawCursor(Graphics g, Color color, Time time)
		{
			drawCursor(g, ClientRectangle, color, time);
		}

		protected void drawCursor(Graphics g,Rectangle rect, Color color, Time time)
		{
			if (CursorTimeDrawed && !ApplicationFactory.BlackBoard.CursorTime.IsEmpty && TimeLine != null)
			{
				float x = time.ToX(TimeLine.FromTime, TimeLine.ToTime, rect.Width);

				if (rect.X + x > Width)
					return;

				g.DrawLine(new System.Drawing.Pen(Color.FromArgb(200, color)), rect.X + x, rect.Y, rect.X + x, rect.Height);
			}
		}

		protected override void OnMouseMove(MouseEventArgs e)
		{
			base.OnMouseMove(e);

			int x = e.X;

			if (CursorTimeTracked && TimeLine != null)
			{
				ApplicationFactory.BlackBoard.CursorTime = Time.FromX(TimeLine.FromTime, TimeLine.ToTime, Width, x);
			}
		}

		protected override void OnMouseDoubleClick(MouseEventArgs e)
		{
			base.OnMouseDoubleClick(e);

			Time time = Time.FromX(TimeLine.FromTime, TimeLine.ToTime, Width, e.X);
			Time span = _data.SettingData.TraceLogDisplayPanelSetting.TimeLine.ViewingSpan / 2;

			_data.SettingData.TraceLogDisplayPanelSetting.TimeLine.SetTime((time - span).Truncate(), (time + span).Truncate());

			_data.SettingData.TraceLogViewerSetting.FirstDisplayedTime = time;
		}

		protected override bool ProcessDialogKey(Keys keyData)
		{
			if (_data == null)
				return base.ProcessDialogKey(keyData);

			Time span = _data.SettingData.TraceLogDisplayPanelSetting.TimeLine.ViewingSpan / 10;
			switch (keyData)
			{
				case Keys.Right:
				case Keys.Down:

					_data.SettingData.TraceLogDisplayPanelSetting.TimeLine.MoveBySettingFromTime(_data.SettingData.TraceLogDisplayPanelSetting.TimeLine.FromTime + span);

					return true;
				case Keys.Up:
				case Keys.Left:

					_data.SettingData.TraceLogDisplayPanelSetting.TimeLine.MoveBySettingFromTime(_data.SettingData.TraceLogDisplayPanelSetting.TimeLine.FromTime - span);

					return true;
				default:
					return base.ProcessDialogKey(keyData);
			}
		}
	}
}
