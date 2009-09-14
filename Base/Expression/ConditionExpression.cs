
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Text;

namespace NU.OJL.MPRTOS.TLV.Base
{
	public static class ConditionExpression
	{
		private static Dictionary<string, bool> _cache = new Dictionary<string, bool>();

		public static bool Result(string expression)
		{
			expression = Regex.Replace(expression, @"\s", "");
			bool result;

			try
			{
				if (_cache.ContainsKey(expression))
					return _cache[expression];

				Match m;
				string e = expression;

				do
				{
					m = Regex.Match(e, @"(?<ini>[=!<>\(\)&\|]+)?(?<comparisonExpression>\((?<left>[^=!<>\(\)&\|]+)(?<ope>(==|!=|<=|>=|>|<))(?<right>[^=!<>\(\)&\|]+)\))(?<fin>[=!<>\(\)&\|]+)?");
					if (m.Success)
						e = Regex.Replace(e, Regex.Escape(m.Groups["ini"].Value + m.Groups["comparisonExpression"].Value + m.Groups["fin"].Value), m.Groups["ini"].Value + ComparisonExpression.Result<string>(m.Groups["left"].Value, m.Groups["ope"].Value, m.Groups["right"].Value).ToString() + m.Groups["fin"].Value);
				} while (m.Success);

				if (_cache.ContainsKey(e))
				{
					if (!_cache.ContainsKey(expression))
					{
						lock (_cache)
						{
							if (!_cache.ContainsKey(expression))
								_cache.Add(expression, _cache[e]);
						}
					}
					return _cache[e];
				}

				do
				{
					m = Regex.Match(e, @"(?<ini>[=!<>\(\)&\|]+)?(?<comparisonExpression>\((?<left>!?[^=!<>\(\)&\|]+)(?<ope>(&&|\|\|))(?<right>!?[^=!<>\(\)&\|]+)\))(?<fin>[=!<>\(\)&\|]+)?");
					if (m.Success)
						e = Regex.Replace(e, Regex.Escape(m.Groups["ini"].Value + m.Groups["comparisonExpression"].Value + m.Groups["fin"].Value), m.Groups["ini"].Value + calc(m.Groups["left"].Value, m.Groups["ope"].Value, m.Groups["right"].Value).ToString() + m.Groups["fin"].Value);
				} while (m.Success);

				if (_cache.ContainsKey(e))
				{
					if (!_cache.ContainsKey(expression))
					{
						lock (_cache)
						{
							if (!_cache.ContainsKey(expression))
								_cache.Add(expression, _cache[e]);
						}
					}
					return _cache[e];
				}

				do
				{
					m = Regex.Match(e, @"(?<ini>[=!<>\(\)&\|]+)?(?<comparisonExpression>(?<left>[^=!<>\(\)&\|]+)(?<ope>(==|!=|<=|>=|>|<))(?<right>[^=!<>\(\)&\|]+))(?<fin>[=!<>\(\)&\|]+)?");
					if (m.Success)
						e = Regex.Replace(e, Regex.Escape(m.Groups["ini"].Value + m.Groups["comparisonExpression"].Value + m.Groups["fin"].Value), m.Groups["ini"].Value + ComparisonExpression.Result<string>(m.Groups["left"].Value, m.Groups["ope"].Value, m.Groups["right"].Value).ToString() + m.Groups["fin"].Value);
				} while (m.Success);

				if (_cache.ContainsKey(e))
				{
					if (!_cache.ContainsKey(expression))
					{
						lock (_cache)
						{
							if (!_cache.ContainsKey(expression))
								_cache.Add(expression, _cache[e]);
						}
					}
					return _cache[e];
				}

				do
				{
					m = Regex.Match(e, @"(?<ini>[=!<>\(\)&\|]+)?(?<comparisonExpression>(?<left>!?[^=!<>\(\)&\|]+)(?<ope>&&)(?<right>!?[^=!<>\(\)&\|]+))(?<fin>[=!<>\(\)&\|]+)?");
					if (m.Success)
						e = Regex.Replace(e, Regex.Escape(m.Groups["ini"].Value + m.Groups["comparisonExpression"].Value + m.Groups["fin"].Value), m.Groups["ini"].Value + calc(m.Groups["left"].Value, m.Groups["ope"].Value, m.Groups["right"].Value).ToString() + m.Groups["fin"].Value);
				} while (m.Success);

				if (_cache.ContainsKey(e))
				{
					if (!_cache.ContainsKey(expression))
					{
						lock (_cache)
						{
							if (!_cache.ContainsKey(expression))
								_cache.Add(expression, _cache[e]);
						}
					}
					return _cache[e];
				}

				do
				{
					m = Regex.Match(e, @"(?<ini>[=!<>\(\)&\|]+)?(?<comparisonExpression>(?<left>!?[^=!<>\(\)&\|]+)(?<ope>\|\|)(?<right>!?[^=!<>\(\)&\|]+))(?<fin>[=!<>\(\)&\|]+)?");
					if (m.Success)
						e = Regex.Replace(e, Regex.Escape(m.Groups["ini"].Value + m.Groups["comparisonExpression"].Value + m.Groups["fin"].Value), m.Groups["ini"].Value + calc(m.Groups["left"].Value, m.Groups["ope"].Value, m.Groups["right"].Value).ToString() + m.Groups["fin"].Value);
				} while (m.Success);

				result = parse(e);

				if (!_cache.ContainsKey(expression))
				{
					lock (_cache)
					{
						if (!_cache.ContainsKey(expression))
							_cache.Add(expression, result);
					}
				}
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
