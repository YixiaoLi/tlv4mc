using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NU.OJL.MPRTOS.TLV.Core
{
	public class TraceLogDisplayPanelSetting : ISetting
	{
		private int _pixelPerScaleMark = 5;
		private TimeLine _timeLine;

		public int PixelPerScaleMark { get { return _pixelPerScaleMark; } set { if (_pixelPerScaleMark != value) { _pixelPerScaleMark = value; onBecameDirty("PixelPerScaleMark"); } } }
		public TimeLine TimeLine
		{
			get { return _timeLine; }
			set
			{
				if (_timeLine != value)
				{
					_timeLine = value;
					onBecameDirty("TimeLine");
					_timeLine.ViewingAreaChanged += (o, e) =>
					{
						onBecameDirty("TimeLine");
					};
				}
			}
		}

		public event SettingChangeEventHandler BecameDirty;

		private void onBecameDirty(string propertyName)
		{
			if (BecameDirty != null)
				BecameDirty(this, propertyName);
		}

	}
}
