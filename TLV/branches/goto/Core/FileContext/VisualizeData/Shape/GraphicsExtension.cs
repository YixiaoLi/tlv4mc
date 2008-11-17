using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Drawing.Drawing2D;
using NU.OJL.MPRTOS.TLV.Base;

namespace NU.OJL.MPRTOS.TLV.Core
{
	public static class GraphicsExtension
	{
		public static void DrawShape(this Graphics graphics, Shape shape, RectangleF rect)
		{
			throwException(shape);

			Area a;
			PointList c;
			Point os;
			ContentAlignment ca;

			if (shape.Area == null)
			{
				if (shape.Location != null && shape.Size != null)
					a = new Area(shape.Location, shape.Size);
				else
					a = Shape.Default.Area;
			}
			else
			{
				a = shape.Area;
			}

			if (shape.Points == null)
				c = Shape.Default.Points;
			else
				c = shape.Points;

			if (shape.Offset == null)
				os = Shape.Default.Offset;
			else
				os = shape.Offset;

			if (shape.Align == null)
				ca = Shape.Default.Align.Value;
			else
				ca = shape.Align.Value;

			RectangleF area = a.ToRectangleF(os, ca, rect);
			PointF[] points = c.ToPointF(os, rect);

			switch (shape.Type)
			{
				case ShapeType.Line:
					graphics.DrawLine(shape.Pen, points[0], points[1]);
					break;
				case ShapeType.Arrow:
					System.Drawing.Pen pen = shape.Pen;
					pen.EndCap = LineCap.Custom;
					pen.CustomEndCap = new AdjustableArrowCap(pen.Width, pen.Width);
					graphics.DrawLine(pen, points[0], points[1]);
					break;
				case ShapeType.Rectangle:
					if (shape.Fill.HasValue)
						graphics.FillRectangle(new SolidBrush(shape.Fill.Value), area);
					if (shape.Pen != null)
						graphics.DrawRectangle(shape.Pen, area.X, area.Y, area.Width, area.Height);
					break;
				case ShapeType.Ellipse:
					if (shape.Fill.HasValue)
						graphics.FillEllipse(new SolidBrush(shape.Fill.Value), area);
					if (shape.Pen != null)
						graphics.DrawEllipse(shape.Pen, area);
					break;
				case ShapeType.Pie:
					if (shape.Fill.HasValue)
						graphics.FillPie(new SolidBrush(shape.Fill.Value), area.X, area.Y, area.Width, area.Height, shape.Arc.Start, shape.Arc.Sweep);
					if (shape.Pen != null)
						graphics.DrawPie(shape.Pen, area, shape.Arc.Start, shape.Arc.Sweep);
					break;
				case ShapeType.Polygon:
					if (shape.Fill.HasValue)
						graphics.FillPolygon(new SolidBrush(shape.Fill.Value), points);
					if (shape.Pen != null)
						graphics.DrawPolygon(shape.Pen, points);
					break;
				case ShapeType.String:
					string value;
					Color color;
					StringAlignment align = StringAlignment.Center;
					StringAlignment lineAlign = StringAlignment.Center;
					Font font;

					if (shape.Text == null)
						value = Shape.Default.Text;
					else
						value = shape.Text;

					if (shape.Font == null)
					{
						font = Shape.Default.Font;
						color = Shape.Default.Font.Color.Value;
					}
					else
					{
						font = shape.Font;
						if (!shape.Font.Color.HasValue)
							color = Shape.Default.Font.Color.Value;
						else
							color = shape.Font.Color.Value;

						if (shape.Font.Align.HasValue)
						{

							if (shape.Font.Align.Value == ContentAlignment.BottomRight || shape.Font.Align.Value == ContentAlignment.MiddleRight || shape.Font.Align.Value == ContentAlignment.TopRight)
								align = StringAlignment.Far;

							if (shape.Font.Align.Value == ContentAlignment.BottomCenter || shape.Font.Align.Value == ContentAlignment.BottomLeft || shape.Font.Align.Value == ContentAlignment.BottomRight)
								lineAlign = StringAlignment.Far;

							if (shape.Font.Align.Value == ContentAlignment.BottomCenter || shape.Font.Align.Value == ContentAlignment.MiddleCenter || shape.Font.Align.Value == ContentAlignment.TopCenter)
								align = StringAlignment.Center;

							if (shape.Font.Align.Value == ContentAlignment.MiddleCenter || shape.Font.Align.Value == ContentAlignment.MiddleLeft || shape.Font.Align.Value == ContentAlignment.MiddleRight)
								lineAlign = StringAlignment.Center;

							if (shape.Font.Align.Value == ContentAlignment.BottomLeft || shape.Font.Align.Value == ContentAlignment.MiddleLeft || shape.Font.Align.Value == ContentAlignment.TopLeft)
								align = StringAlignment.Near;

							if (shape.Font.Align.Value == ContentAlignment.TopCenter || shape.Font.Align.Value == ContentAlignment.TopRight || shape.Font.Align.Value == ContentAlignment.TopLeft)
								lineAlign = StringAlignment.Near;
						}
					}

					graphics.DrawString(value, font, new SolidBrush(color), area, new StringFormat() { Alignment = align, LineAlignment = lineAlign });
					break;
			}
		}

		private static void throwException(Shape shape)
		{
			switch (shape.Type)
			{
				case ShapeType.Line:
					if (shape.Pen == null)
						throw new Exception("Lineの描画にはPenの指定が必要です。");
					if (shape.Points == null)
						throw new Exception("Lineの描画にはCoordinatesの指定が必要です。");
					break;
				case ShapeType.Arrow:
					if (shape.Pen == null)
						throw new Exception("Arrowの描画にはPenの指定が必要です。");
					if (shape.Points == null)
						throw new Exception("Arrowの描画にはCoordinatesの指定が必要です。");
					break;
				case ShapeType.Rectangle:
					if (shape.Area == null && (shape.Location == null && shape.Size == null))
						throw new Exception("Rectangleの描画にはAreaまたはPointとSizeの指定が必要です。");
					if (shape.Fill == null && shape.Pen == null)
						throw new Exception("Rectangleの描画にはFillまたはPenの指定が必要です。");
					break;
				case ShapeType.Ellipse:
					if (shape.Area == null && (shape.Location == null && shape.Size == null))
						throw new Exception("Ellipseの描画にはAreaまたはPointとSizeの指定が必要です。");
					if (shape.Fill == null && shape.Pen == null)
						throw new Exception("Ellipseの描画にはFillまたはPenの指定が必要です。");
					break;
				case ShapeType.Pie:
					if (shape.Arc == null)
						throw new Exception("Pieの描画にはArcの指定が必要です。");
					if (shape.Area == null && (shape.Location == null && shape.Size == null))
						throw new Exception("Pieの描画にはAreaまたはPointとSizeの指定が必要です。");
					if (shape.Fill == null && shape.Pen == null)
						throw new Exception("Pieの描画にはFillまたはPenの指定が必要です。");
					break;
				case ShapeType.Polygon:
					if (shape.Area == null && (shape.Location == null && shape.Size == null))
						throw new Exception("Polygonの描画にはCoordinatesの指定が必要です。");
					if (shape.Fill == null && shape.Pen == null)
						throw new Exception("Polygonの描画にはFillまたはPenの指定が必要です。");
					break;
				case ShapeType.String:
					if (shape.Area == null && (shape.Location == null && shape.Size == null))
						throw new Exception("Stringの描画にはAreaまたはPointとSizeの指定が必要です。");
					break;
				default:
					throw new Exception("未知の図形でず。");
			}
		}
	}
}
