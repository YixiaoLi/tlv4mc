﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NU.OJL.MPRTOS.TLV.Base;

namespace NU.OJL.MPRTOS.TLV.Core
{
	public class ResourceData : IJsonable<ResourceData>
	{
		public string TimeScale { get; private set; }
		public string ConvertRule { get; private set; }
		public string ResourceHeader { get; private set; }
		public ResourceList Resources { get; private set; }

		public ResourceData()
		{
			TimeScale = string.Empty;
			ConvertRule = string.Empty;
			ResourceHeader = string.Empty;
			Resources = new ResourceList();
		}

		public string ToJson()
		{
			return ApplicationFactory.JsonSerializer.Serialize<ResourceData>(this);
		}

		public ResourceData Parse(string resourceData)
		{
			return ApplicationFactory.JsonSerializer.Deserialize<ResourceData>(resourceData);
		}

	}
}