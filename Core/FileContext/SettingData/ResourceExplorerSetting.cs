
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using NU.OJL.MPRTOS.TLV.Base;
using System.Collections;

namespace NU.OJL.MPRTOS.TLV.Core
{
	public class ResourceExplorerSetting : ISetting
	{
		public event SettingChangeEventHandler BecameDirty = null;
		public ObservableMultiKeyDictionary<bool> ResourceVisibility { get; set; }

		public ResourceExplorerSetting()
		{
			ResourceVisibility = new ObservableMultiKeyDictionary<bool>();
			ResourceVisibility.CollectionChanged += CollectionChangedFactory("ResourceVisibility");
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
				if (propertyName == "ResourceVisibility")
				{
					if (e.Action == NotifyCollectionChangedAction.Add && ApplicationData.FileContext.Data.ResourceData != null)
					{
						foreach (KeyValuePair<string, bool> kvp in (IList)e.NewItems)
						{
							ApplicationData.FileContext.Data.ResourceData.Resources[kvp.Key].Visible = kvp.Value;
						}
					}
				}
			};
		}
	}
}
