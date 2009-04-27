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
using System.Text;
using System.IO;
using System.Windows.Forms;
using NU.OJL.MPRTOS.TLV.Base;
using NU.OJL.MPRTOS.TLV.Base.Controls;
using NU.OJL.MPRTOS.TLV.Core.Controls;
using System.ComponentModel;
using System.Threading;
using System.Collections.Generic;

namespace NU.OJL.MPRTOS.TLV.Core.Commands
{
    public class NewCommand : AbstractFileChangeCommand
	{
		private OpenResourceFileAndTraceLogFileOpenForm _fileOpenDialog = new OpenResourceFileAndTraceLogFileOpenForm() { StartPosition = FormStartPosition.CenterParent};
		private BackGroundWorkForm _convertBw = new BackGroundWorkForm() { Text = "���̷����ȥ졼�������Ѵ���", StartPosition = FormStartPosition.CenterParent };
		private BackGroundWorkForm _setDataBw = new BackGroundWorkForm() { Text = "�������", ProgressBarText = "", Message = "�ǡ�����������", Style = ProgressBarStyle.Marquee, CanCancel = false, StartPosition = FormStartPosition.CenterParent };
		private TraceLogVisualizerData _cftl = null;
		private string _resFilePath;
		private string _logFilePath;

		public NewCommand(string resFilePath, string logFilePath)
		{
			_resFilePath = resFilePath;
			_logFilePath = logFilePath;

			Text = "�꥽�����ե�����ȥȥ졼�����ե�����򳫤�";

			_setDataBw.DoWork += (_o, _e) =>
			{
				ApplicationData.FileContext.Close();
				ApplicationData.FileContext.Data = _cftl;
				if (_fileOpenDialog.SaveFilePath != string.Empty)
				{
					ApplicationData.FileContext.Path = _fileOpenDialog.SaveFilePath;
					ApplicationData.FileContext.Save();
				}
			};

			_convertBw.RunWorkerCompleted += (o, e) =>
			{
				if (!e.Cancelled)
				{
					_setDataBw.RunWorkerAsync();
				}
			};

			_convertBw.DoWork += (o, _e) =>
			{
				try
				{
					string[] visualizeRuleFilePaths = Directory.GetFiles(ApplicationData.Setting.VisualizeRulesDirectoryPath, "*." + Properties.Resources.VisualizeRuleFileExtension);

					StandartFormatConverter cfc = new StandartFormatConverter(
						_resFilePath,
						_logFilePath,
						visualizeRuleFilePaths,
						(p, s) =>
						{
							if (_convertBw.CancellationPending) { _e.Cancel = true; return; }
							_convertBw.ReportProgress((int)((double)p * 0.8));
							_convertBw.Invoke(new MethodInvoker(() => { _convertBw.Message = s; }));
						});

					if (_convertBw.CancellationPending) { _e.Cancel = true; return; }
					_convertBw.ReportProgress(90);
					_convertBw.Invoke(new MethodInvoker(() => { _convertBw.Message = "���̷����ǡ�����������"; }));

					_cftl = new TraceLogVisualizerData(cfc.ResourceData, cfc.TraceLogData, cfc.VisualizeData, cfc.SettingData);

					if (_convertBw.CancellationPending) { _e.Cancel = true; return; }
					_convertBw.ReportProgress(100);
					_convertBw.Invoke(new MethodInvoker(() => { _convertBw.Message = "��λ"; }));
				}
				catch (Exception e)
				{
					MessageBox.Show(e.Message, "���̷����ؤ��Ѵ��˼��Ԥ��ޤ�����", MessageBoxButtons.OK, MessageBoxIcon.Error);
					_e.Cancel = true;
					return;
				}
			};
		}

        public NewCommand()
			:this(null, null)
        {


        }

        protected override void action()
        {
			if (_resFilePath == null || _logFilePath == null)
			{
				if (_resFilePath != null)
				{
					_fileOpenDialog.ResourceFilePath = _resFilePath;
				}
				if (_logFilePath != null)
				{
					_fileOpenDialog.TraceLogFilePath = _logFilePath;
				}
				if (_fileOpenDialog.ShowDialog() == DialogResult.OK)
				{
					_resFilePath = _fileOpenDialog.ResourceFilePath;
					_logFilePath = _fileOpenDialog.TraceLogFilePath;

					_convertBw.RunWorkerAsync();
				}
			}
			else
			{
				_convertBw.RunWorkerAsync();
			}
        }
    }
}
