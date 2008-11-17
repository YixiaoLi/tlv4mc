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
			foreach (ResourceType resType in resourceData.ResourceHeader.ResourceTypes)
			{
				_treeView.Nodes.Add(resType.Name, resType.DisplayName);

				_treeView.Nodes[resType.Name].Nodes.Add("attributes", "属性");
				foreach (AttributeType attrType in resType.Attributes)
				{
					string name = resType.Name + ":attributes:" + attrType.Name;
					if (!ApplicationData.FileContext.Data.SettingData.ResourceTypeExplorerSetting.ResourceTypeVisibility.ContainsKey(name))
					{
						ApplicationData.FileContext.Data.SettingData.ResourceTypeExplorerSetting.ResourceTypeVisibility.Add(name, false);
					}
					_treeView.Nodes[resType.Name].Nodes["attributes"].Nodes.Add(name, attrType.DisplayName);
					_treeView.Nodes[resType.Name].Nodes["attributes"].Nodes[name].Checked = ApplicationData.FileContext.Data.SettingData.ResourceTypeExplorerSetting.ResourceTypeVisibility[name];
				}

				_treeView.Nodes[resType.Name].Nodes.Add("behaviors", "振舞い");
				foreach (Behavior bhvr in resType.Behaviors)
				{
					string name = resType.Name + ":behaviors:" + bhvr.Name;
					if (!ApplicationData.FileContext.Data.SettingData.ResourceTypeExplorerSetting.ResourceTypeVisibility.ContainsKey(name))
					{
						ApplicationData.FileContext.Data.SettingData.ResourceTypeExplorerSetting.ResourceTypeVisibility.Add(name, false);
					}
					_treeView.Nodes[resType.Name].Nodes["behaviors"].Nodes.Add(name, bhvr.DisplayName);
					_treeView.Nodes[resType.Name].Nodes["behaviors"].Nodes[name].Checked = ApplicationData.FileContext.Data.SettingData.ResourceTypeExplorerSetting.ResourceTypeVisibility[name];
				}
			}

			ApplicationData.FileContext.Data.SettingData.ResourceTypeExplorerSetting.ResourceTypeVisibility.CollectionChanged += (o, e) =>
			{
				foreach (KeyValuePair<string, bool> kvp in (IList<KeyValuePair<string, bool>>)o)
				{
					foreach (TreeNode tn in _treeView.Nodes)
					{
						if (tn.Name == kvp.Key)
							tn.Checked = kvp.Value;
					}
				}
			};

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
