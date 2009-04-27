/*
 *  TLV - Trace Log Visualizer
 *
 *  Copyright (C) 2008,2009 by Embedded and Real-Time Systems Laboratory
 *              Graduate School of Information Science, Nagoya Univ., JAPAN
 *
 *  �嵭����Ԥϡ��ʲ���(1)��(4)�ξ������������˸¤ꡤ�ܥ��եȥ���
 *  �����ܥ��եȥ���������Ѥ�����Τ�ޤࡥ�ʲ�Ʊ���ˤ���ѡ�ʣ������
 *  �ѡ������ۡʰʲ������ѤȸƤ֡ˤ��뤳�Ȥ�̵���ǵ������롥
 *  (1) �ܥ��եȥ������򥽡��������ɤη������Ѥ�����ˤϡ��嵭������
 *      ��ɽ�����������Ѿ�浪��Ӳ�����̵�ݾڵ��꤬�����Τޤޤη��ǥ���
 *      ����������˴ޤޤ�Ƥ��뤳�ȡ�
 *  (2) �ܥ��եȥ������򡤥饤�֥������ʤɡ�¾�Υ��եȥ�������ȯ�˻�
 *      �ѤǤ�����Ǻ����ۤ�����ˤϡ������ۤ�ȼ���ɥ�����ȡ�����
 *      �ԥޥ˥奢��ʤɡˤˡ��嵭�����ɽ�����������Ѿ�浪��Ӳ���
 *      ��̵�ݾڵ����Ǻܤ��뤳�ȡ�
 *  (3) �ܥ��եȥ������򡤵�����Ȥ߹���ʤɡ�¾�Υ��եȥ�������ȯ�˻�
 *      �ѤǤ��ʤ����Ǻ����ۤ�����ˤϡ����Τ����줫�ξ�����������
 *      �ȡ�
 *    (a) �����ۤ�ȼ���ɥ�����ȡ����Ѽԥޥ˥奢��ʤɡˤˡ��嵭����
 *        �ɽ�����������Ѿ�浪��Ӳ�����̵�ݾڵ����Ǻܤ��뤳�ȡ�
 *    (b) �����ۤη��֤��̤�������ˡ�ˤ�äơ�TOPPERS�ץ������Ȥ�
 *        ��𤹤뤳�ȡ�
 *  (4) �ܥ��եȥ����������Ѥˤ��ľ��Ū�ޤ��ϴ���Ū�������뤤���ʤ�»
 *      ������⡤�嵭����Ԥ����TOPPERS�ץ������Ȥ����դ��뤳�ȡ�
 *      �ޤ����ܥ��եȥ������Υ桼���ޤ��ϥ���ɥ桼������Τ����ʤ���
 *      ͳ�˴�Ť����ᤫ��⡤�嵭����Ԥ����TOPPERS�ץ������Ȥ�
 *      ���դ��뤳�ȡ�
 *
 *  �ܥ��եȥ������ϡ�̵�ݾڤ��󶡤���Ƥ����ΤǤ��롥�嵭����Ԥ�
 *  ���TOPPERS�ץ������Ȥϡ��ܥ��եȥ������˴ؤ��ơ�����λ�����Ū
 *  ���Ф���Ŭ������ޤ�ơ������ʤ��ݾڤ�Ԥ�ʤ����ޤ����ܥ��եȥ���
 *  �������Ѥˤ��ľ��Ū�ޤ��ϴ���Ū�������������ʤ�»���˴ؤ��Ƥ⡤��
 *  ����Ǥ�����ʤ���
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
