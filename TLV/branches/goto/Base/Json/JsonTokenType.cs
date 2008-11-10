using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NU.OJL.MPRTOS.TLV.Base
{
	public enum JsonTokenType
	{
		None,
		StartObject,
		StartArray,
		StartConstructor,
		PropertyName,
		Comment,
		Raw,
		Integer,
		Float,
		String,
		Boolean,
		Null,
		Undefined,
		EndObject,
		EndArray,
		EndConstructor,
		Date
	}
}
