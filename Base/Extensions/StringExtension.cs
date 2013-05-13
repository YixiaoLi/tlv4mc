/*
 *  TLV - Trace Log Visualizer
 *
 *  Copyright (C) 2008-2013 by Nagoya Univ., JAPAN
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
				throw new ArgumentException("radixは2以上36以下でなければなりません。");

			if (value == null || value == string.Empty || !IsValid(value, radix))
				throw new ArgumentException("入力値が異常です。\n基数:" + radix + "\n値:" + value);

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
