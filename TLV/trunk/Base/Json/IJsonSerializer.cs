using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NU.OJL.MPRTOS.TLV.Base
{
	public interface IJsonSerializer
	{
		string Serialize<T>(T obj);
		T Deserialize<T>(string json);
		void AddConverter<T>(IJsonConverter<T> converter) where T:class;
	}
}
