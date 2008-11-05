using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NU.OJL.MPRTOS.TLV.Base;

namespace NU.OJL.MPRTOS.TLV.Core
{
	public class VisualizeData : IJsonable<VisualizeData>
	{
		public Dictionary<string, ApplyRule> ApplyRules { get; set; }
		public VisualizeRuleList VisualizeRules { get; set; }
		public Dictionary<string, ShapeList> Shapes { get; set; }

		public VisualizeData()
		{
			ApplyRules = new Dictionary<string, ApplyRule>();
			VisualizeRules = new VisualizeRuleList();
			Shapes = new Dictionary<string, ShapeList>();
		}

		public string ToJson()
		{
			return ApplicationFactory.JsonSerializer.Serialize<VisualizeData>(this);
		}

		public VisualizeData Parse(string data)
		{
			return ApplicationFactory.JsonSerializer.Deserialize<VisualizeData>(data);
		}
	}
}
