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
using System.Text.RegularExpressions;
using System.Linq;
using System.Text;
using System.IO;
using NU.OJL.MPRTOS.TLV.Base;

namespace NU.OJL.MPRTOS.TLV.Core
{
	public class TraceLogGenerator
	{
		private string _traceLogFilePath;
		private ResourceData _resourceData;
		public Action<int, string> _constructProgressReport = null;
		private int _progressFrom = 0;
		private int _progressTo = 0;

		public TraceLogGenerator(string traceLogFilePath, ResourceData resourceData, Action<int, string> ConstructProgressReport, int progressFrom, int progressTo)
		{
			_constructProgressReport = ConstructProgressReport;
			_traceLogFilePath = traceLogFilePath;
			_resourceData = resourceData;
			_progressFrom = progressFrom;
			_progressTo = progressTo;
		}

		public TraceLogData Generate()
		{
			Dictionary<string, Json> dic = new Dictionary<string, Json>();

			string[] target = _resourceData.ConvertRules.ToArray();

			string[] convertRulePaths = Directory.GetFiles(ApplicationData.Setting.ConvertRulesDirectoryPath, "*." + Properties.Resources.ConvertRuleFileExtension);
			// �ȥ졼�����Ѵ��ե�����򳫤�JsonValue�ǥǥ��ꥢ�饤��
			// �ե����뤬ʣ������������ꤷ�Ƥ���
			foreach (string s in convertRulePaths)
			{
				Json json = new Json().Parse(File.ReadAllText(s));
				foreach (KeyValuePair<string, Json> j in json.GetKeyValuePairEnumerator())
				{
					if (target.Contains(j.Key))
					{
						foreach (KeyValuePair<string, Json> _j in j.Value.GetKeyValuePairEnumerator())
						{
							dic.Add(_j.Key, _j.Value);
						}
					}
				}
			}

			TraceLogData t = new TraceLogData(_resourceData);

			// �ȥ졼�������Ԥ���Ĵ��TraceLog���饹���Ѵ���TraceLogList���ɲä��Ƥ���
			string[] logs = File.ReadAllLines(_traceLogFilePath);
			float i = 1;
			float max = logs.Length;
			foreach (string s in logs)
			{
				if (_constructProgressReport != null)
					_constructProgressReport((int)(((i / max) * (float)(_progressTo - _progressFrom)) + (float)_progressFrom), "�ȥ졼�������̷������Ѵ��� " + i + "/" + max + " ����...");

				foreach (KeyValuePair<string, Json> kvp in dic)
				{
					if (Regex.IsMatch(s, kvp.Key))
					{
						addTraceLog(s, kvp.Key, kvp.Value, t);
					}
				}
				i++;
			}

			t.LogDataBase.SetIds();

			return t;
		}

		/// <summary>
		/// �ɤ߹���������ѥ�����˥ޥå����������Ѵ����ƥ����ɲä���
		/// </summary>
		/// <param name="log">�ɤ߹����</param>
		/// <param name="pattern">�ѥ�����</param>
		/// <param name="value">�Ѵ��ͤ�Value��Json�Ǥ����Ȥ���Ρ�</param>
		/// <param name="traceLogManager">�ɲ���</param>
		private void addTraceLog(string log, string pattern, Json value, TraceLogData traceLogData)
		{
			if (value.IsArray)
			{
				addTraceLogAsArray(log, pattern, value, traceLogData);
			}
			else if (value.IsObject)
			{
				addTraceLogAsObject(log, pattern, value, traceLogData);
			}
			else
			{
				// value��string�ΤȤ������ִ������ɲ�
				string s = Regex.Replace(log, pattern, value);
				// �ؿ���Ŭ��
				s = TLVFunction.Apply(s, _resourceData, traceLogData);
				// �����ɲ�
				traceLogData.Add(new TraceLog(s));
			}
		}

		/// <summary>
		/// �ɤ߹���������ѥ�����˥ޥå����������Ѵ����ƥ����ɲä���
		/// </summary>
		/// <param name="log">�ɤ߹����</param>
		/// <param name="pattern">�ѥ�����</param>
		/// <param name="value">�Ѵ��ͤ�Array��Json�Ǥ����Ȥ���Ρ�</param>
		/// <param name="traceLogManager">�ɲ���</param>
		private void addTraceLogAsArray(string log, string pattern, List<Json> value, TraceLogData traceLogData)
		{
			foreach (Json j in value)
			{
				addTraceLog(log, pattern, j, traceLogData);
			}
		}

		/// <summary>
		/// �ɤ߹���������ѥ�����˥ޥå����������Ѵ����ƥ����ɲä���
		/// </summary>
		/// <param name="log">�ɤ߹����</param>
		/// <param name="condition">�ѥ�����</param>
		/// <param name="value">�Ѵ��ͤ�Object��Json�Ǥ����Ȥ���Ρ�</param>
		/// <param name="traceLogManager">�ɲ���</param>
		private void addTraceLogAsObject(string log, string pattern, Dictionary<string, Json> value, TraceLogData traceLogData)
		{
			foreach (KeyValuePair<string, Json> kvp in value)
			{
				string condition = Regex.Replace(log, pattern, kvp.Key);

				// ���˴ؿ���Ŭ��
				condition = TLVFunction.Apply(condition, _resourceData, traceLogData);

				// ��Ｐ��ɾ��
				bool result;
				try
				{
					result = ConditionExpression.Result(condition);
				}
				catch (Exception e)
				{
					throw new Exception("����Ｐ���۾�Ǥ���\n" + "\"" + kvp.Key + "\"\n" + e.Message);
				}

				// ��Ｐ�����ʤ�ȥ졼�������ɲ�
				if (result)
				{
					addTraceLog(log, pattern, kvp.Value, traceLogData);
				}
			}
		}



	}
}
