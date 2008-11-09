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
	public class GeneralConverter : JsonConverter
	{
		private Type _type;
		public event Func<IJsonReader, object> ReadJsonHandler;
		public event Action<IJsonWriter, object> WriteJsonHandler;

		public GeneralConverter(Type t)
		{
			_type = t;
		}

		public override bool CanConvert(Type objectType)
		{
			return objectType.IsAssignableFrom(_type);
		}

		public override object ReadJson(Newtonsoft.Json.JsonReader reader, Type objectType)
		{
			if (ReadJsonHandler != null)
			{
				return ReadJsonHandler(new JsonReader((JsonTextReader)reader));
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
				WriteJsonHandler(new JsonWriter((JsonTextWriter)writer), value);
			}
		}
	}
}
