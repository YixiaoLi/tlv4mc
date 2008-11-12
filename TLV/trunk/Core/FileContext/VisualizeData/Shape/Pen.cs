using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace NU.OJL.MPRTOS.TLV.Core
{
	public class Pen
	{
		public Color Color { get; set; }
		public float Width { get; set; }
		public DashStyle DashStyle { get; set; }
		public float[] DashPattern { get; set; }
		public LineCap StartCap { get; set; }
		public LineCap EndCap { get; set; }
		public DashCap DashCap { get; set; }

		public Pen()
		{
			Color = Color.Black;
			Width = 1.0f;
			DashStyle = DashStyle.Solid;
			StartCap = LineCap.Flat;
			EndCap = LineCap.Flat;
			DashCap = DashCap.Flat;
			DashPattern = new [] { 2.0f,2.0f };
		}
	}
}
