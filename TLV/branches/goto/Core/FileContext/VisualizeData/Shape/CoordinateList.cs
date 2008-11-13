using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Text;

namespace NU.OJL.MPRTOS.TLV.Core
{
	public class CoordinateList: Collection<Coordinate>
	{
		public PointF[] ToPointF(RectangleF rect)
		{
			List<PointF> points = new List<PointF>();

			foreach(Coordinate c in this)
			{
				points.Add(c.ToPointF(rect));
			}

			return points.ToArray();
		}
	}
}
