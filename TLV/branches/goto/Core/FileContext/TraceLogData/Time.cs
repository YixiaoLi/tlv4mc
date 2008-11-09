using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NU.OJL.MPRTOS.TLV.Core
{
	public class Time : IComparable<Time>
	{
		private long _value;
		public int Radix { get; private set; }

		public Time(string value, int radix)
		{
			Radix = radix;
			_value = Convert.ToInt64(value, radix);
		}

		public static implicit operator long(Time time)
		{
			return time._value;
		}

		public static implicit operator string(Time time)
		{
			return Convert.ToString(time._value, time.Radix);
		}

		public int CompareTo(Time other)
		{
			if ((long)this == (long)other)
				return 0;
			else if ((long)this > (long)other)
				return 1;
			else
				return -1;
		}
	}
}
