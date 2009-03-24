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
		public Json Value { get; private set; }

		public Attribute(string name, Json value)
		{
			Name = name;
			Value = value;
		}
	}
}
