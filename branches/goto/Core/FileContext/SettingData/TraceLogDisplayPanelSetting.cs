using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NU.OJL.MPRTOS.TLV.Core
{
	public class TraceLogDisplayPanelSetting : ISetting
	{
		private Time _timePerScaleMark;
		private int _pixelPerScaleMark = 5;
		private TimeLine _timeLine;
		private int _rowHeight = 60;
		private bool _autoResizeRowHeight = true;

		public Time TimePerScaleMark { get { return _timePerScaleMark; } set { if (_timePerScaleMark != value) { _timePerScaleMark = value; onBecameDirty("TimePerScaleMark"); } } }
		public int RowHeight { get { return _rowHeight; } set { if (_rowHeight != value) { _rowHeight = value; onBecameDirty("RowHeight"); } } }
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
		public bool AutoResizeRowHeight { get { return _autoResizeRowHeight; } set { if (_autoResizeRowHeight != value) { _autoResizeRowHeight = value; onBecameDirty("AutoResizeRowHeight"); } } }

		public event SettingChangeEventHandler BecameDirty;

		private void onBecameDirty(string propertyName)
		{
			if (BecameDirty != null)
				BecameDirty(this, propertyName);
		}

	}
}
