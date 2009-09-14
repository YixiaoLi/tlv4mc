
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NU.OJL.MPRTOS.TLV.Base
{
	public interface IJsonSerializer
	{
		string Serialize(object obj);
		void Serialize(IJsonWriter writer, object obj);

		T Deserialize<T>(string json);
		object Deserialize(string json, Type type);
		T Deserialize<T>(IJsonReader reader);
		object Deserialize(IJsonReader reader, Type type);

		void AddConverter(IJsonConverter converter);
		bool HasConverter(Type type);
		IJsonConverter GetConverter(Type type);
	}
}
