using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace NU.OJL.MPRTOS.TLV.Base
{
	public static class StringExtension
	{
		public static decimal ToDecimal(this string value, int radix)
		{
			if (radix <= 1 || radix > 36)
				throw new ArgumentException("radixは2以上36以下でなければなりません。");


			char maxChar = (char)('a' + radix - 11);

			string fc = radix > 10 ? "a-" : string.Empty;

			if (Regex.IsMatch(value, @"[^\-0-9" + fc + maxChar.ToString() + fc.ToString() + maxChar.ToString().ToUpper() + @"\.]"))
				throw new ArgumentException("valueは0-9"+ fc + maxChar.ToString() + "までの数値でなければなりません。");


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

			return result;
		}
	}
}
