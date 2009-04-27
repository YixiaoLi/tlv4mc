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
