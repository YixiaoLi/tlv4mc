using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace NU.OJL.MPRTOS.TLV.Core
{
	public class Font
	{
		public FontFamily Family { get; set; } 
		public FontStyle? Style { get; set; }
		public Color? Color { get; set; }
		public float? Size { get; set; }
		public ContentAlignment? Align { get;set; }

		public static implicit operator System.Drawing.Font(Font font)
		{
			FontFamily ff;
			FontStyle fs;
			float sz;

			if (font.Family == null)
				ff = FontFamily.GenericSansSerif;
			else
				ff = font.Family;

			if (!font.Size.HasValue)
				sz = 10.5f;
			else
				sz = font.Size.Value;

			if (font.Style.HasValue)
				fs = font.Style.Value;
			else
				fs = FontStyle.Regular;

			System.Drawing.Font f = new System.Drawing.Font(ff, sz, fs);

			return f;
		}
	}
}
