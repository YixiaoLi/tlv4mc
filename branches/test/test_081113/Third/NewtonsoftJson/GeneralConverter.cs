using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NU.OJL.MPRTOS.TLV.Base;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Converters;

namespace NU.OJL.MPRTOS.TLV.Third
{
	public class GeneralConverter : Newtonsoft.Json.JsonConverter
	{
		private IJsonConverter _converter;

		public GeneralConverter(IJsonConverter converter)
		{
			_converter = converter;
		}

		public override bool CanConvert(Type objectType)
		{
			return _converter.Type.IsAssignableFrom(objectType);
		}

		public override object ReadJson(Newtonsoft.Json.JsonReader reader, Type objectType)
		{
			return _converter.ReadJson(new JsonReader(reader));
		}

		public override void WriteJson(Newtonsoft.Json.JsonWriter writer, object value)
		{
			_converter.WriteJson(new JsonWriter(writer), value);
		}
	}
}
