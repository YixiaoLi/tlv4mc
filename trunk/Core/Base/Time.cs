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
using System.Globalization;
using System.Collections;
using NU.OJL.MPRTOS.TLV.Base;

namespace NU.OJL.MPRTOS.TLV.Core
{
	public struct Time : IComparable<Time>, IComparable, IComparer<Time>, IComparer, IEquatable<Time>
	{
		public int Radix { get; set; }
		public decimal Value { get; set; }

		public static Time MaxTime(int radix) { return new Time(decimal.MaxValue.ToString(radix), radix); }
		public static Time MinTime(int radix) { return new Time(decimal.MinValue.ToString(radix), radix); }
		public static Time Empty = new Time(null, 0);

		public Time(string value, int radix)
			:this()
		{
			if (value != null && value != string.Empty && radix != 0)
			{
				try
				{
					Value = value.ToDecimal(radix);
					Radix = radix;
				}
				catch
				{
					this = Empty;
				}
			}
			else
			{
				Value = 0m;
				Radix = 0;
			}
		}

		public bool IsEmpty
		{
			get { return Radix == 0; }
		}

		public static Time operator +(decimal l, Time r)
		{
			return new Time((l + (decimal)r.Value).ToString(r.Radix), r.Radix);
		}
		public static Time operator -(decimal l, Time r)
		{
			return new Time((l - (decimal)r.Value).ToString(r.Radix), r.Radix);
		}
		public static Time operator +(Time l, decimal r)
		{
			return new Time(((decimal)l.Value + r).ToString(l.Radix), l.Radix);
		}
		public static Time operator -(Time l, decimal r)
		{
			return new Time(((decimal)l.Value - r).ToString(l.Radix), l.Radix);
		}
		public static Time operator *(decimal l, Time r)
		{
			return new Time((l * (decimal)r.Value).ToString(r.Radix), r.Radix);
		}
		public static Time operator /(decimal l, Time r)
		{
			return new Time((l / (decimal)r.Value).ToString(r.Radix), r.Radix);
		}
		public static Time operator *(Time l, decimal r)
		{
			return new Time(((decimal)l.Value * r).ToString(l.Radix), l.Radix);
		}
		public static Time operator /(Time l, decimal r)
		{
			return new Time(((decimal)l.Value / r).ToString(l.Radix), l.Radix);
		}

		public static Time operator +(Time l, Time r)
		{
			return new Time((l.Value + r.Value).ToString(l.Radix), l.Radix);
		}
		public static Time operator -(Time l, Time r)
		{
			return new Time((l.Value - r.Value).ToString(l.Radix), l.Radix);
		}
		public static Time operator *(Time l, Time r)
		{
			return new Time((l.Value * r.Value).ToString(l.Radix), l.Radix);
		}
		public static Time operator /(Time l, Time r)
		{
			return new Time((l.Value / r.Value).ToString(l.Radix), l.Radix);
		}

		public static bool operator >(Time l, Time r)
		{
			return l.CompareTo(r) == 1;
		}
		public static bool operator <(Time l, Time r)
		{
			return l.CompareTo(r) == -1;
		}
		public static bool operator >=(Time l, Time r)
		{
			int i = l.CompareTo(r);
			return i == 1 || i == 0;
		}
		public static bool operator <=(Time l, Time r)
		{
			int i = l.CompareTo(r);
			return i == -1 || i == 0;
		}

		public static bool operator ==(Time l, Time r)
		{
			return l.Value == r.Value;
		}
		public static bool operator !=(Time l, Time r)
		{
			return l.Value != r.Value;
		}

		//public static Time operator ++(Time t)
		//{
		//    return new Time((t.Value + 1m).ToString(t.Radix), t.Radix);
		//}
		//public static Time operator --(Time t)
		//{
		//    return new Time((t.Value - 1m).ToString(t.Radix), t.Radix);
		//}

		public override bool Equals(object obj)
		{
			return obj is Time && Equals((Time)obj);
		}

		public override int GetHashCode()
		{
			return Value.GetHashCode() ^ Radix.GetHashCode();
		}

		public override string ToString()
		{
			return Value.ToString(Radix);
		}

		public float ToX(Time from, Time to, int width)
		{
			if (Value < from.Value)
				return -1 * width;

			decimal f = (((decimal)(Value - from.Value) / (to.Value - from.Value)) * (decimal)width);

			return (f > decimal.MaxValue) ? (float)decimal.MaxValue : (float)f;
		}
		public static Time FromX(Time from, Time to, int width, int x)
		{
			return from + new Time(((to.Value - from.Value) * ((decimal)x / (decimal)width)).ToString(to.Radix), to.Radix);
		}

		public int Compare(object x, object y)
		{
			if (!((x is Time) && (y is Time)))
				throw new ArgumentException(x.GetType().ToString() + "����" + y.GetType().ToString() + "������ӤǤ��ޤ���");

			return Compare((Time)x, (Time)y);
		}

		public int Compare(Time x, Time y)
		{
			return x.CompareTo(y);
		}

		public int CompareTo(object obj)
		{
			if (! (obj is Time))
				throw new ArgumentException("Time����object������ӤǤ��ޤ���");

			return CompareTo((Time)obj);
		}

		public int CompareTo(Time other)
		{
			if (Value == other.Value)
				return 0;
			else if (Value > other.Value)
				return 1;
			else
				return -1;
		}

		public bool Equals(Time other)
		{
			return this == other;
		}

		public Time Truncate()
		{
			return new Time(((decimal)Math.Truncate(Value)).ToString(Radix), Radix);
		}

		public Time Round(int carry)
		{
			return new Time(Math.Round(Value, carry, MidpointRounding.ToEven).ToString(Radix), Radix);
		}
	}
}
