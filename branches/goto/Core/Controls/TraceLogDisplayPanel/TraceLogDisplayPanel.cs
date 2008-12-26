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

			if (_data.SettingData.TraceLogDisplayPanelSetting.TimeLine == null)
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
			// 可視化ルール行の追加
			// ノード名「ルール.Name」-「イベント.Name」
			foreach (VisualizeRule vizRule in _data.VisualizeData.VisualizeRules.Where<VisualizeRule>(v => !v.IsBelongedTargetResourceType()))
			{
				TimeLineVisualizer tlv = new TimeLineVisualizer(vizRule);
				_list.Add(tlv);
				treeGridView.Add(vizRule.Name, vizRule.DisplayName, "", tlv);
				treeGridView.Nodes[vizRule.Name].Visible = _data.SettingData.VisualizeRuleExplorerSetting.VisualizeRuleVisibility.ContainsKey(vizRule.Name) ? _data.SettingData.VisualizeRuleExplorerSetting.VisualizeRuleVisibility.GetValue(vizRule.Name) : ApplicationData.Setting.DefaultVisualizeRuleVisible;
				treeGridView.Nodes[vizRule.Name].Image = imageList.Images["visualize"];
				
				// 可視化ルール内のイベント行の追加
				foreach (Event e in vizRule.Events)
				{
					TimeLineVisualizer _tlv = new TimeLineVisualizer(vizRule, e);
					_list.Add(_tlv);
					treeGridView.Nodes[vizRule.Name].Add(e.Name, e.DisplayName, "", _tlv);
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
						TimeLineVisualizer _tlv = new TimeLineVisualizer(res);
						_list.Add(_tlv);
						treeGridView.Add(res.Name, res.DisplayName, "", _tlv);
						treeGridView.Nodes[res.Name].Visible = _data.SettingData.ResourceExplorerSetting.ResourceVisibility.ContainsKey(res.Name) ? _data.SettingData.ResourceExplorerSetting.ResourceVisibility.GetValue(res.Name) : ApplicationData.Setting.DefaultResourceVisible;
						treeGridView.Nodes[res.Name].Image = imageList.Images["resource"];
					}

					TimeLineVisualizer tlv = new TimeLineVisualizer(vizRule, res);
					_list.Add(tlv);
					treeGridView.Nodes[res.Name].Add(vizRule.Name, vizRule.DisplayName, "", tlv);
					treeGridView.Nodes[res.Name].Nodes[vizRule.Name].Visible = treeGridView.Nodes[res.Name].Visible && ApplicationData.FileContext.Data.SettingData.VisualizeRuleExplorerSetting.VisualizeRuleVisibility.ContainsKey(vizRule.Name) ? _data.SettingData.VisualizeRuleExplorerSetting.VisualizeRuleVisibility.GetValue(vizRule.Name) : ApplicationData.Setting.DefaultVisualizeRuleVisible;
					treeGridView.Nodes[res.Name].Nodes[vizRule.Name].Image = imageList.Images["visualize"];

					foreach (Event e in vizRule.Events)
					{
						TimeLineVisualizer _tlv = new TimeLineVisualizer(e, res);
						_list.Add(_tlv);
						treeGridView.Nodes[res.Name].Nodes[vizRule.Name].Add(e.Name, e.DisplayName, "", _tlv);
						treeGridView.Nodes[res.Name].Nodes[vizRule.Name].Nodes[e.Name].Visible = treeGridView.Nodes[res.Name].Nodes[vizRule.Name].Visible && _data.SettingData.VisualizeRuleExplorerSetting.VisualizeRuleVisibility.ContainsKey(vizRule.Name, e.Name) ? _data.SettingData.VisualizeRuleExplorerSetting.VisualizeRuleVisibility.GetValue(vizRule.Name, e.Name) : ApplicationData.Setting.DefaultVisualizeRuleVisible;
						treeGridView.Nodes[res.Name].Nodes[vizRule.Name].Nodes[e.Name].Image = imageList.Images[e.getImageKey()]; ;
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
				if (treeGridView.Nodes[kvp.Key].Visible != kvp.Value)
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
