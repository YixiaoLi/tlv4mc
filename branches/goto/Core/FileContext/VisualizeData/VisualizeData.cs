﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NU.OJL.MPRTOS.TLV.Base;

namespace NU.OJL.MPRTOS.TLV.Core
{
	public class VisualizeData : IJsonable<VisualizeData>
	{
		public GeneralNamedCollection<VisualizeRule> VisualizeRules { get; set; }
		public GeneralNamedCollection<Shapes> Shapes { get; set; }

		public VisualizeData()
		{
		}

		public string ToJson()
		{
			return ApplicationFactory.JsonSerializer.Serialize(this);
		}

		public VisualizeData Parse(string data)
		{
			return ApplicationFactory.JsonSerializer.Deserialize<VisualizeData>(data);
		}
	}
}