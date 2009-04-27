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
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.IO;
using NU.OJL.MPRTOS.TLV.Base;
using System;
using System.Linq;
using System.Threading;
using System.Drawing;

namespace NU.OJL.MPRTOS.TLV.Core
{
	/// <summary>
	/// ���̷����ȥ졼�������Ѵ����뤿��Υ���С���
	/// </summary>
	public class StandartFormatConverter
	{
		public Action<int, string> _constructProgressReport = null;
		private int _progressFrom = 0;
		private int _progressTo = 0;

		public ResourceData ResourceData { get; private set; }
		public TraceLogData TraceLogData { get; private set; }
		public VisualizeData VisualizeData { get; private set; }
		public SettingData SettingData { get; private set; }

		public StandartFormatConverter(string resourceFilePath, string traceLogFilePath, string[] visualizeRuleFilePaths, Action<int, string> ConstructProgressReport)
		{
			_constructProgressReport = ConstructProgressReport;

			progressUpdate(10);
			generateData(
				() => { ResourceData = getResourceData(resourceFilePath); },
				"�꥽�����ǡ�����������",
				"�꥽�����ǡ����������˼��Ԥ��ޤ�����\n�꥽�����ե�����ε��Ҥ˸�꤬�����ǽ��������ޤ���");

			progressUpdate(20);
			generateData(() => { SettingData = getSettingData(); },
				"����ǡ�����������",
				"����ǡ����������˼��Ԥ��ޤ�����");

			progressUpdate(30);
			generateData(
				() => { VisualizeData = getVisualizeData(visualizeRuleFilePaths); },
				"�Ļ벽�ǡ�����������",
				"�Ļ벽�ǡ����������˼��Ԥ��ޤ�����\n�Ļ벽�롼��ե�����ε��Ҥ˸�꤬�����ǽ��������ޤ���");

			progressUpdate(99);
			generateData(
				() => { TraceLogData = getTraceLogData(traceLogFilePath); },
				"�ȥ졼�����ǡ�����������",
				"�ȥ졼�����ǡ����������˼��Ԥ��ޤ�����\n�ȥ졼�����Ѵ��롼��ե�����ε��Ҥ˸�꤬�����ǽ��������ޤ���");

			if (_constructProgressReport != null)
				_constructProgressReport(_progressFrom, "�������");
		}

		private void progressUpdate(int progressTo)
		{
			_progressFrom = _progressTo;
			_progressTo = progressTo;
		}

		private void generateData(Action action, string message, string exceptionMessage)
		{
			if (_constructProgressReport != null)
				_constructProgressReport(_progressFrom, message);
			try
			{
				action();
			}
			catch (Exception _e)
			{
				throw new Exception(exceptionMessage + "\n" + _e.Message);
			}
		}
		private ResourceData getResourceData(string resourceFilePath)
		{
			ResourceData resData = ApplicationFactory.JsonSerializer.Deserialize<ResourceData>(File.ReadAllText(resourceFilePath));

			// ̤���Ҥ�°���ˤĤ��ƥǥե�����ͤ��ɲä���
			foreach (ResourceType resType in resData.ResourceHeaders.ResourceTypes)
			{
				foreach (Resource res in resData.Resources.Where<Resource>(r=>r.Type == resType.Name))
				{
					foreach(AttributeType attrType in resType.Attributes)
					{
						if (!res.Attributes.ContainsKey(attrType.Name))
						{
							res.Attributes.Add(attrType.Name, attrType.Default);
						}
					}
				}
			}

			return resData;
		}
		private VisualizeData getVisualizeData(string[] visualizeRuleFilePaths)
		{
			VisualizeData visualizeData = new VisualizeData();
			visualizeData.VisualizeRules = new GeneralNamedCollection<VisualizeRule>();
			visualizeData.Shapes = new GeneralNamedCollection<Shapes>();

			string[] target = ResourceData.VisualizeRules.ToArray();

			// �ե����뤬ʣ������������ꤷ�Ƥ���
			foreach (string s in visualizeRuleFilePaths)
			{
				Json json = new Json().Parse(File.ReadAllText(s));
				foreach (KeyValuePair<string, Json> j in json.GetKeyValuePairEnumerator())
				{
					if (target.Contains(j.Key))
					{
						string d = ApplicationFactory.JsonSerializer.Serialize(j.Value);
						VisualizeData vizData = ApplicationFactory.JsonSerializer.Deserialize<VisualizeData>(d);

						if (vizData.VisualizeRules != null)
							foreach (VisualizeRule vizRule in vizData.VisualizeRules)
							{
								visualizeData.VisualizeRules.Add(vizRule);
							}
						if (vizData.Shapes != null)
							foreach (Shapes sp in vizData.Shapes)
							{
								visualizeData.Shapes.Add(sp);
							}
					}
				}
			}

			return visualizeData;
		}
		private TraceLogData getTraceLogData(string traceLogFilePath)
		{
			return new TraceLogGenerator(traceLogFilePath, ResourceData, _constructProgressReport, _progressFrom, _progressTo).Generate();
		}
		private SettingData getSettingData()
		{
			SettingData settingData = new SettingData();
			return settingData;
		}
	}

}