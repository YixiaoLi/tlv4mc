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
				throw new Exception("���������꤬�۾�Ǥ���\n" + size);
			if (!Regex.IsMatch(h, @"^" + num + @"(%|px)?$"))
				throw new Exception("���������꤬�۾�Ǥ���\n" + size);

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
