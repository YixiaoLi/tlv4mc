using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NU.OJL.MPRTOS.TLV.Core
{
	public class AttributeChangeLog : Log
	{
		public override LogType Type { get { return LogType.AttributeChange; } }
		public string Attribute { get; private set; }
		public string Value { get; private set; }

		public AttributeChangeLog(Time time, Resource obj, string attr, string val)
			: this(time, null, obj, attr, val)
		{

		}

		public AttributeChangeLog(Time time, Resource subject, Resource obj, string attr, string val)
			:base(time, subject, obj)
		{
			Attribute = attr;
			Value = val;
		}
	}
}
