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

		public static Time MaxTime(int radix) { return new Time(decimal.MaxValue.ToString(), radix);}
		public static Time MinTime(int radix) { return new Time(decimal.MinValue.ToString(), radix); }
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

		public static Time operator ++(Time t)
		{
			return new Time((t.Value + 1m).ToString(t.Radix), t.Radix);
		}
		public static Time operator --(Time t)
		{
			return new Time((t.Value - 1m).ToString(t.Radix), t.Radix);
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
			return Value.ToString(Radix);
		}

		public float ToX(Time from, Time to, int width)
		{
			if (Value < from.Value)
				return -1 * width;

			return (float)(((decimal)(Value - from.Value) / (to.Value - from.Value)) * (decimal)width);
		}
		public static Time FromX(Time from, Time to, int width, int x)
		{
			return new Time(((to.Value - from.Value) * ((decimal)x / (decimal)width)).ToString(to.Radix), to.Radix);
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
