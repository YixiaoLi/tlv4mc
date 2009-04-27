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

namespace NU.OJL.MPRTOS.TLV.Core
{
	public class TLVFunction
	{
		public static readonly Dictionary<string, Func<string[], ResourceData, TraceLogData, string>> TLVFunctions = new Dictionary<string, Func<string[], ResourceData, TraceLogData, string>>()
		{
			{
				"COUNT", (args, resData, logData) =>
				{
					return logData.GetObject(args[0]).Count().ToString();
				}
			},
			{
				"EXIST", (args, resData, logData) =>
				{
					return logData.GetObject(args[0]).Count() != 0 ? "True" : "False";
				}
			},
			{
				"ATTR", (args, resData, logData) =>
				{
					return logData.GetAttributeValue(args[0]).ToString();
				}
			},
			{
				"RES_NAME", (args, resData, logData) =>
				{
					Resource[] i = logData.GetObject(args[0]).ToArray();
					if(i.Length > 1)
						throw new Exception("RES_NAME �� ���ꤷ�����Υ꥽������ʣ��¸�ߤ��ޤ���\n" + args[0]);
					return i[0].Name;
				}
			},
			{
				"RES_DISPLAYNAME", (args, resData, logData) =>
				{
					Resource[] i = logData.GetObject(args[0]).ToArray();
					if(i.Length > 1)
						throw new Exception("RES_NAME �� ���ꤷ�����Υ꥽������ʣ��¸�ߤ��ޤ���\n" + args[0]);

					return i[0].DisplayName;
				}
			},
			{
				"RES_COLOR", (args, resData, logData) =>
				{
					Resource[] i = logData.GetObject(args[0]).ToArray();
					if(i.Length > 1)
						throw new Exception("RES_NAME �� ���ꤷ�����Υ꥽������ʣ��¸�ߤ��ޤ���\n" + args[0]);

					return i[0].Color.Value.ToHexString();
				}
			}
		};

		public static TraceLog Apply(TraceLog value, ResourceData resData, TraceLogData logData)
		{
			return new TraceLog(Apply(value.ToString(), resData, logData));
		}

		public static string Apply(string value, ResourceData resData, TraceLogData logData)
		{
			if (value == null)
				return value;

			value = value.Replace("\\{", "___START_BIG_BRACKET___");
			value = value.Replace("\\}", "___END_BIG_BRACKET___");
			foreach (Match m in Regex.Matches(value, @"\$(?<func>[^{]+){(?<args>[^}]+)}"))
			{
				try
				{
					value = Regex.Replace(value, Regex.Escape(m.Value), apply(m.Groups["func"].Value, m.Groups["args"].Value, resData, logData));
				}
				catch (Exception e)
				{
					throw new Exception("�꥽������Ｐ���۾�Ǥ���\n" + "\"" + value + "\"\n" + e.Message);
				}
			}
			value = value.Replace("___START_BIG_BRACKET___", "\\{");
			value = value.Replace("___END_BIG_BRACKET___", "\\}");
			return value;
		}

		private static string apply(string func, string tlvarguments, ResourceData resData, TraceLogData logData)
		{
			tlvarguments = tlvarguments.Replace("\\,", "___COMMAS___");
			tlvarguments = tlvarguments.Replace("\\\"", "___DOUBLE_QUOTE___");
			string[] tlvargs = tlvarguments.Split(',');
			for (int i=0;i<tlvargs.Length;i++)
			{
				tlvargs[i] = tlvargs[i].Replace("___COMMAS___", "\\,");
				tlvargs[i] = tlvargs[i].Replace("___DOUBLE_QUOTE___", "\\\"");
			}

			string result;

			if (TLVFunctions.ContainsKey(func))
				result = TLVFunctions[func](tlvargs, resData, logData);
			else
				throw new Exception("̤����δؿ��Ǥ���\n" + "\"" + func + "\"");

			return result;
		}

	}
}
