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

		public RectangleF ToRectangleF(Point offset, ContentAlignment align, RectangleF rect)
		{
			PointF point = Point.ToPointF(offset, rect);
			SizeF size = Size.ToSizeF(rect);

			if (align == ContentAlignment.BottomCenter || align == ContentAlignment.BottomLeft || align == ContentAlignment.BottomRight)
				point.Y -= size.Height;

			if (align == ContentAlignment.BottomRight || align == ContentAlignment.MiddleRight || align == ContentAlignment.TopRight)
				point.X -= size.Width;

			if (align == ContentAlignment.BottomCenter || align == ContentAlignment.MiddleCenter || align == ContentAlignment.TopCenter)
				point.X -= size.Width / 2;

			if (align == ContentAlignment.MiddleCenter || align == ContentAlignment.MiddleLeft || align == ContentAlignment.MiddleRight)
				point.Y -= size.Height / 2;

			return new RectangleF(point, size);
		}
	}
}
