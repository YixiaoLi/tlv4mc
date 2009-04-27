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

namespace NU.OJL.MPRTOS.TLV.Base
{
	public class RotateColorFactory
	{
		private uint _i = 0;
		public int Hue { get; set; }
		public int Saturation { get; set; }
		public int Value { get; set; }
		public Random Random { get; set; }
		public int RotateRange { get; set; }
		public int IncrementRange { get; set; }

		public RotateColorFactory()
		{
			Random = new Random(DateTime.Now.Millisecond);
			HueRandomSet();
			Saturation = 100;
			Value = 100;
			RotateRange = 120;
			IncrementRange = RotateRange / 2;
		}

		public Color RamdomColor(int hueFrom, int hueTo, int valueFrom, int valueTo, int saturationFrom, int saturationTo)
		{
			HueRandomSet(hueFrom, hueTo);
			ValueRandomSet(valueFrom, valueTo);
			SaturationRandomSet(saturationFrom, saturationTo);
			return new Color().FromHsv(Hue, Saturation, Value);
		}

		public Color RamdomColor()
		{
			return RamdomColor(0,360,80,100,80,100);
		}

		public Color RotateColor()
		{
			Color Result = new Color().FromHsv(Hue, Saturation, Value);

			if ((_i % (360 / RotateRange)) == 0)
			{
				Hue += IncrementRange;
				IncrementRange /= 2;
				if (IncrementRange == 1)
				{
					IncrementRange = RotateRange / 2;
					HueRandomSet();
				}
			}

			Hue += RotateRange;
			if (Hue > 360)
				Hue %= 360;

			_i++;
			if (_i == uint.MaxValue)
				_i = 0;

			return Result;
		}

		public void Reset()
		{
			_i = 0;
			Hue = 0;
			Saturation = 100;
			Value = 100;
			IncrementRange = RotateRange / 2;
		}

		public void HueRandomSet()
		{
			HueRandomSet(0, 360);
		}

		public void SaturationRandomSet()
		{
			SaturationRandomSet(0, 100);
		}

		public void ValueRandomSet()
		{
			ValueRandomSet(0, 100);
		}

		public void HueRandomSet(int from, int to)
		{
			int h = Hue;
			while(Math.Abs(h - Hue) < 30)
				h = Random.Next(from, to);
			Hue = h;
		}

		public void SaturationRandomSet(int from, int to)
		{
			Saturation = Random.Next(from, to);
		}

		public void ValueRandomSet(int from, int to)
		{
			Value = Random.Next(from, to);
		}
	}
}
