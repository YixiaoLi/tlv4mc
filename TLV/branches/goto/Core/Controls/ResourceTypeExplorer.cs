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
	public partial class ResourceTypeExplorer : UserControl
	{

		public ResourceTypeExplorer()
		{
			InitializeComponent();
		}

		protected override void OnLoad(EventArgs e)
		{
			base.OnLoad(e);

			imageList.Images.Add("attribute", Properties.Resources.attribute);
			imageList.Images.Add("behavior", Properties.Resources.behavior);
			imageList.Images.Add("resource", Properties.Resources.resource);

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

						ApplicationData.FileContext.Data.SettingData.ResourceTypeExplorerSetting.ResourceTypeVisibility.CollectionChanged += (_o, __e) =>
						{
							foreach (KeyValuePair<string, bool> kvp in (IList<KeyValuePair<string, bool>>)_o)
							{
								foreach (TreeNode tn in _treeView.Nodes)
								{
									if (tn.Name == kvp.Key)
										tn.Checked = kvp.Value;
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
				_treeView.Nodes.Add(resType.Name, resType.DisplayName);
				_treeView.Nodes[resType.Name].ImageKey = "resource";
				_treeView.Nodes[resType.Name].SelectedImageKey = "resource";

				_treeView.Nodes[resType.Name].Nodes.Add(ResourceTypeExplorerSetting.AttributeSeparateText, "属性");
				_treeView.Nodes[resType.Name].Nodes[ResourceTypeExplorerSetting.AttributeSeparateText].ImageKey = "attribute";
				_treeView.Nodes[resType.Name].Nodes[ResourceTypeExplorerSetting.AttributeSeparateText].SelectedImageKey = "attribute";

				foreach (AttributeType attrType in resType.Attributes.Where<AttributeType>(a=>a.AllocationType == AllocationType.Dynamic && a.VisualizeRule != null))
				{
					string name = resType.Name + ResourceTypeExplorerSetting.AttributeSeparateText + attrType.Name;
					if (!ApplicationData.FileContext.Data.SettingData.ResourceTypeExplorerSetting.ResourceTypeVisibility.ContainsKey(name))
					{
						ApplicationData.FileContext.Data.SettingData.ResourceTypeExplorerSetting.ResourceTypeVisibility.Add(name, false);
					}
					_treeView.Nodes[resType.Name].Nodes[ResourceTypeExplorerSetting.AttributeSeparateText].Nodes.Add(name, attrType.DisplayName);
					_treeView.Nodes[resType.Name].Nodes[ResourceTypeExplorerSetting.AttributeSeparateText].Nodes[name].Checked = ApplicationData.FileContext.Data.SettingData.ResourceTypeExplorerSetting.ResourceTypeVisibility[name];
					_treeView.Nodes[resType.Name].Nodes[ResourceTypeExplorerSetting.AttributeSeparateText].Nodes[name].ImageKey = "attribute";
					_treeView.Nodes[resType.Name].Nodes[ResourceTypeExplorerSetting.AttributeSeparateText].Nodes[name].SelectedImageKey = "attribute";
				}

				_treeView.Nodes[resType.Name].Nodes.Add(ResourceTypeExplorerSetting.BehaviorSeparateText, "振舞い");
				_treeView.Nodes[resType.Name].Nodes[ResourceTypeExplorerSetting.BehaviorSeparateText].ImageKey = "behavior";
				_treeView.Nodes[resType.Name].Nodes[ResourceTypeExplorerSetting.BehaviorSeparateText].SelectedImageKey = "behavior";

				foreach (Behavior bhvr in resType.Behaviors.Where<Behavior>(b=>b.VisualizeRule != null))
				{
					string name = resType.Name + ResourceTypeExplorerSetting.BehaviorSeparateText + bhvr.Name;
					if (!ApplicationData.FileContext.Data.SettingData.ResourceTypeExplorerSetting.ResourceTypeVisibility.ContainsKey(name))
					{
						ApplicationData.FileContext.Data.SettingData.ResourceTypeExplorerSetting.ResourceTypeVisibility.Add(name, false);
					}
					_treeView.Nodes[resType.Name].Nodes[ResourceTypeExplorerSetting.BehaviorSeparateText].Nodes.Add(name, bhvr.DisplayName);
					_treeView.Nodes[resType.Name].Nodes[ResourceTypeExplorerSetting.BehaviorSeparateText].Nodes[name].Checked = ApplicationData.FileContext.Data.SettingData.ResourceTypeExplorerSetting.ResourceTypeVisibility[name];
					_treeView.Nodes[resType.Name].Nodes[ResourceTypeExplorerSetting.BehaviorSeparateText].Nodes[name].ImageKey = "behavior";
					_treeView.Nodes[resType.Name].Nodes[ResourceTypeExplorerSetting.BehaviorSeparateText].Nodes[name].SelectedImageKey = "behavior";
				}
			}

			_treeView.MouseEnter += (o, e) =>
			{
				ApplicationFactory.StatusManager.ShowHint(_treeView.GetType().ToString() + ":checkIs", "可視化表示", "チェック");
			};
			_treeView.MouseLeave += (o, e) =>
			{
				ApplicationFactory.StatusManager.HideHint(_treeView.GetType().ToString() + ":checkIs");
			};
			_treeView.LostFocus += (o, e) =>
			{
				ApplicationFactory.StatusManager.HideHint(_treeView.GetType().ToString() + ":checkIs");
			};

			foreach (TreeNode tn in _treeView.Nodes)
			{
				setParentNodeCheck(tn);

				foreach (TreeNode _tn in tn.Nodes)
				{
					setParentNodeCheck(_tn);
				}
			}

			_treeView.AfterCheck += (o, e) =>
			{
				if (e.Node.Level != 2)
				{
					foreach(TreeNode tn in e.Node.Nodes)
					{
						if (tn.Checked != e.Node.Checked)
							tn.Checked = e.Node.Checked;
					}
				}
				else
				{
					if (ApplicationData.FileContext.Data.SettingData.ResourceTypeExplorerSetting.ResourceTypeVisibility.ContainsKey(e.Node.Name)
						&& ApplicationData.FileContext.Data.SettingData.ResourceTypeExplorerSetting.ResourceTypeVisibility[e.Node.Name] != e.Node.Checked)
					{
						ApplicationData.FileContext.Data.SettingData.ResourceTypeExplorerSetting.ResourceTypeVisibility[e.Node.Name] = e.Node.Checked;
					}

					setParentNodeCheck(e.Node.Parent);

					if(e.Node.Level == 2)
					{
						setParentNodeCheck(e.Node.Parent.Parent);
					}
				}

			};

			_treeView.ExpandAll();

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

		public void ClearData()
		{
			_treeView.Nodes.Clear();
		}

	}

	class NamedResourceTypeList
	{
		public string Name{get;set;}
		public string DisplayName { get; set; }
		public List<ResourceType> List { get; set; }
	}
}
