/*
 *  TLV - Trace Log Visualizer
 *
 *  Copyright (C) 2008-2013 by Nagoya Univ., JAPAN
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
            if (Radix != 10)
            {
                return Value.ToString(Radix);
            }
            else 
            {
                return Value.ToString(Radix);
//        return string.Format("{0:#,#}", Value);
            }
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
				throw new ArgumentException(x.GetType().ToString() + "型と" + y.GetType().ToString() + "型を比較できません。");

			return Compare((Time)x, (Time)y);
		}

		public int Compare(Time x, Time y)
		{
			return x.CompareTo(y);
		}

		public int CompareTo(object obj)
		{
			if (! (obj is Time))
				throw new ArgumentException("Time型とobject型を比較できません。");

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
