using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.IO;
using NU.OJL.MPRTOS.TLV.Base;
using System;

namespace NU.OJL.MPRTOS.TLV.Core
{
	public class AreaConverter : IJsonConverter
	{
		public Type Type { get { return typeof(Area); } }

		public void WriteJson(IJsonWriter writer, object obj)
		{
			writer.Write(JsonTokenType.StartArray);
			writer.Write(JsonTokenType.String, ((Area)obj).Location.ToString());
			writer.Write(JsonTokenType.String, ((Area)obj).Size.ToString());
			writer.Write(JsonTokenType.EndArray);
		}

		public object ReadJson(IJsonReader reader)
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
