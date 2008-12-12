using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using NU.OJL.MPRTOS.TLV.Core;
using System.Text.RegularExpressions;
using NU.OJL.MPRTOS.TLV.Base.Controls;
using System.Collections;
using NU.OJL.MPRTOS.TLV.Base;

namespace NU.OJL.MPRTOS.TLV.Core.Controls
{
	public partial class ResourceExplorer : UserControl
	{
		private Dictionary<string, TreeView> _treeViews = new Dictionary<string,TreeView>();

		public ResourceExplorer()
		{
			InitializeComponent();
		}

		protected override void OnLoad(EventArgs e)
		{
			base.OnLoad(e);

			imageList.Images.Add("resource", Properties.Resources.resource);
			imageList.Images.Add("resources", Properties.Resources.resources);

			tabControl.MouseEnter += (o, _e) =>
			{
				ApplicationFactory.StatusManager.ShowHint(tabControl.GetType().ToString() + ":checkIs", "分類切替え", "クリック");
			};
			tabControl.MouseLeave += (o, _e) =>
			{
				ApplicationFactory.StatusManager.HideHint(tabControl.GetType().ToString() + ":checkIs");
			};
			tabControl.LostFocus += (o, _e) =>
			{
				ApplicationFactory.StatusManager.HideHint(tabControl.GetType().ToString() + ":checkIs");
			};

			ApplicationData.FileContext.DataChanged += new EventHandler<NU.OJL.MPRTOS.TLV.Base.GeneralEventArgs<TraceLogVisualizerData>>(fileContextDataChanged);

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
					SetData(ApplicationData.FileContext.Data.ResourceData);
				}
			}));
		}

		public void SetData(ResourceData resourceData)
		{
			ClearData();

			List<NamedResourceList> resTypeClass = new List<NamedResourceList>();

			foreach(ResourceType resType in resourceData.ResourceHeaders.ResourceTypes)
			{
				resTypeClass.Add(new NamedResourceList() { Name = resType.Name, DisplayName = resType.DisplayName, List = resourceData.Resources.Where<Resource>(r=>r.Type == resType.Name).ToList() });
			}

			setData("___resType", "リソースタイプ", resTypeClass);

			foreach (ResourceType resType in resourceData.ResourceHeaders.ResourceTypes)
			{
				foreach(AttributeType attr in resType.Attributes.Where<AttributeType>(a=>a.CanGrouping == true))
				{
					List<NamedResourceList> datas = new List<NamedResourceList>();

					foreach(Resource res in resourceData.Resources)
					{
						string value = res.Attributes[attr.Name].Value.ToString();
						if (!datas.Any<NamedResourceList>(d => d.Name == value))
						{
							datas.Add(new NamedResourceList()
							{
								Name = value,
								DisplayName = value,
								List = resourceData.Resources.Where<Resource>(r =>
									{
										return r.Attributes.ContainsKey(attr.Name) && r.Attributes[attr.Name].Value.ToString() == value;
									}).OrderBy(r=>r.Attributes[attr.Name].Value).ToList()
							});
						}
					}
					datas.Sort((n1,n2) =>
						{
							switch(attr.VariableType)
							{
								case NU.OJL.MPRTOS.TLV.Base.JsonValueType.Decimal:
									return Convert.ToDecimal(n1.Name).CompareTo(Convert.ToDecimal(n2.Name));
								default:
									return n1.Name.CompareTo(n1.Name);
							}
						});
					setData(attr.Name, attr.DisplayName, datas);
				}
			}

			foreach (TreeView tv in _treeViews.Values)
			{
				tv.MouseEnter += (o, e) =>
				{
					ApplicationFactory.StatusManager.ShowHint(tv.GetType().ToString() + ":checkIs", "可視化表示", "チェック");
				};
				tv.MouseLeave += (o, e) =>
				{
					ApplicationFactory.StatusManager.HideHint(tv.GetType().ToString() + ":checkIs");
				};
				tv.LostFocus += (o, e) =>
				{
					ApplicationFactory.StatusManager.HideHint(tv.GetType().ToString() + ":checkIs");
				};

				tv.AfterCheck += (o, e) =>
				{
					if (e.Node.Level == 1)
					{
						string[] res = e.Node.Name.Split(':');

						setVisibility(e.Node.Checked, res);
					}
				};

				tv.ExpandAll();
			}

			ApplicationData.FileContext.Data.SettingData.ResourceExplorerSetting.BecameDirty += (_o, __e) =>
			{
				foreach (KeyValuePair<string, bool> kvp in (IList)_o)
				{
					string[] k = kvp.Key.Split(':');

					foreach (TreeView tv in _treeViews.Values)
					{
						foreach (TreeNode tn in tv.Nodes)
						{
							foreach (TreeNode _tn in tn.Nodes.Find(k[0] + ":" + k[1], false))
							{
								if (_tn.Checked != kvp.Value)
									_tn.Checked = kvp.Value;
							}
						}
					}
				}
			};
		}

		private void setVisibility(bool value, params string[] keys)
		{
			if (ApplicationData.FileContext.Data.SettingData.ResourceExplorerSetting.ResourceVisibility.ContainsKey(keys)
				&& (ApplicationData.FileContext.Data.SettingData.ResourceExplorerSetting.ResourceVisibility.GetValue(keys) == value))
				return;
			else
				ApplicationData.FileContext.Data.SettingData.ResourceExplorerSetting.ResourceVisibility.SetValue(value, keys);
		}

		private void setData(string name, string displayName, List<NamedResourceList> groups)
		{
			tabControl.TabPages.Add(name, displayName + "別");
			ExTreeView treeView = new ExTreeView();
			treeView.ImageList = imageList;
			treeView.Dock = DockStyle.Fill;
			treeView.CheckBoxes = true;
			treeView.Name = name;
			foreach (NamedResourceList group in groups)
			{
				treeView.Nodes.Add(name + ":" + group.Name, group.DisplayName);
				treeView.Nodes[name + ":" + group.Name].ImageKey = "resources";
				treeView.Nodes[name + ":" + group.Name].SelectedImageKey = "resources";

				foreach (Resource res in group.List)
				{
					treeView.Nodes[name + ":" + group.Name].Nodes.Add(res.Type + ":" + res.Name, res.DisplayName);
					treeView.Nodes[name + ":" + group.Name].Nodes[res.Type + ":" + res.Name].ImageKey = "resource";
					treeView.Nodes[name + ":" + group.Name].Nodes[res.Type + ":" + res.Name].SelectedImageKey = "resource";

					treeView.Nodes[name + ":" + group.Name].Nodes[res.Type + ":" + res.Name].Checked = ApplicationData.FileContext.Data.SettingData.ResourceExplorerSetting.ResourceVisibility.ContainsKey(res.Type, res.Name) ? ApplicationData.FileContext.Data.SettingData.ResourceExplorerSetting.ResourceVisibility.GetValue(res.Type, res.Name) : ApplicationData.Setting.DefaultResourceVisible;
				}
			}
			tabControl.TabPages[name].Controls.Add(treeView);

			_treeViews.Add(name, treeView);
		}

		public void ClearData()
		{
			tabControl.TabPages.Clear();
			_treeViews.Clear();
		}

	}

	class NamedResourceList
	{
		public string Name{get;set;}
		public string DisplayName { get; set; }
		public List<Resource> List { get;set; }
	}
}
