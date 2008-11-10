using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NU.OJL.MPRTOS.TLV.Base;

namespace NU.OJL.MPRTOS.TLV.Core
{
	public class AttributeConverter : IJsonConverter
	{

		public Type Type
		{
			get { return typeof(Attribute); }
		}

		public void WriteJson(IJsonWriter writer, object obj)
		{
			writer.Write(JsonTokenType.String, ((Attribute)obj).Value.ToString());
		}

		public object ReadJson(IJsonReader reader)
		{
			Attribute attr = new Attribute();
			attr.Value = new Json(reader.Value);
			return attr;
		}

	}
}
