using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NU.OJL.MPRTOS.TLV.Core
{
	public class BehaviorHappenLogData : LogData
	{
		public override TraceLogType Type { get { return TraceLogType.BehaviorHappen; } }
		public Behavior Behavior { get; private set; }

		public BehaviorHappenLogData(Time time, Resource obj, string behavior, ArgumentList args)
			:base(time, obj)
		{
			Behavior = new Behavior(behavior, args);
		}

		public override string ToString()
		{
			return base.ToString() + "." + Behavior.Name + "(" + (Behavior.Arguments != null && Behavior.Arguments.Count != 0 ? Behavior.Arguments.ToString() : string.Empty) + ")";
		}
	}
}
