using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NU.OJL.MPRTOS.TLV.Base
{
	public interface IJsonConverter<T>
	{
		void WriteJson(IJsonWriter writer, T obj);
		T ReadJson(IJsonReader reader);
	}
}
