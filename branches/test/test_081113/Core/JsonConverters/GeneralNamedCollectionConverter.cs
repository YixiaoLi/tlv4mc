using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NU.OJL.MPRTOS.TLV.Base;

namespace NU.OJL.MPRTOS.TLV.Core
{
	public class GeneralNamedCollectionConverter<T,S> : IJsonConverter
		where T : class, INamed
		where S : GeneralNamedCollection<T>, new()
	{
		public Type Type { get { return typeof(S); } }

		public void WriteJson(IJsonWriter writer, object obj)
		{
			writer.Write(JsonTokenType.StartObject);
			foreach (T t in (S)obj)
			{
				writer.Write(JsonTokenType.PropertyName, t.Name);
				ApplicationFactory.JsonSerializer.Serialize(writer, t);
			}
			writer.Write(JsonTokenType.EndObject);
		}

		public object ReadJson(IJsonReader reader)
		{
			S gnc = new S();

			while (reader.TokenType != JsonTokenType.EndObject)
			{
				if (reader.TokenType == JsonTokenType.PropertyName)
				{
					string key = (string)reader.Value;
					T obj = ApplicationFactory.JsonSerializer.Deserialize<T>(reader);
					obj.Name = key;
					gnc.Add(obj.Name, obj);
				}

				reader.Read();
			}

			return gnc;
		}
	}
}
