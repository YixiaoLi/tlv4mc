using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NU.OJL.MPRTOS.TLV.Core
{
	public class ApplyRule
	{
		public Dictionary<string, string> Attribute { get; set; }
		public Dictionary<string, string> Behavior { get; set; }

		public ApplyRule()
		{
			Attribute = new Dictionary<string, string>();
			Behavior = new Dictionary<string, string>();
		}

		public ApplyRule(Dictionary<string, string> attr, Dictionary<string, string> bhvr)
		{
			Attribute = attr;
			Behavior = bhvr;
		}
	}
}
