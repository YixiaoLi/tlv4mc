
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace NU.OJL.MPRTOS.TLV.Core
{
	public class Font : IHavingNullableProperty
	{
		private StringFormat _stringFormat = null;
		private System.Drawing.Font _font = null;
		private Color? _color;
		public FontFamily Family { get; set; }
		public FontStyle? Style { get; set; }
		public Color? Color
		{
			get { return _color; }
			set
			{
				_color = (Alpha.HasValue && value.HasValue && value.Value.A == 0) ? System.Drawing.Color.FromArgb(Alpha.Value, value.Value) : value;
			}
		}
		public int? Alpha { get; set; }
		public float? Size { get; set; }
		public ContentAlignment? Align { get;set; }

		public StringFormat GetStringFormat()
		{
			if(_stringFormat != null)
				return _stringFormat;

			StringAlignment align = StringAlignment.Center;
			StringAlignment lineAlign = StringAlignment.Center;

			if (Align.HasValue)
			{
				if (Align.Value == ContentAlignment.BottomRight || Align.Value == ContentAlignment.MiddleRight || Align.Value == ContentAlignment.TopRight)
					align = StringAlignment.Far;

				if (Align.Value == ContentAlignment.BottomCenter || Align.Value == ContentAlignment.BottomLeft || Align.Value == ContentAlignment.BottomRight)
					lineAlign = StringAlignment.Far;

				if (Align.Value == ContentAlignment.BottomCenter || Align.Value == ContentAlignment.MiddleCenter || Align.Value == ContentAlignment.TopCenter)
					align = StringAlignment.Center;

				if (Align.Value == ContentAlignment.MiddleCenter || Align.Value == ContentAlignment.MiddleLeft || Align.Value == ContentAlignment.MiddleRight)
					lineAlign = StringAlignment.Center;

				if (Align.Value == ContentAlignment.BottomLeft || Align.Value == ContentAlignment.MiddleLeft || Align.Value == ContentAlignment.TopLeft)
					align = StringAlignment.Near;

				if (Align.Value == ContentAlignment.TopCenter || Align.Value == ContentAlignment.TopRight || Align.Value == ContentAlignment.TopLeft)
					lineAlign = StringAlignment.Near;
			}
			_stringFormat = new StringFormat() { Alignment = align, LineAlignment = lineAlign, Trimming = StringTrimming.EllipsisCharacter, FormatFlags = StringFormatFlags.NoWrap };
				return _stringFormat;
		}

		public static implicit operator System.Drawing.Font(Font font)
		{
			if (font._font != null)
				return font._font;

			FontFamily ff;
			FontStyle fs;
			float sz;

			if (font.Family == null)
				ff = Shape.Default.Font.Family;
			else
				ff = font.Family;

			if (font.Size.HasValue)
				sz = font.Size.Value;
			else
				sz = Shape.Default.Font.Size.Value;

			if (font.Style.HasValue)
				fs = font.Style.Value;
			else
				fs = Shape.Default.Font.Style.Value;

			System.Drawing.Font f = new System.Drawing.Font(ff, sz, fs);

			font._font = f;

			return f;
		}
	}
}
