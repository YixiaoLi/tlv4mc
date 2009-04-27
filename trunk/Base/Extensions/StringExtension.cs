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
using System.Text;
using System.Text.RegularExpressions;

namespace NU.OJL.MPRTOS.TLV.Base
{
	public static class StringExtension
	{
		private static Dictionary<string, bool> _isValidCache = new Dictionary<string, bool>();
		private static Dictionary<string, decimal> _toDecimalCache = new Dictionary<string, decimal>();

		public static bool IsValid(this string value, int radix)
		{
			string k = value + "," + radix.ToString();

			if (_isValidCache.ContainsKey(k))
				return _isValidCache[k];

			bool result = true;

			if (radix <= 1 || radix > 36)
				result = false;

			if (radix > 10)
			{
				char maxChar = (char)('a' + radix - 11);
				string fc = "a-" + maxChar.ToString() + "A-" + maxChar.ToString().ToUpper();
				if (!Regex.IsMatch(value, @"^\-?(([1-9" + fc + @"][0-9" + fc + @"]*)|0)(\.[0-9" + fc + @"]+)?$"))
					result =  false;
			}
			else
			{
				string nc = (radix - 1).ToString();
				if (!Regex.IsMatch(value, @"^\-?(([1-" + nc + @"][0-" + nc + @"]*)|0)(\.[0-" + nc + @"]+)?$"))
					result =  false;
			}

			lock (_isValidCache)
			{
				if (!_isValidCache.ContainsKey(k))
					_isValidCache.Add(k, result);
			}

			return result;
		}

		public static decimal ToDecimal(this string value, int radix)
		{
			string k = value + "," + radix.ToString();

			if (_toDecimalCache.ContainsKey(k))
				return _toDecimalCache[k];

			if (radix <= 1 || radix > 36)
				throw new ArgumentException("radix��2�ʾ�36�ʲ��Ǥʤ���Фʤ�ޤ���");

			if (value == null || value == string.Empty || !IsValid(value, radix))
				throw new ArgumentException("�����ͤ��۾�Ǥ���\n���:" + radix + "\n��:" + value);

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

			lock (_toDecimalCache)
			{
				if (!_toDecimalCache.ContainsKey(k))
					_toDecimalCache.Add(k, result);
			}

			return result;
		}
	}
}
