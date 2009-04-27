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
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Text;
using System.Text.RegularExpressions;

namespace NU.OJL.MPRTOS.TLV.Core
{
	public class Size
	{
		private float _width;
		private float _height;
		private string _size;
		public string Width { get; set; }
		public string Height { get; set; }
		private VisualizeAreaUnit widthSizeUnit;
		private VisualizeAreaUnit heightSizeUnit;

		public Size(string size)
		{
			_size = size;
			_size = _size.Replace(" ", "").Replace("\t", "");
			string[] c = _size.Split(',');

			string w = c[0];
			string h = c[1];

			string num = @"-?([1-9][0-9]*)?[0-9](\.[0-9]*)?";

			if (!Regex.IsMatch(w,  @"^" + num + @"(%|px)?$"))
				throw new Exception("サイズ指定が異常です。\n" + size);
			if (!Regex.IsMatch(h, @"^" + num + @"(%|px)?$"))
				throw new Exception("サイズ指定が異常です。\n" + size);

			if (Regex.IsMatch(w, @"^" + num + @"%$"))
				widthSizeUnit = VisualizeAreaUnit.Percentage;
			else
				widthSizeUnit = VisualizeAreaUnit.Pixel;

			if (Regex.IsMatch(h, @"^" + num + @"%$"))
				heightSizeUnit = VisualizeAreaUnit.Percentage;
			else
				heightSizeUnit = VisualizeAreaUnit.Pixel;

			w = w.Replace("%", "").Replace("px", "").Replace("(", "").Replace(")", "").Replace("l", "").Replace("r", "");
			h = h.Replace("%", "").Replace("px", "").Replace("(", "").Replace(")", "").Replace("t", "").Replace("b", "");

			_width = float.Parse(w);
			_height = float.Parse(h);
		}

		public override string ToString()
		{
			return _size;
		}

		public SizeF ToSizeF(RectangleF rect)
		{
			SizeF size = new SizeF();

			switch (widthSizeUnit)
			{
				case VisualizeAreaUnit.Percentage:
					size.Width = rect.Width * _width / 100.0f;
					break;
				case VisualizeAreaUnit.Pixel:
					size.Width = _width;
					break;
			}
			switch (heightSizeUnit)
			{
				case VisualizeAreaUnit.Percentage:
					size.Height = rect.Height * _height / 100.0f;
					break;
				case VisualizeAreaUnit.Pixel:
					size.Height = _height;
					break;
			}

			return size;
		}
	}
}
