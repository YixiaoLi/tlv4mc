using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NU.OJL.MPRTOS.TLV.Base
{
	public class GeneralJsonConverter<T> : IJsonConverter<T>
		where T:class
	{
		public event Func<IJsonReader, T> ReadJsonHandler = null;
		public event Action<IJsonWriter, T> WriteJsonHandler = null;

		public void WriteJson(IJsonWriter writer, T obj)
		{
			if(WriteJsonHandler != null)
				WriteJsonHandler(writer, obj);
		}

		public T ReadJson(IJsonReader reader)
		{
			if (ReadJsonHandler != null)
				return ReadJsonHandler(reader);
			else
				return null;
		}

	}
}
