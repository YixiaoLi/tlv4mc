using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NU.OJL.MPRTOS.TLV.Core
{
	public class Behavior : INamed
	{
		public string Name { get; set; }
		public string DisplayName { get; set; }
		public string VisualizeRule { get; set; }
		public ArgumentTypeList Arguments { get; set; }
	}
}
