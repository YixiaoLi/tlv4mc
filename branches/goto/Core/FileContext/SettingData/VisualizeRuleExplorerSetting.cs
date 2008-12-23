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

		public bool Check(VisualizeRule rule, Event evnt, Resource target)
		{
			List<string> id = new List<string>();
			if (target != null)
			{
				id.Add(target.Type);
			}
			if (rule != null)
			{
				id.Add(rule.Name);
			}
			if (evnt != null)
			{
				id.Add(evnt.DisplayName);
			}

			if (!VisualizeRuleVisibility.ContainsKey(id.ToArray()))
				return false;

			return VisualizeRuleVisibility.GetValue(id.ToArray());
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
