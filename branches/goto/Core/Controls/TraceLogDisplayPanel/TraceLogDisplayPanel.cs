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
using NU.OJL.MPRTOS.TLV.Core.FileContext.VisualizeData;
using System.Collections;

namespace NU.OJL.MPRTOS.TLV.Core.Controls
{
	public partial class TraceLogDisplayPanel : UserControl
	{
		private int _timeLineX = 0;
		private int _timeLineWidth = 0;
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

		public TraceLogDisplayPanel()
		{
			InitializeComponent();
		}

		public void SetData(TraceLogVisualizerData data)
		{
			ClearData();

			foreach (VisualizeRule vizRule in data.VisualizeData.VisualizeRules.Where<VisualizeRule>(v => !v.IsBelongedTargetResourceType()))
			{
				treeGridView.Add(vizRule.Name, vizRule.DisplayName, "", new TimeLineVisualizer());
				treeGridView.Nodes[vizRule.Name].Visible = ApplicationData.FileContext.Data.SettingData.VisualizeRuleExplorerSetting.VisualizeRuleVisibility.ContainsKey(vizRule.Name) ? ApplicationData.FileContext.Data.SettingData.VisualizeRuleExplorerSetting.VisualizeRuleVisibility.GetValue(vizRule.Name) : ApplicationData.Setting.DefaultResourceVisible;
				treeGridView.Nodes[vizRule.Name].Image = imageList.Images["visualize"];
				foreach(Event e in vizRule.Events)
				{
					treeGridView.Nodes[vizRule.Name].Add(e.DisplayName, e.DisplayName, "", new TimeLineVisualizer());
					treeGridView.Nodes[vizRule.Name].Nodes[e.DisplayName].Visible = ApplicationData.FileContext.Data.SettingData.VisualizeRuleExplorerSetting.VisualizeRuleVisibility.ContainsKey(vizRule.Name, e.DisplayName) ? ApplicationData.FileContext.Data.SettingData.VisualizeRuleExplorerSetting.VisualizeRuleVisibility.GetValue(vizRule.Name, e.DisplayName) : ApplicationData.Setting.DefaultResourceVisible;
					setEventImage(treeGridView.Nodes[vizRule.Name].Nodes[e.DisplayName], e);
				}
			}
			foreach (VisualizeRule vizRule in data.VisualizeData.VisualizeRules.Where<VisualizeRule>(v => v.IsBelongedTargetResourceType()))
			{
				foreach(Resource res in data.ResourceData.Resources.Where<Resource>(r=>r.Type == vizRule.Target))
				{
					if (!treeGridView.Nodes.ContainsKey(res.Type + ":" + res.Name))
					{
						treeGridView.Add(res.Type + ":" + res.Name, res.DisplayName, "", new TimeLineVisualizer());
						treeGridView.Nodes[res.Type + ":" + res.Name].Visible = ApplicationData.FileContext.Data.SettingData.ResourceExplorerSetting.ResourceVisibility.ContainsKey(res.Type + ":" + res.Name) ? ApplicationData.FileContext.Data.SettingData.ResourceExplorerSetting.ResourceVisibility.GetValue(res.Type + ":" + res.Name) : ApplicationData.Setting.DefaultResourceVisible;
						treeGridView.Nodes[res.Type + ":" + res.Name].Image = imageList.Images["resource"];
					}

					treeGridView.Nodes[res.Type + ":" + res.Name].Add(res.Type + ":" + res.Name + ":" + vizRule.Name, vizRule.DisplayName, "", new TimeLineVisualizer());
					treeGridView.Nodes[res.Type + ":" + res.Name].Nodes[res.Type + ":" + res.Name + ":" + vizRule.Name].Visible = treeGridView.Nodes[res.Type + ":" + res.Name].Visible && ApplicationData.FileContext.Data.SettingData.VisualizeRuleExplorerSetting.VisualizeRuleVisibility.ContainsKey(res.Type, vizRule.Name) ? ApplicationData.FileContext.Data.SettingData.VisualizeRuleExplorerSetting.VisualizeRuleVisibility.GetValue(res.Type, vizRule.Name) : ApplicationData.Setting.DefaultResourceVisible;
					treeGridView.Nodes[res.Type + ":" + res.Name].Nodes[res.Type + ":" + res.Name + ":" + vizRule.Name].Image = imageList.Images["visualize"];

					foreach (Event e in vizRule.Events)
					{
						treeGridView.Nodes[res.Type + ":" + res.Name].Nodes[res.Type + ":" + res.Name + ":" + vizRule.Name].Add(e.DisplayName, e.DisplayName, "", new TimeLineVisualizer());
						treeGridView.Nodes[res.Type + ":" + res.Name].Nodes[res.Type + ":" + res.Name + ":" + vizRule.Name].Nodes[e.DisplayName].Visible = treeGridView.Nodes[res.Type + ":" + res.Name].Nodes[res.Type + ":" + res.Name + ":" + vizRule.Name].Visible && ApplicationData.FileContext.Data.SettingData.VisualizeRuleExplorerSetting.VisualizeRuleVisibility.ContainsKey(res.Type, vizRule.Name, e.DisplayName) ? ApplicationData.FileContext.Data.SettingData.VisualizeRuleExplorerSetting.VisualizeRuleVisibility.GetValue(res.Type, vizRule.Name, e.DisplayName) : ApplicationData.Setting.DefaultResourceVisible;
						setEventImage(treeGridView.Nodes[res.Type + ":" + res.Name].Nodes[res.Type + ":" + res.Name + ":" + vizRule.Name].Nodes[e.DisplayName], e);
					}
				}
			}

			treeGridViewRowChanged(this, EventArgs.Empty);

			ApplicationData.FileContext.Data.SettingData.ResourceExplorerSetting.BecameDirty += new EventHandler(resourceExplorerSettingBecameDirty);

			ApplicationData.FileContext.Data.SettingData.VisualizeRuleExplorerSetting.BecameDirty += new EventHandler(visualizeRuleExplorerSettingBecameDirty);

		}

		public void ClearData()
		{
			treeGridView.Clear();
		}

		protected override void OnLoad(EventArgs e)
		{
			base.OnLoad(e);

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
			treeGridView.AddColumn(new DataGridViewTextBoxColumn() { Name = "attributeValue", HeaderText = "値" });
			treeGridView.AddColumn(new TimeLineColumn() { Name = "timeLine", HeaderText = "タイムライン", AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill });

			treeGridView.DataGridView.ColumnHeadersVisible = false;
			treeGridView.DataGridView.MultiSelect = false;

			treeGridView.RowHeightChanged += treeGridViewRowChanged;
			treeGridView.RowCountChanged += treeGridViewRowChanged;
			treeGridView.SizeChanged += (o, _e) =>
			{
				bottomTimeLineScale.Location = new System.Drawing.Point(bottomTimeLineScale.Location.X, 1 + topTimeLineScale.Height + treeGridView.Height);
				hScrollBar.Location = new System.Drawing.Point(hScrollBar.Location.X, 1 + topTimeLineScale.Height + treeGridView.Height + bottomTimeLineScale.Height);
			};
			treeGridView.DataGridView.ColumnWidthChanged += (o, _e) =>
			{
				int w = 0;
				for (int i = 0; i < treeGridView.DataGridView.Columns["timeLine"].Index; i++)
				{
					w += treeGridView.DataGridView.Columns[i].Width;
				}
				TimeLineX = w + 2;
				TimeLineWidth = treeGridView.DataGridView.Columns["timeLine"].Width;
			};
			treeGridView.DataGridView.ScrollBars = ScrollBars.Vertical;

			treeGridView.DataGridView.CellPainting += (o, _e) =>
			{
				_e.Paint(_e.ClipBounds, _e.PaintParts & ~DataGridViewPaintParts.Focus);
				_e.Handled = true;
			};
			treeGridView.DataGridView.MouseWheel += (o, _e) =>
			{
				if ((Control.ModifierKeys & Keys.Control) == Keys.Control)
				{
					hScrollBar.Value += _e.Delta > 0 ? hScrollBar.Value < hScrollBar.Maximum ? 1 : 0 : hScrollBar.Value > hScrollBar.Minimum ? -1 : 0;

					((ExMouseEventArgs)_e).Handled = true;
				}
			};

			ApplicationData.FileContext.DataChanged += new EventHandler<GeneralEventArgs<TraceLogVisualizerData>>(fileContextDataChanged);

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

			int maxHeight = Height - 2 - topTimeLineScale.Height - bottomTimeLineScale.Height - hScrollBar.Height;

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

		protected void visualizeRuleExplorerSettingBecameDirty(object sender, EventArgs e)
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
						foreach (ITreeGirdViewNode n in node.Nodes.Values.Where(n => n.Name.Split(':').Last() == keys[1]))
						{
							if (kvp.Value && !node.Visible)
								continue;

							if (n.HasChildren && keys.Length == 3)
							{
								if (kvp.Value && !n.Visible)
									continue;

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
										if (ApplicationData.FileContext.Data.SettingData.ResourceExplorerSetting.ResourceVisibility.ContainsKey(keys[0], keys[1], _n.Name) ? ApplicationData.FileContext.Data.SettingData.ResourceExplorerSetting.ResourceVisibility.GetValue(keys[0], keys[1], _n.Name) : ApplicationData.Setting.DefaultResourceVisible)
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
		}

		protected void resourceExplorerSettingBecameDirty(object sender, EventArgs e)
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
		}

	}
}
