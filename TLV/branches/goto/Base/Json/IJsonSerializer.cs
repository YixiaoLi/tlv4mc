using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NU.OJL.MPRTOS.TLV.Base
{
	public interface IJsonSerializer
	{
		string Serialize(object obj);
		string Serialize(IJsonWriter writer, object obj);
		T Deserialize<T>(string json);
		T Deserialize<T>(IJsonReader reader);
		object Deserialize(string json, Type type);
		object Deserialize(IJsonReader reader, Type type);
		void AddConverter<T>(IJsonConverter<T> converter);
		bool HasConverter(Type type);
	}
}
