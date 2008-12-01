using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using NU.OJL.MPRTOS.TLV.Base;
using System.Collections.Specialized;

namespace NU.OJL.MPRTOS.TLV.Core
{
	public class ColorSetting : ISetting
	{
		public event EventHandler BecameDirty = null;
		public ObservableDictionary<string, Color> ResourceColors { get; set; }
		public ObservableDictionary<string, Color> ResourceTypeColors { get; set; }
		public ObservableDictionary<string, Color> AttributeColors { get; set; }
		public ObservableDictionary<string, Color> ValueColors { get; set; }
		public ObservableDictionary<string, Color> BehaviorColors { get; set; }

		public ColorSetting()
		{
			ResourceColors = new ObservableDictionary<string, Color>();
			ResourceTypeColors = new ObservableDictionary<string, Color>();
			AttributeColors = new ObservableDictionary<string, Color>();
			BehaviorColors = new ObservableDictionary<string, Color>();
			ValueColors = new ObservableDictionary<string, Color>();

			ResourceColors.CollectionChanged += CollectionChanged;
			ResourceTypeColors.CollectionChanged += CollectionChanged;
			AttributeColors.CollectionChanged += CollectionChanged;
			ValueColors.CollectionChanged += CollectionChanged;
			BehaviorColors.CollectionChanged += CollectionChanged;
		}

		void CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
		{
			if (BecameDirty != null)
				BecameDirty(this, EventArgs.Empty);
		}
	}
}
