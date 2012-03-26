/*
 *  TLV - Trace Log Visualizer
 *
 *  Copyright (C) 2008-2011 by Nagoya Univ., JAPAN
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
