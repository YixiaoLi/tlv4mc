
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace NU.OJL.MPRTOS.TLV.Core
{
	public class Area
	{
		public Point Point { get; set; }
		public Size Size { get; set; }
		public Area(Point location, Size size)
		{
			Point = location;
			Size = size;
		}
		public Area(string location, string size)
		{
			Point = new Point(location);
			Size = new Size(size);
		}
		public override string ToString()
		{
			return "[" + Point.ToString() + "," + Size.ToString() + "]";
		}

		public RectangleF ToRectangleF(Size offset, RectangleF rect)
		{
			if (Point == null || Size == null)
				return RectangleF.Empty;

			PointF point = Point.ToPointF(offset, rect);
			SizeF size = Size.ToSizeF(rect);
			point.Y -= size.Height;

			return new RectangleF(point, size);
		}
	}
}
