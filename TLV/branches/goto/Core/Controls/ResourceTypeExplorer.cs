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

				if (!ApplicationData.FileContext.Data.SettingData.ResourceTypeExplorerSetting.ResourceTypeVisibility.ContainsKey(resType.Name))
				{
					ApplicationData.FileContext.Data.SettingData.ResourceTypeExplorerSetting.ResourceTypeVisibility.Add(resType.Name, false);
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
				if (ApplicationData.FileContext.Data.SettingData.ResourceTypeExplorerSetting.ResourceTypeVisibility.ContainsKey(e.Node.Name)
					&& ApplicationData.FileContext.Data.SettingData.ResourceTypeExplorerSetting.ResourceTypeVisibility[e.Node.Name] != e.Node.Checked)
				{
					ApplicationData.FileContext.Data.SettingData.ResourceTypeExplorerSetting.ResourceTypeVisibility[e.Node.Name] = e.Node.Checked;
				}

			};

			_treeView.ExpandAll();

		}

		public void ClearData()
		{
			_treeView.Nodes.Clear();
		}

	}

}
