using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NU.OJL.MPRTOS.TLV.Base;
using System.Reflection;

namespace NU.OJL.MPRTOS.TLV.Core
{
	public class INamedConverter<T> : IJsonConverter<T>
		where T:INamed, new()
	{
		public void WriteJson(IJsonWriter writer, T obj)
		{
			if(ApplicationFactory.JsonSerializer.HasConverter(typeof(T)))
			{
				ApplicationFactory.JsonSerializer.Serialize(writer, obj);
			}
			else
			{
				writer.Write(JsonTokenType.StartObject);

				foreach (PropertyInfo pi in obj.GetType().GetProperties())
				{
					if (pi.CanRead && pi.Name != "Name")
					{
						writer.Write(JsonTokenType.PropertyName, pi.Name);
						ApplicationFactory.JsonSerializer.Serialize(writer, pi.GetValue(obj,null));
					}
				}

				writer.Write(JsonTokenType.EndObject);
			}
		}

		public T ReadJson(IJsonReader reader)
		{
			T obj = new T();

			if (ApplicationFactory.JsonSerializer.HasConverter(typeof(T)))
			{
				obj = ApplicationFactory.JsonSerializer.Deserialize<T>(reader);
			}
			else
			{
				while (reader.TokenType != JsonTokenType.EndObject)
				{
					if (reader.TokenType == JsonTokenType.PropertyName)
					{
						string key = (string)reader.Value;
						reader.Read();

						PropertyInfo pi = obj.GetType().GetProperties().Single<PropertyInfo>(p => p.Name == key);

						object o = ApplicationFactory.JsonSerializer.Deserialize(reader, pi.PropertyType);

						pi.SetValue(obj, o, null);
					}
				}
			}

			return (T)obj;
		}
	}
}
