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

						ApplicationData.FileContext.Data.SettingData.ResourceExplorerSetting.BecameDirty += (_o, __e) =>
						{
							foreach (KeyValuePair<string, bool> kvp in (IList)_o)
							{
								string[] k = kvp.Key.Split(':');

								foreach (TreeView tv in _treeViews.Values)
								{
									foreach (TreeNode tn in tv.Nodes.Find(tv.Name + ":" + k[0], false))
									{
										foreach (TreeNode _tn in tn.Nodes.Find(k[0] + ":" +k[1], false))
										{
											if (_tn.Checked != kvp.Value)
												_tn.Checked = kvp.Value;
										}
									}
								}
							}
						};
					}
				}));
			};
		}

		public void SetData(ResourceData resourceData)
		{
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
						if (ApplicationData.FileContext.Data.SettingData.ResourceExplorerSetting.ResourceVisibility.ContainsKey(res[0], res[1])
							&& ApplicationData.FileContext.Data.SettingData.ResourceExplorerSetting.ResourceVisibility.GetValue(res[0], res[1]) != e.Node.Checked)
							ApplicationData.FileContext.Data.SettingData.ResourceExplorerSetting.ResourceVisibility.SetValue(e.Node.Checked, res[0], res[1]);
					}

				};

				tv.ExpandAll();
			}
		}

		private void setData(string name, string displayName, List<NamedResourceList> datas)
		{
			tabControl.TabPages.Add(name, displayName + "別");
			ExTreeView treeView = new ExTreeView();
			treeView.ImageList = imageList;
			treeView.Dock = DockStyle.Fill;
			treeView.CheckBoxes = true;
			treeView.Name = name;
			foreach (NamedResourceList data in datas)
			{
				treeView.Nodes.Add(name + ":" + data.Name, data.DisplayName);
				treeView.Nodes[name + ":" + data.Name].ImageKey = "resources";
				treeView.Nodes[name + ":" + data.Name].SelectedImageKey = "resources";

				foreach (Resource res in data.List)
				{
					treeView.Nodes[name + ":" + data.Name].Nodes.Add(res.Type + ":" + res.Name, res.DisplayName);
					treeView.Nodes[name + ":" + data.Name].Nodes[res.Type + ":" + res.Name].ImageKey = "resource";
					treeView.Nodes[name + ":" + data.Name].Nodes[res.Type + ":" + res.Name].SelectedImageKey = "resource";

					if (!ApplicationData.FileContext.Data.SettingData.ResourceExplorerSetting.ResourceVisibility.ContainsKey(res.Type, res.Name))
					{
						ApplicationData.FileContext.Data.SettingData.ResourceExplorerSetting.ResourceVisibility.SetValue(false, res.Type, res.Name);
					}
					else
					{
						treeView.Nodes[name + ":" + data.Name].Nodes[res.Type + ":" + res.Name].Checked = ApplicationData.FileContext.Data.SettingData.ResourceExplorerSetting.ResourceVisibility.GetValue(res.Type, res.Name);
					}
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
