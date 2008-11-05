using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.IO;
using NU.OJL.MPRTOS.TLV.Base;
using System;

namespace NU.OJL.MPRTOS.TLV.Core
{
	public class ArcConverter : IJsonConverter<Arc>
	{
		public void WriteJson(IJsonWriter writer, Arc obj)
		{
			writer.Write(JsonTokenType.StartArray);
			writer.Write(JsonTokenType.String, obj.Start);
			writer.Write(JsonTokenType.String, obj.Sweep);
			writer.Write(JsonTokenType.EndArray);
		}

		public Arc ReadJson(IJsonReader reader)
		{
			reader.Read();
			Single st = Convert.ToSingle(reader.Value);
			reader.Read();
			Single sw = Convert.ToSingle(reader.Value);
			reader.Read();
			return new Arc(st, sw);
		}
	}
}
