using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NU.OJL.MPRTOS.TLV.Base;

namespace NU.OJL.MPRTOS.TLV.Core
{
	public class TraceLogConverter : IJsonConverter
	{
		public Type Type
		{
			get { return typeof(TraceLog); }
		}

		public void WriteJson(IJsonWriter writer, object obj)
		{
			writer.Write(JsonTokenType.String, ((TraceLog)obj).ToString());
		}

		public object ReadJson(IJsonReader reader)
		{
			return new TraceLog(((string)reader.Value));
		}
	}
}
