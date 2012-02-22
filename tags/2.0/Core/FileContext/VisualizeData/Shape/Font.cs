/*
 *  TLV - Trace Log Visualizer
 *
 *  Copyright (C) 2008-2011 by Nagoya Univ., JAPAN
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
