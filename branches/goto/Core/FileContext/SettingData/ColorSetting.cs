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
		public event SettingChangeEventHandler BecameDirty = null;
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

			ResourceColors.CollectionChanged += CollectionChangedFactory("ResourceColors");
			ResourceTypeColors.CollectionChanged += CollectionChangedFactory("ResourceTypeColors");
			AttributeColors.CollectionChanged += CollectionChangedFactory("AttributeColors");
			ValueColors.CollectionChanged += CollectionChangedFactory("ValueColors");
			BehaviorColors.CollectionChanged += CollectionChangedFactory("BehaviorColors");
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
