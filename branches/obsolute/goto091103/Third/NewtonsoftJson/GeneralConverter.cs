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
	public class GeneralConverter<T> : JsonConverter
	{
		public event Func<IJsonReader, T> ReadJsonHandler;
		public event Action<IJsonWriter, T> WriteJsonHandler;
		
		public override bool CanConvert(Type objectType)
		{
			return objectType.IsAssignableFrom(typeof(T));
		}

		public override object ReadJson(Newtonsoft.Json.JsonReader reader, Type objectType)
		{
			if (ReadJsonHandler != null)
			{
				return ReadJsonHandler(new JsonReader(reader));
			}
			else
			{
				return null;
			}
		}

		public override void WriteJson(Newtonsoft.Json.JsonWriter writer, object value)
		{
			if (WriteJsonHandler != null)
			{
				WriteJsonHandler(new JsonWriter(writer), (T)value);
			}
		}
	}
}
