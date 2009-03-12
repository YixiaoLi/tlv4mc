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
		protected ToolStripMenuItem _addMarkerContextToolStripItem;
		protected ToolStripMenuItem _delMarkerContextToolStripItem;
		protected ContextMenuStrip _normalContextMenuStrip;
		protected ContextMenuStrip _markerSelectedContextMenuStrip;
		protected TimeLineMarkerManager _timeLineMarkerManager;
		protected IEnumerable<TimeLineMarker> _globalTimeLineMarkers
		{
			get
			{
				if (_timeLineMarkerManager != null)
					return _timeLineMarkerManager.Markers.Values.AsEnumerable();
				else
					return Enumerable.Empty<TimeLineMarker>();
			}
		}

		// カーソルマーカーをマウスの動きに合わせて変化させるかどうか
		public virtual bool CursorTimeTracked { get; set; }
		
		// カーソルマーカーを描画するかどうか
		public virtual bool CursorTimeDrawed { get; set; }
		
		public virtual bool SelectedTimeRangeTracked { get; set; }
		public virtual TimeLine TimeLine { get; set; }
		public virtual GeneralNamedCollection<TimeLineMarker> LocalTimeLineMarkers { get; private set; }

		public TimeLineControl()
		{
			ResizeRedraw = true;
			DoubleBuffered = true;
			CursorTimeTracked = true;
			CursorTimeDrawed = true;
			SelectedTimeRangeTracked = true;
			LocalTimeLineMarkers = new GeneralNamedCollection<TimeLineMarker>();
			InitializeComponent();
			_addMarkerContextToolStripItem = new ToolStripMenuItem("ここにマーカーを追加する");
			_delMarkerContextToolStripItem = new ToolStripMenuItem("マーカーを削除");
			_addMarkerContextToolStripItem.Click += addMarkerContextToolStripItemClick;
			_delMarkerContextToolStripItem.Click += delMarkerContextToolStripItemClick;
			_normalContextMenuStrip = new ContextMenuStrip();
			_normalContextMenuStrip.Items.Add(_addMarkerContextToolStripItem);
			_markerSelectedContextMenuStrip = new ContextMenuStrip();
			_markerSelectedContextMenuStrip.Items.Add(_delMarkerContextToolStripItem);
		}

		protected void addMarkerContextToolStripItemClick(object sender, EventArgs e)
		{
			ApplicationData.FileContext.Data.SettingData.LocalSetting.TimeLineMarkerManager.AddMarker(ApplicationFactory.BlackBoard.CursorTime);
		}

		protected void delMarkerContextToolStripItemClick(object sender, EventArgs e)
		{
			ApplicationData.FileContext.Data.SettingData.LocalSetting.TimeLineMarkerManager.DeleteSelectedMarker();
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

			_timeLineMarkerManager = _data.SettingData.LocalSetting.TimeLineMarkerManager;

			_timeRadix = _data.ResourceData.TimeRadix;
			ContextMenuStrip = _normalContextMenuStrip;

			_timeLineMarkerManager.SelectedMarkerChanged += (o, e) =>
			{
				if (_timeLineMarkerManager.GetSelectedMarker().Count() == 0)
					ContextMenuStrip = _normalContextMenuStrip;
				else
					ContextMenuStrip = _markerSelectedContextMenuStrip;

				Refresh();
			};

			_timeLineMarkerManager.Markers.CollectionChanged += (o, e) =>
				{
					if (_timeLineMarkerManager.Markers.Count == 0 || _timeLineMarkerManager.GetSelectedMarker().Count() == 0)
						ContextMenuStrip = _normalContextMenuStrip;
				};
		}

		public virtual void Draw(Graphics g, Rectangle rect)
		{

		}

		public virtual void ClearData()
		{
			_timeRadix = 10;
			TimeLine = null;
			ContextMenuStrip = null;
			_timeLineMarkerManager = null;
		}

		protected override void OnPaint(PaintEventArgs e)
		{
			base.OnPaint(e);
			Draw(e.Graphics, e.ClipRectangle);

			if (_data != null)
			{
				foreach (TimeLineMarker tlm in LocalTimeLineMarkers)
				{
					DrawCursor(e.Graphics, tlm.Color, tlm.Time);
				}

				foreach (TimeLineMarker tlm in _globalTimeLineMarkers)
				{
					DrawMarker(e.Graphics, tlm);
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

		public virtual void DrawMarker(Graphics g, TimeLineMarker marker)
		{
			drawMarker(g, ClientRectangle, marker);
		}

		protected void drawMarker(Graphics g, Rectangle rect, TimeLineMarker marker)
		{
			if (marker != null && TimeLine != null)
			{
				float x = marker.Time.ToX(TimeLine.FromTime, TimeLine.ToTime, rect.Width);

				if (rect.X + x > Width)
					return;

				float w = marker.Selected ? 3 : 1;
				int a = marker.Selected ? 255 : 200;

				g.DrawLine(new System.Drawing.Pen(Color.FromArgb(a, marker.Color), w), rect.X + x, rect.Y, rect.X + x, rect.Height);
			}
		}

		protected override void OnMouseMove(MouseEventArgs e)
		{
			base.OnMouseMove(e);

			int x = e.X;

			if (TimeLine == null)
				return;

			if (CursorTimeTracked)
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

		protected override void OnMouseClick(MouseEventArgs e)
		{
			base.OnMouseClick(e);

			int x = e.X;

			if (TimeLine == null)
				return;

			Time t = Time.FromX(TimeLine.FromTime, TimeLine.ToTime, Width, x);
			Time b = Time.FromX(TimeLine.FromTime, TimeLine.ToTime, Width, x - 5);
			Time a = Time.FromX(TimeLine.FromTime, TimeLine.ToTime, Width, x + 5);

			TimeLineMarker foucsMarker = _globalTimeLineMarkers.FirstOrDefault<TimeLineMarker>(m => m.Time > b && a > m.Time);
			
			if ((Control.ModifierKeys & Keys.Control) != Keys.Control)
			{
				_timeLineMarkerManager.ResetSelect();
			}
			
			if (foucsMarker != null)
			{
				foucsMarker.SelectToggle();
			}
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
