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
	public class LocalSetting : ISetting
	{
		public TimeLineMarkerManager TimeLineMarkerManager { get; private set; }
		public event SettingChangeEventHandler BecameDirty = null;

		public LocalSetting()
		{
			TimeLineMarkerManager = new TimeLineMarkerManager();
			TimeLineMarkerManager.Markers.CollectionChanged += (o, e) => { BecameDirty(this, "TimeLineMarkerManager"); };
		}
	}
}
