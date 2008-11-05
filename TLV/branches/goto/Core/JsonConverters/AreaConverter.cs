using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.IO;
using NU.OJL.MPRTOS.TLV.Base;
using System;

namespace NU.OJL.MPRTOS.TLV.Core
{
	public class AreaConverter : IJsonConverter<Area>
	{
		public void WriteJson(IJsonWriter writer, Area obj)
		{
			writer.Write(JsonTokenType.StartArray);
			writer.Write(JsonTokenType.String, obj.Location.ToString());
			writer.Write(JsonTokenType.String, obj.Size.ToString());
			writer.Write(JsonTokenType.EndArray);
		}

		public Area ReadJson(IJsonReader reader)
		{
			reader.Read();
			string l = (string)reader.Value;
			reader.Read();
			string s = (string)reader.Value;
			reader.Read();
			return new Area(l, s);
		}
	}
}
