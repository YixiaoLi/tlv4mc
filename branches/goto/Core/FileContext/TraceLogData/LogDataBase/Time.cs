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
		private string _valueByString;
		public int Radix { get; set; }
		public long Value { get; set; }

		public static Time MaxTime = new Time(long.MaxValue.ToString(), 10);
		public static Time MinTime = new Time("0", 10);
		public static Time NaN = new Time(null, 0);

		public Time(string value, int radix)
			:this()
		{
			_valueByString = value;
			Radix = radix;
			if (value != null)
				Value = _valueByString.ToLong(radix);
			else
				Value = 0;
		}

		public static Time operator +(decimal l, Time r)
		{
			return new Time(Convert.ToString((long)(l + (decimal)r.Value), r.Radix), r.Radix);
		}
		public static Time operator -(decimal l, Time r)
		{
			return new Time(Convert.ToString((long)(l - (decimal)r.Value), r.Radix), r.Radix);
		}
		public static Time operator +(Time l, decimal r)
		{
			return new Time(Convert.ToString((long)((decimal)l.Value + r), l.Radix), l.Radix);
		}
		public static Time operator -(Time l, decimal r)
		{
			return new Time(Convert.ToString((long)((decimal)l.Value - r), l.Radix), l.Radix);
		}
		public static Time operator *(decimal l, Time r)
		{
			return new Time(Convert.ToString((long)(l * (decimal)r.Value), r.Radix), r.Radix);
		}
		public static Time operator /(decimal l, Time r)
		{
			return new Time(Convert.ToString((long)(l / (decimal)r.Value), r.Radix), r.Radix);
		}
		public static Time operator *(Time l, decimal r)
		{
			return new Time(Convert.ToString((long)((decimal)l.Value * r), l.Radix), l.Radix);
		}
		public static Time operator /(Time l, decimal r)
		{
			return new Time(Convert.ToString((long)((decimal)l.Value / r), l.Radix), l.Radix);
		}

		public static Time operator +(Time l, Time r)
		{
			return new Time(Convert.ToString(l.Value + r.Value, l.Radix), l.Radix);
		}
		public static Time operator -(Time l, Time r)
		{
			return new Time(Convert.ToString(l.Value - r.Value, l.Radix), l.Radix);
		}
		public static Time operator *(Time l, Time r)
		{
			return new Time(Convert.ToString((long)((decimal)l.Value * (decimal)r.Value), l.Radix), l.Radix);
		}
		public static Time operator /(Time l, Time r)
		{
			return new Time(Convert.ToString((long)((decimal)l.Value / (decimal)r.Value), l.Radix), l.Radix);
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

		public static Time operator ++(Time t)
		{
			return new Time(Convert.ToString(t.Value + 1, t.Radix), t.Radix);
		}
		public static Time operator --(Time t)
		{
			return new Time(Convert.ToString(t.Value - 1, t.Radix), t.Radix);
		}

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
			return _valueByString;
		}

		public float ToX(Time from, Time to, int width)
		{
			float result = (float)(((decimal)(Value - from.Value) / (decimal)(to.Value - from.Value)) * (decimal)width);
			result = result < 0 ? -10f : (result > width ? width + 10f : result);

			return result;
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
	}
}
