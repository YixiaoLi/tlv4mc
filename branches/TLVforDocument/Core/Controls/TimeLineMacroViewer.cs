/*
 *  TLV - Trace Log Visualizer
 *
 *  Copyright (C) 2008-2010 by Nagoya Univ., JAPAN
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
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using NU.OJL.MPRTOS.TLV.Base;
using System.Threading;

namespace NU.OJL.MPRTOS.TLV.Core.Controls
{
	public class TimeLineMacroViewer : TimeLineControl
	{
		private List<TimeLineVisualizer> _list = new List<TimeLineVisualizer>();
		private TimeLineScale _scale = new TimeLineScale();
		private int _num;
		private int _rowHeight;
		private float _fx;
		private float _tx;
		private int _lastX;
        private Cursor HandHoldCursor { get { return new Cursor(Properties.Resources.handHold.Handle) { Tag = "handHold" }; } }
        private Cursor HandCursor { get { return new Cursor(Properties.Resources.hand.Handle) { Tag = "hand" }; } }
		private TimeLine ViewingAreaTimeLine;
		private Bitmap _macroVizData;

		public TimeLineMacroViewer()
		{
			BackColor = Color.FromKnownColor(KnownColor.Control);
			_scale.ScaleMarkDirection = ScaleMarkDirection.Bottom;

			SuspendLayout();

			this.ApplyNativeScroll();

			Controls.Add(_scale);
			_scale.Location = new System.Drawing.Point(1,0);
			_scale.Width = Width - 2;
			_scale.Anchor = AnchorStyles.Left | AnchorStyles.Right| AnchorStyles.Top;
            this.TimeLineX = 1;
			ResumeLayout();

//            TimeLine.ViewingAreaChanged += (o,e) => {               };
		}

		protected override void OnLoad(EventArgs e)
		{
			base.OnLoad(e);
            ApplicationFactory.BlackBoard.CursorTimeChanged += (o, _e) => { Refresh(); };
			SizeChanged += (o, _e) => { updateViewingArea(); };

			EventHandler showStatus = (o, _e) =>
			{
				if (_data == null)
					return;

				ApplicationFactory.StatusManager.ShowHint(GetType() + Name + "mouseWheelMove", "可視化表示領域移動", "Ctrl", "ホイール", ",矢印キー");
				ApplicationFactory.StatusManager.ShowHint(GetType() + Name + "mouseWheelScaleRatioChange", "拡大縮小", "Shift", "ホイール");
				ApplicationFactory.StatusManager.ShowHint(GetType() + Name + "mouseDoubleClickMove", "移動", "左ダブルクリック");
			};
			EventHandler hideStatus = (o, _e) =>
			{
				if (_data == null)
					return;

				ApplicationFactory.StatusManager.HideHint(GetType() + Name + "mouseWheelMove");
				ApplicationFactory.StatusManager.HideHint(GetType() + Name + "mouseWheelScaleRatioChange");
				ApplicationFactory.StatusManager.HideHint(GetType() + Name + "mouseDoubleClickMove");
			};

			MouseEnter += showStatus;
			MouseEnter += (o, _e) => { Focus(); };
			MouseLeave += hideStatus;
		}

     
		public override void SetData(TraceLogVisualizerData data)
		{
			base.SetData(data);

			Thread th = new Thread(new ThreadStart(() =>
			{
				ViewingAreaTimeLine = _data.SettingData.TraceLogDisplayPanelSetting.TimeLine;


				 Time from = ViewingAreaTimeLine.FromTime.Truncate();
				 Time to = ViewingAreaTimeLine.ToTime.Truncate();

				if (!_scale.LocalTimeLineMarkers.ContainsKey("___fromMarker"))
					_scale.LocalTimeLineMarkers.Add(new TimeLineMarker("___fromMarker", _data.SettingData.TimeLineMacroViewerSetting.SelectedAreaColor, from));

				if (!_scale.LocalTimeLineMarkers.ContainsKey("___toMarker"))
					_scale.LocalTimeLineMarkers.Add(new TimeLineMarker("___toMarker", _data.SettingData.TimeLineMacroViewerSetting.SelectedAreaColor, to));

				TimeLine = new TimeLine(_data.TraceLogData.MinTime, _data.TraceLogData.MaxTime);

				ViewingAreaTimeLine.ViewingAreaChanged += timeLineViewingAreaChanged;

				makeList();

				_scale.SetData(_data);
				_scale.TimeLine = new TimeLine(_data.TraceLogData.MinTime, _data.TraceLogData.MaxTime);

				_data.SettingData.ResourceExplorerSetting.BecameDirty += ruleBecameDirty;
				_data.SettingData.VisualizeRuleExplorerSetting.BecameDirty += ruleBecameDirty;

				updateViewingArea();

				Invoke(new MethodInvoker(() =>
				{
					Refresh();
				}));
			}));

			th.Start();
		}

		private void updateViewingArea()
		{
			if (_data == null || TimeLine == null)
				return;

			_macroVizData = null; 

			int w = Width - 2;

			_fx = ViewingAreaTimeLine.FromTime.ToX(ViewingAreaTimeLine.MinTime, ViewingAreaTimeLine.MaxTime, w);
			_tx = ViewingAreaTimeLine.ToTime.ToX(ViewingAreaTimeLine.MinTime, ViewingAreaTimeLine.MaxTime, w);

			_scale.Size = new System.Drawing.Size(w, _scale.Height);
		}

		public override void ClearData()
		{
			base.ClearData();
			resetData();
		}

		private void makeList()
		{

			List<string> targets = new List<string>();

			_num = _data.VisualizeData.VisualizeRules.Count + _data.ResourceData.Resources.Count;

			_rowHeight = _num != 0 ? (Height - _scale.Height) / _num : 0;

			foreach (VisualizeRule rule in _data.VisualizeData.VisualizeRules)
			{
				if (rule.Target == null)
				{
					_list.Add(new TimeLineVisualizer(new TimeLineEvents(rule)));
				}
			}
			foreach (Resource res in _data.ResourceData.Resources)
			{
				_list.Add(new TimeLineVisualizer(new TimeLineEvents(res)));
			}
			foreach(TimeLineVisualizer tv in _list)
			{
				tv.SetData(_data);
				tv.TimeLine = new TimeLine(_data.TraceLogData.MinTime, _data.TraceLogData.MaxTime);
			}
			//foreach (TimeLineVisualizer tv in _list)
			//{
			//    tv.WaitSetData();
			//}
		}

		private void resetData()
		{
			_list.Clear();
			_num = 0;
			_rowHeight = 0;
			_fx = 0f;
			_tx = 0f;
			_mouseDownX = -1;
			_lastX = 0;
			_cursorMode = CursorModes.Normal;
		}

		protected void ruleBecameDirty(object sender, string propertyName)
		{
			//resetData();
			//makeList();
			updateViewingArea();
			Refresh();
		}

		public override void Draw(Graphics graphics, Rectangle rect)
		{
			base.Draw(graphics, rect);

			if (_data == null || _list.Count() == 0)
				return;

			if (_macroVizData == null)
			{
				_macroVizData = new Bitmap(rect.Width, rect.Height, graphics);
				Graphics g = Graphics.FromImage(_macroVizData);

				IEnumerable<TimeLineVisualizer> tlvs = _list.Where<TimeLineVisualizer>(tlv =>
					{

						if (tlv.Rule != null && tlv.Event == null && tlv.Target == null)
						{
							return _data.SettingData.VisualizeRuleExplorerSetting.VisualizeRuleVisibility.ContainsKey(tlv.Rule.Name)
							&& _data.SettingData.VisualizeRuleExplorerSetting.VisualizeRuleVisibility.GetValue(tlv.Rule.Name);
						}
						else if (tlv.Rule == null && tlv.Event == null && tlv.Target != null)
						{
							return _data.SettingData.ResourceExplorerSetting.ResourceVisibility.ContainsKey(tlv.Target.Name)
							&& _data.SettingData.ResourceExplorerSetting.ResourceVisibility.GetValue(tlv.Target.Name);
						}
						else
						{
							return false;
						}
					});

				if (tlvs.Count() == 0)
					return;

				_rowHeight = (Height - _scale.Height) / tlvs.Count();

				int i = 0;
				foreach (TimeLineVisualizer tl in tlvs)
				{
					int y = i * _rowHeight + _scale.Height;
					int x = _scale.Location.X - 1;
					int w = _scale.Width + 1;

					g.FillRectangle(Brushes.White, new Rectangle(x, y, w, _rowHeight));

					g.DrawRectangle(new System.Drawing.Pen(Color.FromKnownColor(KnownColor.DarkGray)), new Rectangle(x, y, w, _rowHeight));
                   
					tl.Draw(g, new Rectangle(x + 1, y + 1, w - 1, _rowHeight - 1));

					i++;
				}

			}

			if (_macroVizData != null)
				graphics.DrawImage(_macroVizData, rect);


			RectangleF r = new RectangleF(_fx, rect.Y + 1, _tx - _fx < 0 ? 1 : _tx - _fx, rect.Height - 2);

			graphics.FillRectangle(new SolidBrush(Color.FromArgb(50, _data.SettingData.TimeLineMacroViewerSetting.SelectedAreaColor)), r);
			graphics.DrawRectangle(new Pen() { Color = Color.FromArgb(100, _data.SettingData.TimeLineMacroViewerSetting.SelectedAreaColor), Width = 1.0f }, r.X, r.Y, r.Width, r.Height);

		}

		protected override void OnMouseDown(MouseEventArgs e)
		{

			if (_data == null)
				return;

            if (e.Button == MouseButtons.Left)
            {
                if (_cursorMode == CursorModes.Move)
                    Cursor = HandHoldCursor;

                _lastX = (int)_fx;
			}

			base.OnMouseDown(e);
		}

		protected override void OnMouseUp(MouseEventArgs e)
		{

			if (_data == null)
				return;

			if (e.Button == MouseButtons.Left)
			{

				if (_cursorMode == CursorModes.Move)
					Cursor = HandCursor;
			}

			base.OnMouseUp(e);
		}

		protected override void OnMouseMove(MouseEventArgs e)
		{

			if (_data == null)
				return;

			int x = e.X;

			if (e.Button == MouseButtons.Left)
			{
				ApplicationFactory.StatusManager.HideHint(GetType() + Name + "viewingAreaMove");
				ApplicationFactory.StatusManager.HideHint(GetType() + Name + "viewingAreaSizeChange");

				if (_cursorMode == CursorModes.ResizeL)
				{
					try
					{
						ViewingAreaTimeLine.SetTime(Time.FromX(ViewingAreaTimeLine.MinTime, ViewingAreaTimeLine.MaxTime, _scale.Width, (int)x).Truncate(), ViewingAreaTimeLine.ToTime);
						Refresh();
					}
					catch { }
				}
				else if (_cursorMode == CursorModes.ResizeR)
				{
					try
					{
						ViewingAreaTimeLine.SetTime(ViewingAreaTimeLine.FromTime, Time.FromX(ViewingAreaTimeLine.MinTime, ViewingAreaTimeLine.MaxTime, _scale.Width, (int)x).Truncate());
						Refresh();
					}
					catch { }
				}
				else if (_cursorMode == CursorModes.Move)
				{
                    int _x = _lastX + x - _mouseDownX - 5;
                    ViewingAreaTimeLine.MoveBySettingFromTime(Time.FromX(ViewingAreaTimeLine.MinTime, ViewingAreaTimeLine.MaxTime, _scale.Width, _x).Round(0));
				}
				else if (_cursorMode == CursorModes.Normal)
				{
					Time t1 = Time.FromX(ViewingAreaTimeLine.MinTime, ViewingAreaTimeLine.MaxTime, _scale.Width, _mouseDownX).Truncate();
					Time t2 = Time.FromX(ViewingAreaTimeLine.MinTime, ViewingAreaTimeLine.MaxTime, _scale.Width, (int)x).Truncate();

					if (t1 != t2)
					{

						Time from = t1 > t2 ? t2 : t1;
						Time to = t1 > t2 ? t1 : t2;

						_data.SettingData.TraceLogDisplayPanelSetting.TimeLine.SetTime(from, to);
					}
				}
			}
			else
			{
				if ((Math.Abs(x - _tx) < 1
					|| Math.Abs(x - _fx) < 1))
				{
					ApplicationFactory.StatusManager.ShowHint(GetType() + Name + "viewingAreaSizeChange", "可視化表示領域サイズ変更", "ドラッグ");
					ApplicationFactory.StatusManager.HideHint(GetType() + Name + "viewingAreaMove");
					if (Cursor != Cursors.SizeWE)
					{
						if (Math.Abs(x - _fx) < 1)
							_cursorMode = CursorModes.ResizeL;
						if (Math.Abs(x - _tx) < 1)
							_cursorMode = CursorModes.ResizeR;

						Cursor = Cursors.SizeWE;

					}
				}
				else if (x > _fx && x < _tx)
				{
					ApplicationFactory.StatusManager.ShowHint(GetType() + Name + "viewingAreaMove", "可視化表示領域移動", "ドラッグ");
					ApplicationFactory.StatusManager.HideHint(GetType() + Name + "viewingAreaSizeChange");
					if (_cursorMode != CursorModes.Move)
					{
						_cursorMode = CursorModes.Move;
						Cursor = HandCursor;
					}
				}
				else
				{
					CursorMode = CursorModes.Normal;
					Cursor = GetCursor(CursorMode);
					ApplicationFactory.StatusManager.HideHint(GetType() + Name + "viewingAreaMove");
					ApplicationFactory.StatusManager.HideHint(GetType() + Name + "viewingAreaSizeChange");
				}
			}

			base.OnMouseMove(e);
		}

		protected override void OnMouseWheel(MouseEventArgs e)
		{
			base.OnMouseWheel(e);
            bool isCtrl = (Control.ModifierKeys & Keys.Control) == Keys.Control;
            bool isShift = (Control.ModifierKeys & Keys.Shift) == Keys.Shift;
            if (isShift)
            {
                Time span = TimeLine.ViewingSpan / 2;
                Time time = TimeLine.FromTime + span;

                decimal ratio = (e.Delta < 0)
                    ? 1.5m
                    : (e.Delta > 0)
                    ? 0.75m
                    : 1m;
                
                Time from = (time - span * ratio).Round(0);
                Time to = (time + span * ratio).Round(0);

                TimeLine.SetTime(from, to);
                foreach (TimeLineVisualizer tlv in _list)
                {
                    tlv.TimeLine.SetTime(from, to);
                }
                _scale.TimeLine.SetTime(from, to);
                updateViewingArea();

                Refresh();

                if (e.GetType() == typeof(ExMouseEventArgs))
                    ((ExMouseEventArgs)e).Handled = true;
            }else if (isCtrl){
                Time span = TimeLine.ViewingSpan / 100;

                span = (e.Delta > 0)
                    ? span * -1m
                    : span;

                _data.SettingData.TraceLogDisplayPanelSetting.TimeLine.SetTime
                    (
                    (_data.SettingData.TraceLogDisplayPanelSetting.TimeLine.FromTime + span).Round(0),
                    (_data.SettingData.TraceLogDisplayPanelSetting.TimeLine.ToTime + span).Round(0)
                    );

                if (e.GetType() == typeof(ExMouseEventArgs))
                    ((ExMouseEventArgs)e).Handled = true;
            }
		}

		protected override void OnMouseLeave(EventArgs e)
		{
			base.OnMouseLeave(e);

			ApplicationFactory.StatusManager.HideHint(GetType() + Name + "viewingAreaMove");
			ApplicationFactory.StatusManager.HideHint(GetType() + Name + "viewingAreaSizeChange");
		}

		private void timeLineViewingAreaChanged(object sender, GeneralChangedEventArgs<TimeLine> e)
		{
			if (e.Old.FromTime != e.New.FromTime)
				_fx = ViewingAreaTimeLine.FromTime.ToX(ViewingAreaTimeLine.MinTime, ViewingAreaTimeLine.MaxTime, _scale.Width);

			if (e.Old.ToTime != e.New.ToTime)
				_tx = ViewingAreaTimeLine.ToTime.ToX(ViewingAreaTimeLine.MinTime, ViewingAreaTimeLine.MaxTime, _scale.Width);

			Time from = ViewingAreaTimeLine.FromTime.Truncate();
			Time to = ViewingAreaTimeLine.ToTime.Truncate();

			if (!_scale.LocalTimeLineMarkers.ContainsKey("___fromMarker"))
				_scale.LocalTimeLineMarkers.Add(new TimeLineMarker("___fromMarker", _data.SettingData.TimeLineMacroViewerSetting.SelectedAreaColor, from));
			else
				_scale.LocalTimeLineMarkers["___fromMarker"] = new TimeLineMarker("___fromMarker", _data.SettingData.TimeLineMacroViewerSetting.SelectedAreaColor, from);
			if (!_scale.LocalTimeLineMarkers.ContainsKey("___toMarker"))
				_scale.LocalTimeLineMarkers.Add(new TimeLineMarker("___toMarker", _data.SettingData.TimeLineMacroViewerSetting.SelectedAreaColor, to));
			else
				_scale.LocalTimeLineMarkers["___toMarker"] = new TimeLineMarker("___toMarker", _data.SettingData.TimeLineMacroViewerSetting.SelectedAreaColor, to);

			Refresh();
		}

	}
}