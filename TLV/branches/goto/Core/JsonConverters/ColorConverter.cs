using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.IO;
using System.Drawing;
using NU.OJL.MPRTOS.TLV.Base;
using System;

namespace NU.OJL.MPRTOS.TLV.Core
{
	public class ColorConverter : GeneralConverter<Color>
	{
		protected override void WriteJson(IJsonWriter writer, Color color)
		{
			writer.WriteValue(color.ToHexString());
		}

		public override object ReadJson(IJsonReader reader)
		{
			return new Color().FromHexString((string)reader.Value);
		}
	}
}
