using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.IO;
using NU.OJL.MPRTOS.TLV.Base;

namespace NU.OJL.MPRTOS.TLV.Core
{
	public class SizeConverter : IJsonConverter<Size>
	{
		public void WriteJson(IJsonWriter writer, Size obj)
		{
			writer.Write(JsonTokenType.String, obj.Width + "," + obj.Height);
		}

		public Size ReadJson(IJsonReader reader)
		{
			string value = (string)reader.Value;
			return new Size(value);
		}
	}
}
