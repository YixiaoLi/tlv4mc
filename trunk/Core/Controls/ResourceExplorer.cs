/*
 *  TLV - Trace Log Visualizer
 *
 *  Copyright (C) 2008,2009 by Embedded and Real-Time Systems Laboratory
 *              Graduate School of Information Science, Nagoya Univ., JAPAN
 *
 *  �嵭����Ԥϡ��ʲ���(1)��(4)�ξ������������˸¤ꡤ�ܥ��եȥ���
 *  �����ܥ��եȥ���������Ѥ�����Τ�ޤࡥ�ʲ�Ʊ���ˤ���ѡ�ʣ������
 *  �ѡ������ۡʰʲ������ѤȸƤ֡ˤ��뤳�Ȥ�̵���ǵ������롥
 *  (1) �ܥ��եȥ������򥽡��������ɤη������Ѥ�����ˤϡ��嵭������
 *      ��ɽ�����������Ѿ�浪��Ӳ�����̵�ݾڵ��꤬�����Τޤޤη��ǥ���
 *      ����������˴ޤޤ�Ƥ��뤳�ȡ�
 *  (2) �ܥ��եȥ������򡤥饤�֥������ʤɡ�¾�Υ��եȥ�������ȯ�˻�
 *      �ѤǤ�����Ǻ����ۤ�����ˤϡ������ۤ�ȼ���ɥ�����ȡ�����
 *      �ԥޥ˥奢��ʤɡˤˡ��嵭�����ɽ�����������Ѿ�浪��Ӳ���
 *      ��̵�ݾڵ����Ǻܤ��뤳�ȡ�
 *  (3) �ܥ��եȥ������򡤵�����Ȥ߹���ʤɡ�¾�Υ��եȥ�������ȯ�˻�
 *      �ѤǤ��ʤ����Ǻ����ۤ�����ˤϡ����Τ����줫�ξ�����������
 *      �ȡ�
 *    (a) �����ۤ�ȼ���ɥ�����ȡ����Ѽԥޥ˥奢��ʤɡˤˡ��嵭����
 *        �ɽ�����������Ѿ�浪��Ӳ�����̵�ݾڵ����Ǻܤ��뤳�ȡ�
 *    (b) �����ۤη��֤��̤�������ˡ�ˤ�äơ�TOPPERS�ץ������Ȥ�
 *        ��𤹤뤳�ȡ�
 *  (4) �ܥ��եȥ����������Ѥˤ��ľ��Ū�ޤ��ϴ���Ū�������뤤���ʤ�»
 *      ������⡤�嵭����Ԥ����TOPPERS�ץ������Ȥ����դ��뤳�ȡ�
 *      �ޤ����ܥ��եȥ������Υ桼���ޤ��ϥ���ɥ桼������Τ����ʤ���
 *      ͳ�˴�Ť����ᤫ��⡤�嵭����Ԥ����TOPPERS�ץ������Ȥ�
 *      ���դ��뤳�ȡ�
 *
 *  �ܥ��եȥ������ϡ�̵�ݾڤ��󶡤���Ƥ����ΤǤ��롥�嵭����Ԥ�
 *  ���TOPPERS�ץ������Ȥϡ��ܥ��եȥ������˴ؤ��ơ�����λ�����Ū
 *  ���Ф���Ŭ������ޤ�ơ������ʤ��ݾڤ�Ԥ�ʤ����ޤ����ܥ��եȥ���
 *  �������Ѥˤ��ľ��Ū�ޤ��ϴ���Ū�������������ʤ�»���˴ؤ��Ƥ⡤��
 *  ����Ǥ�����ʤ���
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
				ApplicationFactory.StatusManager.ShowHint(tabControl.GetType().ToString() + ":checkIs", "ʬ�����ؤ�", "����å�");
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

			setData("___resType", "�꥽����������", resTypeClass);

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
					ApplicationFactory.StatusManager.ShowHint(tv.GetType().ToString() + ":checkIs", "�Ļ벽ɽ��", "�����å�");
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
				ApplicationFactory.CommandManager.Do(new GeneralCommand(Text + " �Ļ벽ɽ���꥽�������ؤ�",
					() => { ApplicationData.FileContext.Data.SettingData.ResourceExplorerSetting.ResourceVisibility.SetValue(value, keys); },
					() => { ApplicationData.FileContext.Data.SettingData.ResourceExplorerSetting.ResourceVisibility.SetValue(!value, keys); }));
			}
		}

		private void setData(string name, string displayName, List<NamedResourceList> groups)
		{
			tabControl.TabPages.Add(name, displayName + "��");
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
