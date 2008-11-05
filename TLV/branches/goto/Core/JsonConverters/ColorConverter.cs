using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.IO;
using System.Drawing;
using NU.OJL.MPRTOS.TLV.Base;
using System;

namespace NU.OJL.MPRTOS.TLV.Core
{
	public class ColorConverter : IJsonConverter<Color>
	{
		public void WriteJson(IJsonWriter writer, Color obj)
		{
			writer.Write(JsonTokenType.String, obj.ToArgb().ToString("x8"));
		}

		public Color ReadJson(IJsonReader reader)
		{
			string value = (string)reader.Value;
			return Color.FromArgb(Convert.ToInt32(value, 16));
		}
	}
}
