﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NU.OJL.MPRTOS.TLV.Base
{
	public enum JsonTokenType
	{
		StartObject,
		StartArray,
		PropertyName,
		Decimal,
		String,
		Boolean,
		Null,
		EndObject,
		EndArray,
		Raw,
		Undefined
	}
}
