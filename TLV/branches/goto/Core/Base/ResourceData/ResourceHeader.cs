using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.IO;
using NU.OJL.MPRTOS.TLV.Base;

namespace NU.OJL.MPRTOS.TLV.Core
{
    /// <summary>
    /// リソースリスト
    /// </summary>
	public class ResourceHeader : IJsonable<ResourceHeader>
	{
		public string Name { get; private set; }
		public Dictionary<string, string[]> Enums { get; private set; }
		public Dictionary<string, ResourceType> Types { get; private set; }

		public ResourceHeader()
			:base()
		{
			Enums = new Dictionary<string, string[]>();
			Types = new Dictionary<string, ResourceType>();
		}

		public ResourceHeader(string name, Dictionary<string, string[]> enums, Dictionary<string, ResourceType> types)
			:this()
		{
			Name = name;
			Enums = enums;
			Types = types;
		}

		public string ToJson()
		{
			return ApplicationFactory.JsonSerializer.Serialize<ResourceHeader>(this);
		}

		public ResourceHeader Parse(string data)
		{
			return ApplicationFactory.JsonSerializer.Deserialize<ResourceHeader>(data);
		}

	}
}
