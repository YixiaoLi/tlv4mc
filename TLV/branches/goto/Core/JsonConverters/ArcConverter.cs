using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.IO;
using NU.OJL.MPRTOS.TLV.Base;
using System;

namespace NU.OJL.MPRTOS.TLV.Core
{
	public class ArcConverter : IJsonConverter
	{
		public Type Type { get { return typeof(Arc); } }

		public void WriteJson(IJsonWriter writer, object obj)
		{
			writer.WriteArray(w =>
				{
					writer.WriteValue(((Arc)obj).Start);
					writer.WriteValue(((Arc)obj).Sweep);
				});
		}

		public object ReadJson(IJsonReader reader)
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
