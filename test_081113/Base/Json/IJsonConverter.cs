using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NU.OJL.MPRTOS.TLV.Base
{
	public interface IJsonConverter
	{
		Type Type { get; }
		void WriteJson(IJsonWriter writer, object obj);
		object ReadJson(IJsonReader reader);
	}
}
