using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.IO;
using NU.OJL.MPRTOS.TLV.Base;
using System;

namespace NU.OJL.MPRTOS.TLV.Core
{
	public class SizeConverter : GeneralConverter<Size>
	{
		protected override void WriteJson(IJsonWriter writer, Size size)
		{
			writer.WriteValue(size.ToString());
		}

		public override object ReadJson(IJsonReader reader)
		{
			string value = (string)reader.Value;
			return new Size(value);
		}
	}
}
