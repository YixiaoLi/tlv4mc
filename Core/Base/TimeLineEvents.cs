
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
