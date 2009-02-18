using System;
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
		private static Dictionary<string, Shape> _cache = new Dictionary<string, Shape>();

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
						object obj;
						lock (ApplicationFactory.JsonSerializer)
						{
							obj = ApplicationFactory.JsonSerializer.Deserialize(kvp.Value.ToJsonString(), pi.PropertyType);
						}
						pi.SetValue(this, obj, null);
					}
					else
					{
						_argPropList.Add(kvp.Key);
					}
				}
			}
		}

		/// <summary>
		/// Shapeに引数の値を代入する。
		/// リフレクションを用いており低速なため、キャッシュを利用している
		/// </summary>
		/// <param name="args">引数</param>
		public void SetArgs(params string[] args)
		{
			string k = args.ToCSVString() + _metaData.ToJsonString();

			if (_cache.ContainsKey(k))
			{
				_metaData = _cache[k]._metaData;
				_argPropList = _cache[k]._argPropList;
				_brush = _cache[k]._brush;
				_fill = _cache[k]._fill;
				Type = _cache[k].Type;
				Text = _cache[k].Text;
				Points = _cache[k].Points;
				Offset = _cache[k].Offset;
				Location = _cache[k].Location;
				Size = _cache[k].Size;
				Arc = _cache[k].Arc;
				Pen = _cache[k].Pen;
				Font = _cache[k].Font;
				Fill = _cache[k].Fill;
				Area = _cache[k].Area;
				Alpha = _cache[k].Alpha;
				return;
			}
			else
			{
				foreach (string str in _argPropList)
				{
					PropertyInfo pi = typeof(Shape).GetProperties().Single(p => p.Name == str);
					string value = _metaData[str].ToJsonString();
					foreach (Match m in Regex.Matches(value, @"\${ARG(?<id>\d+)}"))
					{
						int i = int.Parse(m.Groups["id"].Value);
						if (args != null && args.Length > i)
							value = value.Replace(m.Value, args[i]);
					}
					lock (ApplicationFactory.JsonSerializer)
					{
						pi.SetValue(this, ApplicationFactory.JsonSerializer.Deserialize(value, pi.PropertyType), null);
					}
				}

				lock (_cache)
				{
					if (!_cache.ContainsKey(k))
						_cache.Add(k, this);
				}
			}
		}

		public Shape()
		{
		}

		public void Draw(Graphics graphics, RectangleF rect)
		{

			RectangleF area = (Area != null) ? Area.ToRectangleF(Offset, rect) : RectangleF.Empty;
			PointF[] points = (Points != null) ? Points.ToPointF(Offset, rect) : null;

			//Rectangle area = new Rectangle((int)areaF.X, (int)areaF.Y, (int)areaF.Width, (int)areaF.Height);

			area.Intersect(new RectangleF(
				(graphics.ClipBounds.X - graphics.ClipBounds.Width),
				(graphics.ClipBounds.Y - graphics.ClipBounds.Height),
				(graphics.ClipBounds.Width * 3),
				(graphics.ClipBounds.Height * 3)
				));

			switch (Type)
			{
				case ShapeType.Line:
					graphics.DrawLine(Pen, points[0], points[1]);
					break;

				case ShapeType.Arrow:
					SmoothingMode s = graphics.SmoothingMode;
					graphics.SmoothingMode = SmoothingMode.AntiAlias;
					graphics.DrawLine(Pen, points[0], points[1]);
					graphics.SmoothingMode = s;
					break;

				case ShapeType.Rectangle:
					if (Fill.HasValue)
						graphics.FillRectangle(_brush, area);
					if (Pen != null)
						graphics.DrawRectangle(Pen, area.X, area.Y, area.Width, area.Height);
					break;

				case ShapeType.Ellipse:
					if (Fill.HasValue)
						graphics.FillEllipse(_brush, area);
					if (Pen != null)
						graphics.DrawEllipse(Pen, area);
					break;

				case ShapeType.Pie:
					if (Fill.HasValue)
						graphics.FillPie(_brush, area.X, area.Y, area.Width, area.Height, Arc.Start, Arc.Sweep);
					if (Pen != null)
						graphics.DrawPie(Pen, area, Arc.Start, Arc.Sweep);
					break;

				case ShapeType.Polygon:
					if (Fill.HasValue)
						graphics.FillPolygon(_brush, points);
					if (Pen != null)
						graphics.DrawPolygon(Pen, points);
					break;

				case ShapeType.Text:
					RectangleF a = new RectangleF(area.X + 1, area.Y + 1, area.Width - 2, area.Height - 2);
					SizeF size = graphics.MeasureString(Text, Font);

					float h = size.Height - a.Height;

					if (h > 0)
					{
						a = new RectangleF(a.X, a.Y - h, a.Width, a.Height + h);
					}

					graphics.DrawString(Text, Font, _brush, a, Font.GetStringFormat());
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
				lock(p)
				{
					p.EndCap = LineCap.Custom;
					p.CustomEndCap = new AdjustableArrowCap(p.Width == 1 ? 2 : p.Width, p.Width == 1 ? 2 : p.Width);
					Pen.SetPen(p);
				}
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
				case ShapeType.Ellipse:
				case ShapeType.Pie:
				case ShapeType.Polygon:
				case ShapeType.Rectangle:
					Color c = Fill.Value;
					if (Alpha.HasValue)
						c = Color.FromArgb(Alpha.Value, c);
					_brush = new SolidBrush(c);
					break;

				case ShapeType.Text:
					_brush = new SolidBrush(Font.Color.Value);
					break;
			}
		}

		public object Clone()
		{
			Shape sp = new Shape();

			sp._metaData = _metaData;
			sp._argPropList = _argPropList;
			sp._brush = _brush;
			sp._fill = _fill;

			sp.Type = Type;
			sp.Text = Text;
			sp.Points = Points;
			sp.Offset = Offset;
			sp.Location = Location;
			sp.Size = Size;
			sp.Arc = Arc;
			sp.Pen = Pen;
			sp.Font = Font;
			sp.Fill = Fill;
			sp.Area = Area;
			sp.Alpha = Alpha;

			return sp;
		}
	}
}
