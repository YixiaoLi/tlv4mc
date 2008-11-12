using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NU.OJL.MPRTOS.TLV.Base
{
	public interface IJsonWriter
	{
		void WriteArray(Action<IJsonWriter> contents);
		void WriteObject(Action<IJsonWriter> contents);
		void WriteValue(object obj);
		void WriteValue(object obj, IJsonSerializer serializer);
		void WriteProperty(string name);
	}
}
