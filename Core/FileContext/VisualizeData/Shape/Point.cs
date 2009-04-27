/*
 *  TLV - Trace Log Visualizer
 *
 *  Copyright (C) 2008,2009 by Embedded and Real-Time Systems Laboratory
 *              Graduate School of Information Science, Nagoya Univ., JAPAN
 *
 *  上記著作権者は，以下の(1)〜(4)の条件を満たす場合に限り，本ソフトウェ
 *  ア（本ソフトウェアを改変したものを含む．以下同じ）を使用・複製・改
 *  変・再配布（以下，利用と呼ぶ）することを無償で許諾する．
 *  (1) 本ソフトウェアをソースコードの形で利用する場合には，上記の著作
 *      権表示，この利用条件および下記の無保証規定が，そのままの形でソー
 *      スコード中に含まれていること．
 *  (2) 本ソフトウェアを，ライブラリ形式など，他のソフトウェア開発に使
 *      用できる形で再配布する場合には，再配布に伴うドキュメント（利用
 *      者マニュアルなど）に，上記の著作権表示，この利用条件および下記
 *      の無保証規定を掲載すること．
 *  (3) 本ソフトウェアを，機器に組み込むなど，他のソフトウェア開発に使
 *      用できない形で再配布する場合には，次のいずれかの条件を満たすこ
 *      と．
 *    (a) 再配布に伴うドキュメント（利用者マニュアルなど）に，上記の著
 *        作権表示，この利用条件および下記の無保証規定を掲載すること．
 *    (b) 再配布の形態を，別に定める方法によって，TOPPERSプロジェクトに
 *        報告すること．
 *  (4) 本ソフトウェアの利用により直接的または間接的に生じるいかなる損
 *      害からも，上記著作権者およびTOPPERSプロジェクトを免責すること．
 *      また，本ソフトウェアのユーザまたはエンドユーザからのいかなる理
 *      由に基づく請求からも，上記著作権者およびTOPPERSプロジェクトを
 *      免責すること．
 *
 *  本ソフトウェアは，無保証で提供されているものである．上記著作権者お
 *  よびTOPPERSプロジェクトは，本ソフトウェアに関して，特定の使用目的
 *  に対する適合性も含めて，いかなる保証も行わない．また，本ソフトウェ
 *  アの利用により直接的または間接的に生じたいかなる損害に関しても，そ
 *  の責任を負わない．
 *
 *  @(#) $Id$
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Text.RegularExpressions;

namespace NU.OJL.MPRTOS.TLV.Core
{
	public class Point
	{
		private float _x;
		private float _y;
		private string _point;
		private VisualizeAreaUnit xVisualizeAreaUnit;
		private VisualizeAreaUnit yVisualizeAreaUnit;
		private XAxisReference xAxisReference;
		private YAxisReference yAxisReference;

		public Point(string x, string y)
			:this(x + "," + y)
		{
		}

		public Point(string coordinate)
		{
			_point = coordinate;
			_point = _point.Replace(" ", "").Replace("\t", "");
			string[] c = _point.Split(',');

			string x = c[0];
			string y = c[1];

			string num = @"-?([1-9][0-9]*)?[0-9](\.[0-9]*)?";

			if (!Regex.IsMatch(x, @"^(([lcr]\(" + num + @"(%|px)?\))|(" + num + @"(%|px)?))$"))
				throw new Exception("座標指定が異常です。\n" + coordinate);
			if (!Regex.IsMatch(y, @"^(([tmb]\(" + num + @"(%|px)?\))|(" + num + @"(%|px)?))$"))
				throw new Exception("座標指定が異常です。\n" + coordinate);

			if (Regex.IsMatch(x, @"^(([lcr]\(" + num + @"%\))|(" + num + @"%))$"))
				xVisualizeAreaUnit = VisualizeAreaUnit.Percentage;
			else if (Regex.IsMatch(x, @"^(([lcr]\(" + num + @"(px)?\))|(" + num + @"(px)?))$"))
				xVisualizeAreaUnit = VisualizeAreaUnit.Pixel;

			if (Regex.IsMatch(y, @"^(([tmb]\(" + num + @"%\))|(" + num + @"%))$"))
				yVisualizeAreaUnit = VisualizeAreaUnit.Percentage;
			else if (Regex.IsMatch(y, @"^(([tmb]\(" + num + @"(px)?\))|(" + num + @"(px)?))$"))
				yVisualizeAreaUnit = VisualizeAreaUnit.Pixel;

			if (Regex.IsMatch(x, @"^(l\(" + num + @"(%|px)?\))$"))
				xAxisReference = XAxisReference.Left;
			else if (Regex.IsMatch(x, @"^r\(" + num + @"(%|px)?\)$"))
				xAxisReference = XAxisReference.Right;
			else if (Regex.IsMatch(x, @"^c\(" + num + @"(%|px)?\)$"))
				xAxisReference = XAxisReference.Center;
			else if (Regex.IsMatch(x, @"^(" + num + @"(%|px)?)$"))
				xAxisReference = XAxisReference.Zero;

			if (Regex.IsMatch(y, @"^(b\(" + num + @"(%|px)?\))$"))
				yAxisReference = YAxisReference.Bottom;
			else if (Regex.IsMatch(y, @"^t\(" + num + @"(%|px)?\)$"))
				yAxisReference = YAxisReference.Top;
			else if (Regex.IsMatch(y, @"^m\(" + num + @"(%|px)?\)$"))
				yAxisReference = YAxisReference.Middle;
			else if (Regex.IsMatch(y, @"^(" + num + @"(%|px)?)$"))
				yAxisReference = YAxisReference.Zero;

			x = x.Replace("%", "").Replace("px", "").Replace("(", "").Replace(")", "").Replace("l", "").Replace("r", "").Replace("c", "");
			y = y.Replace("%", "").Replace("px", "").Replace("(", "").Replace(")", "").Replace("t", "").Replace("b", "").Replace("m", "");

			_x = float.Parse(x);
			_y = float.Parse(y);
		}

		public override string ToString()
		{
			return _point;
		}

		public PointF ToPointF(RectangleF rect)
		{
			return ToPointF(new Size("0,0"), rect);
		}

		public PointF ToPointF(Size offset, RectangleF rect)
		{
			PointF point = new PointF();

			switch (xAxisReference)
			{
				case XAxisReference.Zero:
				case XAxisReference.Left:
					switch (xVisualizeAreaUnit)
					{
						case VisualizeAreaUnit.Percentage:
							point.X = rect.Left + rect.Width * _x / 100.0f;
							break;
						case VisualizeAreaUnit.Pixel:
							point.X = rect.Left + _x;
							break;
					}
					break;
				case XAxisReference.Right:
					switch (xVisualizeAreaUnit)
					{
						case VisualizeAreaUnit.Percentage:
							point.X = rect.Right + rect.Width * _x / 100.0f;
							break;
						case VisualizeAreaUnit.Pixel:
							point.X = rect.Right + _x;
							break;
					}
					break;
				case XAxisReference.Center:
					switch (xVisualizeAreaUnit)
					{
						case VisualizeAreaUnit.Percentage:
							point.X = (rect.Left + (rect.Width / 2f)) + rect.Width * _x / 100.0f;
							break;
						case VisualizeAreaUnit.Pixel:
							point.X = (rect.Left + (rect.Width / 2f)) + _x;
							break;
					}
					break;
			}
			switch (yAxisReference)
			{
				case YAxisReference.Zero:
				case YAxisReference.Bottom:
					switch (yVisualizeAreaUnit)
					{
						case VisualizeAreaUnit.Percentage:
							point.Y = rect.Bottom - rect.Height * _y / 100.0f;
							break;
						case VisualizeAreaUnit.Pixel:
							point.Y = rect.Bottom - _y;
							break;
					}
					break;
				case YAxisReference.Top:
					switch (yVisualizeAreaUnit)
					{
						case VisualizeAreaUnit.Percentage:
							point.Y = rect.Top - rect.Height * _y / 100.0f;
							break;
						case VisualizeAreaUnit.Pixel:
							point.Y = rect.Top - _y;
							break;
					}
					break;
				case YAxisReference.Middle:
					switch (yVisualizeAreaUnit)
					{
						case VisualizeAreaUnit.Percentage:
							point.Y = (rect.Bottom - (rect.Height / 2f)) - rect.Height * _y / 100.0f;
							break;
						case VisualizeAreaUnit.Pixel:
							point.Y = (rect.Bottom - (rect.Height / 2f)) - _y;
							break;
					}
					break;
			}

			if (offset.ToString() != "0,0")
			{
				SizeF off = offset.ToSizeF(rect);

				point.X += off.Width;
				point.Y -= off.Height;
			}
			
			return point;
		}
	}

	public enum XAxisReference
	{
		Left,
		Center,
		Right,
		Zero
	}

	public enum YAxisReference
	{
		Top,
		Middle,
		Bottom,
		Zero
	}
}
