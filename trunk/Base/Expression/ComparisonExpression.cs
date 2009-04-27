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
