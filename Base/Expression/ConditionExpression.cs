/*
 *  TLV - Trace Log Visualizer
 *
 *  Copyright (C) 2008,2009 by Embedded and Real-Time Systems Laboratory
 *              Graduate School of Information Science, Nagoya Univ., JAPAN
 *
 *  �嵭����Ԥϡ��ʲ���(1)��(4)�ξ������������˸¤ꡤ�ܥ��եȥ���
 *  �����ܥ��եȥ���������Ѥ�����Τ�ޤࡥ�ʲ�Ʊ���ˤ���ѡ�ʣ������
 *  �ѡ������ۡʰʲ������ѤȸƤ֡ˤ��뤳�Ȥ�̵���ǵ������롥
 *  (1) �ܥ��եȥ������򥽡��������ɤη������Ѥ�����ˤϡ��嵭������
 *      ��ɽ�����������Ѿ�浪��Ӳ�����̵�ݾڵ��꤬�����Τޤޤη��ǥ���
 *      ����������˴ޤޤ�Ƥ��뤳�ȡ�
 *  (2) �ܥ��եȥ������򡤥饤�֥������ʤɡ�¾�Υ��եȥ�������ȯ�˻�
 *      �ѤǤ�����Ǻ����ۤ�����ˤϡ������ۤ�ȼ���ɥ�����ȡ�����
 *      �ԥޥ˥奢��ʤɡˤˡ��嵭�����ɽ�����������Ѿ�浪��Ӳ���
 *      ��̵�ݾڵ����Ǻܤ��뤳�ȡ�
 *  (3) �ܥ��եȥ������򡤵�����Ȥ߹���ʤɡ�¾�Υ��եȥ�������ȯ�˻�
 *      �ѤǤ��ʤ����Ǻ����ۤ�����ˤϡ����Τ����줫�ξ�����������
 *      �ȡ�
 *    (a) �����ۤ�ȼ���ɥ�����ȡ����Ѽԥޥ˥奢��ʤɡˤˡ��嵭����
 *        �ɽ�����������Ѿ�浪��Ӳ�����̵�ݾڵ����Ǻܤ��뤳�ȡ�
 *    (b) �����ۤη��֤��̤�������ˡ�ˤ�äơ�TOPPERS�ץ������Ȥ�
 *        ��𤹤뤳�ȡ�
 *  (4) �ܥ��եȥ����������Ѥˤ��ľ��Ū�ޤ��ϴ���Ū�������뤤���ʤ�»
 *      ������⡤�嵭����Ԥ����TOPPERS�ץ������Ȥ����դ��뤳�ȡ�
 *      �ޤ����ܥ��եȥ������Υ桼���ޤ��ϥ���ɥ桼������Τ����ʤ���
 *      ͳ�˴�Ť����ᤫ��⡤�嵭����Ԥ����TOPPERS�ץ������Ȥ�
 *      ���դ��뤳�ȡ�
 *
 *  �ܥ��եȥ������ϡ�̵�ݾڤ��󶡤���Ƥ����ΤǤ��롥�嵭����Ԥ�
 *  ���TOPPERS�ץ������Ȥϡ��ܥ��եȥ������˴ؤ��ơ�����λ�����Ū
 *  ���Ф���Ŭ������ޤ�ơ������ʤ��ݾڤ�Ԥ�ʤ����ޤ����ܥ��եȥ���
 *  �������Ѥˤ��ľ��Ū�ޤ��ϴ���Ū�������������ʤ�»���˴ؤ��Ƥ⡤��
 *  ����Ǥ�����ʤ���
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
				throw new Exception("��Ｐ�ε��Ҥ��۾�Ǥ���\n" + "\"" + expression + "\"");
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
