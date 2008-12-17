using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace NU.OJL.MPRTOS.TLV.Core
{
	public class Pen : IHavingNullableProperty
	{
		public Color? Color { get; set; }
		public float? Width { get; set; }
		public DashStyle? DashStyle { get; set; }
		public float[] DashPattern { get; set; }
		public DashCap? DashCap { get; set; }
		public int? Alpha { get; set; }

		public Pen()
		{
		}

		public static implicit operator System.Drawing.Pen(Pen pen)
		{
			System.Drawing.Pen p = new System.Drawing.Pen(System.Drawing.Color.Black);

			if (pen.Color.HasValue)
				p.Color = pen.Color.Value;

			if (pen.DashCap.HasValue)
				p.DashCap = pen.DashCap.Value;
			else
				p.DashCap = System.Drawing.Drawing2D.DashCap.Flat;

			if (pen.DashStyle.HasValue)
			{
				p.DashStyle = pen.DashStyle.Value;

				if (p.DashStyle == System.Drawing.Drawing2D.DashStyle.Custom)
				{
					if (pen.DashPattern != null)
						p.DashPattern = pen.DashPattern;
					else
						p.DashPattern = new float[] { 1.0f, 1.0f };
				}
			}
			else
			{
				p.DashStyle = System.Drawing.Drawing2D.DashStyle.Solid;
			}

			if (pen.Width.HasValue)
				p.Width = pen.Width.Value;
			else
				p.Width = 1.0f;

			return p;
		}
	}
}
