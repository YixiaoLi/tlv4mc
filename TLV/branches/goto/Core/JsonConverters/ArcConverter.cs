using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.IO;
using NU.OJL.MPRTOS.TLV.Base;
using System;

namespace NU.OJL.MPRTOS.TLV.Core
{
	public class ArcConverter : GeneralConverter<Arc>
	{
		protected override void WriteJson(IJsonWriter writer, Arc arc)
		{
			writer.WriteArray(w =>
				{
					writer.WriteValue(arc.Start);
					writer.WriteValue(arc.Sweep);
				});
		}

		public override object ReadJson(IJsonReader reader)
		{
			reader.Read();
			Single st = Convert.ToSingle(reader.Value);
			reader.Read();
			Single sw = Convert.ToSingle(reader.Value);
			reader.Read();
			return new Arc(st, sw);
		}
	}
}
