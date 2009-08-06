/*
 *  TLV - Trace Log Visualizer
 *
 *  Copyright (C) 2008,2009 by Embedded and Real-Time Systems Laboratory
 *              Graduate School of Information Science, Nagoya Univ., JAPAN
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
using System.Threading;
using System.Drawing;
using System.Text.RegularExpressions;
using NU.OJL.MPRTOS.TLV.Base;

namespace NU.OJL.MPRTOS.TLV.Core
{
	public class TimeLineEvents
	{
		private VisualizeRule _rule;
		private Event _evnt;
		private List<Event> _evnts = new List<Event>();
		private EventShapes _drawShapes = new EventShapes();
		private Resource _target;

		private bool _dataSet = false;
        private Func<TraceLogVisualizerData, EventShapes> _extract;

		public string Name { get { return ToString(); } }
		//public Thread SetDataThread { get; private set; }
		public VisualizeRule Rule { get { return _rule; } }
		public Event Event { get { return _evnt; } }
		public Resource Target { get { return _target; } }
		public List<Event> Events { get { return _evnts; } }
		public bool IsDataSet { get { return _dataSet; } }


		public TimeLineEvents(VisualizeRule rule) {
            _rule = rule;
            _extract = (data) => {
                return data.VisualizeShapeData.Get(rule);
            }; 
       }
		public TimeLineEvents(VisualizeRule rule, Event evnt) {
            // 古いコードに合わせて、あえて_ruleに代入しない
            // バグの可能性もある
            _evnt = evnt;
            _extract = (data) =>
            {
                return data.VisualizeShapeData.Get(rule,evnt);
            };

        }
        
		public TimeLineEvents(Resource target) {
			//: this(null, null, target) { }
            _target = target;
            _extract = (data) =>
            {
                return data.VisualizeShapeData.Get(target);
            };
        }

        public TimeLineEvents(VisualizeRule rule, Resource target) {
            _rule = rule;
            _target = target;
            _extract = (data) =>
            {
                return data.VisualizeShapeData.Get(rule, target);
            };
        }

		public TimeLineEvents(VisualizeRule rule,Event evnt, Resource target) {
            _evnt = evnt;
            _target = target;
            _extract = (data) => {
                return data.VisualizeShapeData.Get(rule, evnt, target);
            };
        }

		public void SetData(TraceLogVisualizerData data)
		{
			ClearData();
            this._drawShapes = _extract(data);
			_drawShapes.Optimize();
			_dataSet = true;
		}

		public void ClearData()
		{
			_drawShapes.Clear();
			_dataSet = false;
		}

		public IEnumerable<EventShape> GetEventShapes(Time from, Time to)
		{
            if (_drawShapes != null)
            {
                return _drawShapes.GetShapes(from, to);
            }
            else {
                throw new Exception("_drawShapes is null");
            }
		}

		public override string ToString()
		{
			StringBuilder sb = new StringBuilder();
			if (Event != null)
			{
				if (Event.GetVisualizeRuleName() != null)
				{
					sb.Append(Event.GetVisualizeRuleName());
					sb.Append(":");
				}
				sb.Append(Event.Name);
				sb.Append(":");
			}
			if (Target != null)
			{
				sb.Append(Target.Name);
			}
			return sb.ToString();
		}
	}
}
