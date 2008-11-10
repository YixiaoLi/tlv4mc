﻿using System;
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
			if(ApplicationFactory.JsonSerializer.HasConverter(typeof(T)))
			{
				ApplicationFactory.JsonSerializer.Serialize(writer, ((T)obj));
			}
			else
			{
				writer.Write(JsonTokenType.StartObject);

				foreach (PropertyInfo pi in ((T)obj).GetType().GetProperties())
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

		public object ReadJson(IJsonReader reader)
		{
			T obj = new T();
			if (ApplicationFactory.JsonSerializer.HasConverter(typeof(T)) && !ApplicationFactory.JsonSerializer.GetConverter(typeof(T)).GetType().IsAssignableFrom(typeof(INamedConverter<T>)))
			{
				obj = ApplicationFactory.JsonSerializer.Deserialize<T>(reader);
			}
			else
			{

				for ( ; ; )
				{
					if (reader.TokenType == JsonTokenType.PropertyName)
					{

						string key = (string)reader.Value;

						PropertyInfo pi = obj.GetType().GetProperties().Single<PropertyInfo>(p => p.Name == key);

						object o = ApplicationFactory.JsonSerializer.Deserialize(reader, pi.PropertyType);

						pi.SetValue(obj, o, null);
					}

					reader.Read();

					if (reader.TokenType == JsonTokenType.EndObject)
						break;

				}
			}

			return (T)obj;
		}
	}
}
