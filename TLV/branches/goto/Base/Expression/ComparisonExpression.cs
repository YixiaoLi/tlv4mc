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
			if (cache.ContainsKey(typeof(T).ToString() + left + ope + right))
				return cache[typeof(T).ToString() + left + ope + right];

			bool result = compare<T>(left, ope, right);

			cache.Add(typeof(T).ToString() + left + ope + right, result);

			return result;
		}

		public static bool Result(string left, string ope, string right, string type)
		{
			if (cache.ContainsKey(type + left + ope + right))
				return cache[type + left + ope + right];

			bool result;

			switch (type)
			{
				case "string":
					result = compare<string>(left, ope, right);
					break;
				case "char":
					result = compare<char>(left, ope, right);
					break;
				case "bool":
					result = compare<bool>(left, ope, right);
					break;
				case "sbyte":
					result = compare<sbyte>(left, ope, right);
					break;
				case "byte":
					result = compare<byte>(left, ope, right);
					break;
				case "short":
					result = compare<short>(left, ope, right);
					break;
				case "ushort":
					result = compare<ushort>(left, ope, right);
					break;
				case "int":
					result = compare<int>(left, ope, right);
					break;
				case "uint":
					result = compare<uint>(left, ope, right);
					break;
				case "long":
					result = compare<long>(left, ope, right);
					break;
				case "ulong":
					result = compare<ulong>(left, ope, right);
					break;
				case "decimal":
					result = compare<decimal>(left, ope, right);
					break;
				case "double":
					result = compare<double>(left, ope, right);
					break;
				case "float":
					result = compare<float>(left, ope, right);
					break;
				case "DateTime":
					result = compare<DateTime>(left, ope, right);
					break;
				default:
					result = false;
					break;
			}
			cache.Add(type + left + ope + right, result);
			return result;
		}

		public static bool Result<T>(string condition) where T : IComparable, IConvertible
		{
			if (cache.ContainsKey(condition))
				return cache[condition];

			Match m = Regex.Match(condition, @"(?<left>[^=!<>\s]+)\s*(?<ope>(==|!=|<=|>=|>|<))\s*(?<right>[^=!<>\s]+)");
			string left = m.Groups["left"].Value;
			string ope = m.Groups["ope"].Value;
			string right = m.Groups["right"].Value;
			bool result = compare<T>(left, ope, right);

			cache.Add(condition, result);

			return result;
		}

		public static bool Result(string condition, string type)
		{
			if (cache.ContainsKey(type + condition))
				return cache[type + condition];

			Match m = Regex.Match(condition, @"(?<left>[^=!<>\s]+)\s*(?<ope>(==|!=|<=|>=|>|<))\s*(?<right>[^=!<>\s]+)");
			string left = m.Groups["left"].Value;
			string ope = m.Groups["ope"].Value;
			string right = m.Groups["right"].Value;

			bool result = Result(left, ope, right, type);

			cache.Add(type + condition, result);

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
