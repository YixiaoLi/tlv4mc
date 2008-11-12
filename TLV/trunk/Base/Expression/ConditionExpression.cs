using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Text;

namespace NU.OJL.MPRTOS.TLV.Base
{
	public static class ConditionExpression
	{
		private static Dictionary<string, bool> cache = new Dictionary<string, bool>();

		public static bool Result(string expression)
		{
			expression = expression.Replace(" ", "").Replace("\t", "");
			bool result;
			try
			{
				if (cache.ContainsKey(expression))
					return cache[expression];

				Match m;
				string e = expression;

				do
				{
					m = Regex.Match(e, @"(?<comparisonExpression>\((?<left>[^=!<>\(\)&\|]+)(?<ope>(==|!=|<=|>=|>|<))(?<right>[^=!<>\(\)&\|]+)\))");
					if (m.Success)
						e = Regex.Replace(e, Regex.Escape(m.Groups["comparisonExpression"].Value), ComparisonExpression.Result<string>(m.Groups["left"].Value, m.Groups["ope"].Value, m.Groups["right"].Value).ToString());
				} while (m.Success);

				if (cache.ContainsKey(e))
				{
					cache.Add(expression, cache[e]);
					return cache[e];
				}

				do
				{
					m = Regex.Match(e, @"(?<comparisonExpression>\((?<left>!?[^=!<>\(\)&\|]+)(?<ope>(&&|\|\|))(?<right>!?[^=!<>\(\)&\|]+)\))");
					if (m.Success)
						e = Regex.Replace(e, Regex.Escape(m.Groups["comparisonExpression"].Value), calc(m.Groups["left"].Value, m.Groups["ope"].Value, m.Groups["right"].Value).ToString());
				} while (m.Success);

				if (cache.ContainsKey(e))
				{
					cache.Add(expression, cache[e]);
					return cache[e];
				}

				do
				{
					m = Regex.Match(e, @"(?<comparisonExpression>(?<left>[^=!<>\(\)&\|]+)(?<ope>(==|!=|<=|>=|>|<))(?<right>[^=!<>\(\)&\|]+))");
					if (m.Success)
						e = Regex.Replace(e, Regex.Escape(m.Groups["comparisonExpression"].Value), ComparisonExpression.Result<string>(m.Groups["left"].Value, m.Groups["ope"].Value, m.Groups["right"].Value).ToString());
				} while (m.Success);

				if (cache.ContainsKey(e))
				{
					cache.Add(expression, cache[e]);
					return cache[e];
				}

				do
				{
					m = Regex.Match(e, @"(?<comparisonExpression>(?<left>!?[^=!<>\(\)&\|]+)(?<ope>&&)(?<right>!?[^=!<>\(\)&\|]+))");
					if (m.Success)
						e = Regex.Replace(e, Regex.Escape(m.Groups["comparisonExpression"].Value), calc(m.Groups["left"].Value, m.Groups["ope"].Value, m.Groups["right"].Value).ToString());
				} while (m.Success);

				if (cache.ContainsKey(e))
				{
					cache.Add(expression, cache[e]);
					return cache[e];
				}

				do
				{
					m = Regex.Match(e, @"(?<comparisonExpression>(?<left>!?[^=!<>\(\)&\|]+)(?<ope>\|\|)(?<right>!?[^=!<>\(\)&\|]+))");
					if (m.Success)
						e = Regex.Replace(e, Regex.Escape(m.Groups["comparisonExpression"].Value), calc(m.Groups["left"].Value, m.Groups["ope"].Value, m.Groups["right"].Value).ToString());
				} while (m.Success);

				result = parse(e);

				cache.Add(expression, result);
			}
			catch
			{
				throw new Exception("条件式の記述が異常です。\n" + "\"" + expression + "\"");
			}

			return result;
		}

		private static bool calc(string expression)
		{
			Match m = Regex.Match(expression, @"(?<left>[^&\|\s]+)\s*(?<ope>(&&|\|\|))\s*(?<right>[^\s]+)");
			string left = m.Groups["left"].Value;
			string ope = m.Groups["ope"].Value;
			string right = m.Groups["right"].Value;

			return calc(left, ope, right);
		}
		private static bool calc(string left, string ope, string right)
		{
			switch (ope)
			{
				case "&&":
					return parse(left) && parse(right);
				case "||":
					return parse(left) || parse(right);
				default:
					return false;
			}
		}
		private static bool parse(string value)
		{
			bool not = Regex.IsMatch(value, @"\s*!\s*(false|true)\s*", RegexOptions.IgnoreCase);
			bool result = bool.Parse(value);
			return not ? !result : result;
		}
	}
}
