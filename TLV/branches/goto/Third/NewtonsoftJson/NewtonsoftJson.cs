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

		private Dictionary<Type, IJsonConverter> _converterList = new Dictionary<Type, IJsonConverter>();

		public NewtonsoftJson()
		{
			_serializer.Converters.Add(new IsoDateTimeConverter());
			_serializer.Converters.Add(new EnumConverter());
			_serializer.NullValueHandling = NullValueHandling.Ignore;
			_serializer.MissingMemberHandling = MissingMemberHandling.Ignore;
			_serializer.ReferenceLoopHandling = ReferenceLoopHandling.Serialize;
			_serializer.ObjectCreationHandling = ObjectCreationHandling.Auto;
			_serializer.DefaultValueHandling = DefaultValueHandling.Ignore;
		}

		public T Deserialize<T>(IJsonReader reader)
		{
			return (T)Deserialize(reader, typeof(T));
		}
		public T Deserialize<T>(string json)
		{
			return (T)Deserialize(json, typeof(T));
		}
		public object Deserialize(IJsonReader reader, Type type)
		{
			if (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Nullable<>))
				type = type.GetGenericArguments()[0];

			return _serializer.Deserialize(((JsonReader)reader).Reader, type);
		}
		public object Deserialize(string json, Type type)
		{
			return Deserialize(new JsonReader(new JsonTextReader(new StringReader(json))), type);
		}

		public void Serialize(IJsonWriter writer, object obj)
		{
			_serializer.Serialize(((JsonWriter)writer).Writer, obj);
		}
		public string Serialize(object obj)
		{
			StringBuilder sb = new StringBuilder();
			Serialize(new JsonWriter(new JsonTextWriter(new StringWriter(sb)) { Formatting = Formatting.Indented }), obj);
			return sb.ToString();
		}

		public void AddConverter(IJsonConverter converter)
		{
			GeneralConverter cnvtr = new GeneralConverter(converter);

			_serializer.Converters.Add(cnvtr);

			_converterList.Add(converter.Type, converter);
		}
		public bool HasConverter(Type type)
		{
			return _converterList.ContainsKey(type);
		}
		public IJsonConverter GetConverter(Type type)
		{
			return _converterList[type];
		}

	}
}
