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
	public partial class TraceLogDisplayPanel : TimeLineControl
	{
		private List<TimeLineVisualizer> _list = new List<TimeLineVisualizer>();
		private bool _hScrollUpdateFlag = true;
		private int _timeLineX = 0;
		private int _timeLineWidth = 0;
		private string _timeScale = string.Empty;
		public int TimeLineX
		{
			get { return _timeLineX; }
			private set
			{
				if (_timeLineX != value)
				{
					_timeLineX = value;

					infoPanel.Size = new System.Drawing.Size(_timeLineX - 1, topTimeLineScale.Size.Height);
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

		public TraceLogDisplayPanel()
		{
			InitializeComponent();

			DoubleBuffered = true;
			ResizeRedraw = true;

			hScrollBar.Minimum = 1;
			hScrollBar.Maximum = int.MaxValue;
			hScrollBar.Value = hScrollBar.Minimum;

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

			_data.SettingData.TraceLogDisplayPanelSetting.TimeLine = new TimeLine(_data.TraceLogData.MinTime, _data.TraceLogData.MaxTime);
			TimeLine = _data.SettingData.TraceLogDisplayPanelSetting.TimeLine;

			topTimeLineScale.TimeLine = TimeLine;
			bottomTimeLineScale.TimeLine = TimeLine;

			_data.SettingData.ResourceExplorerSetting.BecameDirty += resourceExplorerSettingBecameDirty;
			_data.SettingData.VisualizeRuleExplorerSetting.BecameDirty += visualizeRuleExplorerSettingBecameDirty;
			_data.SettingData.TraceLogDisplayPanelSetting.TimeLine.ViewingAreaChanged += timeLineViewingAreaChanged;

			_timeScale = _data.ResourceData.TimeScale;

			viewingTimeRangeFromTextBox.Text = TimeLine.FromTime.ToString();
			viewingTimeRangeToTextBox.Text = TimeLine.ToTime.ToString();
			viewingTimeRangeFromTextBox.Enabled = true;
			viewingTimeRangeToTextBox.Enabled = true;
			viewingTimeRangeFromScaleLabel.Text = formatTimeScale(_timeScale);
			viewingTimeRangeToScaleLabel.Text = formatTimeScale(_timeScale);
			viewableSpanTextBox.Visible = true;
			viewableSpanLabel.Visible = true;
			viewableSpanTextBox.Text = TimeLine.MinTime.ToString() + " ～ " + TimeLine.MaxTime.ToString() + " " + formatTimeScale(_timeScale);
			viewableSpanTextBox.Width = TextRenderer.MeasureText(viewableSpanTextBox.Text, viewableSpanTextBox.Font).Width;

			setNodes();

			foreach(TimeLineVisualizer tlv in _list)
			{
				tlv.SetData(_data);
				tlv.TimeLine = TimeLine;
			}

			treeGridViewRowChanged(this, EventArgs.Empty);

			hScrollBarChangeRateUpdate();

			hScrollBarValueUpdate();

			timeLineRedraw();

		}

		private void setNodes()
		{
			foreach (VisualizeRule vizRule in _data.VisualizeData.VisualizeRules.Where<VisualizeRule>(v => !v.IsBelongedTargetResourceType()))
			{
				TimeLineVisualizer tlv = new TimeLineVisualizer(vizRule);
				_list.Add(tlv);
				treeGridView.Add(vizRule.Name, vizRule.DisplayName, "", tlv);
				treeGridView.Nodes[vizRule.Name].Visible = _data.SettingData.VisualizeRuleExplorerSetting.VisualizeRuleVisibility.ContainsKey(vizRule.Name) ? _data.SettingData.VisualizeRuleExplorerSetting.VisualizeRuleVisibility.GetValue(vizRule.Name) : ApplicationData.Setting.DefaultResourceVisible;
				treeGridView.Nodes[vizRule.Name].Image = imageList.Images["visualize"];
				foreach (Event e in vizRule.Events)
				{
					TimeLineVisualizer _tlv = new TimeLineVisualizer(vizRule, e);
					_list.Add(_tlv);
					treeGridView.Nodes[vizRule.Name].Add(e.DisplayName, e.DisplayName, "", _tlv);
					treeGridView.Nodes[vizRule.Name].Nodes[e.DisplayName].Visible = _data.SettingData.VisualizeRuleExplorerSetting.VisualizeRuleVisibility.ContainsKey(vizRule.Name, e.DisplayName) ? _data.SettingData.VisualizeRuleExplorerSetting.VisualizeRuleVisibility.GetValue(vizRule.Name, e.DisplayName) : ApplicationData.Setting.DefaultResourceVisible;
					setEventImage(treeGridView.Nodes[vizRule.Name].Nodes[e.DisplayName], e);
				}
			}
			foreach (VisualizeRule vizRule in _data.VisualizeData.VisualizeRules.Where<VisualizeRule>(v => v.IsBelongedTargetResourceType()))
			{
				foreach (Resource res in _data.ResourceData.Resources.Where<Resource>(r => r.Type == vizRule.Target))
				{
					if (!treeGridView.Nodes.ContainsKey(res.Type + ":" + res.Name))
					{
						TimeLineVisualizer _tlv = new TimeLineVisualizer(res);
						_list.Add(_tlv);
						treeGridView.Add(res.Type + ":" + res.Name, res.DisplayName, "", _tlv);
						treeGridView.Nodes[res.Type + ":" + res.Name].Visible = _data.SettingData.ResourceExplorerSetting.ResourceVisibility.ContainsKey(res.Type + ":" + res.Name) ? _data.SettingData.ResourceExplorerSetting.ResourceVisibility.GetValue(res.Type + ":" + res.Name) : ApplicationData.Setting.DefaultResourceVisible;
						treeGridView.Nodes[res.Type + ":" + res.Name].Image = imageList.Images["resource"];
					}

					TimeLineVisualizer tlv = new TimeLineVisualizer(vizRule, res);
					_list.Add(tlv);
					treeGridView.Nodes[res.Type + ":" + res.Name].Add(res.Type + ":" + res.Name + ":" + vizRule.Name, vizRule.DisplayName, "", tlv);
					treeGridView.Nodes[res.Type + ":" + res.Name].Nodes[res.Type + ":" + res.Name + ":" + vizRule.Name].Visible = treeGridView.Nodes[res.Type + ":" + res.Name].Visible && ApplicationData.FileContext.Data.SettingData.VisualizeRuleExplorerSetting.VisualizeRuleVisibility.ContainsKey(res.Type, vizRule.Name) ? _data.SettingData.VisualizeRuleExplorerSetting.VisualizeRuleVisibility.GetValue(res.Type, vizRule.Name) : ApplicationData.Setting.DefaultResourceVisible;
					treeGridView.Nodes[res.Type + ":" + res.Name].Nodes[res.Type + ":" + res.Name + ":" + vizRule.Name].Image = imageList.Images["visualize"];

					foreach (Event e in vizRule.Events)
					{
						TimeLineVisualizer _tlv = new TimeLineVisualizer(e, res);
						_list.Add(_tlv);
						treeGridView.Nodes[res.Type + ":" + res.Name].Nodes[res.Type + ":" + res.Name + ":" + vizRule.Name].Add(e.DisplayName, e.DisplayName, "", _tlv);
						treeGridView.Nodes[res.Type + ":" + res.Name].Nodes[res.Type + ":" + res.Name + ":" + vizRule.Name].Nodes[e.DisplayName].Visible = treeGridView.Nodes[res.Type + ":" + res.Name].Nodes[res.Type + ":" + res.Name + ":" + vizRule.Name].Visible && _data.SettingData.VisualizeRuleExplorerSetting.VisualizeRuleVisibility.ContainsKey(res.Type, vizRule.Name, e.DisplayName) ? _data.SettingData.VisualizeRuleExplorerSetting.VisualizeRuleVisibility.GetValue(res.Type, vizRule.Name, e.DisplayName) : ApplicationData.Setting.DefaultResourceVisible;
						setEventImage(treeGridView.Nodes[res.Type + ":" + res.Name].Nodes[res.Type + ":" + res.Name + ":" + vizRule.Name].Nodes[e.DisplayName], e);
					}
				}
			}
		}

		public override void ClearData()
		{
			base.ClearData();

			viewingTimeRangeFromTextBox.Text = string.Empty;
			viewingTimeRangeToTextBox.Text = string.Empty;
			viewingTimeRangeFromScaleLabel.Text = string.Empty;
			viewingTimeRangeToScaleLabel.Text = string.Empty;
			viewingTimeRangeFromTextBox.Enabled = false;
			viewingTimeRangeToTextBox.Enabled = false;
			viewingTimeRangeFromScaleLabel.Text = string.Empty;
			viewingTimeRangeToScaleLabel.Text = string.Empty;
			viewableSpanTextBox.Text = string.Empty;
			viewableSpanTextBox.Width = 30;
			viewableSpanTextBox.Visible = false;
			viewableSpanLabel.Visible = false;

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
			treeGridView.AddColumn(new DataGridViewTextBoxColumn() { Name = "value", HeaderText = "値" });
			treeGridView.AddColumn(new TimeLineColumn() { Name = "timeLine", HeaderText = "タイムライン", AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill });

			treeGridView.DataGridView.ColumnHeadersVisible = false;
			treeGridView.DataGridView.MultiSelect = false;

			treeGridView.RowHeightChanged += treeGridViewRowChanged;
			treeGridView.RowCountChanged += treeGridViewRowChanged;
			SizeChanged += treeGridViewRowChanged;
			treeGridView.DataGridView.SizeChanged += (o, _e) =>
			{
				bottomTimeLineScale.Location = new System.Drawing.Point(bottomTimeLineScale.Location.X, 1 + topTimeLineScale.Height + treeGridView.Height);
				hScrollBar.Location = new System.Drawing.Point(hScrollBar.Location.X, 1 + topTimeLineScale.Height + treeGridView.Height + bottomTimeLineScale.Height);
				treeGridViewRowChanged(this, e);
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
			#endregion

			#region StatusManager初期化
			EventHandler onTimeLineEvent = (o, _e) =>
			{
				ApplicationFactory.StatusManager.ShowHint(GetType() + Name + "mouseWheelMove", "可視化表示領域移動", "Ctrl", "ホイール", ",矢印キー");
			};
			EventHandler offTimeLineEvent = (o, _e) =>
			{
				ApplicationFactory.StatusManager.HideHint(GetType() + Name + "mouseWheelMove");
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
			EventHandler viewingTimeRangeFromTextBoxHandler = (o, _e) =>
				{
					Time lastValue = TimeLine.FromTime;
					try
					{
						Time t = new Time(viewingTimeRangeFromTextBox.Text, _timeRadix);
						if (t.IsEmpty) throw new Exception();
						TimeLine.SetTime(t, Time.Empty);
					}
					catch
					{
						viewingTimeRangeFromTextBox.Text = lastValue.ToString();
						TimeLine.SetTime(lastValue, Time.Empty);
					}
				};
			EventHandler viewingTimeRangeToTextBoxHandler = (o, _e) =>
				{
					Time lastValue = TimeLine.ToTime;
					try
					{
						Time t = new Time(viewingTimeRangeToTextBox.Text, _timeRadix);
						if (t.IsEmpty) throw new Exception();
						TimeLine.SetTime(Time.Empty, t);
					}
					catch
					{
						viewingTimeRangeToTextBox.Text = lastValue.ToString();
						TimeLine.SetTime(Time.Empty, lastValue);
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
						: (((hScrollBar.Value - hScrollBar.SmallChange) > hScrollBar.Minimum) ? hScrollBar.Value - hScrollBar.SmallChange : hScrollBar.Minimum);

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
							if (_data.SettingData.ResourceExplorerSetting.ResourceVisibility.ContainsKey(keys[0], node.Name.Split(':')[1]) ? _data.SettingData.ResourceExplorerSetting.ResourceVisibility.GetValue(keys[0], node.Name.Split(':')[1]) : ApplicationData.Setting.DefaultResourceVisible)
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
									if (_data.SettingData.ResourceExplorerSetting.ResourceVisibility.ContainsKey(keys[0], n.Name) ? _data.SettingData.ResourceExplorerSetting.ResourceVisibility.GetValue(keys[0], n.Name) : ApplicationData.Setting.DefaultResourceVisible)
										n.Visible = kvp.Value;
									else
										n.Visible = false;
								}
							}
						}
					}
					else
					{
						if (_data.SettingData.ResourceExplorerSetting.ResourceVisibility.ContainsKey(node.Name.Split(':'))
							? !_data.SettingData.ResourceExplorerSetting.ResourceVisibility.GetValue(node.Name.Split(':'))
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
										if (_data.SettingData.VisualizeRuleExplorerSetting.VisualizeRuleVisibility.ContainsKey(keys[0], keys[1], _n.Name)
											? _data.SettingData.VisualizeRuleExplorerSetting.VisualizeRuleVisibility.GetValue(keys[0], keys[1], _n.Name)
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
							node.Visible = _data.SettingData.VisualizeRuleExplorerSetting.VisualizeRuleVisibility.ContainsKey(res[0], res[2]) ? _data.SettingData.VisualizeRuleExplorerSetting.VisualizeRuleVisibility.GetValue(res[0], res[2]) : ApplicationData.Setting.DefaultResourceVisible;
						else
							node.Visible = false;

						foreach (ITreeGirdViewNode n in node.Nodes.Values)
						{
							if (kvp.Value)
								n.Visible = _data.SettingData.VisualizeRuleExplorerSetting.VisualizeRuleVisibility.ContainsKey(res[0], res[2], n.Name) ? _data.SettingData.VisualizeRuleExplorerSetting.VisualizeRuleVisibility.GetValue(res[0], res[2], n.Name) : ApplicationData.Setting.DefaultResourceVisible;
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
				hScrollBar.Value = (int)v;
				_hScrollUpdateFlag = true;
			}
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

		protected void timeLineViewingAreaChanged(object sender, GeneralChangedEventArgs<TimeLine> e)
		{
			if (e.Old.FromTime != e.New.ToTime)
				viewingTimeRangeFromTextBox.Text = e.New.FromTime.ToString();

			if (e.Old.ToTime != e.New.ToTime)
				viewingTimeRangeToTextBox.Text = e.New.ToTime.ToString();

			if (e.Old.ViewingSpan != e.New.ViewingSpan)
				hScrollBarChangeRateUpdate();

			if (e.Old.FromTime != e.New.FromTime)
				hScrollBarValueUpdate();

			timeLineRedraw();
		}
	}
}
