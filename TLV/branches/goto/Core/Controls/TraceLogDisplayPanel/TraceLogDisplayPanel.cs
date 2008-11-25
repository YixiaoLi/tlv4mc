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

namespace NU.OJL.MPRTOS.TLV.Core.Controls
{
	public partial class TraceLogDisplayPanel : UserControl
	{
		public TraceLogDisplayPanel()
		{
			InitializeComponent();
		}

		protected override void OnLoad(EventArgs e)
		{
			base.OnLoad(e);

			treeGridView.AddColumn(new TreeGridViewColumn() { Name = "resourceName", HeaderText = "リソース" });
			treeGridView.AddColumn(new DataGridViewTextBoxColumn() { Name = "attributeValue", HeaderText = "値" });
			treeGridView.AddColumn(new TimeLineColumn() { Name = "timeLine", HeaderText = "タイムライン", AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill });

			treeGridView.DataGridView.ColumnHeadersVisible = false;
			treeGridView.DataGridView.MultiSelect = false;

			treeGridView.RowHeightChanged += treeGridViewRowChanged;
			treeGridView.RowCountChanged += treeGridViewRowChanged;

			treeGridView.DataGridView.CellPainting += (o, _e) =>
				{
					_e.Paint(_e.ClipBounds, _e.PaintParts & ~DataGridViewPaintParts.Focus);
					_e.Handled = true;
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
						SetData(ApplicationData.FileContext.Data.ResourceData);
						treeGridViewRowChanged(this, EventArgs.Empty);

						ApplicationData.FileContext.Data.SettingData.ResourceExplorerSetting.ResourceVisibility.CollectionChanged += (_o, __e) =>
						{
							foreach (KeyValuePair<string, bool> kvp in (IList<KeyValuePair<string, bool>>)_o)
							{
								Match m = Regex.Match(kvp.Key, @"(?<type>.*)" + ResourceExplorerSetting.ResourceSeparateText + @"(?<name>.*)");
								if(m.Success)
								{
									treeGridView.Nodes[m.Groups["type"].Value].Nodes[m.Groups["name"].Value].Visible = kvp.Value;
									foreach (ITreeGirdViewNode node in treeGridView.Nodes[m.Groups["type"].Value].Nodes[m.Groups["name"].Value].Nodes.Values)
									{
										if (kvp.Value
											&& ApplicationData.FileContext.Data.SettingData.ResourceTypeExplorerSetting.ResourceTypeVisibility.ContainsKey(m.Groups["type"].Value + node.Name))
										{
											node.Visible = ApplicationData.FileContext.Data.SettingData.ResourceTypeExplorerSetting.ResourceTypeVisibility[m.Groups["type"].Value + node.Name];
										}
										else
										{
											node.Visible = false;
										}
									}
									treeGridViewRowChanged(this, EventArgs.Empty);
								}
							}
						};

						ApplicationData.FileContext.Data.SettingData.ResourceTypeExplorerSetting.ResourceTypeVisibility.CollectionChanged += (_o, __e) =>
						{
							foreach (KeyValuePair<string, bool> kvp in (IList<KeyValuePair<string, bool>>)_o)
							{
								Match m = Regex.Match(kvp.Key, @"(?<type>.*)(?<attr_or_bhvr>(" + ResourceTypeExplorerSetting.AttributeSeparateText + @"|" + ResourceTypeExplorerSetting.BehaviorSeparateText + @"))(?<name>.*)");
								if(m.Success)
								{
									foreach (ITreeGirdViewNode node in treeGridView.Nodes[m.Groups["type"].Value].Nodes.Values)
									{
										if(node.Visible)
											node.Nodes[m.Groups["attr_or_bhvr"].Value + m.Groups["name"].Value].Visible = kvp.Value;
									}
									treeGridViewRowChanged(this, EventArgs.Empty);
								}
							}
						};
					}
				}));
			};
		}

		public void SetData(ResourceData resourceData)
		{
			foreach (ResourceType resType in resourceData.ResourceHeader.ResourceTypes)
			{
				treeGridView.Add(resType.Name, resType.DisplayName, "", new TimeLine());
				treeGridView.Nodes[resType.Name].Expand();
				foreach (Resource res in resourceData.Resources.Where<Resource>(r=>r.Type == resType.Name))
				{
					treeGridView.Nodes[resType.Name].Add(res.Name, res.Name, "", new TimeLine());

					if (ApplicationData.FileContext.Data.SettingData.ResourceExplorerSetting.ResourceVisibility.ContainsKey(resType.Name + ResourceExplorerSetting.ResourceSeparateText + res.Name))
						treeGridView.Nodes[resType.Name].Nodes[res.Name].Visible = ApplicationData.FileContext.Data.SettingData.ResourceExplorerSetting.ResourceVisibility[resType.Name + ResourceExplorerSetting.ResourceSeparateText + res.Name];

					foreach (AttributeType attrType in resType.Attributes.Where<AttributeType>(a=>a.AllocationType == AllocationType.Dynamic && a.VisualizeRule != null))
					{
						treeGridView.Nodes[resType.Name].Nodes[res.Name].Add(ResourceTypeExplorerSetting.AttributeSeparateText + attrType.Name, attrType.DisplayName, res.Attributes[attrType.Name].Value.ToString(), new TimeLine());
						treeGridView.Nodes[resType.Name].Nodes[res.Name].Nodes[ResourceTypeExplorerSetting.AttributeSeparateText + attrType.Name].Image = Properties.Resources.attribute;

						if (ApplicationData.FileContext.Data.SettingData.ResourceTypeExplorerSetting.ResourceTypeVisibility.ContainsKey(resType.Name + ResourceTypeExplorerSetting.AttributeSeparateText + attrType.Name))
							treeGridView.Nodes[resType.Name].Nodes[res.Name].Nodes[ResourceTypeExplorerSetting.AttributeSeparateText + attrType.Name].Visible = ApplicationData.FileContext.Data.SettingData.ResourceTypeExplorerSetting.ResourceTypeVisibility[resType.Name + ResourceTypeExplorerSetting.AttributeSeparateText + attrType.Name];
					}

					foreach (Behavior bhvr in resType.Behaviors.Where<Behavior>(b=>b.VisualizeRule != null))
					{
						treeGridView.Nodes[resType.Name].Nodes[res.Name].Add(ResourceTypeExplorerSetting.BehaviorSeparateText + bhvr.Name, bhvr.DisplayName, bhvr.Arguments.ToString(), new TimeLine());
						treeGridView.Nodes[resType.Name].Nodes[res.Name].Nodes[ResourceTypeExplorerSetting.BehaviorSeparateText + bhvr.Name].Image = Properties.Resources.behavior;

						if (ApplicationData.FileContext.Data.SettingData.ResourceTypeExplorerSetting.ResourceTypeVisibility.ContainsKey(resType.Name + ResourceTypeExplorerSetting.BehaviorSeparateText + bhvr.Name))
							treeGridView.Nodes[resType.Name].Nodes[res.Name].Nodes[ResourceTypeExplorerSetting.BehaviorSeparateText + bhvr.Name].Visible = ApplicationData.FileContext.Data.SettingData.ResourceTypeExplorerSetting.ResourceTypeVisibility[resType.Name + ResourceTypeExplorerSetting.BehaviorSeparateText + bhvr.Name];
					}
				}
			}
		}

		public void ClearData()
		{
			treeGridView.Clear();
		}

		protected override void OnSizeChanged(EventArgs e)
		{
			base.OnSizeChanged(e);
			treeGridViewRowChanged(this, e);
		}

		void treeGridViewRowChanged(object sender, EventArgs e)
		{
			int allRowHeight = treeGridView.VisibleRowsCount * treeGridView.RowHeight;

			allRowHeight += treeGridView.DataGridView.ColumnHeadersVisible ? treeGridView.ColumnHeadersHeight : 1;

			int maxHeight = Height - 2;

			if ( allRowHeight > maxHeight)
			{
				treeGridView.Height = (int)((double)allRowHeight - Math.Ceiling((double)(allRowHeight - maxHeight) / (double)treeGridView.RowHeight) * (double)treeGridView.RowHeight);
			}
			else
			{
				treeGridView.Height = allRowHeight;
			}
		}

	}
}
