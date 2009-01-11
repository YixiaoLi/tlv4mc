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
	partial class TimeLineVisualizer : TimeLineControl
	{
		private TimeLineEvents _timeLineVizData;

		public TimeLineEvents TimeLineEvents { get { return _timeLineVizData; } }
		public VisualizeRule Rule { get { return _timeLineVizData.Rule; } }
		public Event Event { get { return _timeLineVizData.Event; } }
		public Resource Target { get { return _timeLineVizData.Target; } }

		public TimeLineVisualizer(TimeLineEvents timeLineVizData)
			:base()
		{
			_timeLineVizData = timeLineVizData;
			InitializeComponent();
		}

		public override void SetData(TraceLogVisualizerData data)
		{
			base.SetData(data);
			_timeLineVizData.SetData(data);
		}

		public override void ClearData()
		{
			base.ClearData();
			_timeLineVizData.ClearData();
		}

		public override void Draw(Graphics g, Rectangle rect)
		{
			base.Draw(g, rect);

			if (TimeLine == null)
				return;

			if (rect.Width == 0)
				return;

			if (!_timeLineVizData.IsDataSet)
				return;

			foreach (EventShape ds in _timeLineVizData.GetEventShapes(TimeLine.FromTime, TimeLine.ToTime))
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

				ds.Shape.Draw(g, new RectangleF(rect.X + x1, rect.Y, w, rect.Height));
			}
		}

		public void WaitSetData()
		{
			TimeLineEvents.SetDataThread.Join();
		}

	}
}
