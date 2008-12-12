using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using NU.OJL.MPRTOS.TLV.Base;

namespace NU.OJL.MPRTOS.TLV.Core
{
	public class VisualizeRuleExplorerSetting : ISetting
	{
		public event SettingChangeEventHandler BecameDirty = null;
		public ObservableMultiKeyDictionary<bool> VisualizeRuleVisibility { get; set; }

		public VisualizeRuleExplorerSetting()
		{
			VisualizeRuleVisibility = new ObservableMultiKeyDictionary<bool>();
			VisualizeRuleVisibility.CollectionChanged += CollectionChangedFactory("VisualizeRuleVisibility");
		}


		NotifyCollectionChangedEventHandler CollectionChangedFactory(string propertyName)
		{
			return (object sender, NotifyCollectionChangedEventArgs e) =>
			{
				if (BecameDirty != null)
				{
					switch (e.Action)
					{
						case NotifyCollectionChangedAction.Add:
							BecameDirty(e.NewItems, propertyName);
							break;

						//case NotifyCollectionChangedAction.Remove:
						//    BecameDirty(e.OldItems, EventArgs.Empty);
						//    break;
					}
				}
			};
		}
	}
}
