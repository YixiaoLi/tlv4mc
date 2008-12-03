using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using NU.OJL.MPRTOS.TLV.Base;

namespace NU.OJL.MPRTOS.TLV.Core
{
	public class ResourceExplorerSetting : ISetting
	{
		public event EventHandler BecameDirty = null;
		public ObservableDictionary<string, bool> ResourceVisibilityVisibility { get; set; }
		public void SetResourceVisibilityVisibility(string resType, string name, bool value)
		{
			if (!ContainsResourceVisibilityVisibility(resType, name))
				ResourceVisibilityVisibility.Add(resType + ":" + name, value);
			else
				ResourceVisibilityVisibility[resType + ":" + name] = value;
		}
		public bool GetResourceVisibilityVisibility(string resType, string name)
		{
			return ResourceVisibilityVisibility[resType + ":" + name];
		}
		public bool ContainsResourceVisibilityVisibility(string resType, string name)
		{
			return ResourceVisibilityVisibility.ContainsKey(resType + ":" + name);
		}
		public IEnumerable<KeyValuePair<string[], bool>> GetResourceEnumeralor()
		{
			return ResourceVisibilityVisibility.Select(kvp=>new KeyValuePair<string[], bool>(kvp.Key.Split(':'),kvp.Value));
		}

		public ResourceExplorerSetting()
		{
			ResourceVisibilityVisibility = new ObservableDictionary<string, bool>();
			ResourceVisibilityVisibility.CollectionChanged += CollectionChanged;
		}

		void CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
		{
			if (BecameDirty != null)
				BecameDirty(sender, EventArgs.Empty);
		}
	}
}
