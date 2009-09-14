
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NU.OJL.MPRTOS.TLV.Core
{
	public class Behavior : INamed
	{
		public string Name { get; set; }
		public ArgumentList Arguments { get; private set; }

		public Behavior(string name, ArgumentList args)
		{
			Name = name;
			Arguments = args;
		}
	}
}
