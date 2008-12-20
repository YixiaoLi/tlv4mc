using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using NU.OJL.MPRTOS.TLV.Base;

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
				ApplicationFactory.StatusManager.ShowHint(GetType() + Name + "mouseWheelBigMove", "大きく可視化表示領域移動", "Shift", "ホイール", ",Shift", "矢印キー");
			};
			EventHandler hideStatus = (o, _e) =>
			{
				if (_data == null)
					return;

				ApplicationFactory.StatusManager.HideHint(GetType() + Name + "mouseWheelMove");
				ApplicationFactory.StatusManager.HideHint(GetType() + Name + "mouseWheelBigMove");
			};

			MouseEnter += showStatus;
			MouseLeave += hideStatus;
		}

		public override void SetData(TraceLogVisualizerData data)
		{
			base.SetData(data);

			TimeLine = _data.SettingData.TraceLogDisplayPanelSetting.TimeLine;

			TimeLine.ViewingAreaChanged += timeLineViewingAreaChanged;

			makeList();

			_scale.SetData(_data);
			_scale.TimeLine = new TimeLine(_data.TraceLogData.MinTime, _data.TraceLogData.MaxTime);

			_data.SettingData.ResourceExplorerSetting.BecameDirty += ruleBecameDirty;
			_data.SettingData.VisualizeRuleExplorerSetting.BecameDirty += ruleBecameDirty;

			updateViewingArea();

			Refresh();
		}

		private void updateViewingArea()
		{
			if (_data == null)
				return;

			_fx = TimeLine.FromTime.ToX(TimeLine.MinTime, TimeLine.MaxTime, Width - 2);
			_tx = TimeLine.ToTime.ToX(TimeLine.MinTime, TimeLine.MaxTime, Width - 2);
		}

		public override void ClearData()
		{
			base.ClearData();
			resetData();
		}

		private void makeList()
		{

			List<string> targets = new List<string>();

			IEnumerable<VisualizeRule> rules = _data.VisualizeData.VisualizeRules.Where<VisualizeRule>(r =>
			{
				if (r.Target != null)
				{
					if (targets.Contains(r.Target))
						return false;

					targets.Add(r.Target);

					return false;
				}
				else
				{
					if (_data.SettingData.VisualizeRuleExplorerSetting.VisualizeRuleVisibility.ContainsKey(r.Name))
						return _data.SettingData.VisualizeRuleExplorerSetting.VisualizeRuleVisibility.GetValue(r.Name);
					else
						return ApplicationData.Setting.DefaultResourceVisible;
				}
			});

			IEnumerable<Resource> ress = _data.ResourceData.Resources.Where<Resource>(r => targets.Contains(r.Type) && 
				_data.SettingData.ResourceExplorerSetting.ResourceVisibility.ContainsKey(r.Type, r.Name)
				? _data.SettingData.ResourceExplorerSetting.ResourceVisibility.GetValue(r.Type, r.Name)
				: ApplicationData.Setting.DefaultResourceVisible);

			_num = rules.Count() + ress.Count();

			_rowHeight = (Height - _scale.Height) / _num;

			foreach (VisualizeRule rule in rules)
			{
				if (rule.Target == null)
				{
					_list.Add(new TimeLineVisualizer(rule));
				}
			}
			foreach (Resource res in ress)
			{
				_list.Add(new TimeLineVisualizer(res));
			}
			foreach(TimeLineVisualizer tv in _list)
			{
				tv.SetData(_data);
				tv.TimeLine = new TimeLine(_data.TraceLogData.MinTime, _data.TraceLogData.MaxTime);
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
			resetData();
			makeList();
			updateViewingArea();
			Refresh();
		}

		public override void Draw(PaintEventArgs e)
		{
			base.Draw(e);

			if (_data == null || _list.Count() == 0)
				return;

			_rowHeight = (Height - _scale.Height) / _num;

			e.Graphics.FillRectangle(new SolidBrush(_scale.BackColor), new Rectangle(e.ClipRectangle.X + 1, e.ClipRectangle.Y, Width - 4, _scale.Height));
			_scale.Draw(new PaintEventArgs(e.Graphics, new Rectangle(e.ClipRectangle.X + 1, e.ClipRectangle.Y, e.ClipRectangle.Width - 4, _scale.Height)));

			int i = 0;
			foreach(TimeLineVisualizer tl in _list)
			{
				e.Graphics.DrawRectangle(Pens.LightGray, new Rectangle(e.ClipRectangle.X, i * _rowHeight + _scale.Height, Width - 2, _rowHeight));
				tl.Draw(new PaintEventArgs(e.Graphics, new Rectangle(e.ClipRectangle.X + 1, i * _rowHeight + 1 + _scale.Height, Width - 4, _rowHeight - 2)));
				i++;
			}

			RectangleF rect = new RectangleF(_fx + 1, e.ClipRectangle.Y + 1, _tx - _fx, e.ClipRectangle.Height - 2);

			e.Graphics.FillRectangle(new SolidBrush(Color.FromArgb(50, Color.Purple)), rect);
			e.Graphics.DrawRectangle(new Pen() { Color = Color.FromArgb(100, Color.Purple), Width=1.0f }, rect.X, rect.Y, rect.Width, rect.Height);

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

				_lastX = e.X - 1;
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

			int x = e.X - 2;

			if (e.Button == MouseButtons.Left)
			{
				ApplicationFactory.StatusManager.HideHint(GetType() + Name + "viewingAreaMove");
				ApplicationFactory.StatusManager.HideHint(GetType() + Name + "viewingAreaSizeChange");

				if (_cursorMode == CursorMode.ResizeL)
				{
					if (x < _tx)
					{
						//_fx = x;
						TimeLine.SetTime(Time.FromX(TimeLine.MinTime, TimeLine.MaxTime, Width - 4, (int)x).Truncate(), TimeLine.ToTime);
						Refresh();
					}
				}
				else
				if (_cursorMode == CursorMode.ResizeR)
				{
					if (x > _fx)
					{
						//_tx = x;
						TimeLine.SetTime(TimeLine.FromTime, Time.FromX(TimeLine.MinTime, TimeLine.MaxTime, Width - 4, (int)x).Truncate());
						Refresh();
					}
				}
				else if (_cursorMode == CursorMode.Move)
				{
					int _x = _mouseDownFX + x - _lastX;
					TimeLine.MoveBySettingFromTime(TimeLine.MinTime + new Time(Math.Truncate(Time.FromX(TimeLine.MinTime, TimeLine.MaxTime, Width - 4, _x).Value).ToString(), _data.ResourceData.TimeRadix));
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

			int d = (Control.ModifierKeys & Keys.Shift) == Keys.Shift ? 10 * _delta : 3 * _delta;

			if (e.Delta < 0)
			{
				_tx = _tx + d <= Width - 2 ? _tx + d : Width - 2;
				TimeLine.MoveBySettingToTime(TimeLine.MinTime + new Time(Math.Truncate(Time.FromX(TimeLine.MinTime, TimeLine.MaxTime, Width - 1, (int)_tx).Value).ToString(), _data.ResourceData.TimeRadix));
			}
			else if (e.Delta > 0)
			{
				_fx = _fx - d >= 1 ? _fx - d : 1;
				TimeLine.MoveBySettingFromTime(TimeLine.MinTime + new Time(Math.Truncate(Time.FromX(TimeLine.MinTime, TimeLine.MaxTime, Width - 1, (int)_fx).Value).ToString(), _data.ResourceData.TimeRadix));
			}
		}

		protected override void OnMouseLeave(EventArgs e)
		{
			base.OnMouseLeave(e);

			ApplicationFactory.StatusManager.HideHint(GetType() + Name + "viewingAreaMove");
			ApplicationFactory.StatusManager.HideHint(GetType() + Name + "viewingAreaSizeChange");
		}

		protected override void OnPreviewKeyDown(PreviewKeyDownEventArgs e)
		{
			if (_data == null)
				return;

			base.OnPreviewKeyDown(e);

			int d = (Control.ModifierKeys & Keys.Shift) == Keys.Shift ? 10 * _delta : _delta;

			switch (e.KeyCode)
			{
				case Keys.Right:
				case Keys.Down:
					_tx = _tx + d <= Width - 4 ? _tx + d : Width - 4;
					TimeLine.MoveBySettingToTime(TimeLine.MinTime + new Time(Math.Truncate(Time.FromX(TimeLine.MinTime, TimeLine.MaxTime, Width - 4, (int)_tx).Value).ToString(), _data.ResourceData.TimeRadix));
					break;
				case Keys.Up:
				case Keys.Left:
					_fx = _fx - d >= 2 ? _fx - d: 2;
					TimeLine.MoveBySettingFromTime(TimeLine.MinTime + new Time(Math.Truncate(Time.FromX(TimeLine.MinTime, TimeLine.MaxTime, Width - 4, (int)_fx).Value).ToString(), _data.ResourceData.TimeRadix));
					break;
			}
		}

		private void timeLineViewingAreaChanged(object sender, GeneralChangedEventArgs<TimeLine> e)
		{
			if (e.Old.FromTime != e.New.FromTime)
				_fx = TimeLine.FromTime.ToX(TimeLine.MinTime, TimeLine.MaxTime, Width - 4);

			if (e.Old.ToTime != e.New.ToTime)
				_tx = TimeLine.ToTime.ToX(TimeLine.MinTime, TimeLine.MaxTime, Width - 4);

			Refresh();
		}

	}
}