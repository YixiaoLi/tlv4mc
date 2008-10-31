using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NU.OJL.MPRTOS.TLV.Base
{
	public interface IJsonReader
	{
		bool Read();
		void Skip();
		JsonTokenType TokenType { get; }
		object Value { get; }
	}
}
