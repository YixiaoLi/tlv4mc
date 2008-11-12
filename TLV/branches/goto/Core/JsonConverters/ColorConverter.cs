using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.IO;
using System.Drawing;
using NU.OJL.MPRTOS.TLV.Base;
using System;

namespace NU.OJL.MPRTOS.TLV.Core
{
	public class ColorConverter : IJsonConverter
	{
		public Type Type { get { return typeof(Color); } }

		public void WriteJson(IJsonWriter writer, object obj)
		{
			writer.Write(JsonTokenType.String, ((Color)obj).ToString());
		}

		public object ReadJson(IJsonReader reader)
		{
			return new Color().FromHexString((string)reader.Value);
		}
	}
}
