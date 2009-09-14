/*
 *  TLV - Trace Log Visualizer
 *
 *  Copyright (C) 2008,2009 by Nagoya Univ., JAPAN
 *
 *  上記著作権者は，以下の(1)〜(4)の条件を満たす場合に限り，本ソフトウェ
 *  ア（本ソフトウェアを改変したものを含む．以下同じ）を使用・複製・改
 *  変・再配布（以下，利用と呼ぶ）することを無償で許諾する．
 *  (1) 本ソフトウェアをソースコードの形で利用する場合には，上記の著作
 *      権表示，この利用条件および下記の無保証規定が，そのままの形でソー
 *      スコード中に含まれていること．
 *  (2) 本ソフトウェアを，ライブラリ形式など，他のソフトウェア開発に使
 *      用できる形で再配布する場合には，再配布に伴うドキュメント（利用
 *      者マニュアルなど）に，上記の著作権表示，この利用条件および下記
 *      の無保証規定を掲載すること．
 *  (3) 本ソフトウェアを，機器に組み込むなど，他のソフトウェア開発に使
 *      用できない形で再配布する場合には，次のいずれかの条件を満たすこ
 *      と．
 *    (a) 再配布に伴うドキュメント（利用者マニュアルなど）に，上記の著
 *        作権表示，この利用条件および下記の無保証規定を掲載すること．
 *    (b) 再配布の形態を，別に定める方法によって，TOPPERSプロジェクトに
 *        報告すること．
 *  (4) 本ソフトウェアの利用により直接的または間接的に生じるいかなる損
 *      害からも，上記著作権者およびTOPPERSプロジェクトを免責すること．
 *      また，本ソフトウェアのユーザまたはエンドユーザからのいかなる理
 *      由に基づく請求からも，上記著作権者およびTOPPERSプロジェクトを
 *      免責すること．
 *
 *  本ソフトウェアは，無保証で提供されているものである．上記著作権者お
 *  よびTOPPERSプロジェクトは，本ソフトウェアに関して，特定の使用目的
 *  に対する適合性も含めて，いかなる保証も行わない．また，本ソフトウェ
 *  アの利用により直接的または間接的に生じたいかなる損害に関しても，そ
 *  の責任を負わない．
 *
 *  @(#) $Id$
 */
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
