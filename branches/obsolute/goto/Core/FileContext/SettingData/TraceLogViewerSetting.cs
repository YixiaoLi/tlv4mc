using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using NU.OJL.MPRTOS.TLV.Base;

namespace NU.OJL.MPRTOS.TLV.Core
{
	public class TraceLogViewerSetting : ISetting
	{
		private Time _firstDisplayedTime;

		public Time FirstDisplayedTime { get { return _firstDisplayedTime; } set { if (_firstDisplayedTime != value) { _firstDisplayedTime = value; onBecameDirty("FirstDisplayedTime"); } } }

		public event SettingChangeEventHandler BecameDirty;

		private void onBecameDirty(string propertyName)
		{
			if (BecameDirty != null)
				BecameDirty(this, propertyName);
		}
	}
}
