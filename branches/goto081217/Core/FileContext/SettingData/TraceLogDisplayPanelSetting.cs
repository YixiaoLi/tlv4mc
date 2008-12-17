using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NU.OJL.MPRTOS.TLV.Core
{
	public class TraceLogDisplayPanelSetting : ISetting
	{
		private string _fromTime;
		private string _toTime;
		private int _pixelPerScaleMark = 5;

		public string FromTime { get { return _fromTime; } set { if (_fromTime != value) { _fromTime = value; onBecameDirty("FromTime"); } } }
		public string ToTime { get { return _toTime; } set { if (_toTime != value) { _toTime = value; onBecameDirty("ToTime"); } } }
		public int PixelPerScaleMark { get { return _pixelPerScaleMark; } set { if (_pixelPerScaleMark != value) { _pixelPerScaleMark = value; onBecameDirty("PixelPerScaleMark"); } } }

		public event SettingChangeEventHandler BecameDirty;

		private void onBecameDirty(string propertyName)
		{
			if (BecameDirty != null)
				BecameDirty(this, propertyName);
		}


	}
}
