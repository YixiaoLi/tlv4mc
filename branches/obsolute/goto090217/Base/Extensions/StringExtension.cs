using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace NU.OJL.MPRTOS.TLV.Base
{
	public static class StringExtension
	{
		private static Dictionary<string, bool> _isValidCache = new Dictionary<string, bool>();
		private static Dictionary<string, decimal> _toDecimalCache = new Dictionary<string, decimal>();

		public static bool IsValid(this string value, int radix)
		{
			string k = value + "," + radix.ToString();

			if (_isValidCache.ContainsKey(k))
				return _isValidCache[k];

			bool result = true;

			if (radix <= 1 || radix > 36)
				result = false;

			if (radix > 10)
			{
				char maxChar = (char)('a' + radix - 11);
				string fc = "a-" + maxChar.ToString() + "A-" + maxChar.ToString().ToUpper();
				if (!Regex.IsMatch(value, @"^\-?(([1-9" + fc + @"][0-9" + fc + @"]*)|0)(\.[0-9" + fc + @"]+)?$"))
					result =  false;
			}
			else
			{
				string nc = (radix - 1).ToString();
				if (!Regex.IsMatch(value, @"^\-?(([1-" + nc + @"][0-" + nc + @"]*)|0)(\.[0-" + nc + @"]+)?$"))
					result =  false;
			}

			lock (_isValidCache)
			{
				if (!_isValidCache.ContainsKey(k))
					_isValidCache.Add(k, result);
			}

			return result;
		}

		public static decimal ToDecimal(this string value, int radix)
		{
			string k = value + "," + radix.ToString();

			if (_toDecimalCache.ContainsKey(k))
				return _toDecimalCache[k];

			if (radix <= 1 || radix > 36)
				throw new ArgumentException("radixは2以上36以下でなければなりません。");

			if (value == null || value == string.Empty || !IsValid(value, radix))
				throw new ArgumentException("入力値が異常です。\n基数:" + radix + "\n値:" + value);

			string i;
			string d;
			decimal result = 0m;
			bool minus = false;

			if (value.ToCharArray()[0] == '-')
			{
				minus = true;
				value = value.Remove(0, 1);
			}

			if (value.Contains('.'))
			{
				i = value.Split('.')[0];
				d = value.Split('.')[1];
			}
			else
			{
				i = value;
				d = "0";
			}

			char[] ic = i.ToCharArray();
			char[] id = d.ToCharArray();


			for (int j = ic.Length - 1; j >= 0; j--)
			{
				result += ic[j].ToDecimal() * (decimal)Math.Pow(radix, ic.Length - 1 - j);
			}
			for (int j = 0; j < id.Length; j++)
			{
				result += id[j].ToDecimal() * (decimal)Math.Pow(radix, (j+1) * -1);
			}

			if (minus)
				result *= -1m;

			lock (_toDecimalCache)
			{
				if (!_toDecimalCache.ContainsKey(k))
					_toDecimalCache.Add(k, result);
			}

			return result;
		}
	}
}
