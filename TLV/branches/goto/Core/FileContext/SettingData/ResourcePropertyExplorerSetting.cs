using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using NU.OJL.MPRTOS.TLV.Base;

namespace NU.OJL.MPRTOS.TLV.Core
{
	public class ResourcePropertyExplorerSetting : ISetting
	{
		public event EventHandler BecameDirty = null;
		public ObservableDictionary<string, bool> ResourcePropertyVisibility { get; set; }
		public const string AttributeSeparateText = ":attribute:";
		public const string BehaviorSeparateText = ":behavir:";

		public ResourcePropertyExplorerSetting()
		{
			ResourcePropertyVisibility = new ObservableDictionary<string, bool>();
			ResourcePropertyVisibility.CollectionChanged += CollectionChanged;
		}

		void CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
		{
			if (BecameDirty != null)
				BecameDirty(this, EventArgs.Empty);
		}
	}
}
