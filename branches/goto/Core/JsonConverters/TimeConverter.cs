using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NU.OJL.MPRTOS.TLV.Base;
using System.Text.RegularExpressions;

namespace NU.OJL.MPRTOS.TLV.Core
{
	public class TimeConverter : GeneralConverter<Time>
	{
		public override object ReadJson(IJsonReader reader)
		{
			Match m = Regex.Match((string)reader.Value, @"(?<value>[^\(]+)\((?<radix>[^\)]+)\)");
			return new Time(m.Groups["value"].Value, int.Parse(m.Groups["radix"].Value));
		}

		protected override void WriteJson(IJsonWriter writer, Time obj)
		{
			writer.WriteValue(obj.ToString() + "(" + obj.Radix.ToString() + ")");
		}
	}
}
