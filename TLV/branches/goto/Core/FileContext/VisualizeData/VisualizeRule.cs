using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NU.OJL.MPRTOS.TLV.Base;
using NU.OJL.MPRTOS.TLV.Core.FileContext.VisualizeData;

namespace NU.OJL.MPRTOS.TLV.Core
{
	public class VisualizeRule : INamed
	{
		public string Name { get; set; }
		public string DisplayName { get; set; }
		public string Target { get; set; }
		public Event[] Events { get; set; }

		public bool IsBelongedTargetResourceType()
		{
			return Target != null;
		}
	}
}
