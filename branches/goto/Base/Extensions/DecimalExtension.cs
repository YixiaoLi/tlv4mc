using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NU.OJL.MPRTOS.TLV.Base
{
	public static class DecimalExtension
	{
		private static Dictionary<string, string> _cache = new Dictionary<string, string>();

		public static string ToString(this decimal value, int radix)
		{
			string k = value.ToString() + "," + radix.ToString();

			if (_cache.ContainsKey(k))
				return _cache[k];

			if (radix <= 1 || radix > 36)
				throw new ArgumentException("radixは2以上36以下でなければなりません。");

			bool minus = false;

			if (value < 0)
			{
				minus = true;
				value *= -1m;
			}

			StringBuilder result = new StringBuilder();
			decimal i = Math.Truncate(value);
			decimal d = value - i;
			decimal r = 0m;
			decimal t = i;

			if (i != 0m)
			{
				for (; t > 0; )
				{
					r = t % (decimal)radix;
					result.Insert(0, r >= 10m ? ((char)('a' + r - 10 )).ToString() : r.ToString());
					t = (t - r) / (decimal)radix;
				}
			}
			else
			{
				result.Append("0");
			}
			if(d != 0m)
			{
				result.Append(".");

				t = d;

				for (int j = 0 ; j < 10 && t != 0m; j++ )
				{
					r = Math.Truncate(t * (decimal)radix);
					result.Append(r >= 10m ? ((char)('a' + r - 10)).ToString() : r.ToString());
					t = t * (decimal)radix - r;
				}
			}

			if (minus)
				result.Insert(0, "-");

			lock (_cache)
			{
				if (!_cache.ContainsKey(k))
					_cache.Add(k, result.ToString());
			}

			return result.ToString();
		}
	}
}
