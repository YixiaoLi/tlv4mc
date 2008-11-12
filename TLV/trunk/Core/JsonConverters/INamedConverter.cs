using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NU.OJL.MPRTOS.TLV.Base;
using System.Reflection;

namespace NU.OJL.MPRTOS.TLV.Core
{
	public class INamedConverter<T> : IJsonConverter
		where T:INamed, new()
	{
		public Type Type { get { return typeof(T); } }

		public void WriteJson(IJsonWriter writer, object obj)
		{
			writer.Write(JsonTokenType.StartObject);

			foreach (PropertyInfo pi in ((T)obj).GetType().GetProperties())
			{
				if (pi.Name != "Name")
				{
					writer.Write(JsonTokenType.PropertyName, pi.Name);
					ApplicationFactory.JsonSerializer.Serialize(writer, pi.GetValue(obj,null));
				}
			}

			writer.Write(JsonTokenType.EndObject);
		}

		public object ReadJson(IJsonReader reader)
		{
			T obj = new T();

			while (reader.TokenType != JsonTokenType.EndObject)
			{
				if (reader.TokenType == JsonTokenType.PropertyName)
				{
					string key = (string)reader.Value;

					PropertyInfo pi = obj.GetType().GetProperties().Single<PropertyInfo>(p => p.Name == key);

					object o = ApplicationFactory.JsonSerializer.Deserialize(reader, pi.PropertyType);

					pi.SetValue(obj, o, null);
				}
				reader.Read();
			}

			return (T)obj;
		}
	}
}
