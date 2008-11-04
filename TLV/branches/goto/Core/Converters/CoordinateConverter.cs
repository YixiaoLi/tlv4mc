using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.IO;
using NU.OJL.MPRTOS.TLV.Base;

namespace NU.OJL.MPRTOS.TLV.Core
{
	public class CoordinateConverter : IJsonConverter<Coordinate>
	{
		public void WriteJson(IJsonWriter writer, Coordinate obj)
		{
			writer.Write(JsonTokenType.String, obj.X + "," + obj.Y);
		}

		public Coordinate ReadJson(IJsonReader reader)
		{
			string value = (string)reader.Value;
			return new Coordinate(value);
		}
	}
}
