using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NU.OJL.MPRTOS.TLV.Core
{
	public class BehaviorList : GeneralKeyedJsonableCollection<string, List<string>, BehaviorList>
	{
		public BehaviorList()
			: base(new Dictionary<string, List<string>>())
		{
		}

		public BehaviorList(IDictionary<string, List<string>> d)
			:base(d)
		{
		}
	}
}
