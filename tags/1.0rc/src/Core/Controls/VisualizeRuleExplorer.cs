/*
 *  TLV - Trace Log Visualizer
 *
 *  Copyright (C) 2008,2009 by Embedded and Real-Time Systems Laboratory
 *              Graduate School of Information Science, Nagoya Univ., JAPAN
 *
 *  上記著作権者は，以下の(1)〜(4)の条件を満たす場合に限り，本ソフトウェ
 *  ア（本ソフトウェアを改変したものを含む．以下同じ）を使用・複製・改
 *  変・再配布（以下，利用と呼ぶ）することを無償で許諾する．
 *  (1) 本ソフトウェアをソースコードの形で利用する場合には，上記の著作
 *      権表示，この利用条件および下記の無保証規定が，そのままの形でソー
 *      スコード中に含まれていること．
 *  (2) 本ソフトウェアを，ライブラリ形式など，他のソフトウェア開発に使
 *      用できる形で再配布する場合には，再配布に伴うドキュメント（利用
 *      者マニュアルなど）に，上記の著作権表示，この利用条件および下記
 *      の無保証規定を掲載すること．
 *  (3) 本ソフトウェアを，機器に組み込むなど，他のソフトウェア開発に使
 *      用できない形で再配布する場合には，次のいずれかの条件を満たすこ
 *      と．
 *    (a) 再配布に伴うドキュメント（利用者マニュアルなど）に，上記の著
 *        作権表示，この利用条件および下記の無保証規定を掲載すること．
 *    (b) 再配布の形態を，別に定める方法によって，TOPPERSプロジェクトに
 *        報告すること．
 *  (4) 本ソフトウェアの利用により直接的または間接的に生じるいかなる損
 *      害からも，上記著作権者およびTOPPERSプロジェクトを免責すること．
 *      また，本ソフトウェアのユーザまたはエンドユーザからのいかなる理
 *      由に基づく請求からも，上記著作権者およびTOPPERSプロジェクトを
 *      免責すること．
 *
 *  本ソフトウェアは，無保証で提供されているものである．上記著作権者お
 *  よびTOPPERSプロジェクトは，本ソフトウェアに関して，特定の使用目的
 *  に対する適合性も含めて，いかなる保証も行わない．また，本ソフトウェ
 *  アの利用により直接的または間接的に生じたいかなる損害に関しても，そ
 *  の責任を負わない．
 *
 *  @(#) $Id$
 */
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
		private List<TreeNode> _validTN = new List<TreeNode>();

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

				if (vizRule.IsBelongedTargetResourceType())
				{
					if (!_treeView.Nodes.ContainsKey(vizRule.Target))
					{
						_treeView.Nodes.Add(vizRule.Target, data.ResourceData.ResourceHeaders[vizRule.Target].DisplayName);
						_treeView.Nodes[vizRule.Target].Checked = ApplicationData.Setting.DefaultResourceVisible;
						_treeView.Nodes[vizRule.Target].ImageKey = "resource";
						_treeView.Nodes[vizRule.Target].SelectedImageKey = "resource";
						_treeView.Nodes[vizRule.Target].Tag = null;
					}

					tnc = _treeView.Nodes[vizRule.Target].Nodes;
				}
				else
				{
					tnc = _treeView.Nodes;
				}

				tnc.Add(vizRule.Name, vizRule.DisplayName);
				tnc[vizRule.Name].Checked = ApplicationData.FileContext.Data.SettingData.VisualizeRuleExplorerSetting.VisualizeRuleVisibility.ContainsKey(vizRule.Name) ? ApplicationData.FileContext.Data.SettingData.VisualizeRuleExplorerSetting.VisualizeRuleVisibility.GetValue(vizRule.Name) : ApplicationData.Setting.DefaultVisualizeRuleVisible;
				tnc[vizRule.Name].ImageKey = "visualize";
				tnc[vizRule.Name].SelectedImageKey = "visualize";
				tnc[vizRule.Name].Tag = "visualizeRule";

				_validTN.Add(tnc[vizRule.Name]);

				if (!ApplicationData.FileContext.Data.SettingData.VisualizeRuleExplorerSetting.VisualizeRuleVisibility.ContainsKey(vizRule.Name))
				{
					ApplicationData.FileContext.Data.SettingData.VisualizeRuleExplorerSetting.VisualizeRuleVisibility.SetValue(ApplicationData.Setting.DefaultResourceVisible, vizRule.Name);
				}

				foreach(Event e in vizRule.Shapes)
				{
					string[] i = new string[]{ vizRule.Name, e.Name};
					tnc[vizRule.Name].Nodes.Add(e.Name, e.DisplayName);
					tnc[vizRule.Name].Nodes[e.Name].Checked = ApplicationData.FileContext.Data.SettingData.VisualizeRuleExplorerSetting.VisualizeRuleVisibility.ContainsKey(i) ? ApplicationData.FileContext.Data.SettingData.VisualizeRuleExplorerSetting.VisualizeRuleVisibility.GetValue(i) : ApplicationData.Setting.DefaultVisualizeRuleVisible;

					tnc[vizRule.Name].Nodes[e.Name].ImageKey = e.getImageKey();
					tnc[vizRule.Name].Nodes[e.Name].SelectedImageKey = e.getImageKey();
					tnc[vizRule.Name].Nodes[e.Name].Tag = "event";

					_validTN.Add(tnc[vizRule.Name].Nodes[e.Name]);

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

				if ((string)e.Node.Tag == "visualizeRule")
				{
					setVisibility(e.Node.Checked, new string[] { e.Node.Name });
				}
				else if ((string)e.Node.Tag == "event")
				{
					setVisibility(e.Node.Checked, new string[] { e.Node.Parent.Name, e.Node.Name });
				}

			};

			_treeView.ExpandAll();


			ApplicationData.FileContext.Data.SettingData.VisualizeRuleExplorerSetting.BecameDirty += (_o, __e) =>
			{
				foreach (KeyValuePair<string, bool> kvp in (IList)_o)
				{
					string[] k = kvp.Key.Split(':');

					if (k.Length == 1)
					{
						foreach (TreeNode tn in _validTN.Where(t=> (string)t.Tag == "visualizeRule" && t.Name == k[0]))
						{
							if (tn.Checked != kvp.Value)
								tn.Checked = kvp.Value;
						}
					}
					else if (k.Length == 2)
					{
						foreach (TreeNode tn in _validTN.Where(t => (string)t.Tag == "event" && t.Name == k[1] && t.Parent.Name == k[0]))
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
