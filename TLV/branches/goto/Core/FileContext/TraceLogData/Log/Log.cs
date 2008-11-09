using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NU.OJL.MPRTOS.TLV.Core
{
	public abstract class Log
	{
		public abstract LogType Type { get; }
		public Time Time { get; private set; }
		public Resource Subject { get; private set; }
		public Resource Object { get; private set; }

		public Log(Time time, Resource obj)
			:this(time, null, obj)
		{

		}

		public Log(Time time, Resource subject, Resource obj)
		{
			Time = time;
			Subject = subject;
			Object = obj;
		}
	}
}
