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
		private string _point;
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
			_point = coordinate;
			_point = _point.Replace(" ", "").Replace("\t", "");
			string[] c = _point.Split(',');

			string x = c[0];
			string y = c[1];

			string num = @"-?([1-9][0-9]*)?[0-9](\.[0-9]*)?";

			if (!Regex.IsMatch(x, @"^(([lcr]\(" + num + @"(%|px)?\))|(" + num + @"(%|px)?))$"))
				throw new Exception("座標指定が異常です。\n" + coordinate);
			if (!Regex.IsMatch(y, @"^(([tmb]\(" + num + @"(%|px)?\))|(" + num + @"(%|px)?))$"))
				throw new Exception("座標指定が異常です。\n" + coordinate);

			if (Regex.IsMatch(x, @"^(([lcr]\(" + num + @"%\))|(" + num + @"%))$"))
				xVisualizeAreaUnit = VisualizeAreaUnit.Percentage;
			else if (Regex.IsMatch(x, @"^(([lcr]\(" + num + @"(px)?\))|(" + num + @"(px)?))$"))
				xVisualizeAreaUnit = VisualizeAreaUnit.Pixel;

			if (Regex.IsMatch(y, @"^(([tmb]\(" + num + @"%\))|(" + num + @"%))$"))
				yVisualizeAreaUnit = VisualizeAreaUnit.Percentage;
			else if (Regex.IsMatch(y, @"^(([tmb]\(" + num + @"(px)?\))|(" + num + @"(px)?))$"))
				yVisualizeAreaUnit = VisualizeAreaUnit.Pixel;

			if (Regex.IsMatch(x, @"^(l\(" + num + @"(%|px)?\))$"))
				xAxisReference = XAxisReference.Left;
			else if (Regex.IsMatch(x, @"^r\(" + num + @"(%|px)?\)$"))
				xAxisReference = XAxisReference.Right;
			else if (Regex.IsMatch(x, @"^c\(" + num + @"(%|px)?\)$"))
				xAxisReference = XAxisReference.Center;
			else if (Regex.IsMatch(x, @"^(" + num + @"(%|px)?)$"))
				xAxisReference = XAxisReference.Zero;

			if (Regex.IsMatch(y, @"^(b\(" + num + @"(%|px)?\))$"))
				yAxisReference = YAxisReference.Bottom;
			else if (Regex.IsMatch(y, @"^t\(" + num + @"(%|px)?\)$"))
				yAxisReference = YAxisReference.Top;
			else if (Regex.IsMatch(y, @"^m\(" + num + @"(%|px)?\)$"))
				yAxisReference = YAxisReference.Middle;
			else if (Regex.IsMatch(y, @"^(" + num + @"(%|px)?)$"))
				yAxisReference = YAxisReference.Zero;

			x = x.Replace("%", "").Replace("px", "").Replace("(", "").Replace(")", "").Replace("l", "").Replace("r", "").Replace("c", "");
			y = y.Replace("%", "").Replace("px", "").Replace("(", "").Replace(")", "").Replace("t", "").Replace("b", "").Replace("m", "");

			_x = float.Parse(x);
			_y = float.Parse(y);
		}

		public override string ToString()
		{
			return _point;
		}

		public PointF ToPointF(RectangleF rect)
		{
			return ToPointF(new Size("0,0"), rect);
		}

		public PointF ToPointF(Size offset, RectangleF rect)
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
							point.X = rect.Right + rect.Width * _x / 100.0f;
							break;
						case VisualizeAreaUnit.Pixel:
							point.X = rect.Right + _x;
							break;
					}
					break;
				case XAxisReference.Center:
					switch (xVisualizeAreaUnit)
					{
						case VisualizeAreaUnit.Percentage:
							point.X = (rect.Left + (rect.Width / 2f)) + rect.Width * _x / 100.0f;
							break;
						case VisualizeAreaUnit.Pixel:
							point.X = (rect.Left + (rect.Width / 2f)) + _x;
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
							point.Y = rect.Top - rect.Height * _y / 100.0f;
							break;
						case VisualizeAreaUnit.Pixel:
							point.Y = rect.Top - _y;
							break;
					}
					break;
				case YAxisReference.Middle:
					switch (yVisualizeAreaUnit)
					{
						case VisualizeAreaUnit.Percentage:
							point.Y = (rect.Bottom - (rect.Height / 2f)) - rect.Height * _y / 100.0f;
							break;
						case VisualizeAreaUnit.Pixel:
							point.Y = (rect.Bottom - (rect.Height / 2f)) - _y;
							break;
					}
					break;
			}

			if (offset.ToString() != "0,0")
			{
				SizeF off = offset.ToSizeF(rect);

				point.X += off.Width;
				point.Y -= off.Height;
			}
			
			return point;
		}
	}

	public enum XAxisReference
	{
		Left,
		Center,
		Right,
		Zero
	}

	public enum YAxisReference
	{
		Top,
		Middle,
		Bottom,
		Zero
	}
}
