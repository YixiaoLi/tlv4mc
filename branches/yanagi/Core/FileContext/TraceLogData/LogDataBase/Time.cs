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

		public Time(string value, int radix)
		{
			Radix = radix;
			Value = Convert.ToInt64(value, Radix);
		}

		public static implicit operator long(Time time)
		{
			return time.Value;
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

	}
}
