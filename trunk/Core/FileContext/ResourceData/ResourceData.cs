
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NU.OJL.MPRTOS.TLV.Base;

namespace NU.OJL.MPRTOS.TLV.Core
{
	public class ResourceData : IJsonable<ResourceData>
	{
		public string TimeScale { get; private set; }
        public int TimeRadix { get; private set; }
        public string Path { get; set; }
		public List<string> ConvertRules { get; private set; }
		public List<string> VisualizeRules { get; private set; }
		public ResourceHeader ResourceHeaders { get; private set; }
		public GeneralNamedCollection<Resource> Resources { get; private set; }

		public ResourceData()
		{
			TimeScale = string.Empty;
			TimeRadix = 10;
			ConvertRules = null;
			VisualizeRules = null;
			ResourceHeaders = null;
			Resources = null;
		}

		public string ToJson()
		{
			return ApplicationFactory.JsonSerializer.Serialize(this);
		}

		public ResourceData Parse(string resourceData)
		{
			return ApplicationFactory.JsonSerializer.Deserialize<ResourceData>(resourceData);
		}

	}
}
