using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using NU.OJL.MPRTOS.TLV.Core;

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
					}
				}));
			};
		}

		public void SetData(ResourceData resourceData)
		{
			List<NamedResourceList> resTypeClass = new List<NamedResourceList>();

			foreach(ResourceType resType in resourceData.ResourceHeader.ResourceTypes)
			{
				resTypeClass.Add(new NamedResourceList() { Name = resType.Name, DisplayName = resType.DisplayName, List = resourceData.Resources.Where<Resource>(r=>r.Type == resType.Name).ToList() });
			}

			setData("___resType", "リソースタイプ", resTypeClass);

			foreach (ResourceType resType in resourceData.ResourceHeader.ResourceTypes)
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

			ApplicationData.FileContext.Data.SettingData.ResourceExplorerSetting.ResourceVisibility.CollectionChanged += (o, e) =>
			{
				foreach (KeyValuePair<string, bool> kvp in (IList<KeyValuePair<string, bool>>)o)
				{
					foreach (TreeView tv in _treeViews.Values)
					{
						foreach (TreeNode tn in tv.Nodes)
						{
							foreach (TreeNode _tn in tn.Nodes)
							{
								if (_tn.Name == kvp.Key)
									_tn.Checked = kvp.Value;
							}
						}
					}
				}
			};

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

				foreach (TreeNode tn in tv.Nodes)
				{
					setParentNodeCheck(tn);
					setParentNodeCheck(tn);
				}

				tv.AfterCheck += (o, e) =>
				{
					if (e.Node.Level == 0)
					{
						foreach(TreeNode tn in e.Node.Nodes)
						{
							if (tn.Checked != e.Node.Checked)
								tn.Checked = e.Node.Checked;
						}
					}
					else
					{
						if (ApplicationData.FileContext.Data.SettingData.ResourceExplorerSetting.ResourceVisibility.ContainsKey(e.Node.Name)
							&& ApplicationData.FileContext.Data.SettingData.ResourceExplorerSetting.ResourceVisibility[e.Node.Name] != e.Node.Checked)
							ApplicationData.FileContext.Data.SettingData.ResourceExplorerSetting.ResourceVisibility[e.Node.Name] = e.Node.Checked;

						setParentNodeCheck(e.Node.Parent);
						setParentNodeCheck(e.Node.Parent);
					}

				};

				tv.ExpandAll();
			}
		}

		private void setParentNodeCheck(TreeNode parent)
		{
			setParentNodeCheck(parent, true);
			setParentNodeCheck(parent, false);
		}
		private void setParentNodeCheck(TreeNode parent, bool f)
		{
			bool flag = true;
			foreach (TreeNode tn in parent.Nodes)
			{
				if (tn.Checked != f)
				{
					flag = false;
					break;
				}
			}
			if (flag)
			{
				if (parent.Checked != f)
					parent.Checked = f;
			}
		}

		private void setData(string name, string displayName, List<NamedResourceList> datas)
		{
			tabControl.TabPages.Add(name, displayName + "別");
			TreeView treeView = new TreeView();
			treeView.Dock = DockStyle.Fill;
			treeView.CheckBoxes = true;
			foreach (NamedResourceList data in datas)
			{
				treeView.Nodes.Add(name + ":" + data.Name, data.DisplayName);

				foreach (Resource res in data.List)
				{
					treeView.Nodes[name + ":" + data.Name].Nodes.Add(res.Name, res.Name);
					if (!ApplicationData.FileContext.Data.SettingData.ResourceExplorerSetting.ResourceVisibility.ContainsKey(res.Name))
					{
						ApplicationData.FileContext.Data.SettingData.ResourceExplorerSetting.ResourceVisibility.Add(res.Name, false);
					}
					else
					{
						treeView.Nodes[name + ":" + data.Name].Nodes[res.Name].Checked = ApplicationData.FileContext.Data.SettingData.ResourceExplorerSetting.ResourceVisibility[res.Name];
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
