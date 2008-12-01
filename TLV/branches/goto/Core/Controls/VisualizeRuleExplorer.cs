using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using NU.OJL.MPRTOS.TLV.Core;
using NU.OJL.MPRTOS.TLV.Core.FileContext.VisualizeData;

namespace NU.OJL.MPRTOS.TLV.Core.Controls
{
	public partial class VisualizeRuleExplorer : UserControl
	{

		public VisualizeRuleExplorer()
		{
			InitializeComponent();
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

						ApplicationData.FileContext.Data.SettingData.VisualizeRuleExplorerSetting.BecameDirty += (_o, __e) =>
						{
							//foreach (TreeNode tn in _treeView.Nodes)
							//{
							//    foreach (TreeNode _tn in tn.Nodes)
							//    {
							//        foreach (KeyValuePair<string[], bool> kvp in ApplicationData.FileContext.Data.SettingData.VisualizeRuleExplorerSetting.GetResourceEnumeralor())
							//        {
							//            bool flag = false;

							//            if (kvp.Key.Length == 1)
							//            {
							//                flag = (tn.Name == kvp.Key[0]);
							//                if (flag && (tn.Checked != kvp.Value))
							//                    tn.Checked = kvp.Value;
							//            }
							//            else
							//            {
							//                flag = ((tn.Name == kvp.Key[0]) && (_tn.Name == kvp.Key[1]));
							//                if (flag && (_tn.Checked != kvp.Value))
							//                    _tn.Checked = kvp.Value;
							//            }
							//        }
							//    }
							//}
						};
					}
				}));
			};
		}

		public void SetData(TraceLogVisualizerData data)
		{
			foreach (VisualizeRule vizRule in data.VisualizeData.VisualizeRules)
			{
				TreeNodeCollection tnc;
				string[] id;

				if (vizRule.IsBelongedTargetResourceType())
				{
					if (!_treeView.Nodes.ContainsKey(vizRule.Target))
					{
						_treeView.Nodes.Add(vizRule.Target, data.ResourceData.ResourceHeaders[vizRule.Target].DisplayName);
						_treeView.Nodes[vizRule.Target].Checked = false;
						_treeView.Nodes[vizRule.Target].ImageKey = "resource";
						_treeView.Nodes[vizRule.Target].SelectedImageKey = "resource";
					}

					if (!ApplicationData.FileContext.Data.SettingData.VisualizeRuleExplorerSetting.ContainsVisualizeRuleBelongedResourceVisibility(vizRule.Target, vizRule.Name))
					{
						ApplicationData.FileContext.Data.SettingData.VisualizeRuleExplorerSetting.SetVisualizeRuleBelongedResourceVisibility(false, vizRule.Target, vizRule.Name);
					}
					tnc = _treeView.Nodes[vizRule.Target].Nodes;
					id = new string[] { vizRule.Target, vizRule.Name };
				}
				else
				{
					if (!ApplicationData.FileContext.Data.SettingData.VisualizeRuleExplorerSetting.ContainsVisualizeRuleBelongedResourceVisibility(vizRule.Name))
					{
						ApplicationData.FileContext.Data.SettingData.VisualizeRuleExplorerSetting.SetVisualizeRuleBelongedResourceVisibility(false, vizRule.Name);
					}
					tnc = _treeView.Nodes;
					id = new string[] { vizRule.Name };
				}

				tnc.Add(vizRule.Name, vizRule.DisplayName);
				tnc[vizRule.Name].Checked = ApplicationData.FileContext.Data.SettingData.VisualizeRuleExplorerSetting.GetVisualizeRuleBelongedResourceVisibility(id);
				tnc[vizRule.Name].ImageKey = "visualize";
				tnc[vizRule.Name].SelectedImageKey = "visualize";

				foreach(Event e in vizRule.Events)
				{
					List<string> list = new List<string>(id);
					list.Add(e.DisplayName);
					string[] i = list.ToArray();
					if (!ApplicationData.FileContext.Data.SettingData.VisualizeRuleExplorerSetting.ContainsVisualizeRuleBelongedResourceVisibility(i))
					{
						ApplicationData.FileContext.Data.SettingData.VisualizeRuleExplorerSetting.SetVisualizeRuleBelongedResourceVisibility(false, i);
					}
					tnc[vizRule.Name].Nodes.Add(e.DisplayName, e.DisplayName);
					tnc[vizRule.Name].Nodes[e.DisplayName].Checked = ApplicationData.FileContext.Data.SettingData.VisualizeRuleExplorerSetting.GetVisualizeRuleBelongedResourceVisibility(i);
					
					if((e.Type & EventTypes.FromAttributeChange) == EventTypes.FromAttributeChange
						&& (e.Type & EventTypes.ToAttributeChange) == EventTypes.ToAttributeChange)
					{
						tnc[vizRule.Name].Nodes[e.DisplayName].ImageKey = "atr2atr";
						tnc[vizRule.Name].Nodes[e.DisplayName].SelectedImageKey = "atr2atr";
					}

					if ((e.Type & EventTypes.FromBehaviorHappen) == EventTypes.FromBehaviorHappen
						&& (e.Type & EventTypes.ToBehaviorHappen) == EventTypes.ToBehaviorHappen)
					{
						tnc[vizRule.Name].Nodes[e.DisplayName].ImageKey = "bhr2bhr";
						tnc[vizRule.Name].Nodes[e.DisplayName].SelectedImageKey = "bhr2bhr";
					}

					if ((e.Type & EventTypes.FromAttributeChange) == EventTypes.FromAttributeChange
						&& (e.Type & EventTypes.ToBehaviorHappen) == EventTypes.ToBehaviorHappen)
					{
						tnc[vizRule.Name].Nodes[e.DisplayName].ImageKey = "atr2bhr";
						tnc[vizRule.Name].Nodes[e.DisplayName].SelectedImageKey = "atr2bhr";
					}

					if ((e.Type & EventTypes.FromBehaviorHappen) == EventTypes.FromBehaviorHappen
						&& (e.Type & EventTypes.ToAttributeChange) == EventTypes.ToAttributeChange)
					{
						tnc[vizRule.Name].Nodes[e.DisplayName].ImageKey = "bhr2atr";
						tnc[vizRule.Name].Nodes[e.DisplayName].SelectedImageKey = "bhr2atr";
					}

					if ((e.Type & EventTypes.WhenAttributeChange) == EventTypes.WhenAttributeChange)
					{
						tnc[vizRule.Name].Nodes[e.DisplayName].ImageKey = "attribute";
						tnc[vizRule.Name].Nodes[e.DisplayName].SelectedImageKey = "attribute";
					}

					if ((e.Type & EventTypes.WhenBehaviorHappen) == EventTypes.WhenBehaviorHappen)
					{
						tnc[vizRule.Name].Nodes[e.DisplayName].ImageKey = "behavior";
						tnc[vizRule.Name].Nodes[e.DisplayName].SelectedImageKey = "behavior";
					}

					if ((e.Type & EventTypes.Error) == EventTypes.Error)
					{
						tnc[vizRule.Name].Nodes[e.DisplayName].ImageKey = "warning";
						tnc[vizRule.Name].Nodes[e.DisplayName].SelectedImageKey = "warning";
					}
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
			}

			_treeView.AfterCheck += (o, e) =>
			{
				if (e.Node.Level == 0)
				{
					foreach(TreeNode tn in e.Node.Nodes)
					{
						if (tn.Checked != e.Node.Checked)
							tn.Checked = e.Node.Checked;
					}
				}
				else if (e.Node.Level == 1)
				{
					if (ApplicationData.FileContext.Data.SettingData.VisualizeRuleExplorerSetting.ContainsVisualizeRuleBelongedResourceVisibility(e.Node.Parent.Name, e.Node.Name)
						&& ApplicationData.FileContext.Data.SettingData.VisualizeRuleExplorerSetting.GetVisualizeRuleBelongedResourceVisibility(e.Node.Parent.Name, e.Node.Name) != e.Node.Checked)
					{
						ApplicationData.FileContext.Data.SettingData.VisualizeRuleExplorerSetting.SetVisualizeRuleBelongedResourceVisibility(e.Node.Checked, e.Node.Parent.Name, e.Node.Name);
					}

					setParentNodeCheck(e.Node.Parent);

					foreach (TreeNode tn in e.Node.Nodes)
					{
						if (tn.Checked != e.Node.Checked)
							tn.Checked = e.Node.Checked;
					}
				}
				else
				{
					if (ApplicationData.FileContext.Data.SettingData.VisualizeRuleExplorerSetting.ContainsVisualizeRuleBelongedResourceVisibility(e.Node.Parent.Parent.Name, e.Node.Parent.Name, e.Node.Name)
						&& ApplicationData.FileContext.Data.SettingData.VisualizeRuleExplorerSetting.GetVisualizeRuleBelongedResourceVisibility(e.Node.Parent.Parent.Name, e.Node.Parent.Name, e.Node.Name) != e.Node.Checked)
					{
						ApplicationData.FileContext.Data.SettingData.VisualizeRuleExplorerSetting.SetVisualizeRuleBelongedResourceVisibility(e.Node.Checked, e.Node.Parent.Parent.Name, e.Node.Parent.Name, e.Node.Name);
					}

					setParentNodeCheck(e.Node.Parent);
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
}
