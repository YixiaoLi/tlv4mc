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
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;
using NU.OJL.MPRTOS.TLV.Base;

namespace NU.OJL.MPRTOS.TLV.Core
{
    /// <summary>
    /// ���̷����ȥ졼��������ӥޡ��������ξ����ɽ�����饹
    /// </summary>
    public class TraceLogVisualizerData : IFileContextData
    {
        private bool _isDirty = false;

        /// <summary>
        /// �ǡ������������줿�Ȥ���ȯ�����륤�٥��
        /// </summary>
        public event EventHandler<GeneralEventArgs<bool>> IsDirtyChanged = null;

        /// <summary>
        /// �ǡ�������������Ƥ��뤫�ɤ���
        /// </summary>
        public bool IsDirty
        {
            get { return _isDirty; }
            set
            {
                if(_isDirty != value)
                {
                    _isDirty = value;

                    if (IsDirtyChanged != null)
                        IsDirtyChanged(this, new GeneralEventArgs<bool>(_isDirty));
                }
            }
        }
        /// <summary>
        /// �꥽�����ǡ���
        /// </summary>
		public ResourceData ResourceData { get; private set; }
        /// <summary>
        /// �ȥ졼�����Υꥹ��
        /// </summary>
		public TraceLogData TraceLogData { get; private set; }
		/// <summary>
		/// �Ļ벽�ǡ���
		/// </summary>
		public VisualizeData VisualizeData { get; set; }
		/// <summary>
		/// ����ǡ���
		/// </summary>
		public SettingData SettingData { get; set; }

		/// <summary>
		/// <c>CommonFormatTraceLog</c>�Υ��󥹥��󥹤���������
		/// </summary>
        public TraceLogVisualizerData()
        {
        }

		/// <summary>
		/// <c>CommonFormatTraceLog</c>�Υ��󥹥��󥹤���������
        /// </summary>
        /// <param name="resourceData">���̷����Υ꥽�����ǡ���</param>
        /// <param name="traceLogData">���̷����Υȥ졼�����ǡ���</param>
		public TraceLogVisualizerData(ResourceData resourceData, TraceLogData traceLogData, VisualizeData visualizeData, SettingData settingData)
        {
			ResourceData = resourceData;
			TraceLogData = traceLogData;
			VisualizeData = visualizeData;
			SettingData = settingData;

			setVisualizeRuleToEvent();
        }

		private void setVisualizeRuleToEvent()
		{
			foreach (VisualizeRule rule in VisualizeData.VisualizeRules)
			{
				foreach (Event evnt in rule.Shapes)
				{
					evnt.SetVisualizeRuleName(rule.Name);
				}
			}
		}

        /// <summary>
        /// �ѥ�����ꤷ�ƥ��ꥢ�饤��
        /// </summary>
        /// <param name="path">��¸������Υѥ�</param>
        public void Serialize(string path)
        {
			// ����ǥ��쥯�ȥ����
			if(!Directory.Exists(ApplicationData.Setting.TemporaryDirectoryPath))
				Directory.CreateDirectory(ApplicationData.Setting.TemporaryDirectoryPath);

			string targetTmpDirPath = Path.Combine(ApplicationData.Setting.TemporaryDirectoryPath, "tlv_convertRuleTmp_" + DateTime.Now.Ticks.ToString() + @"\");
			Directory.CreateDirectory(targetTmpDirPath);

			IZip zip = ApplicationFactory.Zip;

			string name = Path.GetFileNameWithoutExtension(path);

			File.WriteAllText(targetTmpDirPath + name + "." + Properties.Resources.ResourceFileExtension, ResourceData.ToJson());
			File.WriteAllText(targetTmpDirPath + name + "." + Properties.Resources.TraceLogFileExtension, TraceLogData.TraceLogs.ToJson());
			File.WriteAllText(targetTmpDirPath + name + "." + Properties.Resources.VisualizeRuleFileExtension, VisualizeData.ToJson());
			File.WriteAllText(targetTmpDirPath + name + "." + Properties.Resources.SettingFileExtension, SettingData.ToJson());

			zip.Compress(path, targetTmpDirPath);

			Directory.Delete(targetTmpDirPath, true);
        }

        /// <summary>
        /// �ѥ�����ꤷ�ƥǥ��ꥢ�饤��
        /// </summary>
        /// <param name="path">�ɤ߹���ѥ�</param>
        public void Deserialize(string path)
        {
			// ����ǥ��쥯�ȥ����
			if (!Directory.Exists(ApplicationData.Setting.TemporaryDirectoryPath))
				Directory.CreateDirectory(ApplicationData.Setting.TemporaryDirectoryPath);

			string targetTmpDirPath = Path.Combine(ApplicationData.Setting.TemporaryDirectoryPath, "tlv_convertRuleTmp_" + DateTime.Now.Ticks.ToString() + @"\");
			Directory.CreateDirectory(targetTmpDirPath);

			IZip zip = ApplicationFactory.Zip;

			zip.Extract(path, targetTmpDirPath);

			string resFilePath = Directory.GetFiles(targetTmpDirPath, "*." + Properties.Resources.ResourceFileExtension)[0];
			string logFilePath = Directory.GetFiles(targetTmpDirPath, "*." + Properties.Resources.TraceLogFileExtension)[0];
			string vixFilePath = Directory.GetFiles(targetTmpDirPath, "*." + Properties.Resources.VisualizeRuleFileExtension)[0];
			string settingFilePath = Directory.GetFiles(targetTmpDirPath, "*." + Properties.Resources.SettingFileExtension)[0];

			ResourceData res = ApplicationFactory.JsonSerializer.Deserialize<ResourceData>(File.ReadAllText(resFilePath));
			TraceLogList log = ApplicationFactory.JsonSerializer.Deserialize<TraceLogList>(File.ReadAllText(logFilePath));
			VisualizeData viz = ApplicationFactory.JsonSerializer.Deserialize<VisualizeData>(File.ReadAllText(vixFilePath));
			SettingData setting = ApplicationFactory.JsonSerializer.Deserialize<SettingData>(File.ReadAllText(settingFilePath));
			
			ResourceData = res;
			TraceLogData = new TraceLogData(log, res);
			VisualizeData = viz;
			SettingData = setting;
			
			setVisualizeRuleToEvent();

			foreach(KeyValuePair<string, TimeLineMarker> tlm in (ObservableDictionary<string, TimeLineMarker>)(SettingData.LocalSetting.TimeLineMarkerManager.Markers))
			{
				tlm.Value.Name = tlm.Key;
			}

			Directory.Delete(targetTmpDirPath, true);
        }
    }
}
