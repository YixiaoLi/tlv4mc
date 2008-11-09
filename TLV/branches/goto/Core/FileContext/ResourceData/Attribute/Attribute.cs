using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NU.OJL.MPRTOS.TLV.Base;

namespace NU.OJL.MPRTOS.TLV.Core
{
	public class Attribute : INamed
	{
		public string Name { get; set; }
		public Json Value { get; set; }

		public static implicit operator Json(Attribute attr)
		{
			return attr.Value;
		}
	}
}
