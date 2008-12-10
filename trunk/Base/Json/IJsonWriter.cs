using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NU.OJL.MPRTOS.TLV.Base
{
	public interface IJsonWriter
	{
		void Write(JsonTokenType type, object value);
		void Write(JsonTokenType type);
		void Write(object obj);
		void Flush();
	}
}
