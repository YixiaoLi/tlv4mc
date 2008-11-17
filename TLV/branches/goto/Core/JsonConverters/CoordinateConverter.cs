using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.IO;
using NU.OJL.MPRTOS.TLV.Base;
using System;

namespace NU.OJL.MPRTOS.TLV.Core
{
	public class CoordinateConverter : IJsonConverter
	{
		public Type Type { get { return typeof(Point); } }

		public void WriteJson(IJsonWriter writer, object obj)
		{
			writer.WriteValue(((Point)obj).ToString());
		}

		public object ReadJson(IJsonReader reader)
		{
			string value = (string)reader.Value;
			return new Point(value);
		}
	}
}
