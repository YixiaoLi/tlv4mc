using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NU.OJL.MPRTOS.TLV.Base;

namespace NU.OJL.MPRTOS.TLV.Core
{
	public class GeneralNamedCollectionConverter<T> : IJsonConverter<GeneralNamedCollection<T>>
		where T : class, INamed
	{
		public void WriteJson(IJsonWriter writer, GeneralNamedCollection<T> obj)
		{
			writer.Write(JsonTokenType.StartObject);
			foreach (KeyValuePair<string, T> kvp in obj)
			{
				writer.Write(JsonTokenType.PropertyName, kvp.Key);
				ApplicationFactory.JsonSerializer.Serialize(writer, kvp.Value);
			}
			writer.Write(JsonTokenType.EndObject);
		}

		public GeneralNamedCollection<T> ReadJson(IJsonReader reader)
		{
			GeneralNamedCollection<T> gnc = new GeneralNamedCollection<T>();

			while(reader.TokenType != JsonTokenType.EndObject)
			{
				if(reader.TokenType == JsonTokenType.PropertyName)
				{
					string key = (string)reader.Value;
					reader.Read();
					T obj = (T)reader.Value;
					obj.Name = key;
					gnc.Add(obj.Name, obj);
				}
			}

			return gnc;
		}
	}
}
