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
using System.Configuration;
using System.Windows.Forms;
using NU.OJL.MPRTOS.TLV.Base;
using System.IO;

namespace NU.OJL.MPRTOS.TLV.Core
{
	public class ApplicationSetting : ApplicationSettingsBase
	{
		[UserScopedSetting]
		public string ResourceHeadersDirectoryPath { get { return (string)this["ResourceHeadersDirectoryPath"]; } set { this["ResourceHeadersDirectoryPath"] = value; } }

		[UserScopedSetting]
		public string ConvertRulesDirectoryPath { get { return (string)this["ConvertRulesDirectoryPath"]; } set { this["ConvertRulesDirectoryPath"] = value; } }

		[UserScopedSetting]
		public string VisualizeRulesDirectoryPath { get { return (string)this["VisualizeRulesDirectoryPath"]; } set { this["VisualizeRulesDirectoryPath"] = value; } }

		[UserScopedSetting]
		public string TemporaryDirectoryPath { get { return (string)this["TemporaryDirectoryPath"]; } set { this["TemporaryDirectoryPath"] = value; } }

		[UserScopedSetting]
		public bool DefaultResourceVisible { get { return (bool)this["DefaultResourceVisible"]; } set { this["DefaultResourceVisible"] = value; } }

		[UserScopedSetting]
		public bool DefaultVisualizeRuleVisible { get { return (bool)this["DefaultVisualizeRuleVisible"]; } set { this["DefaultVisualizeRuleVisible"] = value; } }

		public ApplicationSetting()
		{
			if (ResourceHeadersDirectoryPath == null)
				ResourceHeadersDirectoryPath = Path.Combine(ApplicationData.ApplicationDirectory, NU.OJL.MPRTOS.TLV.Core.Properties.Resources.DefaultResourceHeadersDirectoryName);

			if (ConvertRulesDirectoryPath == null)
				ConvertRulesDirectoryPath = Path.Combine(ApplicationData.ApplicationDirectory, NU.OJL.MPRTOS.TLV.Core.Properties.Resources.DefaultConvertRulesDirectoryName);

			if (VisualizeRulesDirectoryPath == null)
				VisualizeRulesDirectoryPath = Path.Combine(ApplicationData.ApplicationDirectory,  NU.OJL.MPRTOS.TLV.Core.Properties.Resources.DefaultVisualizeRulesDirectoryName);

			if (TemporaryDirectoryPath == null)
				TemporaryDirectoryPath = Path.Combine(Path.GetTempPath(), NU.OJL.MPRTOS.TLV.Core.Properties.Resources.DefaultTemporaryDirectoryName);

			if (this["DefaultResourceVisible"] == null)
				DefaultResourceVisible = true;

			if (this["DefaultVisualizeRuleVisible"] == null)
				DefaultVisualizeRuleVisible = true;
		}
	}
}
