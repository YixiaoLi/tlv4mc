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
			writer.Write(JsonTokenType.String, ((Color)obj).ToArgb().ToString("x8"));
		}

		public object ReadJson(IJsonReader reader)
		{
			string value = (string)reader.Value;
			return Color.FromArgb(Convert.ToInt32(value, 16));
		}
	}
}
