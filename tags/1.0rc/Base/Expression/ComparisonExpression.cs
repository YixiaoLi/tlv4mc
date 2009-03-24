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
		private static Dictionary<string, bool> _cache = new Dictionary<string, bool>();

		public static bool Result<T>(string left, string ope, string right) where T : IComparable, IConvertible
		{
			bool result;

			string k = typeof(T).ToString() + left + ope + right;

			if (_cache.ContainsKey(k))
				return _cache[k];

			result = compare<T>(left, ope, right);

			lock (_cache)
			{
				if (!_cache.ContainsKey(k))
					_cache.Add(k, result);
			}

			return result;
		}

		public static bool Result<T>(string condition) where T : IComparable, IConvertible
		{
			bool result;

			condition = Regex.Replace(condition, @"\s", "");

			string k = typeof(T).ToString() + condition;

			if (_cache.ContainsKey(k))
				return _cache[k];

			Match m = Regex.Match(condition, @"(?<left>[^=!<>&\|]+)(?<ope>(==|!=|<=|>=|>|<))(?<right>[^=!<>&\|]+)");
			string left = m.Groups["left"].Value;
			string ope = m.Groups["ope"].Value;
			string right = m.Groups["right"].Value;
			result = compare<T>(left, ope, right);
			lock (_cache)
			{
				if(!_cache.ContainsKey(k))
					_cache.Add(k, result);
			}
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
