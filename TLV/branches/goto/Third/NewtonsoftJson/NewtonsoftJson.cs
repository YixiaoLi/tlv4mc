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

		private List<Type> _converterList = new List<Type>();

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

		public object Deserialize(IJsonReader reader, Type type)
		{
			return _serializer.Deserialize((JsonReader)reader, type);
		}
		public object Deserialize(string json, Type type)
		{
			return _serializer.Deserialize(new JsonTextReader(new StringReader(json)), type);
		}

		public T Deserialize<T>(IJsonReader reader)
		{
			return (T)Deserialize(reader, typeof(T));
		}
		public T Deserialize<T>(string json)
		{
			return (T)Deserialize(json, typeof(T));
		}

		public void Serialize(IJsonWriter writer, object obj)
		{
			_serializer.Serialize((JsonWriter)writer, obj);
		}

		public string Serialize(object obj)
		{
			StringBuilder sb = new StringBuilder();
			JsonTextWriter writer = new JsonTextWriter(new StringWriter(sb));
			_serializer.Serialize(writer, obj);
			return sb.ToString();
		}


		public void AddConverter(IJsonConverter converter)
		{
			GeneralConverter cnvtr = new GeneralConverter(converter.Type);
			cnvtr.ReadJsonHandler += (r) =>
			{
				return converter.ReadJson(r);
			};
			cnvtr.WriteJsonHandler += (w, o) =>
			{
				converter.WriteJson(w, o);
			};
			_serializer.Converters.Add(cnvtr);

			_converterList.Add(converter.Type);
		}

		public bool HasConverter(Type type)
		{
			return _converterList.Contains(type);
		}

	}
}
