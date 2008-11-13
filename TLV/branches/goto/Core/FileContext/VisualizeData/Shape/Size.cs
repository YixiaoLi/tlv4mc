using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Text.RegularExpressions;

namespace NU.OJL.MPRTOS.TLV.Core
{
	public class Size
	{
		private float _width;
		private float _height;
		private string _size;
		public string Width { get; set; }
		public string Height { get; set; }
		private SizeUnit widthSizeUnit;
		private SizeUnit heightSizeUnit;

		public Size(string width, string height)
			:this(width + "," + height)
		{
			Width = width;
			Height = height;
		}
		public Size(string size)
		{
			_size = size;
			_size = _size.Replace(" ", "").Replace("\t", "");
			string[] c = _size.Split(',');

			string w = c[0];
			string h = c[1];

			string num = @"-?([1-9][0-9]*)?[0-9](\.[0-9]*)?";

			if (!Regex.IsMatch(w,  @"^" + num + @"(%|px)?$"))
				throw new Exception("サイズ指定が異常です。\n" + size);
			if (!Regex.IsMatch(h, @"^" + num + @"(%|px)?$"))
				throw new Exception("サイズ指定が異常です。\n" + size);

			if (Regex.IsMatch(w, @"^" + num + @"%$"))
				widthSizeUnit = SizeUnit.Percentage;
			if (Regex.IsMatch(w, num + @"^" + num + @"(px)?$"))
				widthSizeUnit = SizeUnit.Pixel;

			if (Regex.IsMatch(h, @"^" + num + @"%$"))
				heightSizeUnit = SizeUnit.Percentage;
			if (Regex.IsMatch(h, num + @"^" + num + @"(px)?$"))
				heightSizeUnit = SizeUnit.Pixel;

			w = w.Replace("%", "").Replace("px", "").Replace("(", "").Replace(")", "").Replace("l", "").Replace("r", "");
			h = h.Replace("%", "").Replace("px", "").Replace("(", "").Replace(")", "").Replace("t", "").Replace("b", "");

			_width = float.Parse(w);
			_height = float.Parse(h);
		}

		public override string ToString()
		{
			return _size;
		}

		public SizeF ToSizeF(RectangleF rect)
		{
			SizeF size = new SizeF();

			switch (widthSizeUnit)
			{
				case SizeUnit.Percentage:
					size.Width = rect.Width * _width / 100.0f;
					break;
				case SizeUnit.Pixel:
					size.Width = _width;
					break;
			}
			switch (heightSizeUnit)
			{
				case SizeUnit.Percentage:
					size.Height = rect.Height * _height / 100.0f;
					break;
				case SizeUnit.Pixel:
					size.Height = _height;
					break;
			}

			return size;
		}
	}

	enum SizeUnit
	{
		Percentage,
		Pixel
	}
}
