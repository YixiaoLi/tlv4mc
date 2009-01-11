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
		enum CursorMode
		{
			Normal,
			Move,
			ResizeL,
			ResizeR
		}

		private int _delta = 1;
		private List<TimeLineVisualizer> _list = new List<TimeLineVisualizer>();
		private TimeLineScale _scale = new TimeLineScale();
		private int _num;
		private int _rowHeight;
		private float _fx;
		private float _tx;
		private int _lastX;
		private int _mouseDownFX;
		private Cursor HandHoldCursor { get { return new Cursor(Properties.Resources.handHold.Handle) { Tag = "handHold" }; } }
		private Cursor HandCursor { get { return new Cursor(Properties.Resources.hand.Handle) { Tag = "hand" }; } }
		private CursorMode _cursorMode = CursorMode.Normal;

		public TimeLineMacroViewer()
		{
			BackColor = Color.White;
			_scale.ScaleMarkDirection = ScaleMarkDirection.Bottom;

			SuspendLayout();

			this.ApplyNativeScroll();

			Controls.Add(_scale);
			_scale.Location = new System.Drawing.Point(1,0);
			_scale.Width = Width - 2;
			_scale.Anchor = AnchorStyles.Left | AnchorStyles.Right| AnchorStyles.Top;

			ResumeLayout();
		}

		protected override void OnLoad(EventArgs e)
		{
			base.OnLoad(e);

			SizeChanged += (o, _e) => { updateViewingArea(); };

			EventHandler showStatus = (o, _e) =>
			{
				if (_data == null)
					return;

				ApplicationFactory.StatusManager.ShowHint(GetType() + Name + "mouseWheelMove", "可視化表示領域移動", "ホイール", ",矢印キー");
			};
			EventHandler hideStatus = (o, _e) =>
			{
				if (_data == null)
					return;

				ApplicationFactory.StatusManager.HideHint(GetType() + Name + "mouseWheelMove");
			};

			MouseEnter += showStatus;
			MouseLeave += hideStatus;
		}

		public override void SetData(TraceLogVisualizerData data)
		{
			base.SetData(data);

			Thread th = new Thread(new ThreadStart(() =>
			{
				TimeLine = _data.SettingData.TraceLogDisplayPanelSetting.TimeLine;

				TimeLine.ViewingAreaChanged += timeLineViewingAreaChanged;

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

			int w = Width - 2;

			_fx = TimeLine.FromTime.ToX(TimeLine.MinTime, TimeLine.MaxTime, w);
			_tx = TimeLine.ToTime.ToX(TimeLine.MinTime, TimeLine.MaxTime, w);

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
			foreach (TimeLineVisualizer tv in _list)
			{
				tv.WaitSetData();
			}
		}

		private void resetData()
		{
			_list.Clear();
			_num = 0;
			_rowHeight = 0;
			_fx = 0f;
			_tx = 0f;
			_lastX = 0;
			_mouseDownFX = 0;
			_cursorMode = CursorMode.Normal;
		}

		protected void ruleBecameDirty(object sender, string propertyName)
		{
			//resetData();
			//makeList();
			updateViewingArea();
			Refresh();
		}

		public override void Draw(Graphics g, Rectangle rect)
		{
			base.Draw(g, rect);

			if (_data == null || _list.Count() == 0)
				return;

			//g.FillRectangle(new SolidBrush(_scale.BackColor), new Rectangle(rect.X + 1, rect.Y, _scale.Width, _scale.Height));
			//_scale.Draw(g, new Rectangle(rect.X + 1, rect.Y, _scale.Width, _scale.Height));

			IEnumerable<TimeLineVisualizer> tlvs = _list.Where<TimeLineVisualizer>(tlv=>
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
				g.FillRectangle(Brushes.White, new Rectangle(rect.X, i * _rowHeight + _scale.Height, Width - 1, _rowHeight));
				g.DrawRectangle(Pens.LightGray, new Rectangle(rect.X, i * _rowHeight + _scale.Height, Width - 1, _rowHeight));
				tl.Draw(g, new Rectangle(rect.X + 1, i * _rowHeight + 1 + _scale.Height, _scale.Width - 1, _rowHeight - 2));
				i++;
			}

			RectangleF r = new RectangleF(_fx, rect.Y + 1, _tx - _fx - 1 < 0 ? 1 : _tx - _fx, rect.Height - 2);

			g.FillRectangle(new SolidBrush(Color.FromArgb(50, Color.Purple)), r);
			g.DrawRectangle(new Pen() { Color = Color.FromArgb(100, Color.Purple), Width=1.0f }, r.X, r.Y, r.Width, r.Height);

		}

		protected override void OnMouseDown(MouseEventArgs e)
		{
			base.OnMouseDown(e);

			if (_data == null)
				return;

			if (e.Button == MouseButtons.Left)
			{
				if(_cursorMode == CursorMode.Move)
					Cursor = HandHoldCursor;

				_lastX = e.X - _scale.Location.X;
				_mouseDownFX = (int)_fx;
			}
		}

		protected override void OnMouseUp(MouseEventArgs e)
		{
			base.OnMouseUp(e);

			if (_data == null)
				return;

			if (e.Button == MouseButtons.Left)
			{
				if (_cursorMode == CursorMode.Move)
					Cursor = HandCursor;
			}
		}

		protected override void OnMouseMove(MouseEventArgs e)
		{
			base.OnMouseMove(e);

			if (_data == null)
				return;

			int x = e.X - _scale.Location.X;

			if (e.Button == MouseButtons.Left)
			{
				ApplicationFactory.StatusManager.HideHint(GetType() + Name + "viewingAreaMove");
				ApplicationFactory.StatusManager.HideHint(GetType() + Name + "viewingAreaSizeChange");

				if (_cursorMode == CursorMode.ResizeL)
				{
					try
					{
						TimeLine.SetTime(Time.FromX(TimeLine.MinTime, TimeLine.MaxTime, _scale.Width, (int)x).Truncate(), TimeLine.ToTime);
						Refresh();
					}
					catch { }
				}
				else
				if (_cursorMode == CursorMode.ResizeR)
				{
					try
					{
						TimeLine.SetTime(TimeLine.FromTime, Time.FromX(TimeLine.MinTime, TimeLine.MaxTime, _scale.Width, (int)x).Truncate());
						Refresh();
					}
					catch { }
				}
				else if (_cursorMode == CursorMode.Move)
				{
					int _x = _mouseDownFX + x - _lastX;
					TimeLine.MoveBySettingFromTime(TimeLine.MinTime + new Time(Math.Truncate(Time.FromX(TimeLine.MinTime, TimeLine.MaxTime, _scale.Width, _x).Value).ToString(), _data.ResourceData.TimeRadix));
				}
				else if (_cursorMode == CursorMode.Normal)
				{

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
							_cursorMode = CursorMode.ResizeL;
						if (Math.Abs(x - _tx) < 1)
							_cursorMode = CursorMode.ResizeR;

						Cursor = Cursors.SizeWE;

					}
				}
				else if (x > _fx && x < _tx)
				{
					ApplicationFactory.StatusManager.ShowHint(GetType() + Name + "viewingAreaMove", "可視化表示領域移動", "ドラッグ");
					ApplicationFactory.StatusManager.HideHint(GetType() + Name + "viewingAreaSizeChange");
					if (_cursorMode != CursorMode.Move)
					{
						_cursorMode = CursorMode.Move;
						Cursor = HandCursor;
					}
				}
				else if (Cursor != Cursors.Default)
				{
					ApplicationFactory.StatusManager.HideHint(GetType() + Name + "viewingAreaMove");
					ApplicationFactory.StatusManager.HideHint(GetType() + Name + "viewingAreaSizeChange");

					Cursor = Cursors.Default;
					_cursorMode = CursorMode.Normal;
				}
			}

		}

		protected override void OnMouseWheel(MouseEventArgs e)
		{
			if (_data == null)
				return;

			base.OnMouseWheel(e);

			if (e.Delta < 0)
			{
				_tx = _tx + _delta <= _scale.Location.X + _scale.Width ? _tx + _delta : _scale.Location.X + _scale.Width;
				TimeLine.MoveBySettingToTime(TimeLine.MinTime + new Time(Math.Truncate(Time.FromX(TimeLine.MinTime, TimeLine.MaxTime, _scale.Width, (int)_tx).Value).ToString(), _data.ResourceData.TimeRadix));
			}
			else if (e.Delta > 0)
			{
				_fx = _fx - _delta <= _scale.Location.X ? _scale.Location.X : _fx - _delta;
				TimeLine.MoveBySettingFromTime(TimeLine.MinTime + new Time(Math.Truncate(Time.FromX(TimeLine.MinTime, TimeLine.MaxTime, _scale.Width, (int)_fx).Value).ToString(), _data.ResourceData.TimeRadix));
			}
		}

		protected override void OnMouseLeave(EventArgs e)
		{
			base.OnMouseLeave(e);

			ApplicationFactory.StatusManager.HideHint(GetType() + Name + "viewingAreaMove");
			ApplicationFactory.StatusManager.HideHint(GetType() + Name + "viewingAreaSizeChange");
		}

		protected override bool ProcessDialogKey(Keys keyData)
		{
			if (_data == null)
				return base.ProcessDialogKey(keyData);

			switch (keyData)
			{
				case Keys.Right:
				case Keys.Down:
					_tx = _tx + _delta <= _scale.Location.X + _scale.Width ? _tx + _delta : _scale.Location.X + _scale.Width;
					TimeLine.MoveBySettingToTime(TimeLine.MinTime + new Time(Math.Truncate(Time.FromX(TimeLine.MinTime, TimeLine.MaxTime, _scale.Width, (int)_tx).Value).ToString(), _data.ResourceData.TimeRadix));
					return true;
				case Keys.Up:
				case Keys.Left:
					_fx = _fx - _delta <= _scale.Location.X ? _scale.Location.X : _fx - _delta;
					TimeLine.MoveBySettingFromTime(TimeLine.MinTime + new Time(Math.Truncate(Time.FromX(TimeLine.MinTime, TimeLine.MaxTime, _scale.Width, (int)_fx).Value).ToString(), _data.ResourceData.TimeRadix));
					return true;
				default:
					return base.ProcessDialogKey(keyData);
			}
		}

		private void timeLineViewingAreaChanged(object sender, GeneralChangedEventArgs<TimeLine> e)
		{
			if (e.Old.FromTime != e.New.FromTime)
				_fx = TimeLine.FromTime.ToX(TimeLine.MinTime, TimeLine.MaxTime, _scale.Width);

			if (e.Old.ToTime != e.New.ToTime)
				_tx = TimeLine.ToTime.ToX(TimeLine.MinTime, TimeLine.MaxTime, _scale.Width);

			Refresh();
		}

	}
}