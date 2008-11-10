using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NU.OJL.MPRTOS.TLV.Base;

namespace NU.OJL.MPRTOS.TLV.Core
{
	public class GeneralJsonableCollectionConverter<T,S> : IJsonConverter
		where T : class
		where S : GeneralJsonableCollection<T,S>, new()
	{
		public Type Type { get { return typeof(S); } }

		public void WriteJson(IJsonWriter writer, object obj)
		{
			writer.Write(JsonTokenType.StartArray);
			foreach(T t in (S)obj)
			{
				ApplicationFactory.JsonSerializer.Serialize(writer, t);
			}
			writer.Write(JsonTokenType.EndArray);
		}

		public object ReadJson(IJsonReader reader)
		{
			S gjc = new S();

			for ( ; ; )
			{
				if (reader.TokenType == JsonTokenType.StartObject)
				{
					T obj = ApplicationFactory.JsonSerializer.Deserialize<T>(reader);
					gjc.Add(obj);
				}
				reader.Read();

				if (reader.TokenType == JsonTokenType.EndArray)
					break;
			}

			return gjc;
		}
	}
}
