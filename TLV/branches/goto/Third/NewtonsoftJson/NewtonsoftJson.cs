using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using NU.OJL.MPRTOS.TLV.Base;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Converters;

namespace NU.OJL.MPRTOS.TLV.Third
{
	public	class NewtonsoftJson :IJsonSerializer
	{
		private JsonSerializer _serializer = new JsonSerializer();
	
		public NewtonsoftJson()
		{
			_serializer.Converters.Add(new IsoDateTimeConverter());
			_serializer.Converters.Add(new EnumConverter());
			_serializer.Converters.Add(new JsonValueConverter());
			_serializer.NullValueHandling = NullValueHandling.Ignore;
			_serializer.MissingMemberHandling = MissingMemberHandling.Ignore;
			_serializer.ReferenceLoopHandling = ReferenceLoopHandling.Serialize;
			_serializer.ObjectCreationHandling = ObjectCreationHandling.Auto;
			_serializer.DefaultValueHandling = DefaultValueHandling.Ignore;
		}
	
		public T Deserialize<T>(string json)
		{
			return (T)_serializer.Deserialize(new JsonTextReader(new StringReader(json)), typeof(T));
		}

		public string Serialize<T>(T obj)
		{
			StringBuilder sb = new StringBuilder();
			JsonTextWriter writer = new JsonTextWriter(new StringWriter(sb));
			writer.Formatting = Formatting.Indented;
			_serializer.Serialize(writer, obj);
			return sb.ToString();
		}

		public void AddConverter<T>(IJsonConverter<T> converter)
		{
			GeneralConverter<T> cnvtr = new GeneralConverter<T>();
			cnvtr.ReadJsonHandler += (r) =>
			{
				return converter.ReadJson(r);
			};
			cnvtr.WriteJsonHandler += (w, o) =>
			{
				converter.WriteJson(w, o);
			};

			_serializer.Converters.Add(cnvtr);
		}
	}
}
