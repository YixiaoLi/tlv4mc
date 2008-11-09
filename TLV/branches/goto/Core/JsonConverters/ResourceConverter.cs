using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NU.OJL.MPRTOS.TLV.Base;

namespace NU.OJL.MPRTOS.TLV.Core
{
	public class ResourceConverter : IJsonConverter
	{
		public Type Type { get { return typeof(Resource); } }

		public void WriteJson(IJsonWriter writer, object obj)
		{
			AttributeList attrs = ((Resource)obj).Attributes;
			ApplicationFactory.JsonSerializer.Serialize(writer, attrs);
		}

		public object ReadJson(IJsonReader reader)
		{
			AttributeList attrs = ApplicationFactory.JsonSerializer.Deserialize<AttributeList>(reader);
			return new Resource() { Attributes = attrs };
		}
	}
}
