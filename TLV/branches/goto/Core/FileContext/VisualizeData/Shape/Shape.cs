using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Reflection;

namespace NU.OJL.MPRTOS.TLV.Core
{
	public class Shape : IHavingNullableProperty
	{
		public static Shape Default
		{
			get
			{
				Shape shape = new Shape();
				shape.Pen = new Pen();
				shape.Pen.Width = 1.0f;
				shape.Pen.Color = Color.Black;
				shape.Pen.DashCap = DashCap.Flat;
				shape.Pen.DashStyle = DashStyle.Solid;
				shape.Pen.DashPattern = new float[] { 1.0f, 1.0f };
				shape.Font = new Font();
				shape.Font.Color = Color.Black;
				shape.Font.Family = FontFamily.GenericSansSerif;
				shape.Font.Size = 10.5f;
				shape.Font.Style = FontStyle.Regular;
				shape.Font.Align = ContentAlignment.MiddleCenter;
				shape.Type = ShapeType.Undefined;
				shape.Align = ContentAlignment.BottomLeft;
				shape.Arc = new Arc(0.0f, 90.0f);
				shape.Area = new Area("0,0","100%,100%");
				shape.Location = new Point("0,0");
				shape.Size = new Size("100%,100%");
				shape.Points = new PointList() { new Point("0,0"), new Point("100%,0"), new Point("100%,100%"), new Point("0,100%") };
				shape.Fill = Color.White;
				shape.Offset = new Point("0,0");
				shape.Text = string.Empty;
				return shape;
			}
		}

		public ShapeType? Type { get; set; }
		public string Text { get; set; }
		public PointList Points { get; set; }
		public Point Offset { get; set; }
		public Point Location { get; set; }
		public Size Size { get;set; }
		public Arc Arc { get; set; }
		public Pen Pen { get; set; }
		public Font Font { get; set; }
		public Color? Fill { get; set; }
		public ContentAlignment? Align { get; set; }
		public Area Area { get; set; }

		public Shape()
		{
		}

		public void SetDefaultValueToNullProperty()
		{
			foreach (PropertyInfo pi in this.GetType().GetProperties())
			{
				if (pi.GetValue(this, null) == null)
				{
					pi.SetValue(this, pi.GetValue(Shape.Default, null), null);
				}
			}
		}

	}
}
