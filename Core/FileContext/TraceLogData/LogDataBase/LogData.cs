using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NU.OJL.MPRTOS.TLV.Core
{
	public abstract class LogData
	{
		public abstract LogType Type { get; }
		public Time Time { get; private set; }
		public Resource Object { get; private set; }

		public LogData(Time time, Resource obj)
		{
			Time = time;
			Object = obj;
		}
	}
}
