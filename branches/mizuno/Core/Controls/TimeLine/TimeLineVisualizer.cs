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
 *    (b) �����ۤη��֤��̤�������ˡ�ˤ�äơ�TOPPERS�ץ��������Ȥ�
 *        ��𤹤뤳�ȡ�
 *  (4) �ܥ��եȥ����������Ѥˤ��ľ��Ū�ޤ��ϴ���Ū�������뤤���ʤ�»
 *      ������⡤�嵭����Ԥ����TOPPERS�ץ��������Ȥ����դ��뤳�ȡ�
 *      �ޤ����ܥ��եȥ������Υ桼���ޤ��ϥ���ɥ桼������Τ����ʤ���
 *      ͳ�˴�Ť����ᤫ��⡤�嵭����Ԥ����TOPPERS�ץ��������Ȥ�
 *      ���դ��뤳�ȡ�
 *
 *  �ܥ��եȥ������ϡ�̵�ݾڤ��󶡤���Ƥ����ΤǤ��롥�嵭����Ԥ�
 *  ���TOPPERS�ץ��������Ȥϡ��ܥ��եȥ������˴ؤ��ơ�����λ�����Ū
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
using System.Drawing;
using System.Windows.Forms;
using NU.OJL.MPRTOS.TLV.Base;
using System.Text.RegularExpressions;
using System.Threading;

namespace NU.OJL.MPRTOS.TLV.Core.Controls
{
	public partial class TimeLineVisualizer : TimeLineControl
	{
		private TimeLineEvents _timeLineEvents;

		public TimeLineEvents TimeLineEvents { get { return _timeLineEvents; } set { _timeLineEvents = value; } }
		public VisualizeRule Rule { get { return _timeLineEvents.Rule; } }
		public Event Event { get { return _timeLineEvents.Event; } }
		public Resource Target { get { return _timeLineEvents.Target; } }

		public TimeLineVisualizer()
			: base()
		{
			InitializeComponent();
		}

		public TimeLineVisualizer(TimeLineEvents timeLineVizData)
			:base()
		{
			_timeLineEvents = timeLineVizData;
			InitializeComponent();
		}

		public override void SetData(TraceLogVisualizerData data)
		{
			base.SetData(data);
			if (TimeLineEvents != null)
				TimeLineEvents.SetData(data);
		}

		public override void ClearData()		
		{
			base.ClearData();
			if (TimeLineEvents != null)
				TimeLineEvents.ClearData();
		}

		public override void Draw(Graphics g, Rectangle rect)
		{
			base.Draw(g, rect);

			if (TimeLine == null)
				return;

			if (rect.Width == 0)
				return;

			if (TimeLineEvents == null)
				return;

			if (!TimeLineEvents.IsDataSet)
				return;

			foreach (EventShape ds in _timeLineEvents.GetEventShapes(TimeLine.FromTime, TimeLine.ToTime))
			{
				if (ds.To < TimeLine.FromTime)
					continue;

				float x1 = ds.From.ToX(TimeLine.FromTime, TimeLine.ToTime, rect.Width);
				float x2 = ds.To.ToX(TimeLine.FromTime, TimeLine.ToTime, rect.Width);
				float w = x2 - x1;

				if (rect.X + x2 < 0)
					continue;

				if (w <= 0)
					w = 1;

				if (rect.X + x1 + w < 0)
					continue;

				if (w + x1 > rect.Width)
					w = rect.Width * 2 - x1;

				ds.Shape.Draw(g, new RectangleF(rect.X + x1, rect.Y, w, rect.Height));
			}
		}

		//public void WaitSetData()
		//{
		//    TimeLineEvents.SetDataThread.Join();
		//}

	}
}