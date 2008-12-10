using System;
using System.Collections.Generic;
using System.Linq;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Text;
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
		private VisualizeAreaUnit widthSizeUnit;
		private VisualizeAreaUnit heightSizeUnit;

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
				widthSizeUnit = VisualizeAreaUnit.Percentage;
			if (Regex.IsMatch(w, num + @"^" + num + @"(px)?$"))
				widthSizeUnit = VisualizeAreaUnit.Pixel;

			if (Regex.IsMatch(h, @"^" + num + @"%$"))
				heightSizeUnit = VisualizeAreaUnit.Percentage;
			if (Regex.IsMatch(h, num + @"^" + num + @"(px)?$"))
				heightSizeUnit = VisualizeAreaUnit.Pixel;

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
				case VisualizeAreaUnit.Percentage:
					size.Width = rect.Width * _width / 100.0f;
					break;
				case VisualizeAreaUnit.Pixel:
					size.Width = _width;
					break;
			}
			switch (heightSizeUnit)
			{
				case VisualizeAreaUnit.Percentage:
					size.Height = rect.Height * _height / 100.0f;
					break;
				case VisualizeAreaUnit.Pixel:
					size.Height = _height;
					break;
			}

			return size;
		}
	}
}
