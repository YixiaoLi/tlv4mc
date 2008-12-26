﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Reflection;
using NU.OJL.MPRTOS.TLV.Base;
using System.Text.RegularExpressions;

namespace NU.OJL.MPRTOS.TLV.Core
{
	public class Shape : IHavingNullableProperty, ICloneable
	{
		private SolidBrush _brush = null;

		private Json _metaData;
		private List<string> _argPropList = new List<string>();
		private Color? _fill;

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
				shape.Pen.Alpha = 255;
				shape.Pen.DashPattern = new float[] { 1.0f, 1.0f };
				shape.Font = new Font();
				shape.Font.Color = Color.Black;
				shape.Font.Family = FontFamily.GenericSansSerif;
				shape.Font.Size = 8f;
				shape.Font.Style = FontStyle.Regular;
				shape.Font.Align = ContentAlignment.MiddleCenter;
				shape.Type = ShapeType.Undefined;
				shape.Arc = new Arc(0.0f, 90.0f);
				shape.Area = new Area("0,0","100%,100%");
				shape.Location = new Point("0,0");
				shape.Size = new Size("100%,100%");
				shape.Points = new PointList() { new Point("0,0"), new Point("100%,0"), new Point("100%,100%"), new Point("0,100%") };
				shape.Fill = Color.White;
				shape.Offset = new Size("0,0");
				shape.Text = string.Empty;
				shape.Alpha = 255;
				return shape;
			}
		}

		public ShapeType? Type { get; set; }
		public string Text { get; set; }
		public PointList Points { get; set; }
		public Size Offset { get; set; }
		public Point Location { get; set; }
		public Size Size { get;set; }
		public Arc Arc { get; set; }
		public Pen Pen { get; set; }
		public Font Font { get; set; }
		public Color? Fill
		{
			get { return _fill; }
			set
			{
				_fill = (Alpha.HasValue && value.HasValue && value.Value.A == 0) ? Color.FromArgb(Alpha.Value, value.Value) : value;
			}
		}
		public Area Area { get; set; }
		public int? Alpha { get; set; }

		public Json MetaData
		{
			get { return _metaData; }
			set
			{
				_metaData = value;

				PropertyInfo[] pis = typeof(Shape).GetProperties();

				foreach (KeyValuePair<string,Json> kvp in _metaData.GetKeyValuePairEnumerator())
				{
					PropertyInfo pi;

					try
					{
						pi = pis.Single(p => p.Name == kvp.Key);
					}
					catch
					{
						continue;
					}
					if (!kvp.Value.ToJsonString().Contains("ARG"))
					{
						object obj = ApplicationFactory.JsonSerializer.Deserialize(kvp.Value.ToJsonString(), pi.PropertyType);
						pi.SetValue(this, obj, null);
					}
					else
					{
						_argPropList.Add(kvp.Key);
					}
				}
			}
		}

		public void SetArgs(params string[] args)
		{
			foreach(string str in _argPropList)
			{
				PropertyInfo pi = typeof(Shape).GetProperties().Single(p=>p.Name == str);
				string value = _metaData[str].ToJsonString();
				foreach(Match m in Regex.Matches(value, @"\${ARG(?<id>\d+)}"))
				{
					int i = int.Parse(m.Groups["id"].Value);
					if (args != null && args.Length > i)
						value = value.Replace(m.Value, args[i]);
				}
				pi.SetValue(this, ApplicationFactory.JsonSerializer.Deserialize(value, pi.PropertyType), null);
			}

		}

		public Shape()
		{
		}

		public void Draw(Graphics graphics, RectangleF rect)
		{

			RectangleF area;
			PointF[] points;

			switch (Type)
			{
				case ShapeType.Line:
					points = Points.ToPointF(Offset, rect);
					graphics.DrawLine(Pen, points[0], points[1]);
					break;

				case ShapeType.Arrow:
					points = Points.ToPointF(Offset, rect);
					SmoothingMode s = graphics.SmoothingMode;
					graphics.SmoothingMode = SmoothingMode.AntiAlias;
					graphics.DrawLine(Pen, points[0], points[1]);
					graphics.SmoothingMode = s;
					break;

				case ShapeType.Rectangle:
					area = Area.ToRectangleF(Offset, rect);
					if (Fill.HasValue)
						graphics.FillRectangle(_brush, area);
					if (Pen != null)
						graphics.DrawRectangle(Pen, area.X, area.Y, area.Width, area.Height);
					break;

				case ShapeType.Ellipse:
					area = Area.ToRectangleF(Offset, rect);
					if (Fill.HasValue)
						graphics.FillEllipse(_brush, area);
					if (Pen != null)
						graphics.DrawEllipse(Pen, area);
					break;

				case ShapeType.Pie:
					area = Area.ToRectangleF(Offset, rect);
					if (Fill.HasValue)
						graphics.FillPie(_brush, area.X, area.Y, area.Width, area.Height, Arc.Start, Arc.Sweep);
					if (Pen != null)
						graphics.DrawPie(Pen, area, Arc.Start, Arc.Sweep);
					break;

				case ShapeType.Polygon:
					points = Points.ToPointF(Offset, rect);
					if (Fill.HasValue)
						graphics.FillPolygon(_brush, points);
					if (Pen != null)
						graphics.DrawPolygon(Pen, points);
					break;

				case ShapeType.Text:
					area = Area.ToRectangleF(Offset, rect);
					graphics.DrawString(Text, Font, _brush, area, Font.GetStringFormat());
					break;
			}
		}

		public void ChackValidate()
		{
			switch (Type)
			{
				case ShapeType.Line:
					if (Pen == null)
						throw new Exception("Lineの描画にはPenの指定が必要です。");
					if (Points == null)
						throw new Exception("Lineの描画にはCoordinatesの指定が必要です。");
					break;
				case ShapeType.Arrow:
					if (Pen == null)
						throw new Exception("Arrowの描画にはPenの指定が必要です。");
					if (Points == null)
						throw new Exception("Arrowの描画にはCoordinatesの指定が必要です。");
					break;
				case ShapeType.Rectangle:
					if (Area == null && (Location == null && Size == null))
						throw new Exception("Rectangleの描画にはAreaまたはPointとSizeの指定が必要です。");
					if (Fill == null && Pen == null)
						throw new Exception("Rectangleの描画にはFillまたはPenの指定が必要です。");
					break;
				case ShapeType.Ellipse:
					if (Area == null && (Location == null && Size == null))
						throw new Exception("Ellipseの描画にはAreaまたはPointとSizeの指定が必要です。");
					if (Fill == null && Pen == null)
						throw new Exception("Ellipseの描画にはFillまたはPenの指定が必要です。");
					break;
				case ShapeType.Pie:
					if (Arc == null)
						throw new Exception("Pieの描画にはArcの指定が必要です。");
					if (Area == null && (Location == null && Size == null))
						throw new Exception("Pieの描画にはAreaまたはPointとSizeの指定が必要です。");
					if (Fill == null && Pen == null)
						throw new Exception("Pieの描画にはFillまたはPenの指定が必要です。");
					break;
				case ShapeType.Polygon:
					if (Area == null && (Location == null && Size == null))
						throw new Exception("Polygonの描画にはCoordinatesの指定が必要です。");
					if (Fill == null && Pen == null)
						throw new Exception("Polygonの描画にはFillまたはPenの指定が必要です。");
					break;
				case ShapeType.Text:
					if (Area == null && (Location == null && Size == null))
						throw new Exception("Textの描画にはAreaまたはPointとSizeの指定が必要です。");
					break;
				default:
					throw new Exception("未知の図形でず。");
			}
		}

		public void SetDefaultValue()
		{
			if (Area == null)
			{
				if (Location != null && Size != null)
					Area = new Area(Location, Size);
				else if (Location != null && Size == null)
					Area = new Area(Location, Default.Size);
				else if (Location == null && Size != null)
					Area = new Area(Default.Location, Size);
				else
					Area = Default.Area;
			}

			if (Points == null)
				Points = Default.Points;

			if (Offset == null)
				Offset = Default.Offset;

			if (Type == ShapeType.Arrow)
			{
				System.Drawing.Pen p = Pen.GetPen();
				p.EndCap = LineCap.Custom;
				p.CustomEndCap = new AdjustableArrowCap(p.Width == 1 ? 2 : p.Width, p.Width == 1 ? 2 : p.Width);
				Pen.SetPen(p);
			}

			if (Type == ShapeType.Text)
			{
				if (Text == null)
					Text = Default.Text;

				if (Font == null)
					Font = Default.Font;

				if (!Font.Color.HasValue)
					Font.Color = Default.Font.Color.Value;
			}
			
			switch (Type)
			{
				case ShapeType.Rectangle:
					_brush = new SolidBrush(Fill.Value);
					break;

				case ShapeType.Ellipse:
					_brush = new SolidBrush(Fill.Value);
					break;

				case ShapeType.Pie:
					_brush = new SolidBrush(Fill.Value);
					break;

				case ShapeType.Polygon:
					_brush = new SolidBrush(Fill.Value);
					break;

				case ShapeType.Text:
					_brush = new SolidBrush(Font.Color.Value);
					break;
			}
		}

		public object Clone()
		{
			Shape sp = new Shape();
			sp.MetaData = MetaData;
			sp._argPropList = _argPropList;

			return sp;
		}
	}
}
