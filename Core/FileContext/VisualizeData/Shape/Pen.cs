/*
 *  TLV - Trace Log Visualizer
 *
 *  Copyright (C) 2008,2009 by Nagoya Univ., JAPAN
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
	public class Pen : IHavingNullableProperty
	{
		private System.Drawing.Pen _pen = null;
		private Color? _color;
		public Color? Color
		{
			get { return _color; }
			set
			{
				_color = (Alpha.HasValue && value.HasValue && value.Value.A == 0) ? System.Drawing.Color.FromArgb(Alpha.Value, value.Value) : value;
			}
		}
		public int? Alpha { get; set; }
		public float? Width { get; set; }
		public DashStyle? DashStyle { get; set; }
		public float[] DashPattern { get; set; }
		public DashCap? DashCap { get; set; }

		public Pen()
		{

		}

		public void SetPen(System.Drawing.Pen pen)
		{
			_pen = pen;
		}

		public System.Drawing.Pen GetPen()
		{
			if (_pen == null)
				_pen = this;

			return _pen;
		}

		public static implicit operator System.Drawing.Pen(Pen pen)
		{
			if (pen == null)
				return null;

			if (pen._pen != null)
				return pen._pen;

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

			p.Color = (pen.Alpha.HasValue && pen.Color.HasValue && pen.Color.Value.A == 0) ? System.Drawing.Color.FromArgb(pen.Alpha.Value, pen.Color.Value) : pen.Color.Value;

			pen._pen = p;

			return p;
		}
	}
}
