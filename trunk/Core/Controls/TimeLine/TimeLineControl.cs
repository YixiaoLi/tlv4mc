/*
 *  TLV - Trace Log Visualizer
 *
 *  Copyright (C) 2008,2009 by Embedded and Real-Time Systems Laboratory
 *              Graduate School of Information Science, Nagoya Univ., JAPAN
 *
 *  �嵭����Ԥϡ��ʲ���(1)��(4)�ξ������������˸¤ꡤ�ܥ��եȥ���
 *  �����ܥ��եȥ���������Ѥ�����Τ�ޤࡥ�ʲ�Ʊ���ˤ���ѡ�ʣ������
 *  �ѡ������ۡʰʲ������ѤȸƤ֡ˤ��뤳�Ȥ�̵���ǵ������롥
 *  (1) �ܥ��եȥ������򥽡��������ɤη������Ѥ�����ˤϡ��嵭������
 *      ��ɽ�����������Ѿ�浪��Ӳ�����̵�ݾڵ��꤬�����Τޤޤη��ǥ���
 *      ����������˴ޤޤ�Ƥ��뤳�ȡ�
 *  (2) �ܥ��եȥ������򡤥饤�֥������ʤɡ�¾�Υ��եȥ�������ȯ�˻�
 *      �ѤǤ�����Ǻ����ۤ�����ˤϡ������ۤ�ȼ���ɥ�����ȡ�����
 *      �ԥޥ˥奢��ʤɡˤˡ��嵭�����ɽ�����������Ѿ�浪��Ӳ���
 *      ��̵�ݾڵ����Ǻܤ��뤳�ȡ�
 *  (3) �ܥ��եȥ������򡤵�����Ȥ߹���ʤɡ�¾�Υ��եȥ�������ȯ�˻�
 *      �ѤǤ��ʤ����Ǻ����ۤ�����ˤϡ����Τ����줫�ξ�����������
 *      �ȡ�
 *    (a) �����ۤ�ȼ���ɥ�����ȡ����Ѽԥޥ˥奢��ʤɡˤˡ��嵭����
 *        �ɽ�����������Ѿ�浪��Ӳ�����̵�ݾڵ����Ǻܤ��뤳�ȡ�
 *    (b) �����ۤη��֤��̤�������ˡ�ˤ�äơ�TOPPERS�ץ������Ȥ�
 *        ��𤹤뤳�ȡ�
 *  (4) �ܥ��եȥ����������Ѥˤ��ľ��Ū�ޤ��ϴ���Ū�������뤤���ʤ�»
 *      ������⡤�嵭����Ԥ����TOPPERS�ץ������Ȥ����դ��뤳�ȡ�
 *      �ޤ����ܥ��եȥ������Υ桼���ޤ��ϥ���ɥ桼������Τ����ʤ���
 *      ͳ�˴�Ť����ᤫ��⡤�嵭����Ԥ����TOPPERS�ץ������Ȥ�
 *      ���դ��뤳�ȡ�
 *
 *  �ܥ��եȥ������ϡ�̵�ݾڤ��󶡤���Ƥ����ΤǤ��롥�嵭����Ԥ�
 *  ���TOPPERS�ץ������Ȥϡ��ܥ��եȥ������˴ؤ��ơ�����λ�����Ū
 *  ���Ф���Ŭ������ޤ�ơ������ʤ��ݾڤ�Ԥ�ʤ����ޤ����ܥ��եȥ���
 *  �������Ѥˤ��ľ��Ū�ޤ��ϴ���Ū�������������ʤ�»���˴ؤ��Ƥ⡤��
 *  ����Ǥ�����ʤ���
 *
 *  @(#) $Id$
 */
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
		public enum CursorModes
		{
			Normal,
			Move,
			ResizeL,
			ResizeR,
			MarkerMode
		}

		protected int _lastMouseMoveX;
		protected int _mouseDownX = -1;
		protected CursorModes _cursorMode = CursorModes.Normal;
		protected CursorModes _lastCursorMode;
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

		public virtual int TimeLineWidth { get { return Width; } set { } }
		public virtual int TimeLineX { get { return 0; } set { } }

		public CursorModes CursorMode
		{
			get { return _cursorMode; }
			set
			{
				if (_cursorMode != value)
				{
					_lastCursorMode = _cursorMode;
					_cursorMode = value;
				}
			}
		}

		// ��������ޡ�������ޥ�����ư���˹�碌���Ѳ������뤫�ɤ���
		public virtual bool CursorTimeTracked { get; set; }
		
		// ��������ޡ����������褹�뤫�ɤ���
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
			_addMarkerContextToolStripItem = new ToolStripMenuItem("�����˥ޡ��������ɲä���");
			_delMarkerContextToolStripItem = new ToolStripMenuItem("�ޡ���������");
			_addMarkerContextToolStripItem.Click += addMarkerContextToolStripItemClick;
			_delMarkerContextToolStripItem.Click += delMarkerContextToolStripItemClick;
			_normalContextMenuStrip = new ContextMenuStrip();
			_normalContextMenuStrip.Items.Add(_addMarkerContextToolStripItem);
			_markerSelectedContextMenuStrip = new ContextMenuStrip();
			_markerSelectedContextMenuStrip.Items.Add(_delMarkerContextToolStripItem);
		}

		public virtual Cursor GetCursor(CursorModes mode)
		{
			switch (mode)
			{
				default:
				case CursorModes.Normal:
					return Cursors.Default;
				case CursorModes.Move:
					return Cursors.SizeAll;
				case CursorModes.ResizeL:
				case CursorModes.ResizeR:
					return Cursors.SizeWE;
				case CursorModes.MarkerMode:
					return Cursors.VSplit;
			}
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

				DrawCursor(e.Graphics, _data.SettingData.TraceLogDisplayPanelSetting.CursorColor, ApplicationFactory.BlackBoard.CursorTime);

				foreach (TimeLineMarker tlm in LocalTimeLineMarkers)
				{
					DrawCursor(e.Graphics, tlm.Color, tlm.Time);
				}

				foreach (TimeLineMarker tlm in _globalTimeLineMarkers)
				{
					DrawMarker(e.Graphics, tlm);
				}

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

			int x = e.X - TimeLineX;

			if (TimeLine == null)
				return;

			if (e.Button == MouseButtons.Left)
			{
				if (CursorMode == CursorModes.MarkerMode)
				{
					if (e.Button == MouseButtons.Left && _timeLineMarkerManager.GetSelectedMarker().Count() != 0 && _mouseDownX != -1)
					{
						foreach (TimeLineMarker tlm in _timeLineMarkerManager.GetSelectedMarker())
						{
							Time dt = Time.FromX(TimeLine.FromTime, TimeLine.ToTime, TimeLineWidth, x) - Time.FromX(TimeLine.FromTime, TimeLine.ToTime, TimeLineWidth, _lastMouseMoveX);
							tlm.Time += dt;
						}
					}
				}
			}
			else
			{
				Time t = Time.FromX(TimeLine.FromTime, TimeLine.ToTime, TimeLineWidth, x);
				Time b = Time.FromX(TimeLine.FromTime, TimeLine.ToTime, TimeLineWidth, x - 3);
				Time a = Time.FromX(TimeLine.FromTime, TimeLine.ToTime, TimeLineWidth, x + 3);

				TimeLineMarker onMarker = _globalTimeLineMarkers.FirstOrDefault<TimeLineMarker>(m => m.Time > b && a > m.Time);

				if (onMarker != null)
				{
					CursorMode = CursorModes.MarkerMode;
					Cursor = GetCursor(CursorMode);
				}
				else if(CursorMode == CursorModes.MarkerMode)
				{
					CursorMode = _lastCursorMode;
					Cursor = GetCursor(CursorMode);
				}

			}

			if (CursorTimeTracked)
			{
				ApplicationFactory.BlackBoard.CursorTime = Time.FromX(TimeLine.FromTime, TimeLine.ToTime, TimeLineWidth, x);
			}

			_lastMouseMoveX = x;
		}

		protected override void OnMouseDown(MouseEventArgs e)
		{
			base.OnMouseDown(e);

			if (_data == null)
				return;

			if (e.Button == MouseButtons.Left)
			{
				_timeLineMarkerManager.ResetSelect();

				int x = e.X - TimeLineX;
				_mouseDownX = x;

				if (TimeLine == null)
					return;

				Time t = Time.FromX(TimeLine.FromTime, TimeLine.ToTime, TimeLineWidth, x);
				Time b = Time.FromX(TimeLine.FromTime, TimeLine.ToTime, TimeLineWidth, x - 5);
				Time a = Time.FromX(TimeLine.FromTime, TimeLine.ToTime, TimeLineWidth, x + 5);

				TimeLineMarker foucsMarker = _globalTimeLineMarkers.FirstOrDefault<TimeLineMarker>(m => m.Time > b && a > m.Time);

				if (foucsMarker != null)
				{
					foucsMarker.SelectToggle();
					if (foucsMarker.Selected)
					{
						_timeLineMarkerManager.Markers.Move(_timeLineMarkerManager.Markers.IndexOf(foucsMarker.Name), _timeLineMarkerManager.Markers.Count - 1);
					}
				}

				Refresh();
			}
		}

		protected override void OnMouseUp(MouseEventArgs e)
		{
			base.OnMouseUp(e);

			int x = e.X - TimeLineX;

			if (_data == null)
				return;

			if (e.Button == MouseButtons.Left)
			{
				_mouseDownX = -1;
			}
		}

		protected override void OnMouseDoubleClick(MouseEventArgs e)
		{
			base.OnMouseDoubleClick(e);

			Time time = Time.FromX(TimeLine.FromTime, TimeLine.ToTime, TimeLineWidth, e.X);
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
