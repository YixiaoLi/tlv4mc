using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NU.OJL.MPRTOS.TLV.Core
{
	public class EventShape
	{
		public Time From { get; set; }
		public Time To { get; set; }
		public Shape Shape { get; set; }
		public Event Event { get; set; }

		public EventShape(Time from, Time to, Shape shape, Event evnt)
		{
			From = from;
			To = to;
			Shape = shape;

			Event = evnt;
		}
	}
}
