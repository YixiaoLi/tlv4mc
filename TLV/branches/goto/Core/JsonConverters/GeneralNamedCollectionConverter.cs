using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NU.OJL.MPRTOS.TLV.Base;

namespace NU.OJL.MPRTOS.TLV.Core
{
	public class GeneralNamedCollectionConverter<T> : IJsonConverter
		where T : class, INamed
	{
		public Type Type { get { return typeof(GeneralNamedCollection<T>); } }

		public void WriteJson(IJsonWriter writer, object obj)
		{
			writer.Write(JsonTokenType.StartObject);
			foreach (T t in (GeneralNamedCollection<T>)obj)
			{
				writer.Write(JsonTokenType.PropertyName, t.Name);
				ApplicationFactory.JsonSerializer.Serialize(writer, t);
			}
			writer.Write(JsonTokenType.EndObject);
		}

		public object ReadJson(IJsonReader reader)
		{
			GeneralNamedCollection<T> gnc = new GeneralNamedCollection<T>();

			while(reader.TokenType != JsonTokenType.EndObject)
			{
				if(reader.TokenType == JsonTokenType.PropertyName)
				{
					string key = (string)reader.Value;
					reader.Read();
					T obj = ApplicationFactory.JsonSerializer.Deserialize<T>(reader);
					obj.Name = key;
					gnc.Add(obj.Name, obj);
				}
			}

			return gnc;
		}
	}
}
