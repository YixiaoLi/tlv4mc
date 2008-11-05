using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NU.OJL.MPRTOS.TLV.Core
{
	public class Arc
	{
		public Single Start { get; set; }
		public Single Sweep { get; set; }

		public Arc(Single start, Single sweep)
		{
			Start = start;
			Sweep = sweep;
		}

		public override string ToString()
		{
			return Start.ToString() + "," + Sweep.ToString();
		}
	}
}
