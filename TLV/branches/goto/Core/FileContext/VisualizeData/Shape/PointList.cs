using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Text;

namespace NU.OJL.MPRTOS.TLV.Core
{
	public class PointList: Collection<Point>
	{
		public PointF[] ToPointF(Point offset, RectangleF rect)
		{
			List<PointF> points = new List<PointF>();

			foreach (Point c in this)
			{
				points.Add(c.ToPointF(offset, rect));
			}

			return points.ToArray();
		}
		public PointF[] ToPointF(RectangleF rect)
		{
			return ToPointF(new Point("0,0"), rect);
		}
	}
}
