using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Globalization;

namespace NU.OJL.MPRTOS.TLV.Base
{
	public static class StringExtension
	{
		public static long ToLong(this string value, int radix)
		{
			if (value == long.MaxValue.ToString())
				return long.MaxValue;

			if (radix == 16)
				return long.Parse(value, NumberStyles.AllowHexSpecifier);
			else if (radix == 10)
				return long.Parse(value, NumberStyles.Any);
			else
				throw new ArgumentException("基数は10か16しか扱えません。");
		}
	}
}
