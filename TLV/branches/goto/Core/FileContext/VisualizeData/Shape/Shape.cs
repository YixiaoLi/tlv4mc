using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Reflection;

namespace NU.OJL.MPRTOS.TLV.Core
{
	public class Shape
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
				shape.Pen.EndCap = LineCap.Flat;
				shape.Pen.StartCap = LineCap.Flat;
				shape.Pen.DashPattern = new float[] { 1.0f, 1.0f };
				shape.Type = ShapeType.Undefined;
				shape.Align = ContentAlignment.BottomLeft;
				shape.Arc = new Arc(0.0f, 90.0f);
				shape.Area = new Area("0,0","100%,100%");
				shape.Coordinates = new CoordinateList() { new Coordinate("0,0"), new Coordinate("100%,0"), new Coordinate("100%,100%"), new Coordinate("0,100%") };
				shape.Fill = Color.White;
				shape.Offset = new Coordinate("0,0");
				shape.Value = string.Empty;
				return shape;
			}
		}

		public ShapeType? Type { get; set; }
		public string Value { get; set; }
		public CoordinateList Coordinates { get; set; }
		public Coordinate Offset { get; set; }
		public Area Area { get; set; }
		public Arc Arc { get; set; }	
		public Pen Pen { get; set; }
		public Color? Fill { get; set; }
		public ContentAlignment? Align { get; set; }

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

		public void Draw(Graphics graphics, RectangleF rect)
		{
			RectangleF area;
			PointF[] points;

			if (Offset == null)
				Offset = Default.Offset;

			switch(Type)
			{
				case ShapeType.Line:
					points = Coordinates.ToPointF(rect);
					if (Pen == null)
						throw new Exception("Lineの描画にはPenの指定が必要です。");
					graphics.DrawLine(Pen, points[0], points[1]);
					break;
				case ShapeType.Rectangle:
					area = Area.ToRectangleF(Offset, rect);
					if (Fill.HasValue)
						graphics.FillRectangle(new SolidBrush(Fill.Value), area.X, area.Y, area.Width, area.Height);
					if (Pen != null)
						graphics.DrawRectangle(Pen, area.X, area.Y, area.Width, area.Height);
					break;
				case ShapeType.Ellipse:
					area = Area.ToRectangleF(Offset, rect);
					if (Fill.HasValue)
						graphics.FillEllipse(new SolidBrush(Fill.Value), area);
					if (Pen != null)
						graphics.DrawEllipse(Pen, area);
					break;
				case ShapeType.Pie:
					if (Arc == null)
						throw new Exception("Pieの描画にはArcの指定が必要です。");
					area = Area.ToRectangleF(Offset, rect);
					if (Fill.HasValue)
						graphics.FillPie(new SolidBrush(Fill.Value), area.X, area.Y, area.Width, area.Height, Arc.Start, Arc.Sweep);
					if (Pen != null)
						graphics.DrawPie(Pen, area, Arc.Start, Arc.Sweep);
					break;
				case ShapeType.Polygon:
					points = Coordinates.ToPointF(rect);
					if (Pen != null)
						graphics.DrawPolygon(Pen, points);
					break;
				default:
					throw new Exception("未知の図形でず。");
			}
		}
	}
}
