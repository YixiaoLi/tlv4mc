using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NU.OJL.MPRTOS.TLV.Base;

namespace NU.OJL.MPRTOS.TLV.Core
{
	public class Resource : INamed
	{
		public string Name { get; set; }
		public string Type { get; set; }
		public AttributeList Attributes { get; set; }
	}
}
