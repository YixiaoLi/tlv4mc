using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NU.OJL.MPRTOS.TLV.Base;

namespace NU.OJL.MPRTOS.TLV.Core
{
	public class TraceLogConverter : GeneralConverter<TraceLog>
	{
		protected override void WriteJson(IJsonWriter writer, TraceLog traceLog)
		{
			writer.WriteValue(traceLog.ToString());
		}

		public override object ReadJson(IJsonReader reader)
		{
			return new TraceLog(((string)reader.Value));
		}
	}
}
