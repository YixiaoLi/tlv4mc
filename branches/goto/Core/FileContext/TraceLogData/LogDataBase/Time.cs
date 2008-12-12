using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NU.OJL.MPRTOS.TLV.Core
{
	public class Time : IComparable<Time>
	{
		public int Radix { get; private set; }
		public long Value { get; private set; }

		//public Time(long value, int radix)
		//{
		//    Value = value;
		//    Radix = radix;
		//}

		public Time(string value, int radix)
		{
			Radix = radix;
			try
			{
				Value = Convert.ToInt64(value, Radix);
			}
			catch
			{
				throw new Exception("時間指定が異常です。\n" + value + "\n基数:" + radix);
			}
		}

		public static implicit operator long(Time time)
		{
			return time.Value;
		}

		public static Time operator +(Time t, long l)
		{
			return new Time((t.Value + l).ToString(), t.Radix);
		}
		public static Time operator +(long l, Time t)
		{
			return new Time((l + t.Value).ToString(), t.Radix);
		}
		public static Time operator -(Time t, long l)
		{
			return new Time((t.Value - l).ToString(), t.Radix);
		}
		public static Time operator -(long l, Time t)
		{
			return new Time((l - t.Value).ToString(), t.Radix);
		}

		public static Time operator +(Time l, Time r)
		{
			return new Time((l.Value + r.Value).ToString(), l.Radix);
		}
		public static Time operator -(Time l, Time r)
		{
			return new Time((l.Value - r.Value).ToString(), l.Radix);
		}

		public static Time operator ++(Time t)
		{
			return new Time((t.Value++).ToString(), t.Radix);
		}
		public static Time operator --(Time t)
		{
			return new Time((t.Value--).ToString(), t.Radix);
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

		public override string ToString()
		{
			return Convert.ToString(Value, Radix);
		}

		public static implicit operator string(Time time)
		{
			return time.ToString();
		}

	}
}
