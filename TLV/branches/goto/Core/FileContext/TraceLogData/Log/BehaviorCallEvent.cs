using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NU.OJL.MPRTOS.TLV.Core
{
	public class BehaviorCallLog : Log
	{
		public override LogType Type { get { return LogType.BehaviorCall; } }
		public string Behavior { get; private set; }
		public ArgumentList Arguments { get; private set; }

		public BehaviorCallLog(Time time, Resource obj, string behavior, ArgumentList args)
			: this(time, null, obj, behavior, args)
		{

		}

		public BehaviorCallLog(Time time, Resource subject, Resource obj, string behavior, ArgumentList args)
			:base(time, subject, obj)
		{
			Behavior = behavior;
			Arguments = args;
		}
	}
}
