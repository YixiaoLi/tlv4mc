using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NU.OJL.MPRTOS.TLV.Base
{
	public class Pair<L,R>
	{
		public L Data1{ get; set; }
		public R Data2 { get; set; }

		public Pair(L l, R r)
		{
			Data1 = l;
			Data2 = r;
		}

		public override string ToString()
		{
			return Data1.ToString() + ":" + Data2.ToString();
		}
	}
}
