
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Converters;

namespace NU.OJL.MPRTOS.TLV.Third
{
	public class EnumConverter : Newtonsoft.Json.JsonConverter
	{
		public override bool CanConvert(Type objectType)
		{
			return objectType.IsEnum;
		}

		public override object ReadJson(Newtonsoft.Json.JsonReader reader, Type objectType)
		{
			return Enum.Parse(objectType, reader.Value.ToString());
		}

		public override void WriteJson(Newtonsoft.Json.JsonWriter writer, object value)
		{
			writer.WriteValue(value.ToString());
		}
	}
}
