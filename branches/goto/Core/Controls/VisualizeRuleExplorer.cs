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
using System.Collections;

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
							foreach (KeyValuePair<string, bool> kvp in (IList)_o)
							{
								string[] k = kvp.Key.Split(':');

								foreach (TreeNode tn in _treeView.Nodes.Find(k[0], false))
								{
									if (k.Length > 1)
									{
										foreach (TreeNode _tn in tn.Nodes.Find(k[1], false))
										{
											if (k.Length == 3 && _tn.Nodes != null && _tn.Nodes.Count != 0)
											{
												foreach (TreeNode __tn in _tn.Nodes.Find(k[2], false))
												{
													if (__tn.Checked != kvp.Value)
														__tn.Checked = kvp.Value;
												}
											}
											else
											{
												if (_tn.Checked != kvp.Value)
													_tn.Checked = kvp.Value;
											}
										}
									}
								}
							}
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
						_treeView.Nodes[vizRule.Target].Checked = ApplicationData.Setting.DefaultResourceVisible;
						_treeView.Nodes[vizRule.Target].ImageKey = "resource";
						_treeView.Nodes[vizRule.Target].SelectedImageKey = "resource";
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
				tnc[vizRule.Name].Checked = ApplicationData.FileContext.Data.SettingData.VisualizeRuleExplorerSetting.VisualizeRuleVisibility.ContainsKey(id) ? ApplicationData.FileContext.Data.SettingData.VisualizeRuleExplorerSetting.VisualizeRuleVisibility.GetValue(id) : ApplicationData.Setting.DefaultResourceVisible;
				tnc[vizRule.Name].ImageKey = "visualize";
				tnc[vizRule.Name].SelectedImageKey = "visualize";

				foreach(Event e in vizRule.Events)
				{
					List<string> list = new List<string>(id);
					list.Add(e.DisplayName);
					string[] i = list.ToArray();
					tnc[vizRule.Name].Nodes.Add(e.DisplayName, e.DisplayName);
					tnc[vizRule.Name].Nodes[e.DisplayName].Checked = ApplicationData.FileContext.Data.SettingData.VisualizeRuleExplorerSetting.VisualizeRuleVisibility.ContainsKey(i) ? ApplicationData.FileContext.Data.SettingData.VisualizeRuleExplorerSetting.VisualizeRuleVisibility.GetValue(i) : ApplicationData.Setting.DefaultResourceVisible;

					setEventImage(tnc[vizRule.Name].Nodes[e.DisplayName], e);
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

		}

		private void setVisibility(bool value, params string[] keys)
		{
			if (ApplicationData.FileContext.Data.SettingData.VisualizeRuleExplorerSetting.VisualizeRuleVisibility.ContainsKey(keys)
				&& (ApplicationData.FileContext.Data.SettingData.VisualizeRuleExplorerSetting.VisualizeRuleVisibility.GetValue(keys) == value))
				return;
			else
				ApplicationData.FileContext.Data.SettingData.VisualizeRuleExplorerSetting.VisualizeRuleVisibility.SetValue(value, keys);
		}

		private void setEventImage(TreeNode tn, Event e)
		{
			if ((e.Type & EventTypes.FromAttributeChange) == EventTypes.FromAttributeChange
				&& (e.Type & EventTypes.ToAttributeChange) == EventTypes.ToAttributeChange)
			{
				tn.ImageKey = "atr2atr";
				tn.SelectedImageKey = "atr2atr";
			}

			if ((e.Type & EventTypes.FromBehaviorHappen) == EventTypes.FromBehaviorHappen
				&& (e.Type & EventTypes.ToBehaviorHappen) == EventTypes.ToBehaviorHappen)
			{
				tn.ImageKey = "bhr2bhr";
				tn.SelectedImageKey = "bhr2bhr";
			}

			if ((e.Type & EventTypes.FromAttributeChange) == EventTypes.FromAttributeChange
				&& (e.Type & EventTypes.ToBehaviorHappen) == EventTypes.ToBehaviorHappen)
			{
				tn.ImageKey = "atr2bhr";
				tn.SelectedImageKey = "atr2bhr";
			}

			if ((e.Type & EventTypes.FromBehaviorHappen) == EventTypes.FromBehaviorHappen
				&& (e.Type & EventTypes.ToAttributeChange) == EventTypes.ToAttributeChange)
			{
				tn.ImageKey = "bhr2atr";
				tn.SelectedImageKey = "bhr2atr";
			}

			if ((e.Type & EventTypes.WhenAttributeChange) == EventTypes.WhenAttributeChange)
			{
				tn.ImageKey = "attribute";
				tn.SelectedImageKey = "attribute";
			}

			if ((e.Type & EventTypes.WhenBehaviorHappen) == EventTypes.WhenBehaviorHappen)
			{
				tn.ImageKey = "behavior";
				tn.SelectedImageKey = "behavior";
			}

			if ((e.Type & EventTypes.Error) == EventTypes.Error)
			{
				tn.ImageKey = "warning";
				tn.SelectedImageKey = "warning";
			}
		}

		public void ClearData()
		{
			_treeView.Nodes.Clear();
		}

	}
}
