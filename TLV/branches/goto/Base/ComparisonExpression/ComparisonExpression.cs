using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Text;

namespace NU.OJL.MPRTOS.TLV.Base
{
	public class ComparisonExpression
	{
		public string Left { get; set; }
		public string Ope { get; set; }
		public string Right { get; set; }

		public ComparisonExpression(string condition)
		{
			Match m = Regex.Match(condition, @"(?<left>\w+)\s*(?<ope>(==|!=|<=|>=|>|<))\s*(?<right>\w+)");
			Left = m.Groups["left"].Value;
			Ope = m.Groups["ope"].Value;
			Right = m.Groups["right"].Value;
		}

		public ComparisonExpression(string left, string ope, string right)
		{
			Left = left;
			Ope = ope;
			Right = right;
		}

		public bool Result(string type)
		{
			switch (type)
			{
				case "string":
					return compare<string>(Left, Ope, Right);
				case "char":
					return compare<char>(Left, Ope, Right);
				case "bool":
					return compare<bool>(Left, Ope, Right);
				case "sbyte":
					return compare<sbyte>(Left, Ope, Right);
				case "byte":
					return compare<byte>(Left, Ope, Right);
				case "short":
					return compare<short>(Left, Ope, Right);
				case "ushort":
					return compare<ushort>(Left, Ope, Right);
				case "int":
					return compare<int>(Left, Ope, Right);
				case "uint":
					return compare<uint>(Left, Ope, Right);
				case "long":
					return compare<long>(Left, Ope, Right);
				case "ulong":
					return compare<ulong>(Left, Ope, Right);
				case "decimal":
					return compare<decimal>(Left, Ope, Right);
				case "double":
					return compare<double>(Left, Ope, Right);
				case "float":
					return compare<float>(Left, Ope, Right);
				case "DateTime":
					return compare<DateTime>(Left, Ope, Right);
				default:
					return false;
			}
		}

		private bool compare<T>(string left, string ope, string right) where T : IComparable, IConvertible
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
