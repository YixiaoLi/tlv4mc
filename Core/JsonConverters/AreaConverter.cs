
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.IO;
using NU.OJL.MPRTOS.TLV.Base;
using System;

namespace NU.OJL.MPRTOS.TLV.Core
{
	public class AreaConverter : GeneralConverter<Area>
	{
		protected override void WriteJson(IJsonWriter writer, Area area)
		{
			writer.WriteArray(w =>
			{
				writer.WriteValue(area.Point.ToString());
				writer.WriteValue(area.Size.ToString());
			});
		}

		public override object ReadJson(IJsonReader reader)
		{
			reader.Read();
			string l = (string)reader.Value;
			reader.Read();
			string s = (string)reader.Value;
			reader.Read();
			return new Area(l, s);
		}
	}
}
