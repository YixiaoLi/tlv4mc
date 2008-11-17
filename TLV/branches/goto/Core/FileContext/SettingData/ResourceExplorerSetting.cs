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
		public ObservableDictionary<string, bool> ResourceVisibility { get; set; }

		public ResourceExplorerSetting()
		{
			ResourceVisibility = new ObservableDictionary<string, bool>();
			ResourceVisibility.CollectionChanged += CollectionChanged;
		}

		void CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
		{
			if (BecameDirty != null)
				BecameDirty(this, EventArgs.Empty);
		}
	}
}
