using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace NU.OJL.MPRTOS.TLV.Core
{
	public class Shape
	{
		public string Type { get; set; }
		public string Value { get; set; }
		public CoordinateList Coordinates { get; set; }
		public Coordinate Offset { get; set; }
		public Pen Pen { get; set; }
		public Color Fill { get; set; }
		public ContentAlignment Align { get; set; }

		public Shape()
		{
			Align = ContentAlignment.TopLeft;
		}
	}
}
