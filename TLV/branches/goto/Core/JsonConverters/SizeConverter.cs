using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.IO;
using NU.OJL.MPRTOS.TLV.Base;
using System;

namespace NU.OJL.MPRTOS.TLV.Core
{
	public class SizeConverter : IJsonConverter
	{
		public Type Type { get { return typeof(Size); } }

		public void WriteJson(IJsonWriter writer, object obj)
		{
			writer.Write(JsonTokenType.String, ((Size)obj).Width + "," + ((Size)obj).Height);
		}

		public object ReadJson(IJsonReader reader)
		{
			string value = (string)reader.Value;
			return new Size(value);
		}
	}
}
