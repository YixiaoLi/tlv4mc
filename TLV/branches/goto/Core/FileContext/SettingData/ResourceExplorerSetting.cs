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
		public ObservableMultiKeyDictionary<bool> ResourceVisibility { get; set; }

		public ResourceExplorerSetting()
		{
			ResourceVisibility = new ObservableMultiKeyDictionary<bool>();
			ResourceVisibility.CollectionChanged += CollectionChanged;
		}

		void CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
		{
			if (BecameDirty != null)
			{
				switch(e.Action)
				{
					case NotifyCollectionChangedAction.Add:
						BecameDirty(e.NewItems, EventArgs.Empty);
						break;

					case NotifyCollectionChangedAction.Remove:
						BecameDirty(e.OldItems, EventArgs.Empty);
						break;
				}
			}
		}
	}
}
