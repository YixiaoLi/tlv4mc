using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Text.RegularExpressions;

namespace NU.OJL.MPRTOS.TLV.Core
{
	public class Coordinate
	{
		private float _x;
		private float _y;
		private string _coordinate;
		private CoordinateUnit xCoordinateUnit;
		private CoordinateUnit yCoordinateUnit;
		private CoordinateOffset xCoordinateOffset;
		private CoordinateOffset yCoordinateOffset;

		public Coordinate(string x, string y)
			:this(x + "," + y)
		{
		}

		public Coordinate(string coordinate)
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
				xCoordinateUnit = CoordinateUnit.Percentage;
			if (Regex.IsMatch(x, @"^(([lr]\(" + num + @"\))|(" + num + @"))(px)?$"))
				xCoordinateUnit = CoordinateUnit.Pixel;

			if (Regex.IsMatch(y, @"^(([lr]\(" + num + @"\))|(" + num + @"))%$"))
				yCoordinateUnit = CoordinateUnit.Percentage;
			if (Regex.IsMatch(y, @"^(([lr]\(" + num + @"\))|(" + num + @"))(px)?$"))
				yCoordinateUnit = CoordinateUnit.Pixel;

			if (Regex.IsMatch(x, @"^((l\(" + num + @"\))|(" + num + @"))(%|px)?$"))
				xCoordinateOffset = CoordinateOffset.Left;
			if (Regex.IsMatch(x, @"^r\(" + num + @"\)(%|px)?$"))
				xCoordinateOffset = CoordinateOffset.Right;

			if (Regex.IsMatch(y, @"^((b\(" + num + @"\))|(" + num + @"))(%|px)?$"))
				yCoordinateOffset = CoordinateOffset.Bottom;
			if (Regex.IsMatch(y, @"^t\(" + num + @"\)(%|px)?$"))
				yCoordinateOffset = CoordinateOffset.Top;

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
			return ToPointF(new Coordinate("0,0"), rect);
		}

		public PointF ToPointF(Coordinate offset, RectangleF rect)
		{
			PointF point = new PointF();

			switch(xCoordinateOffset)
			{
				case CoordinateOffset.Left:
					switch (xCoordinateUnit)
					{
						case CoordinateUnit.Percentage:
							point.X = rect.Left + rect.Width * _x / 100.0f;
							break;
						case CoordinateUnit.Pixel:
							point.X = rect.Left + _x;
							break;
					}
					break;
				case CoordinateOffset.Right:
					switch (xCoordinateUnit)
					{
						case CoordinateUnit.Percentage:
							point.X = rect.Right - rect.Width * _x / 100.0f;
							break;
						case CoordinateUnit.Pixel:
							point.X = rect.Right - _x;
							break;
					}
					break;
			}
			switch (yCoordinateOffset)
			{
				case CoordinateOffset.Top:
					switch (yCoordinateUnit)
					{
						case CoordinateUnit.Percentage:
							point.Y = rect.Top + rect.Height * _y / 100.0f;
							break;
						case CoordinateUnit.Pixel:
							point.Y = rect.Top + _y;
							break;
					}
					break;
				case CoordinateOffset.Bottom:
					switch (yCoordinateUnit)
					{
						case CoordinateUnit.Percentage:
							point.Y = rect.Bottom - rect.Height * _y / 100.0f;
							break;
						case CoordinateUnit.Pixel:
							point.Y = rect.Bottom - _y;
							break;
					}
					break;
			}

			if (offset.ToString() != "0,0")
			{
				PointF off = offset.ToPointF(rect);

				point.X += off.X;
				point.Y -= off.Y;
			}
			return point;
		}
	}

	enum CoordinateUnit
	{
		Percentage,
		Pixel
	}

	enum CoordinateOffset
	{
		Top,
		Bottom,
		Left,
		Right
	}
}
