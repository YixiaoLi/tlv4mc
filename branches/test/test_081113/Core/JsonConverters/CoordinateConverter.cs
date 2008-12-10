using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.IO;
using NU.OJL.MPRTOS.TLV.Base;
using System;

namespace NU.OJL.MPRTOS.TLV.Core
{
	public class CoordinateConverter : IJsonConverter
	{
		public Type Type { get { return typeof(Coordinate); } }

		public void WriteJson(IJsonWriter writer, object obj)
		{
			writer.Write(JsonTokenType.String, ((Coordinate)obj).X + "," + ((Coordinate)obj).Y);
		}

		public object ReadJson(IJsonReader reader)
		{
			string value = (string)reader.Value;
			return new Coordinate(value);
		}
	}
}
