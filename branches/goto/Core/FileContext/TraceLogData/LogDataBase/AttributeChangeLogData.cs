using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NU.OJL.MPRTOS.TLV.Base;

namespace NU.OJL.MPRTOS.TLV.Core
{
	public class AttributeChangeLogData : LogData
	{
		public override TraceLogType Type { get { return TraceLogType.AttributeChange; } }
		public Attribute Attribute { get; private set; }

		public AttributeChangeLogData(Time time, Resource obj, string attr, Json val)
			:base(time, obj)
		{
			Attribute = new Attribute(attr, val);
		}
	}
}
