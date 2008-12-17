﻿using System;
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
using NU.OJL.MPRTOS.TLV.Core.FileContext.VisualizeData;
using System.Collections;

namespace NU.OJL.MPRTOS.TLV.Core.Controls
{
	public partial class TraceLogDisplayPanel : UserControl
	{
		private int _timeLineX = 0;
		private int _timeLineWidth = 0;
		private int _timeRadix = 10;
		private string _timeScale = string.Empty;
		private Time _minTime = new Time("0", 10);
		private Time _maxTime = new Time(long.MaxValue.ToString(), 10);
		private Time _fromTime;
		private Time _toTime;
		private StatusManager statusManager = new StatusManager();
		public int TimeLineX
		{
			get { return _timeLineX; }
			private set
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
		public int TimeLineWidth
		{
			get { return _timeLineWidth; }
			private set
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
		public string TimeScale
		{
			get { return _timeScale; }
			set
			{
				if (_timeScale != value)
				{
					_timeScale = value;
					if (_timeScale != string.Empty)
					{
						viewingTimeRangeFromScaleLabel.Text = formatTimeScale(_timeScale);
						viewingTimeRangeToScaleLabel.Text = formatTimeScale(_timeScale);
					}
					else
					{
						viewingTimeRangeFromScaleLabel.Text = string.Empty;
						viewingTimeRangeToScaleLabel.Text = string.Empty;
					}
				}
			}
		}
		public Time MaxTime
		{
			get { return _maxTime; }
			set
			{
				if (_maxTime != value)
				{
					_maxTime = value;

					if (_maxTime != null && _minTime != null)
					{
						statusManager.ShowInfo("viewableTimeRange", "表示可能領域 : " + MinTime.ToString() + formatTimeScale(_timeScale) + " ～ " + MaxTime.ToString() + formatTimeScale(_timeScale));
					}
					else
					{
						statusManager.HideInfo("viewableTimeRange");
					}
				}
			}
		}
		public Time MinTime
		{
			get { return _minTime; }
			set
			{
				if (_minTime != value)
				{
					_minTime = value;

					if (_maxTime != null && _minTime != null)
					{
						statusManager.ShowInfo("viewableTimeRange", "表示可能領域 : " + MinTime.ToString() + formatTimeScale(_timeScale) + " ～ " + MaxTime.ToString() + formatTimeScale(_timeScale));
					}
					else
					{
						statusManager.HideInfo("viewableTimeRange");
					}
				}
			}
		}
		public Time FromTime
		{
			get { return _fromTime; }
			set
			{
				if (_fromTime != value)
				{
					_fromTime = value;

					if (_fromTime != Time.NaN)
					{
						if (_minTime > _fromTime)
							_fromTime = _minTime;
						if (_maxTime < _fromTime)
							_fromTime = _maxTime;

						if (_fromTime >= _toTime)
							throw new Exception("FromTimeはToTimeより小さくなければなりません。");

						if (!viewingTimeRangeFromTextBox.Enabled)
							viewingTimeRangeFromTextBox.Enabled = true;

						if (viewingTimeRangeFromTextBox.Text != _fromTime.ToString())
							viewingTimeRangeFromTextBox.Text = _fromTime.ToString();

						if (ApplicationData.FileContext.Data != null
							&& ApplicationData.FileContext.Data.SettingData.TraceLogDisplayPanelSetting.FromTime != _fromTime.ToString())
							ApplicationData.FileContext.Data.SettingData.TraceLogDisplayPanelSetting.FromTime = _fromTime.ToString();
					}
					else if (_fromTime == Time.NaN && _toTime == Time.NaN)
					{
						viewingTimeRangeFromTextBox.Text = string.Empty;
						viewingTimeRangeFromTextBox.Enabled = false;
					}

				}
			}
		}
		public Time ToTime
		{
			get { return _toTime; }
			set
			{
				if (_toTime != value)
				{
					_toTime = value;

					if (_toTime != Time.NaN)
					{
						if (_minTime > _toTime)
							_toTime = _minTime;
						if (_maxTime < _toTime)
							_toTime = _maxTime;

						if (_fromTime >= _toTime)
							throw new Exception("FromTimeはToTimeより小さくなければなりません。");

						if (!viewingTimeRangeToTextBox.Enabled)
							viewingTimeRangeToTextBox.Enabled = true;

						if (viewingTimeRangeToTextBox.Text != _toTime.ToString())
							viewingTimeRangeToTextBox.Text = _toTime.ToString();

						if (ApplicationData.FileContext.Data != null
							&& ApplicationData.FileContext.Data.SettingData.TraceLogDisplayPanelSetting.ToTime != _toTime.ToString())
							ApplicationData.FileContext.Data.SettingData.TraceLogDisplayPanelSetting.ToTime = _toTime.ToString();
					}
					else if (_fromTime == Time.NaN && _toTime == Time.NaN)
					{
						viewingTimeRangeToTextBox.Text = string.Empty;
						viewingTimeRangeToTextBox.Enabled = false;
					}

				}
			}
		}
		public Time ViewingTimeSpan
		{
			get { return ToTime - FromTime; }
		}
		public Time ViewableTimeSpan
		{
			get { return MaxTime - MinTime; }
		}

		public TraceLogDisplayPanel()
		{
			InitializeComponent();
		}

		public void SetData(TraceLogVisualizerData data)
		{
			ClearData();

			_timeRadix = data.ResourceData.TimeRadix;
			TimeScale = data.ResourceData.TimeScale;
			MaxTime = data.TraceLogData.MaxTime;
			MinTime = data.TraceLogData.MinTime;

			foreach (VisualizeRule vizRule in data.VisualizeData.VisualizeRules.Where<VisualizeRule>(v => !v.IsBelongedTargetResourceType()))
			{
				treeGridView.Add(vizRule.Name, vizRule.DisplayName, "", new TimeLineVisualizer(vizRule));
				treeGridView.Nodes[vizRule.Name].Visible = data.SettingData.VisualizeRuleExplorerSetting.VisualizeRuleVisibility.ContainsKey(vizRule.Name) ? data.SettingData.VisualizeRuleExplorerSetting.VisualizeRuleVisibility.GetValue(vizRule.Name) : ApplicationData.Setting.DefaultResourceVisible;
				treeGridView.Nodes[vizRule.Name].Image = imageList.Images["visualize"];
				foreach(Event e in vizRule.Events)
				{
					treeGridView.Nodes[vizRule.Name].Add(e.DisplayName, e.DisplayName, "", new TimeLineVisualizer(vizRule, e));
					treeGridView.Nodes[vizRule.Name].Nodes[e.DisplayName].Visible = data.SettingData.VisualizeRuleExplorerSetting.VisualizeRuleVisibility.ContainsKey(vizRule.Name, e.DisplayName) ? data.SettingData.VisualizeRuleExplorerSetting.VisualizeRuleVisibility.GetValue(vizRule.Name, e.DisplayName) : ApplicationData.Setting.DefaultResourceVisible;
					setEventImage(treeGridView.Nodes[vizRule.Name].Nodes[e.DisplayName], e);
				}
			}
			foreach (VisualizeRule vizRule in data.VisualizeData.VisualizeRules.Where<VisualizeRule>(v => v.IsBelongedTargetResourceType()))
			{
				foreach(Resource res in data.ResourceData.Resources.Where<Resource>(r=>r.Type == vizRule.Target))
				{
					if (!treeGridView.Nodes.ContainsKey(res.Type + ":" + res.Name))
					{
						treeGridView.Add(res.Type + ":" + res.Name, res.DisplayName, "", new TimeLineVisualizer(res));
						treeGridView.Nodes[res.Type + ":" + res.Name].Visible = data.SettingData.ResourceExplorerSetting.ResourceVisibility.ContainsKey(res.Type + ":" + res.Name) ? data.SettingData.ResourceExplorerSetting.ResourceVisibility.GetValue(res.Type + ":" + res.Name) : ApplicationData.Setting.DefaultResourceVisible;
						treeGridView.Nodes[res.Type + ":" + res.Name].Image = imageList.Images["resource"];
					}

					treeGridView.Nodes[res.Type + ":" + res.Name].Add(res.Type + ":" + res.Name + ":" + vizRule.Name, vizRule.DisplayName, "", new TimeLineVisualizer(vizRule, res));
					treeGridView.Nodes[res.Type + ":" + res.Name].Nodes[res.Type + ":" + res.Name + ":" + vizRule.Name].Visible = treeGridView.Nodes[res.Type + ":" + res.Name].Visible && ApplicationData.FileContext.Data.SettingData.VisualizeRuleExplorerSetting.VisualizeRuleVisibility.ContainsKey(res.Type, vizRule.Name) ? data.SettingData.VisualizeRuleExplorerSetting.VisualizeRuleVisibility.GetValue(res.Type, vizRule.Name) : ApplicationData.Setting.DefaultResourceVisible;
					treeGridView.Nodes[res.Type + ":" + res.Name].Nodes[res.Type + ":" + res.Name + ":" + vizRule.Name].Image = imageList.Images["visualize"];

					foreach (Event e in vizRule.Events)
					{
						treeGridView.Nodes[res.Type + ":" + res.Name].Nodes[res.Type + ":" + res.Name + ":" + vizRule.Name].Add(e.DisplayName, e.DisplayName, "", new TimeLineVisualizer(e, res));
						treeGridView.Nodes[res.Type + ":" + res.Name].Nodes[res.Type + ":" + res.Name + ":" + vizRule.Name].Nodes[e.DisplayName].Visible = treeGridView.Nodes[res.Type + ":" + res.Name].Nodes[res.Type + ":" + res.Name + ":" + vizRule.Name].Visible && data.SettingData.VisualizeRuleExplorerSetting.VisualizeRuleVisibility.ContainsKey(res.Type, vizRule.Name, e.DisplayName) ? data.SettingData.VisualizeRuleExplorerSetting.VisualizeRuleVisibility.GetValue(res.Type, vizRule.Name, e.DisplayName) : ApplicationData.Setting.DefaultResourceVisible;
						setEventImage(treeGridView.Nodes[res.Type + ":" + res.Name].Nodes[res.Type + ":" + res.Name + ":" + vizRule.Name].Nodes[e.DisplayName], e);
					}
				}
			}

			treeGridViewRowChanged(this, EventArgs.Empty);

			ApplicationData.FileContext.Data.SettingData.ResourceExplorerSetting.BecameDirty += resourceExplorerSettingBecameDirty;

			ApplicationData.FileContext.Data.SettingData.VisualizeRuleExplorerSetting.BecameDirty += visualizeRuleExplorerSettingBecameDirty;

			ApplicationData.FileContext.Data.SettingData.TraceLogDisplayPanelSetting.BecameDirty += (o, p) =>
				{
					viewingTimeChangeAction(() =>
					{
						switch (p)
						{
							case "FromTime":
								if (data.SettingData.TraceLogDisplayPanelSetting.FromTime != null)
								{
									if (FromTime.ToString() != data.SettingData.TraceLogDisplayPanelSetting.FromTime)
										FromTime = new Time(data.SettingData.TraceLogDisplayPanelSetting.FromTime, _timeRadix);
								}
								break;
							case "ToTime":
								if (data.SettingData.TraceLogDisplayPanelSetting.ToTime != null)
								{
									if (ToTime.ToString() != data.SettingData.TraceLogDisplayPanelSetting.ToTime)
										ToTime = new Time(data.SettingData.TraceLogDisplayPanelSetting.ToTime, _timeRadix);
								}
								break;
							case "PixelPerScaleMark":
								break;
						}
					});
				};

			if (data.SettingData.TraceLogDisplayPanelSetting.ToTime != null)
			{
				ToTime = new Time(data.SettingData.TraceLogDisplayPanelSetting.ToTime, _timeRadix);
			}
			if (ToTime == Time.NaN)
			{
				ToTime = _maxTime;
			}
			if (data.SettingData.TraceLogDisplayPanelSetting.FromTime != null)
			{
				FromTime = new Time(data.SettingData.TraceLogDisplayPanelSetting.FromTime, _timeRadix);
			}
			if (FromTime == Time.NaN)
			{
				FromTime = _minTime;
			}

			hScrollBarChangeRateChange();

			hScrollBar.Value = (int)(((decimal)(FromTime.Value) / (decimal)(_maxTime.Value + 1 - _minTime.Value)) * (decimal)int.MaxValue);
			
			timeLineRedraw();
		}

		public void ClearData()
		{
			_timeRadix = 10;
			TimeScale = string.Empty;
			hScrollBar.Value = 1;
			clearTime();
			treeGridView.Clear();
			statusManager.Clear();
		}

		protected override void OnLoad(EventArgs e)
		{
			base.OnLoad(e);
			this.ApplyNativeScroll();

			hScrollBar.Minimum = 1;
			hScrollBar.Value = 1;
			hScrollBar.Maximum = int.MaxValue;

			statusManager.StatusStrip = statusStrip;

			imageList.Images.Add("visualize", Properties.Resources.visualize);
			imageList.Images.Add("resource", Properties.Resources.resource);
			imageList.Images.Add("bhr2bhr", Properties.Resources.bhr2bhr);
			imageList.Images.Add("atr2atr", Properties.Resources.atr2atr);
			imageList.Images.Add("atr2bhr", Properties.Resources.atr2bhr);
			imageList.Images.Add("bhr2atr", Properties.Resources.bhr2atr);
			imageList.Images.Add("attribute", Properties.Resources.attribute);
			imageList.Images.Add("behavior", Properties.Resources.behavior);
			imageList.Images.Add("warning", Properties.Resources.warning);

			treeGridView.AddColumn(new TreeGridViewColumn() { Name = "resourceName", HeaderText = "リソース" });
			treeGridView.AddColumn(new DataGridViewTextBoxColumn() { Name = "value", HeaderText = "値" });
			treeGridView.AddColumn(new TimeLineColumn() { Name = "timeLine", HeaderText = "タイムライン", AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill });

			this.DoubleBuffered = true;

			treeGridView.DataGridView.ColumnHeadersVisible = false;
			treeGridView.DataGridView.MultiSelect = false;

			treeGridView.RowHeightChanged += treeGridViewRowChanged;
			treeGridView.RowCountChanged += treeGridViewRowChanged;
			treeGridView.SizeChanged += (o, _e) =>
			{
				bottomTimeLineScale.Location = new System.Drawing.Point(bottomTimeLineScale.Location.X, 1 + topTimeLineScale.Height + treeGridView.Height);
				hScrollBar.Location = new System.Drawing.Point(hScrollBar.Location.X, 1 + topTimeLineScale.Height + treeGridView.Height + bottomTimeLineScale.Height);
				timeLineScaleRedraw();
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
				timeLineScaleRedraw();
			};
			treeGridView.DataGridView.ScrollBars = ScrollBars.Vertical;

			treeGridView.DataGridView.CellPainting += (o, _e) =>
			{
				_e.Paint(_e.ClipBounds, _e.PaintParts & ~DataGridViewPaintParts.Focus);
				_e.Handled = true;
			};

			treeGridView.DataGridView.RowPostPaint += new DataGridViewRowPostPaintEventHandler(dataGridViewRowPostPaint);

			EventHandler onTimeLineEvent = (o, _e) =>
			{
				ApplicationFactory.StatusManager.ShowHint(GetType() + Name + "mouseWheelMove", "可視化表示領域移動", "Ctrl", "ホイール");
			};
			EventHandler offTimeLineEvent = (o, _e) =>
			{
				ApplicationFactory.StatusManager.HideHint(GetType() + Name + "mouseWheelMove");
			};
			MouseEventHandler onMouseWheel = (o, _e) =>
			{
				if ((Control.ModifierKeys & Keys.Control) == Keys.Control)
				{
					hScrollBar.Value =
						(_e.Delta < 0)
						? (((hScrollBar.Value + hScrollBar.SmallChange) < hScrollBar.Maximum - hScrollBar.LargeChange + 1) ? hScrollBar.Value + hScrollBar.SmallChange : hScrollBar.Maximum - hScrollBar.LargeChange + 1)
						: (((hScrollBar.Value - hScrollBar.SmallChange) > hScrollBar.Minimum) ? hScrollBar.Value - hScrollBar.SmallChange : hScrollBar.Minimum);

					if (_e.GetType() == typeof(ExMouseEventArgs))
						((ExMouseEventArgs)_e).Handled = true;
				}
			};
			treeGridView.DataGridView.MouseEnter += onTimeLineEvent;
			treeGridView.DataGridView.MouseLeave += offTimeLineEvent;
			treeGridView.DataGridView.MouseWheel += onMouseWheel;
			topTimeLineScale.MouseEnter += onTimeLineEvent;
			topTimeLineScale.MouseLeave += offTimeLineEvent;
			topTimeLineScale.MouseWheel += onMouseWheel;
			bottomTimeLineScale.MouseEnter += onTimeLineEvent;
			bottomTimeLineScale.MouseLeave += offTimeLineEvent;
			bottomTimeLineScale.MouseWheel += onMouseWheel;
			hScrollBar.MouseEnter += onTimeLineEvent;
			hScrollBar.MouseLeave += offTimeLineEvent;
			hScrollBar.MouseWheel += onMouseWheel;
			MouseEnter += onTimeLineEvent;
			MouseLeave += offTimeLineEvent;

			ApplicationData.FileContext.DataChanged += new EventHandler<GeneralEventArgs<TraceLogVisualizerData>>(fileContextDataChanged);

			EventHandler viewingTimeRangeFromTextBoxHandler = (o, _e) =>
				{
					lock (this)
					{
						viewingTimeChangeAction(() =>
						{
							string lastValue = FromTime.ToString();
							try
							{
								FromTime = new Time(viewingTimeRangeFromTextBox.Text, _timeRadix);
							}
							catch
							{
								viewingTimeRangeFromTextBox.Text = lastValue;
								FromTime = new Time(lastValue, _timeRadix);
							}
						});
					}
				};
			EventHandler viewingTimeRangeToTextBoxHandler = (o, _e) =>
				{
					lock(this)
					{
						viewingTimeChangeAction(() =>
						{
							string lastValue = ToTime.ToString();
							try
							{
								ToTime = new Time(viewingTimeRangeToTextBox.Text, _timeRadix);
							}
							catch
							{
								viewingTimeRangeToTextBox.Text = lastValue;
								ToTime = new Time(lastValue, _timeRadix);
							}
						});
					}
				};

			viewingTimeRangeFromTextBox.KeyUp += (o, _e) => { if (_e.KeyData == Keys.Enter) { viewingTimeRangeFromTextBoxHandler(this, EventArgs.Empty); _e.Handled = true; } };
			viewingTimeRangeToTextBox.KeyUp += (o, _e) => { if (_e.KeyData == Keys.Enter) { viewingTimeRangeToTextBoxHandler(this, EventArgs.Empty); _e.Handled = true; } };
			viewingTimeRangeFromTextBox.Validated += viewingTimeRangeFromTextBoxHandler;
			viewingTimeRangeToTextBox.Validated += viewingTimeRangeToTextBoxHandler;
			viewingTimeRangeFromTextBox.TextChanged += (o, _e) =>
				{
					viewingTimeRangeFromTextBox.Width = TextRenderer.MeasureText(viewingTimeRangeFromTextBox.Text, viewingTimeRangeFromTextBox.Font).Width;
					if (viewingTimeRangeFromTextBox.Width == 0)
						viewingTimeRangeFromTextBox.Width = 50;
				};
			viewingTimeRangeToTextBox.TextChanged += (o, _e) =>
				{
					viewingTimeRangeToTextBox.Width = TextRenderer.MeasureText(viewingTimeRangeToTextBox.Text, viewingTimeRangeToTextBox.Font).Width;
					if (viewingTimeRangeToTextBox.Width == 0)
						viewingTimeRangeToTextBox.Width = 50;
				};
			hScrollBar.ValueChanged += (o, _e) =>
				{
					Time span = ViewingTimeSpan;
					viewingTimeChangeAction(() =>
					{
						if (hScrollBar.Value == hScrollBar.Maximum - hScrollBar.LargeChange + 1)
						{
							ToTime = MaxTime;
							FromTime = ToTime - span;
						}
						else if (hScrollBar.Value == hScrollBar.Minimum)
						{
							FromTime = MinTime;
							ToTime = FromTime + span;
						}
						else
						{
							Time d = ViewableTimeSpan * ((decimal)hScrollBar.Value / (decimal)int.MaxValue);

							if (d > FromTime)
							{
								ToTime = d + span;
								FromTime = ToTime - span;
							}
							else if (d != FromTime)
							{
								FromTime = d;
								ToTime = FromTime + span;
							}
						}
						});

				};
		}

		protected void dataGridViewRowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
		{
			//e.Graphics.DrawLine(new System.Drawing.Pen(Color.FromArgb(200,200,200)), _timeLineX - 1, e.RowBounds.Y + e.RowBounds.Height - 1, _timeLineX + _timeLineWidth - 2, e.RowBounds.Y + e.RowBounds.Height - 1);
		}

		protected override void OnSizeChanged(EventArgs e)
		{
			base.OnSizeChanged(e);
			treeGridViewRowChanged(this, e);
		}

		protected void setEventImage(ITreeGirdViewNode tn, Event e)
		{
			if ((e.Type & EventTypes.FromAttributeChange) == EventTypes.FromAttributeChange
				&& (e.Type & EventTypes.ToAttributeChange) == EventTypes.ToAttributeChange)
			{
				tn.Image = imageList.Images["atr2atr"];
			}

			if ((e.Type & EventTypes.FromBehaviorHappen) == EventTypes.FromBehaviorHappen
				&& (e.Type & EventTypes.ToBehaviorHappen) == EventTypes.ToBehaviorHappen)
			{
				tn.Image = imageList.Images["bhr2bhr"];
			}

			if ((e.Type & EventTypes.FromAttributeChange) == EventTypes.FromAttributeChange
				&& (e.Type & EventTypes.ToBehaviorHappen) == EventTypes.ToBehaviorHappen)
			{
				tn.Image = imageList.Images["atr2bhr"];
			}

			if ((e.Type & EventTypes.FromBehaviorHappen) == EventTypes.FromBehaviorHappen
				&& (e.Type & EventTypes.ToAttributeChange) == EventTypes.ToAttributeChange)
			{
				tn.Image = imageList.Images["bhr2atr"];
			}

			if ((e.Type & EventTypes.WhenAttributeChange) == EventTypes.WhenAttributeChange)
			{
				tn.Image = imageList.Images["attribute"];
			}

			if ((e.Type & EventTypes.WhenBehaviorHappen) == EventTypes.WhenBehaviorHappen)
			{
				tn.Image = imageList.Images["behavior"];
			}

			if ((e.Type & EventTypes.Error) == EventTypes.Error)
			{
				tn.Image = imageList.Images["warning"];
			}
		}

		protected void treeGridViewRowChanged(object sender, EventArgs e)
		{
			int allRowHeight = treeGridView.VisibleRowsCount * treeGridView.RowHeight;

			allRowHeight += treeGridView.DataGridView.ColumnHeadersVisible ? treeGridView.ColumnHeadersHeight : 1;

			int maxHeight = toolStripContainer.ContentPanel.Height - 2
				- topTimeLineScale.Height
				- bottomTimeLineScale.Height
				- hScrollBar.Height;

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

			if ( allRowHeight > maxHeight)
			{
				treeGridView.Height = (int)((double)allRowHeight - Math.Ceiling((double)(allRowHeight - maxHeight) / (double)treeGridView.RowHeight) * (double)treeGridView.RowHeight);
			}
			else
			{
				treeGridView.Height = allRowHeight;
			}
		}

		protected void fileContextDataChanged(object sender, GeneralEventArgs<TraceLogVisualizerData> e)
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
		}

		protected void visualizeRuleExplorerSettingBecameDirty(object sender, string propertyName)
		{
			foreach (KeyValuePair<string, bool> kvp in (IList)sender)
			{
				string[] keys = kvp.Key.Split(':');

				foreach (ITreeGirdViewNode node in treeGridView.Nodes.Values.Where(n => n.Name.Split(':')[0] == keys[0]))
				{

					if (keys.Length == 1)
					{
						if (node.Name.Split(':').Length > 1)
						{
							if (ApplicationData.FileContext.Data.SettingData.ResourceExplorerSetting.ResourceVisibility.ContainsKey(keys[0], node.Name.Split(':')[1]) ? ApplicationData.FileContext.Data.SettingData.ResourceExplorerSetting.ResourceVisibility.GetValue(keys[0], node.Name.Split(':')[1]) : ApplicationData.Setting.DefaultResourceVisible)
								node.Visible = kvp.Value;
							else
								node.Visible = false;
						}
						else if (node.Name.Split(':').Length == 1)
						{
							node.Visible = kvp.Value;

							if (node.HasChildren)
							{
								foreach (ITreeGirdViewNode n in node.Nodes.Values)
								{
									if (ApplicationData.FileContext.Data.SettingData.ResourceExplorerSetting.ResourceVisibility.ContainsKey(keys[0], n.Name) ? ApplicationData.FileContext.Data.SettingData.ResourceExplorerSetting.ResourceVisibility.GetValue(keys[0], n.Name) : ApplicationData.Setting.DefaultResourceVisible)
										n.Visible = kvp.Value;
									else
										n.Visible = false;
								}
							}
						}
					}
					else
					{
						if (ApplicationData.FileContext.Data.SettingData.ResourceExplorerSetting.ResourceVisibility.ContainsKey(node.Name.Split(':'))
							? !ApplicationData.FileContext.Data.SettingData.ResourceExplorerSetting.ResourceVisibility.GetValue(node.Name.Split(':'))
							: !ApplicationData.Setting.DefaultResourceVisible)
							continue;

						foreach (ITreeGirdViewNode n in node.Nodes.Values.Where(n => n.Name.Split(':').Last() == keys[1]))
						{
							if (n.HasChildren && keys.Length == 3)
							{
								foreach (ITreeGirdViewNode _n in n.Nodes.Values.Where(_n => _n.Name == keys[2]))
								{
									_n.Visible = kvp.Value;
								}
							}
							else
							{
								n.Visible = kvp.Value;

								if (n.HasChildren)
								{
									foreach (ITreeGirdViewNode _n in n.Nodes.Values)
									{
										if (ApplicationData.FileContext.Data.SettingData.VisualizeRuleExplorerSetting.VisualizeRuleVisibility.ContainsKey(keys[0], keys[1], _n.Name)
											? ApplicationData.FileContext.Data.SettingData.VisualizeRuleExplorerSetting.VisualizeRuleVisibility.GetValue(keys[0], keys[1], _n.Name)
											: ApplicationData.Setting.DefaultResourceVisible)
											_n.Visible = kvp.Value;
										else
											_n.Visible = false;
									}
								}
							}
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
				if (treeGridView.Nodes[kvp.Key].Visible != kvp.Value)
				{
					treeGridView.Nodes[kvp.Key].Visible = kvp.Value;

					foreach (ITreeGirdViewNode node in treeGridView.Nodes[kvp.Key].Nodes.Values)
					{
						string[] res = node.Name.Split(':');

						if (kvp.Value)
							node.Visible = ApplicationData.FileContext.Data.SettingData.VisualizeRuleExplorerSetting.VisualizeRuleVisibility.ContainsKey(res[0], res[2]) ? ApplicationData.FileContext.Data.SettingData.VisualizeRuleExplorerSetting.VisualizeRuleVisibility.GetValue(res[0], res[2]) : ApplicationData.Setting.DefaultResourceVisible;
						else
							node.Visible = false;

						foreach (ITreeGirdViewNode n in node.Nodes.Values)
						{
							if (kvp.Value)
								n.Visible = ApplicationData.FileContext.Data.SettingData.VisualizeRuleExplorerSetting.VisualizeRuleVisibility.ContainsKey(res[0], res[2], n.Name) ? ApplicationData.FileContext.Data.SettingData.VisualizeRuleExplorerSetting.VisualizeRuleVisibility.GetValue(res[0], res[2], n.Name) : ApplicationData.Setting.DefaultResourceVisible;
							else
								n.Visible = false;
						}
					}
				}
			}
			treeGridViewRowChanged(this, EventArgs.Empty);
			timeLineRedraw();
		}

		protected void hScrollBarChangeRateChange()
		{
			hScrollBar.LargeChange = (int)((((decimal)ToTime.Value - (decimal)FromTime.Value) / ((decimal)_maxTime.Value - (decimal)_minTime.Value)) * (decimal)int.MaxValue);
			hScrollBar.SmallChange = (hScrollBar.LargeChange >= 20 ? hScrollBar.LargeChange : 20) / 20;
		}

		protected void clearTime()
		{
			MinTime = new Time("0", 10);
			MaxTime = new Time(long.MaxValue.ToString(), 10);
			FromTime = Time.NaN;
			ToTime = Time.NaN;
		}

		protected string formatTimeScale(string scale)
		{
			return "[" + scale + "]";
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

		protected void viewingTimeChangeAction(Action action)
		{
			Time span = ViewingTimeSpan;

			action();

			if (span != ViewingTimeSpan)
				hScrollBarChangeRateChange();

			timeLineRedraw();
		}
	}
}
