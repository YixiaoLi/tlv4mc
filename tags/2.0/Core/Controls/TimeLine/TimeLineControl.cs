/*
 *  TLV - Trace Log Visualizer
 *
 *  Copyright (C) 2008-2011 by Nagoya Univ., JAPAN
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

            ApplicationFactory.BlackBoard.CursorTimeChanged += (o, _e) =>
            {
                if (!ApplicationFactory.BlackBoard.dragFlag) { Refresh(); }
            };

            ApplicationFactory.BlackBoard.DetailSearchFlagChanged += (o, _e) =>
             {
                 if (!ApplicationFactory.BlackBoard.DetailSearchFlag) { this.Enabled = true; }
                 else { this.Enabled = false; }
             };


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
            // TimeLineScale で override されている
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
            Draw(e.Graphics, e.ClipRectangle); //時刻目盛部分の描画


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
                {
                    return;
                }
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
                else if (CursorMode == CursorModes.MarkerMode)
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
