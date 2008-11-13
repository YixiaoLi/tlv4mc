﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace NU.OJL.MPRTOS.TLV.Core
{
	public class Area
	{
		public Coordinate Location { get; set; }
		public Size Size { get; set; }
		public Area(Coordinate location, Size size)
		{
			Location = location;
			Size = size;
		}
		public Area(string location, string size)
		{
			Location = new Coordinate(location);
			Size = new Size(size);
		}
		public override string ToString()
		{
			return "[" + Location.ToString() + "," + Size.ToString() + "]";
		}

		public RectangleF ToRectangleF(Coordinate offset, RectangleF rect)
		{
			return new RectangleF(Location.ToPointF(offset, rect), Size.ToSizeF(rect));
		}
	}
}
