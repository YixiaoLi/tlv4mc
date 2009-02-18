using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace NU.OJL.MPRTOS.TLV.Core
{
	public class TimeLineMarker : INamed
	{
		public string Name { get; set; }
		public Color Color { get; set; }
		public Time Time { get; set; }

		public TimeLineMarker(string name, Color color, Time time)
		{
			Name = name;
			Color = color;
			Time = time;
		}

		public TimeLineMarker(string name, Time time)
			: this(name, ApplicationFactory.ColorFactory.RamdomColor(), time)
		{
		}
	}
}
