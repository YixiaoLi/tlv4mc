﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NU.OJL.MPRTOS.TLV.Base;

namespace NU.OJL.MPRTOS.TLV.Core
{
	public class ArgumentType : INamed
	{
		public string Name { get; set; }
		public JsonValueType Type { get; set; }
	}
}