using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NU.OJL.MPRTOS.TLV.Base;

namespace NU.OJL.MPRTOS.TLV.Core
{
	public class AttributeChangeLogData : LogData
	{
		public override LogType Type { get { return LogType.AttributeChange; } }
		public string Attribute { get; private set; }
		public Json Value { get; private set; }

		public AttributeChangeLogData(Time time, Resource obj, string attr, Json val)
			:base(time, obj)
		{
			Attribute = attr;
			Value = val;
		}
	}
}
