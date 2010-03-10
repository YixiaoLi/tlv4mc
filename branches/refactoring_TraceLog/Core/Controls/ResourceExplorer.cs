/*
 *  TLV - Trace Log Visualizer
 *
 *  Copyright (C) 2008-2010 by Nagoya Univ., JAPAN
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

					foreach(Resource res in resourceData.Resources.Where<Resource>(r=>r.Attributes.ContainsKey(attr.Name)))
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
								case NU.OJL.MPRTOS.TLV.Base.JsonValueType.Number:
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
						setVisibility(e.Node.Checked, e.Node.Name);
					}
				};

				tv.ExpandAll();
			}

			ApplicationData.FileContext.Data.SettingData.ResourceExplorerSetting.BecameDirty += (_o, __e) =>
			{
				foreach (KeyValuePair<string, bool> kvp in (IList)_o)
				{
					foreach (TreeView tv in _treeViews.Values)
					{
						foreach (TreeNode tn in tv.Nodes)
						{
							foreach (TreeNode _tn in tn.Nodes.Find(kvp.Key, false))
							{
								if (_tn.Checked != kvp.Value)
									_tn.Checked = kvp.Value;
							}
						}
					}
				}
			};
		}

		private void setVisibility(bool value, string keys)
		{
			if (ApplicationData.FileContext.Data.SettingData.ResourceExplorerSetting.ResourceVisibility.ContainsKey(keys)
				&& (ApplicationData.FileContext.Data.SettingData.ResourceExplorerSetting.ResourceVisibility.GetValue(keys) == value))
			{
				return;
			}
			else
			{
				ApplicationFactory.CommandManager.Do(new GeneralCommand(Text + " 可視化表示リソース切替え",
					() => { ApplicationData.FileContext.Data.SettingData.ResourceExplorerSetting.ResourceVisibility.SetValue(value, keys); },
					() => { ApplicationData.FileContext.Data.SettingData.ResourceExplorerSetting.ResourceVisibility.SetValue(!value, keys); }));
			}
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
					treeView.Nodes[name + ":" + group.Name].Nodes.Add(res.Name, res.DisplayName);
					treeView.Nodes[name + ":" + group.Name].Nodes[res.Name].ImageKey = "resource";
					treeView.Nodes[name + ":" + group.Name].Nodes[res.Name].SelectedImageKey = "resource";

					treeView.Nodes[name + ":" + group.Name].Nodes[res.Name].Checked = ApplicationData.FileContext.Data.SettingData.ResourceExplorerSetting.ResourceVisibility.ContainsKey(res.Name) ? ApplicationData.FileContext.Data.SettingData.ResourceExplorerSetting.ResourceVisibility.GetValue(res.Name) : ApplicationData.Setting.DefaultResourceVisible;

					if (!ApplicationData.FileContext.Data.SettingData.ResourceExplorerSetting.ResourceVisibility.ContainsKey(res.Name))
					{
						ApplicationData.FileContext.Data.SettingData.ResourceExplorerSetting.ResourceVisibility.SetValue(ApplicationData.Setting.DefaultResourceVisible, res.Name);
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
