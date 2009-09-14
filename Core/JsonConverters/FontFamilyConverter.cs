
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NU.OJL.MPRTOS.TLV.Base;
using System.Drawing;

namespace NU.OJL.MPRTOS.TLV.Core
{
	public class FontFamilyConverter : GeneralConverter<FontFamily>
	{
		public override object ReadJson(IJsonReader reader)
		{
			return new FontFamily((string)reader.Value);
		}

		protected override void WriteJson(IJsonWriter writer, FontFamily obj)
		{
			writer.WriteValue(obj.Name.ToString());
		}
	}
}
