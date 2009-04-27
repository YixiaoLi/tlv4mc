/*
 *  TLV - Trace Log Visualizer
 *
 *  Copyright (C) 2008,2009 by Embedded and Real-Time Systems Laboratory
 *              Graduate School of Information Science, Nagoya Univ., JAPAN
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
using System.Text.RegularExpressions;
using System.Windows.Forms;
using NU.OJL.MPRTOS.TLV.Base;
using NU.OJL.MPRTOS.TLV.Base.Controls;
using NU.OJL.MPRTOS.TLV.Third;
using System.Collections;

namespace NU.OJL.MPRTOS.TLV.Core.Controls
{
	public partial class TraceLogDisplayPanel : TimeLineControl
	{
		private List<TimeLineVisualizer> _list = new List<TimeLineVisualizer>();
		private bool _hScrollUpdateFlag = true;
		private int _timeLineX = 0;
		private int _timeLineWidth = 0;
		private string _timeScale = string.Empty;
		public override int TimeLineX
		{
			get { return _timeLineX; }
			set
			{
				if (_timeLineX != value)
				{
					_timeLineX = value;

					topTimeLineScale.Location = new System.Drawing.Point(_timeLineX, topTimeLineScale.Location.Y);
					bottomTimeLineScale.Location = new System.Drawing.Point(_timeLineX, bottomTimeLineScale.Location.Y);
					hScrollBar.Location = new System.Drawing.Point(_timeLineX, hScrollBar.Location.Y);
				}
			}
		}
		public override int TimeLineWidth
		{
			get { return _timeLineWidth; }
			set
			{
				if (_timeLineWidth != value)
				{
					_timeLineWidth = value;

					topTimeLineScale.Width = _timeLineWidth;
					bottomTimeLineScale.Width = _timeLineWidth;
					hScrollBar.Width = _timeLineWidth;
				}
			}
		}
		public int MaxHeight
		{
			get
			{

				return toolStripContainer.ContentPanel.Height - 2
					- topTimeLineScale.Height
					- bottomTimeLineScale.Height
					- hScrollBar.Height;
			}
		}

		public TraceLogDisplayPanel()
		{
			InitializeComponent();

			ResizeRedraw = true;

			hScrollBar.Minimum = 1;
			hScrollBar.Maximum = int.MaxValue;
			hScrollBar.Value = hScrollBar.Minimum;
			viewingAreaToolStrip.Enabled = false;

			imageList.Images.Add("visualize", Properties.Resources.visualize);
			imageList.Images.Add("resource", Properties.Resources.resource);
			imageList.Images.Add("bhr2bhr", Properties.Resources.bhr2bhr);
			imageList.Images.Add("atr2atr", Properties.Resources.atr2atr);
			imageList.Images.Add("atr2bhr", Properties.Resources.atr2bhr);
			imageList.Images.Add("bhr2atr", Properties.Resources.bhr2atr);
			imageList.Images.Add("attribute", Properties.Resources.attribute);
			imageList.Images.Add("behavior", Properties.Resources.behavior);
			imageList.Images.Add("warning", Properties.Resources.warning);
		}

		public override void SetData(TraceLogVisualizerData data)
		{
			base.SetData(data);

			viewingTimeRangeFromTextBox.Radix = _data.ResourceData.TimeRadix;
			viewingTimeRangeToTextBox.Radix = _data.ResourceData.TimeRadix;

			if (_data.SettingData.TraceLogDisplayPanelSetting.TimeLine == null)
				_data.SettingData.TraceLogDisplayPanelSetting.TimeLine = new TimeLine(_data.TraceLogData.MinTime, _data.TraceLogData.MaxTime);
			
			TimeLine = _data.SettingData.TraceLogDisplayPanelSetting.TimeLine;

			topTimeLineScale.TimeLine = TimeLine;
			bottomTimeLineScale.TimeLine = TimeLine;
			topTimeLineScale.TimePerScaleMarkChanged += timeLineScaleTimePerScaleMarkChanged;

			viewingTimeRangeFromTextBox.Minimum = TimeLine.MinTime.Value;
			viewingTimeRangeFromTextBox.Maximum = TimeLine.ToTime.Value - 1;
			viewingTimeRangeToTextBox.Minimum = TimeLine.FromTime.Value + 1;
			viewingTimeRangeToTextBox.Maximum = TimeLine.MaxTime.Value;

			_data.SettingData.ResourceExplorerSetting.BecameDirty += resourceExplorerSettingBecameDirty;
			_data.SettingData.VisualizeRuleExplorerSetting.BecameDirty += visualizeRuleExplorerSettingBecameDirty;
			_data.SettingData.TraceLogDisplayPanelSetting.TimeLine.ViewingAreaChanged += timeLineViewingAreaChanged;
			_data.SettingData.TraceLogDisplayPanelSetting.BecameDirty += traceLogDisplayPanelSettingBecameDirty;

			_timeScale = _data.ResourceData.TimeScale;
			timePerSclaeUnitLabel.Text = _timeScale + "/目盛り";

			if(!_data.SettingData.TraceLogDisplayPanelSetting.TimePerScaleMark.IsEmpty)
				timePerSclaeLabel.Text = _data.SettingData.TraceLogDisplayPanelSetting.TimePerScaleMark.ToString();
			autoResizeRowHeightToolStripButton.Checked = _data.SettingData.TraceLogDisplayPanelSetting.AutoResizeRowHeight;
			pixelPerScaleToolStripTextNumericUpDown.Value = _data.SettingData.TraceLogDisplayPanelSetting.PixelPerScaleMark;
			rowHeightToolStripTextNumericUpDown.Value = _data.SettingData.TraceLogDisplayPanelSetting.RowHeight;
			viewingTimeRangeFromTextBox.Text = TimeLine.FromTime.ToString();
			viewingTimeRangeToTextBox.Text = TimeLine.ToTime.ToString();
			viewingTimeRangeFromScaleLabel.Text = _timeScale;
			viewingTimeRangeToScaleLabel.Text = _timeScale;
			viewableSpanTextBox.Visible = true;
			viewableSpanTextBox.Text = TimeLine.MinTime.ToString() + " 〜 " + TimeLine.MaxTime.ToString() + " " + _timeScale;
			viewableSpanTextBox.Width = TextRenderer.MeasureText(viewableSpanTextBox.Text, viewableSpanTextBox.Font).Width;
			viewingAreaToolStrip.Enabled = true;

			setNodes();

			foreach (TimeLineVisualizer tlv in _list)
			{
				tlv.SetData(_data);
				tlv.TimeLine = TimeLine;
			}

			setRowHeight(treeGridView.Nodes.Values, _data.SettingData.TraceLogDisplayPanelSetting.RowHeight);

			if (autoResizeRowHeightToolStripButton.Checked)
				autoResizeRowHeight();

			treeGridViewRowChanged(this, EventArgs.Empty);

			hScrollBarChangeRateUpdate();

			hScrollBarValueUpdate();

			_timeLineMarkerManager.SelectedMarkerChanged += (o, _e) => { timeLineRedraw(); };

			timeLineRedraw();

		}

		private void setNodes()
		{
			// 可視化ルール行の追加
			// ノード名「ルール.Name」-「イベント.Name」
			foreach (VisualizeRule vizRule in _data.VisualizeData.VisualizeRules.Where<VisualizeRule>(v => !v.IsBelongedTargetResourceType()))
			{
				TimeLineVisualizer tlv = new TimeLineVisualizer(new TimeLineEvents(vizRule));
				_list.Add(tlv);
				treeGridView.Add(vizRule.Name, vizRule.DisplayName, tlv);
				treeGridView.Nodes[vizRule.Name].Visible = _data.SettingData.VisualizeRuleExplorerSetting.VisualizeRuleVisibility.ContainsKey(vizRule.Name) ? _data.SettingData.VisualizeRuleExplorerSetting.VisualizeRuleVisibility.GetValue(vizRule.Name) : ApplicationData.Setting.DefaultVisualizeRuleVisible;
				treeGridView.Nodes[vizRule.Name].Image = imageList.Images["visualize"];
				
				// 可視化ルール内のイベント行の追加
				foreach (Event e in vizRule.Shapes)
				{
					TimeLineVisualizer _tlv = new TimeLineVisualizer(new TimeLineEvents(vizRule, e));
					_list.Add(_tlv);
					treeGridView.Nodes[vizRule.Name].Add(e.Name, e.DisplayName, _tlv);
					treeGridView.Nodes[vizRule.Name].Nodes[e.Name].Visible = _data.SettingData.VisualizeRuleExplorerSetting.VisualizeRuleVisibility.ContainsKey(vizRule.Name, e.Name) ? _data.SettingData.VisualizeRuleExplorerSetting.VisualizeRuleVisibility.GetValue(vizRule.Name, e.Name) : ApplicationData.Setting.DefaultVisualizeRuleVisible;
					treeGridView.Nodes[vizRule.Name].Nodes[e.Name].Image = imageList.Images[e.getImageKey()];
				}
			}

			// リソースに属する可視化ルールの追加
			// ノード名「リソース.Name」-「ルール.Name」-「イベント.Name」
			foreach (VisualizeRule vizRule in _data.VisualizeData.VisualizeRules.Where<VisualizeRule>(v => v.IsBelongedTargetResourceType()))
			{
				foreach (Resource res in _data.ResourceData.Resources.Where<Resource>(r => r.Type == vizRule.Target))
				{
					if (!treeGridView.Nodes.ContainsKey(res.Name))
					{
						TimeLineVisualizer _tlv = new TimeLineVisualizer(new TimeLineEvents(res));
						_list.Add(_tlv);
						treeGridView.Add(res.Name, res.DisplayName, _tlv);
						treeGridView.Nodes[res.Name].Visible = _data.SettingData.ResourceExplorerSetting.ResourceVisibility.ContainsKey(res.Name) ? _data.SettingData.ResourceExplorerSetting.ResourceVisibility.GetValue(res.Name) : ApplicationData.Setting.DefaultResourceVisible;
						treeGridView.Nodes[res.Name].Image = imageList.Images["resource"];
					}

					TimeLineVisualizer tlv = new TimeLineVisualizer(new TimeLineEvents(vizRule, res));
					_list.Add(tlv);
					treeGridView.Nodes[res.Name].Add(vizRule.Name, vizRule.DisplayName, tlv);
					treeGridView.Nodes[res.Name].Nodes[vizRule.Name].Visible = treeGridView.Nodes[res.Name].Visible && ApplicationData.FileContext.Data.SettingData.VisualizeRuleExplorerSetting.VisualizeRuleVisibility.ContainsKey(vizRule.Name) ? _data.SettingData.VisualizeRuleExplorerSetting.VisualizeRuleVisibility.GetValue(vizRule.Name) : ApplicationData.Setting.DefaultVisualizeRuleVisible;
					treeGridView.Nodes[res.Name].Nodes[vizRule.Name].Image = imageList.Images["visualize"];

					foreach (Event e in vizRule.Shapes)
					{
						TimeLineVisualizer _tlv = new TimeLineVisualizer(new TimeLineEvents(e, res));
						_list.Add(_tlv);
						treeGridView.Nodes[res.Name].Nodes[vizRule.Name].Add(e.Name, e.DisplayName, _tlv);
						treeGridView.Nodes[res.Name].Nodes[vizRule.Name].Nodes[e.Name].Visible = treeGridView.Nodes[res.Name].Nodes[vizRule.Name].Visible && _data.SettingData.VisualizeRuleExplorerSetting.VisualizeRuleVisibility.ContainsKey(vizRule.Name, e.Name) ? _data.SettingData.VisualizeRuleExplorerSetting.VisualizeRuleVisibility.GetValue(vizRule.Name, e.Name) : ApplicationData.Setting.DefaultVisualizeRuleVisible;
						treeGridView.Nodes[res.Name].Nodes[vizRule.Name].Nodes[e.Name].Image = imageList.Images[e.getImageKey()]; ;
					}
				}
			}
		}

		public override void ClearData()
		{
			base.ClearData();

			timePerSclaeLabel.Text = string.Empty;
			pixelPerScaleToolStripTextNumericUpDown.Text = string.Empty;
			viewingTimeRangeFromTextBox.Text = string.Empty;
			viewingTimeRangeToTextBox.Text = string.Empty;
			viewingTimeRangeFromScaleLabel.Text = string.Empty;
			viewingTimeRangeToScaleLabel.Text = string.Empty;
			viewingTimeRangeFromScaleLabel.Text = string.Empty;
			viewingTimeRangeToScaleLabel.Text = string.Empty;
			viewableSpanTextBox.Text = string.Empty;
			viewableSpanTextBox.Width = 30;
			viewableSpanTextBox.Visible = false;
			viewingAreaToolStrip.Enabled = false;

			_timeScale = string.Empty;
			_hScrollUpdateFlag = false;
			hScrollBar.Value = hScrollBar.Minimum;
			_hScrollUpdateFlag = true;
			_list.Clear();
			treeGridView.Clear();
		}

		protected override void OnLoad(EventArgs e)
		{
			base.OnLoad(e);
			this.ApplyNativeScroll();

			#region treeGridView初期化
			treeGridView.AddColumn(new TreeGridViewColumn() { Name = "resourceName", HeaderText = "リソース" });
			//treeGridView.AddColumn(new DataGridViewTextBoxColumn() { Name = "value", HeaderText = "値" });
			treeGridView.AddColumn(new TimeLineColumn() { Name = "timeLine", HeaderText = "タイムライン", AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill });

			treeGridView.DataGridView.ColumnHeadersVisible = false;
			treeGridView.DataGridView.MultiSelect = false;

			treeGridView.RowHeightChanged += treeGridViewRowChanged;
			treeGridView.RowCountChanged += treeGridViewRowChanged;
			SizeChanged += treeGridViewRowChanged;
			SizeChanged += (o, _e) =>
				{
					rowHeightToolStripTextNumericUpDown.Maximum = MaxHeight - 1;
					autoResizeRowHeight();
				};

			treeGridView.DataGridView.ColumnWidthChanged += (o, _e) =>
			{
				int w = 0;
				for (int i = 0; i < treeGridView.DataGridView.Columns["timeLine"].Index; i++)
				{
					w += treeGridView.DataGridView.Columns[i].Width;
				}
				TimeLineX = w + 2;
				TimeLineWidth = treeGridView.DataGridView.Columns["timeLine"].Width - 1;
			};
			treeGridView.DataGridView.ScrollBars = ScrollBars.Vertical;

			treeGridView.DataGridView.CellPainting += (o, _e) =>
			{
				_e.Paint(_e.ClipBounds, _e.PaintParts & ~DataGridViewPaintParts.Focus);
				_e.Handled = true;
			};

			treeGridView.DataGridView.Paint += (o, _e) =>
			{
				Rectangle rect = new Rectangle(_timeLineX - 1, _e.ClipRectangle.Y, _timeLineWidth, _e.ClipRectangle.Width);
				if (_data != null)
					DrawCursor(_e.Graphics, _data.SettingData.TraceLogDisplayPanelSetting.CursorColor, ApplicationFactory.BlackBoard.CursorTime);
				if (_data != null && _data.SettingData != null)
				{
					foreach(TimeLineMarker tlm in _globalTimeLineMarkers)
					{
						DrawMarker(_e.Graphics, tlm);
					}
				}
			};

			treeGridView.DataGridView.MouseMove += (o, _e) =>
			{
				if (_e.X > _timeLineX && TimeLine != null)
				{
					OnMouseMove(_e);
				}
			};

			treeGridView.DataGridView.MouseDown += (o, _e) =>
			{
				if (_e.X > _timeLineX && TimeLine != null)
				{
					OnMouseDown(_e);
				}
			};

			treeGridView.DataGridView.MouseUp += (o, _e) =>
			{
				if (_e.X > _timeLineX && TimeLine != null)
				{
					OnMouseUp(_e);
				}
			};

			treeGridView.DataGridView.MouseDoubleClick += (o, _e) =>
			{
				if (_e.X > _timeLineX && TimeLine != null)
				{
					OnMouseDoubleClick(_e);
				}
			};

			//treeGridView.DataGridView.MouseClick += (o, _e) =>
			//{
			//    int x = _e.X - _timeLineX + 1;

			//    if (TimeLine == null)
			//        return;

			//    Time t = Time.FromX(TimeLine.FromTime, TimeLine.ToTime, _timeLineWidth, x);
			//    Time b = Time.FromX(TimeLine.FromTime, TimeLine.ToTime, _timeLineWidth, x - 5);
			//    Time a = Time.FromX(TimeLine.FromTime, TimeLine.ToTime, _timeLineWidth, x + 5);

			//    TimeLineMarker foucsMarker = _globalTimeLineMarkers.FirstOrDefault<TimeLineMarker>(m => m.Time > b && a > m.Time);

			//    if ((Control.ModifierKeys & Keys.Control) != Keys.Control)
			//    {
			//        _timeLineMarkerManager.ResetSelect();
			//    }

			//    if (foucsMarker != null)
			//    {
			//        foucsMarker.SelectToggle();
			//    }
			//};

			//treeGridView.DataGridView.MouseDoubleClick += (o, _e) =>
			//{
			//    if (_e.X > _timeLineX)
			//    {
			//        Time time = Time.FromX(TimeLine.FromTime, TimeLine.ToTime, _timeLineWidth, _e.X - _timeLineX + 1);
			//        Time span = _data.SettingData.TraceLogDisplayPanelSetting.TimeLine.ViewingSpan / 2;

			//        _data.SettingData.TraceLogDisplayPanelSetting.TimeLine.SetTime((time - span).Truncate(), (time + span).Truncate());
			//        _data.SettingData.TraceLogViewerSetting.FirstDisplayedTime = time;
			//    }
			//};

			#endregion

			#region StatusManager初期化
			EventHandler onTimeLineEvent = (o, _e) =>
			{
				Focus();
				ApplicationFactory.StatusManager.ShowHint(GetType() + Name + "mouseWheelMove", "可視化表示領域移動", "Ctrl", "ホイール", ",矢印キー");
				ApplicationFactory.StatusManager.ShowHint(GetType() + Name + "mouseWheelScaleRatioChange", "拡大縮小", "Shift", "ホイール");
				ApplicationFactory.StatusManager.ShowHint(GetType() + Name + "mouseDoubleClickMove", "移動", "左ダブルクリック");
			};
			EventHandler offTimeLineEvent = (o, _e) =>
			{
				ApplicationFactory.StatusManager.HideHint(GetType() + Name + "mouseWheelMove");
				ApplicationFactory.StatusManager.HideHint(GetType() + Name + "mouseWheelScaleRatioChange");
				ApplicationFactory.StatusManager.HideHint(GetType() + Name + "mouseDoubleClickMove");
			};

			treeGridView.DataGridView.MouseEnter += onTimeLineEvent;
			treeGridView.DataGridView.MouseLeave += offTimeLineEvent;
			topTimeLineScale.MouseEnter += onTimeLineEvent;
			topTimeLineScale.MouseLeave += offTimeLineEvent;
			bottomTimeLineScale.MouseEnter += onTimeLineEvent;
			bottomTimeLineScale.MouseLeave += offTimeLineEvent;
			hScrollBar.MouseEnter += onTimeLineEvent;
			hScrollBar.MouseLeave += offTimeLineEvent;
			MouseEnter += onTimeLineEvent;
			MouseLeave += offTimeLineEvent;
			#endregion

			#region viewingTimeRangeTextBox初期化

			viewingTimeRangeFromTextBox.TextChanged += (o, _e) =>
				{
					if (viewingTimeRangeFromTextBox.Width == 0)
						viewingTimeRangeFromTextBox.Width = 50;
				};

			viewingTimeRangeToTextBox.TextChanged += (o, _e) =>
				{
					if (viewingTimeRangeToTextBox.Width == 0)
						viewingTimeRangeToTextBox.Width = 50;
				};

			viewingTimeRangeFromTextBox.Validated += (o, _e) =>
				{
					if (TimeLine == null)
						return;

					Time lastValue = TimeLine.FromTime;
					try
					{
						Time t = new Time(viewingTimeRangeFromTextBox.Text, _timeRadix);
						if (t.IsEmpty || t < TimeLine.MinTime || t > TimeLine.ToTime)
							throw new Exception();
						TimeLine.SetTime(t, Time.Empty);
					}
					catch
					{
						viewingTimeRangeFromTextBox.Text = lastValue.ToString();
						TimeLine.SetTime(lastValue, Time.Empty);
					}
				};
			viewingTimeRangeToTextBox.Validated += (o, _e) =>
				{
					if (TimeLine == null)
						return;

					Time lastValue = TimeLine.ToTime;
					try
					{
						Time t = new Time(viewingTimeRangeToTextBox.Text, _timeRadix);
						if (t.IsEmpty || t > TimeLine.MaxTime || t < TimeLine.FromTime)
							throw new Exception();
						TimeLine.SetTime(Time.Empty, t);
					}
					catch
					{
						viewingTimeRangeToTextBox.Text = lastValue.ToString();
						TimeLine.SetTime(Time.Empty, lastValue);
					}
				};
			#endregion

			#region pixelPerScaleToolStripTextNumericUpDown初期化

			pixelPerScaleToolStripTextNumericUpDown.Validated += (o, _e) =>
				{
					_data.SettingData.TraceLogDisplayPanelSetting.PixelPerScaleMark = (int)pixelPerScaleToolStripTextNumericUpDown.Value;
				};

			#endregion

			#region rowSizeToolStripTextNumericUpDown初期化

			autoResizeRowHeightToolStripButton.CheckedChanged += (o, _e) =>
				{
					_data.SettingData.TraceLogDisplayPanelSetting.AutoResizeRowHeight = autoResizeRowHeightToolStripButton.Checked;
				};

			rowHeightToolStripTextNumericUpDown.Validated += (o, _e) =>
				{
					int h = (int)rowHeightToolStripTextNumericUpDown.Value;
					if (_data.SettingData.TraceLogDisplayPanelSetting.RowHeight != h)
					{
						_data.SettingData.TraceLogDisplayPanelSetting.RowHeight = h;
						if (autoResizeRowHeightToolStripButton.Checked)
							autoResizeRowHeightToolStripButton.Checked = false;
					}
				};

			#endregion

			#region hScrollBar初期化
			hScrollBar.ValueChanged += (o, _e) =>
				{
					if (!_hScrollUpdateFlag)
						return;

					if (hScrollBar.Value == hScrollBar.Maximum - hScrollBar.LargeChange + 1)
					{
						TimeLine.MoveBySettingToTime(TimeLine.MaxTime);
					}
					else if (hScrollBar.Value == hScrollBar.Minimum)
					{
						TimeLine.MoveBySettingFromTime(TimeLine.MinTime);
					}
					else
					{
						TimeLine.MoveBySettingFromTime((TimeLine.ViewableSpan * (decimal)hScrollBar.Value / (decimal)(hScrollBar.Maximum)).Truncate());
					}
				};
			#endregion

			#region 可視化領域移動イベント
			PreviewKeyDownEventHandler onPreviewKeyDownEventHandler = (o, _e) =>
				{
					if (_data == null)
						return;

					switch (_e.KeyCode)
					{
						case Keys.Right:
							hScrollBar.Value = ((hScrollBar.Value + hScrollBar.SmallChange) < hScrollBar.Maximum - hScrollBar.LargeChange + 1) ? hScrollBar.Value + hScrollBar.SmallChange : hScrollBar.Maximum - hScrollBar.LargeChange + 1;
							break;
						case Keys.Left:
							hScrollBar.Value = ((hScrollBar.Value - hScrollBar.SmallChange) > hScrollBar.Minimum) ? hScrollBar.Value - hScrollBar.SmallChange : hScrollBar.Minimum;
							break;
					}
				};

			MouseEventHandler onMouseWheel = (o, _e) =>
			{
				if ((Control.ModifierKeys & Keys.Control) == Keys.Control)
				{
					hScrollBar.Value =
						(_e.Delta < 0)
						? (((hScrollBar.Value + hScrollBar.SmallChange) < hScrollBar.Maximum - hScrollBar.LargeChange + 1) ? hScrollBar.Value + hScrollBar.SmallChange : hScrollBar.Maximum - hScrollBar.LargeChange + 1)
						: (_e.Delta > 0)
						? (((hScrollBar.Value - hScrollBar.SmallChange) > hScrollBar.Minimum) ? hScrollBar.Value - hScrollBar.SmallChange : hScrollBar.Minimum)
						: hScrollBar.Value;

					if (_e.GetType() == typeof(ExMouseEventArgs))
						((ExMouseEventArgs)_e).Handled = true;
				}
				else if ((Control.ModifierKeys & Keys.Shift) == Keys.Shift)
				{
					Time time = ApplicationFactory.BlackBoard.CursorTime;
					Time left = time - TimeLine.FromTime;
					Time right = TimeLine.ToTime - time;

					decimal ratio = (_e.Delta < 0)
						? 1.5m
						: (_e.Delta > 0)
						? 0.75m
						: 1m;

					_data.SettingData.TraceLogDisplayPanelSetting.TimeLine.SetTime((time - left * ratio).Round(0), (time + right * ratio).Round(0));

					if (_e.GetType() == typeof(ExMouseEventArgs))
						((ExMouseEventArgs)_e).Handled = true;
				}
			};
			treeGridView.MouseWheel += onMouseWheel;
			treeGridView.DataGridView.MouseWheel += onMouseWheel;
			topTimeLineScale.MouseWheel += onMouseWheel;
			bottomTimeLineScale.MouseWheel += onMouseWheel;
			hScrollBar.MouseWheel += onMouseWheel;

			treeGridView.PreviewKeyDown += onPreviewKeyDownEventHandler;
			treeGridView.DataGridView.PreviewKeyDown += onPreviewKeyDownEventHandler;
			topTimeLineScale.PreviewKeyDown += onPreviewKeyDownEventHandler;
			bottomTimeLineScale.PreviewKeyDown += onPreviewKeyDownEventHandler;
			hScrollBar.PreviewKeyDown += onPreviewKeyDownEventHandler;
			#endregion
		}

		protected void treeGridViewRowChanged(object sender, EventArgs e)
		{
			if (_data == null)
				return;

			int height = _data.SettingData.TraceLogDisplayPanelSetting.RowHeight;

			int allRowHeight = treeGridView.VisibleRowsCount * height;

			allRowHeight += treeGridView.DataGridView.ColumnHeadersVisible ? treeGridView.ColumnHeadersHeight : 1;


			if(allRowHeight == 1)
			{
				treeGridView.Visible = false;
				topTimeLineScale.Visible = false;
				bottomTimeLineScale.Visible = false;
				hScrollBar.Visible = false;
			}
			else
			{
				if (!treeGridView.Visible)
					treeGridView.Visible = true;
				if (!topTimeLineScale.Visible)
					topTimeLineScale.Visible = true;
				if (!bottomTimeLineScale.Visible)
					bottomTimeLineScale.Visible = true;
				if (!hScrollBar.Visible)
					hScrollBar.Visible = true;
			}

			if (allRowHeight > MaxHeight)
			{
				//treeGridView.Height = (int)((double)allRowHeight - Math.Ceiling((double)(allRowHeight - MaxHeight) / (double)height) * (double)height);
				treeGridView.Height = MaxHeight;
			}
			else
			{
				treeGridView.Height = allRowHeight;
			}

			bottomTimeLineScale.Location = new System.Drawing.Point(bottomTimeLineScale.Location.X, 1 + topTimeLineScale.Height + treeGridView.Height);
			hScrollBar.Location = new System.Drawing.Point(hScrollBar.Location.X, 1 + topTimeLineScale.Height + treeGridView.Height + bottomTimeLineScale.Height);
		}

		protected void visualizeRuleExplorerSettingBecameDirty(object sender, string propertyName)
		{
			foreach (KeyValuePair<string, bool> kvp in (IList)sender)
			{
				string[] keys = kvp.Key.Split(':');

				// 考えられるkeys
				// ルール
				// ルール:イベント

				string target = _data.VisualizeData.VisualizeRules[keys[0]].Target;

				Action<ITreeGirdViewNode> nodeVisibleSet = (node) =>
					{
						if (keys.Length == 1)
						{
							node.Visible = kvp.Value;

							foreach (ITreeGirdViewNode n in node.Nodes.Values)
							{
								n.Visible = kvp.Value;
							}
						}
						else if (keys.Length == 2)
						{
							node.Nodes[keys[1]].Visible = kvp.Value;
						}
					};

				if (target == null)
				{
					ITreeGirdViewNode node = treeGridView.Nodes.Values.Single(n => n.Name == keys[0]);
					nodeVisibleSet(node);
				}
				else
				{
					foreach(ITreeGirdViewNode node in treeGridView.Nodes.Values.Where(n =>_data.ResourceData.Resources.ContainsKey(n.Name) && _data.ResourceData.Resources[n.Name].Type == target))
					{
						foreach(ITreeGirdViewNode n in node.Nodes.Values.Where(n=>n.Name == keys[0]))
						{
							nodeVisibleSet(n);
						}
					}
				}
			}
			treeGridViewRowChanged(this, EventArgs.Empty);
			timeLineRedraw();
		}

		protected void resourceExplorerSettingBecameDirty(object sender, string propertyName)
		{
			foreach (KeyValuePair<string, bool> kvp in (IList)sender)
			{
				if (treeGridView.Nodes.ContainsKey(kvp.Key) && treeGridView.Nodes[kvp.Key].Visible != kvp.Value)
				{
					treeGridView.Nodes[kvp.Key].Visible = kvp.Value;

					foreach (ITreeGirdViewNode node in treeGridView.Nodes[kvp.Key].Nodes.Values)
					{
						string[] res = new string[] { node.Name };

						if (kvp.Value)
							node.Visible = _data.SettingData.VisualizeRuleExplorerSetting.VisualizeRuleVisibility.ContainsKey(res) ? _data.SettingData.VisualizeRuleExplorerSetting.VisualizeRuleVisibility.GetValue(res) : ApplicationData.Setting.DefaultVisualizeRuleVisible;
						else
							node.Visible = false;

						foreach (ITreeGirdViewNode n in node.Nodes.Values)
						{
							string[] r = new string[] { node.Name, n.Name};

							if (kvp.Value)
								n.Visible = _data.SettingData.VisualizeRuleExplorerSetting.VisualizeRuleVisibility.ContainsKey(r) ? _data.SettingData.VisualizeRuleExplorerSetting.VisualizeRuleVisibility.GetValue(r) : ApplicationData.Setting.DefaultVisualizeRuleVisible;
							else
								n.Visible = false;
						}
					}
				}
			}
			treeGridViewRowChanged(this, EventArgs.Empty);
			timeLineRedraw();
		}

		protected void hScrollBarChangeRateUpdate()
		{
			hScrollBar.LargeChange = (int)((((decimal)TimeLine.ToTime.Value - (decimal)TimeLine.FromTime.Value) / ((decimal)TimeLine.MaxTime.Value - (decimal)TimeLine.MinTime.Value)) * (decimal)int.MaxValue);
			hScrollBar.SmallChange = (hScrollBar.LargeChange >= 20 ? hScrollBar.LargeChange : 20) / 20;
		}

		protected void hScrollBarValueUpdate()
		{
			decimal v = (TimeLine.FromTime.Truncate().Value * (decimal)(hScrollBar.Maximum) / TimeLine.ViewableSpan.Value);

			v = v > hScrollBar.Maximum ? hScrollBar.Maximum : v < hScrollBar.Minimum ? hScrollBar.Minimum : v;

			if (hScrollBar.Value != v)
			{
				_hScrollUpdateFlag = false;
				try
				{
					hScrollBar.Value = (int)v;
				}
				catch { }
				_hScrollUpdateFlag = true;
			}
		}

		protected void timeLineScaleRedraw()
		{
			topTimeLineScale.Refresh();
			bottomTimeLineScale.Refresh();
		}

		protected void timeLineRedraw()
		{
			if (ApplicationData.FileContext.Data != null)
			{
				treeGridView.DataGridView.Refresh();
				timeLineScaleRedraw();
			}
		}

		protected void timeLineViewingAreaChanged(object sender, GeneralChangedEventArgs<TimeLine> e)
		{
			if (e.Old.FromTime != e.New.FromTime)
			{
				viewingTimeRangeToTextBox.Minimum = e.New.FromTime.Value + 0.0000000001m;
				viewingTimeRangeFromTextBox.Text = e.New.FromTime.ToString();
			}

			if (e.Old.ToTime != e.New.ToTime)
			{
				viewingTimeRangeFromTextBox.Maximum = e.New.ToTime.Value - 0.0000000001m;
				viewingTimeRangeToTextBox.Text = e.New.ToTime.ToString();
			}

			if (e.Old.ViewingSpan != e.New.ViewingSpan)
				hScrollBarChangeRateUpdate();

			if (e.Old.FromTime != e.New.FromTime)
				hScrollBarValueUpdate();

			timeLineRedraw();
		}

		protected void timeLineScaleTimePerScaleMarkChanged(object sender, EventArgs e)
		{
			if (_data.SettingData.TraceLogDisplayPanelSetting.TimePerScaleMark != topTimeLineScale.TimePerScaleMark)
				_data.SettingData.TraceLogDisplayPanelSetting.TimePerScaleMark = topTimeLineScale.TimePerScaleMark;

			timePerSclaeLabel.Text = topTimeLineScale.TimePerScaleMark.ToString();

		}

		protected void traceLogDisplayPanelSettingBecameDirty(object sender, string propertyName)
		{
			switch(propertyName)
			{
				case "RowHeight":
					setRowHeight(treeGridView.Nodes.Values, _data.SettingData.TraceLogDisplayPanelSetting.RowHeight);
					rowHeightToolStripTextNumericUpDown.Text = _data.SettingData.TraceLogDisplayPanelSetting.RowHeight.ToString();
					treeGridViewRowChanged(this, EventArgs.Empty);
					break;
				case "AutoResizeRowHeight":
					autoResizeRowHeightToolStripButton.Checked = _data.SettingData.TraceLogDisplayPanelSetting.AutoResizeRowHeight;
					autoResizeRowHeight();
					break;
			}
		}

		protected void setRowHeight(IEnumerable<ITreeGirdViewNode> nodes, int height)
		{
			foreach (ITreeGirdViewNode node in nodes)
			{
				((DataGridViewRow)node).Height = height;
				if (node.HasChildren)
					setRowHeight(node.Nodes.Values, height);
			}
		}

		protected void autoResizeRowHeight()
		{
			if (treeGridView.VisibleRowsCount == 0)
				return;

			if (autoResizeRowHeightToolStripButton.Checked)
			{
				int height = MaxHeight / treeGridView.VisibleRowsCount;

				_data.SettingData.TraceLogDisplayPanelSetting.RowHeight = height;
				treeGridView.Height = height * treeGridView.VisibleRowsCount + (treeGridView.DataGridView.ColumnHeadersVisible ? treeGridView.ColumnHeadersHeight : 1);
			}
		}

		public override void DrawCursor(Graphics graphics, Color color, Time time)
		{
			drawCursor(graphics, new Rectangle(Location.X + _timeLineX, Location.Y, _timeLineWidth, Height), color, time);
		}

		public override void DrawMarker(Graphics g, TimeLineMarker marker)
		{
			drawMarker(g, new Rectangle(Location.X + _timeLineX, Location.Y, _timeLineWidth, Height), marker);
		}
	}
}
