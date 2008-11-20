using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using NU.OJL.MPRTOS.TLV.Base;
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

			treeGridView.RowHeightChanged += treeGridViewRowChanged;
			treeGridView.RowCountChanged += treeGridViewRowChanged;

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

						ApplicationData.FileContext.Data.SettingData.ResourceExplorerSetting.ResourceVisibility.CollectionChanged += (_o, __e) =>
						{
							foreach (KeyValuePair<string, bool> kvp in (IList<KeyValuePair<string, bool>>)_o)
							{

							}
						};

						ApplicationData.FileContext.Data.SettingData.ResourceTypeExplorerSetting.ResourceTypeVisibility.CollectionChanged += (_o, __e) =>
						{
							foreach (KeyValuePair<string, bool> kvp in (IList<KeyValuePair<string, bool>>)_o)
							{

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
				foreach (Resource res in resourceData.Resources.Where<Resource>(r=>r.Type == resType.Name))
				{
					treeGridView.Nodes[resType.Name].Add(res.Name, res.Name, "", new TimeLine());

					foreach (AttributeType attrType in resType.Attributes.Where<AttributeType>(a=>a.AllocationType == AllocationType.Dynamic && a.VisualizeRule != null))
					{
						treeGridView.Nodes[resType.Name].Nodes[res.Name].Add(attrType.Name, attrType.DisplayName, res.Attributes[attrType.Name].Value.ToString(), new TimeLine());
					}

					foreach (Behavior bhvr in resType.Behaviors.Where<Behavior>(b=>b.VisualizeRule != null))
					{
						treeGridView.Nodes[resType.Name].Nodes[res.Name].Add(bhvr.Name, bhvr.DisplayName, bhvr.Arguments.ToString(), new TimeLine());
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
			int allRowHeight = treeGridView.RowsCount * treeGridView.RowHeight + treeGridView.ColumnHeadersHeight;
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
