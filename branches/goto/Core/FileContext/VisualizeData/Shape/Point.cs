using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Text.RegularExpressions;

namespace NU.OJL.MPRTOS.TLV.Core
{
	public class Point
	{
		private float _x;
		private float _y;
		private string _coordinate;
		private VisualizeAreaUnit xVisualizeAreaUnit;
		private VisualizeAreaUnit yVisualizeAreaUnit;
		private XAxisReference xAxisReference;
		private YAxisReference yAxisReference;

		public Point(string x, string y)
			:this(x + "," + y)
		{
		}

		public Point(string coordinate)
		{
			_coordinate = coordinate;
			_coordinate = _coordinate.Replace(" ", "").Replace("\t", "");
			string[] c = _coordinate.Split(',');

			string x = c[0];
			string y = c[1];

			string num = @"-?([1-9][0-9]*)?[0-9](\.[0-9]*)?";

			if (!Regex.IsMatch(x, @"^(([lr]\(" + num + @"\))|(" + num + @"))(%|px)?$"))
				throw new Exception("座標指定が異常です。\n" + coordinate);
			if (!Regex.IsMatch(y, @"^(([tb]\(" + num + @"\))|(" + num + @"))(%|px)?$"))
				throw new Exception("座標指定が異常です。\n" + coordinate);

			if (Regex.IsMatch(x, @"^(([lr]\(" + num + @"\))|(" + num + @"))%$"))
				xVisualizeAreaUnit = VisualizeAreaUnit.Percentage;
			if (Regex.IsMatch(x, @"^(([lr]\(" + num + @"\))|(" + num + @"))(px)?$"))
				xVisualizeAreaUnit = VisualizeAreaUnit.Pixel;

			if (Regex.IsMatch(y, @"^(([lr]\(" + num + @"\))|(" + num + @"))%$"))
				yVisualizeAreaUnit = VisualizeAreaUnit.Percentage;
			if (Regex.IsMatch(y, @"^(([lr]\(" + num + @"\))|(" + num + @"))(px)?$"))
				yVisualizeAreaUnit = VisualizeAreaUnit.Pixel;

			if (Regex.IsMatch(x, @"^(l\(" + num + @"\))(%|px)?$"))
				xAxisReference = XAxisReference.Left;
			if (Regex.IsMatch(x, @"^(" + num + @")(%|px)?$"))
				xAxisReference = XAxisReference.Zero;
			if (Regex.IsMatch(x, @"^r\(" + num + @"\)(%|px)?$"))
				xAxisReference = XAxisReference.Right;

			if (Regex.IsMatch(y, @"^(b\(" + num + @"\))(%|px)?$"))
				yAxisReference = YAxisReference.Bottom;
			if (Regex.IsMatch(y, @"^(" + num + @")(%|px)?$"))
				yAxisReference = YAxisReference.Zero;
			if (Regex.IsMatch(y, @"^t\(" + num + @"\)(%|px)?$"))
				yAxisReference = YAxisReference.Top;

			x = x.Replace("%", "").Replace("px", "").Replace("(", "").Replace(")", "").Replace("l", "").Replace("r", "");
			y = y.Replace("%", "").Replace("px", "").Replace("(", "").Replace(")", "").Replace("t", "").Replace("b", "");

			_x = float.Parse(x);
			_y = float.Parse(y);
		}

		public override string ToString()
		{
			return _coordinate;
		}

		public PointF ToPointF(RectangleF rect)
		{
			return ToPointF(new Point("0,0"), rect);
		}

		public PointF ToPointF(Point offset, RectangleF rect)
		{
			PointF point = new PointF();

			switch (xAxisReference)
			{
				case XAxisReference.Zero:
				case XAxisReference.Left:
					switch (xVisualizeAreaUnit)
					{
						case VisualizeAreaUnit.Percentage:
							point.X = rect.Left + rect.Width * _x / 100.0f;
							break;
						case VisualizeAreaUnit.Pixel:
							point.X = rect.Left + _x;
							break;
					}
					break;
				case XAxisReference.Right:
					switch (xVisualizeAreaUnit)
					{
						case VisualizeAreaUnit.Percentage:
							point.X = rect.Right - rect.Width * _x / 100.0f;
							break;
						case VisualizeAreaUnit.Pixel:
							point.X = rect.Right - _x;
							break;
					}
					break;
			}
			switch (yAxisReference)
			{
				case YAxisReference.Zero:
				case YAxisReference.Bottom:
					switch (yVisualizeAreaUnit)
					{
						case VisualizeAreaUnit.Percentage:
							point.Y = rect.Bottom - rect.Height * _y / 100.0f;
							break;
						case VisualizeAreaUnit.Pixel:
							point.Y = rect.Bottom - _y;
							break;
					}
					break;
				case YAxisReference.Top:
					switch (yVisualizeAreaUnit)
					{
						case VisualizeAreaUnit.Percentage:
							point.Y = rect.Top + rect.Height * _y / 100.0f;
							break;
						case VisualizeAreaUnit.Pixel:
							point.Y = rect.Top + _y;
							break;
					}
					break;
			}

			if (offset.ToString() != "0,0")
			{
				PointF off = offset.ToPointF(rect);

				if (xAxisReference == XAxisReference.Zero)
					point.X += off.X;
				if (yAxisReference == YAxisReference.Zero)
					point.Y -= rect.Height - off.Y;
			}
			
			return point;
		}
	}

	public enum XAxisReference
	{
		Left,
		Right,
		Zero
	}

	public enum YAxisReference
	{
		Top,
		Bottom,
		Zero
	}
}
