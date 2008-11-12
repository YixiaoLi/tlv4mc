using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Text;
using System.Reflection;

namespace NU.OJL.MPRTOS.TLV.Base
{
	public static class ComparisonExpression
	{
		private static Dictionary<string, bool> cache = new Dictionary<string, bool>();

		public static bool Result<T>(string left, string ope, string right) where T : IComparable, IConvertible
		{
			if (cache.ContainsKey(left + ope + right))
				return cache[left + ope + right];

			bool result = compare<T>(left, ope, right);

			cache.Add(left + ope + right, result);

			return result;
		}

		public static bool Result<T>(string condition) where T : IComparable, IConvertible
		{
			condition = condition.Replace(" ", "").Replace("\t", "");

			if (cache.ContainsKey(condition))
				return cache[condition];

			Match m = Regex.Match(condition, @"(?<left>[^=!<>&\|\s]+)\s*(?<ope>(==|!=|<=|>=|>|<))\s*(?<right>[^=!<>&\|\s]+)\s");
			string left = m.Groups["left"].Value;
			string ope = m.Groups["ope"].Value;
			string right = m.Groups["right"].Value;
			bool result = compare<T>(left, ope, right);

			cache.Add(condition, result);

			return result;
		}

		private static bool compare<T>(string left, string ope, string right) where T : IComparable, IConvertible
		{
			int result = 0;
			switch (ope)
			{
				case "==":
					result = ((T)Convert.ChangeType(left, typeof(T))).CompareTo((T)Convert.ChangeType(right, typeof(T)));
					return result == 0;
				case "!=":
					result = ((T)Convert.ChangeType(left, typeof(T))).CompareTo((T)Convert.ChangeType(right, typeof(T)));
					return result != 0;
				case "<=":
					result = ((T)Convert.ChangeType(left, typeof(T))).CompareTo((T)Convert.ChangeType(right, typeof(T)));
					return result == 0 || result < 0;
				case ">=":
					result = ((T)Convert.ChangeType(left, typeof(T))).CompareTo((T)Convert.ChangeType(right, typeof(T)));
					return result == 0 || result > 0;
				case "<":
					result = ((T)Convert.ChangeType(left, typeof(T))).CompareTo((T)Convert.ChangeType(right, typeof(T)));
					return result < 0;
				case ">":
					result = ((T)Convert.ChangeType(left, typeof(T))).CompareTo((T)Convert.ChangeType(right, typeof(T)));
					return result > 0;
				default:
					return false;
			}
		}
	}
}
