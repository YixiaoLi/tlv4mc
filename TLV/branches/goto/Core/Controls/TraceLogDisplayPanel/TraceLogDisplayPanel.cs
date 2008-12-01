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
						SetData(ApplicationData.FileContext.Data);
						treeGridViewRowChanged(this, EventArgs.Empty);

						//ApplicationData.FileContext.Data.SettingData.ResourceExplorerSetting.BecameDirty += (_o, __e) =>
						//    {
						//        string[] res = (string[])_o;

						//        if (res.Length == 1)
						//        {

						//        }
						//        else
						//        {

						//        }

						//        if (treeGridView.Nodes[res[0]].Visible)
						//        {
						//            treeGridView.Nodes[res[0]].Nodes[res[1]].Visible = kvp.Value;

						//            foreach (ITreeGirdViewNode node in treeGridView.Nodes[res[0]].Nodes[res[1]].Nodes.Values)
						//            {
						//                if (kvp.Value
						//                    && ApplicationData.FileContext.Data.SettingData.VisualizeRuleExplorerSetting.ResourcePropertyVisibility.ContainsKey(m.Groups["type"].Value + node.Name))
						//                {
						//                    node.Visible = ApplicationData.FileContext.Data.SettingData.ResourcePropertyExplorerSetting.ResourcePropertyVisibility[m.Groups["type"].Value + node.Name];
						//                }
						//                else
						//                {
						//                    node.Visible = false;
						//                }
						//            }
						//            treeGridViewRowChanged(this, EventArgs.Empty);
						//        }
						//    };

						//ApplicationData.FileContext.Data.SettingData.ResourcePropertyExplorerSetting.ResourcePropertyVisibility.CollectionChanged += (_o, __e) =>
						//    {
						//        foreach (KeyValuePair<string, bool> kvp in (IList<KeyValuePair<string, bool>>)_o)
						//        {
						//            Match m = Regex.Match(kvp.Key, @"(?<type>.*)(?<attr_or_bhvr>(" + ResourcePropertyExplorerSetting.AttributeSeparateText + @"|" + ResourcePropertyExplorerSetting.BehaviorSeparateText + @"))(?<name>.*)");
						//            if(m.Success)
						//            {
						//                if (treeGridView.Nodes[m.Groups["type"].Value].Visible)
						//                {
						//                    foreach (ITreeGirdViewNode node in treeGridView.Nodes[m.Groups["type"].Value].Nodes.Values)
						//                    {
						//                        if(node.Visible)
						//                            node.Nodes[m.Groups["attr_or_bhvr"].Value + m.Groups["name"].Value].Visible = kvp.Value;
						//                    }
						//                    treeGridViewRowChanged(this, EventArgs.Empty);
						//                }
						//            }
						//        }
						//    };
					}
				}));
			};
		}

		public void SetData(TraceLogVisualizerData data)
		{
			foreach (VisualizeRule vizRule in data.VisualizeData.VisualizeRules.Where<VisualizeRule>(v => !v.IsBelongedTargetResourceType()))
			{
				treeGridView.Add(vizRule.Name, vizRule.DisplayName, "", new TimeLine());
				foreach(Event e in vizRule.Events)
				{
					treeGridView.Nodes[vizRule.Name].Add(e.DisplayName, e.DisplayName, "", new TimeLine());
				}
			}
			foreach (VisualizeRule vizRule in data.VisualizeData.VisualizeRules.Where<VisualizeRule>(v => v.IsBelongedTargetResourceType()))
			{
				foreach(Resource res in data.ResourceData.Resources.Where<Resource>(r=>r.Type == vizRule.Target))
				{
					if (!treeGridView.Nodes.ContainsKey(res.Type + ":" + res.Name))
					{
						treeGridView.Add(res.Type + ":" + res.Name, res.DisplayName, "", new TimeLine());
					}

					treeGridView.Nodes[res.Type + ":" + res.Name].Add(res.Type + ":" + res.Name + ":" + vizRule.Name, vizRule.DisplayName, "", new TimeLine());

					foreach (Event e in vizRule.Events)
					{
						treeGridView.Nodes[res.Type + ":" + res.Name].Nodes[res.Type + ":" + res.Name + ":" + vizRule.Name].Add(e.DisplayName, e.DisplayName, "", new TimeLine());
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
