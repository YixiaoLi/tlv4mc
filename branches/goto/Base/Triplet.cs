using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NU.OJL.MPRTOS.TLV.Base
{
	public class Triplet<L,C,R>
	{
		public L Data1 { get; set; }
		public C Data2 { get; set; }
		public R Data3 { get; set; }

		public Triplet() { }

		public Triplet(L l, C c, R r)
		{
			Data1 = l;
			Data2 = c;
			Data3 = r;
		}
	}
}
