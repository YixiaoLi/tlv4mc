using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NU.OJL.MPRTOS.TLV.Core
{
	public class BehaviorHappenLogData : LogData
	{
		public override LogType Type { get { return LogType.BehaviorHappen; } }
		public string Behavior { get; private set; }
		public ArgumentList Arguments { get; private set; }

		public BehaviorHappenLogData(Time time, Resource obj, string behavior, ArgumentList args)
			:base(time, obj)
		{
			Behavior = behavior;
			Arguments = args;
		}
	}
}
