using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using NU.OJL.MPRTOS.TLV.Core;
using System.Collections;
using NU.OJL.MPRTOS.TLV.Base;

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
					}
				}));
			};
		}

		public void SetData(TraceLogVisualizerData data)
		{
			ClearData();

			foreach (VisualizeRule vizRule in data.VisualizeData.VisualizeRules)
			{
				TreeNodeCollection tnc;
				string[] id;

				if (vizRule.IsBelongedTargetResourceType())
				{
					if (!_treeView.Nodes.ContainsKey(vizRule.Target))
					{
						_treeView.Nodes.Add(vizRule.Target, data.ResourceData.ResourceHeaders[vizRule.Target].DisplayName);
						_treeView.Nodes[vizRule.Target].Checked = ApplicationData.Setting.DefaultResourceVisible;
						_treeView.Nodes[vizRule.Target].ImageKey = "resource";
						_treeView.Nodes[vizRule.Target].SelectedImageKey = "resource";

						if (!ApplicationData.FileContext.Data.SettingData.VisualizeRuleExplorerSetting.VisualizeRuleVisibility.ContainsKey(vizRule.Target))
						{
							ApplicationData.FileContext.Data.SettingData.VisualizeRuleExplorerSetting.VisualizeRuleVisibility.SetValue(ApplicationData.Setting.DefaultVisualizeRuleVisible, vizRule.Target);
						}
					}

					tnc = _treeView.Nodes[vizRule.Target].Nodes;
					id = new string[] { vizRule.Target, vizRule.Name };
				}
				else
				{
					tnc = _treeView.Nodes;
					id = new string[] { vizRule.Name };
				}

				tnc.Add(vizRule.Name, vizRule.DisplayName);
				tnc[vizRule.Name].Checked = ApplicationData.FileContext.Data.SettingData.VisualizeRuleExplorerSetting.VisualizeRuleVisibility.ContainsKey(id) ? ApplicationData.FileContext.Data.SettingData.VisualizeRuleExplorerSetting.VisualizeRuleVisibility.GetValue(id) : ApplicationData.Setting.DefaultVisualizeRuleVisible;
				tnc[vizRule.Name].ImageKey = "visualize";
				tnc[vizRule.Name].SelectedImageKey = "visualize";

				if (!ApplicationData.FileContext.Data.SettingData.VisualizeRuleExplorerSetting.VisualizeRuleVisibility.ContainsKey(id))
				{
					ApplicationData.FileContext.Data.SettingData.VisualizeRuleExplorerSetting.VisualizeRuleVisibility.SetValue(ApplicationData.Setting.DefaultResourceVisible, id);
				}

				foreach(Event e in vizRule.Events)
				{
					List<string> list = new List<string>(id);
					list.Add(e.DisplayName);
					string[] i = list.ToArray();
					tnc[vizRule.Name].Nodes.Add(e.DisplayName, e.DisplayName);
					tnc[vizRule.Name].Nodes[e.DisplayName].Checked = ApplicationData.FileContext.Data.SettingData.VisualizeRuleExplorerSetting.VisualizeRuleVisibility.ContainsKey(i) ? ApplicationData.FileContext.Data.SettingData.VisualizeRuleExplorerSetting.VisualizeRuleVisibility.GetValue(i) : ApplicationData.Setting.DefaultVisualizeRuleVisible;

					tnc[vizRule.Name].Nodes[e.DisplayName].ImageKey = e.getImageKey();
					tnc[vizRule.Name].Nodes[e.DisplayName].SelectedImageKey = e.getImageKey();

					if (!ApplicationData.FileContext.Data.SettingData.VisualizeRuleExplorerSetting.VisualizeRuleVisibility.ContainsKey(i))
					{
						ApplicationData.FileContext.Data.SettingData.VisualizeRuleExplorerSetting.VisualizeRuleVisibility.SetValue(ApplicationData.Setting.DefaultVisualizeRuleVisible, i);
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

			_treeView.AfterCheck += (o, e) =>
			{
				if (e.Node.Level == 0)
				{
					setVisibility(e.Node.Checked, new string[]{e.Node.Name});
				}
				else if (e.Node.Level == 1)
				{
					setVisibility(e.Node.Checked, new string[] { e.Node.Parent.Name, e.Node.Name });
				}
				else if (e.Node.Level == 2)
				{
					setVisibility(e.Node.Checked, new string[] { e.Node.Parent.Parent.Name, e.Node.Parent.Name, e.Node.Name });
				}

			};

			_treeView.ExpandAll();


			ApplicationData.FileContext.Data.SettingData.VisualizeRuleExplorerSetting.BecameDirty += (_o, __e) =>
			{
				foreach (KeyValuePair<string, bool> kvp in (IList)_o)
				{
					string[] k = kvp.Key.Split(':');

					foreach (TreeNode tn in _treeView.Nodes.Find(k[0], false))
					{
						if (k.Length > 1 && tn.Nodes != null && tn.Nodes.Count != 0)
						{
							foreach (TreeNode _tn in tn.Nodes.Find(k[1], false))
							{
								if (k.Length == 2)
								{
									if (_tn.Checked != kvp.Value)
										_tn.Checked = kvp.Value;
								}
								else if (k.Length == 3 && _tn.Nodes != null && _tn.Nodes.Count != 0)
								{
									foreach (TreeNode __tn in _tn.Nodes.Find(k[2], false))
									{
										if (__tn.Checked != kvp.Value)
											__tn.Checked = kvp.Value;
									}
								}
							}
						}
						else
						{
							if (tn.Checked != kvp.Value)
								tn.Checked = kvp.Value;
						}
					}
				}
			};
		}

		private void setVisibility(bool value, params string[] keys)
		{
			if (ApplicationData.FileContext.Data.SettingData.VisualizeRuleExplorerSetting.VisualizeRuleVisibility.ContainsKey(keys)
				&& (ApplicationData.FileContext.Data.SettingData.VisualizeRuleExplorerSetting.VisualizeRuleVisibility.GetValue(keys) == value))
			{
				return;
			}
			else
			{
				ApplicationFactory.CommandManager.Do(new GeneralCommand(Text + " 可視化ルール表示切替え",
					() => { ApplicationData.FileContext.Data.SettingData.VisualizeRuleExplorerSetting.VisualizeRuleVisibility.SetValue(value, keys); },
					() => { ApplicationData.FileContext.Data.SettingData.VisualizeRuleExplorerSetting.VisualizeRuleVisibility.SetValue(!value, keys); }));
			}
		}


		public void ClearData()
		{
			_treeView.Nodes.Clear();
		}

	}
}
