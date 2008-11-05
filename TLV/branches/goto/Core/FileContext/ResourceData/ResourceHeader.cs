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
	public class ResourceHeader : IJsonable<ResourceHeader>, IEnumerable<KeyValuePair<string,ResourceType>>
	{
		private Dictionary<string, ResourceType> _types = new Dictionary<string, ResourceType>();
		public string Name { get; private set; }
		public ResourceType this[string name] { get { return _types[name]; } }

		public ResourceHeader()
			:base()
		{
		}

		public ResourceHeader(string name, Dictionary<string, ResourceType> types)
			:this()
		{
			Name = name;
			_types = types;
		}

		public string ToJson()
		{
			return ApplicationFactory.JsonSerializer.Serialize<ResourceHeader>(this);
		}

		public ResourceHeader Parse(string data)
		{
			return ApplicationFactory.JsonSerializer.Deserialize<ResourceHeader>(data);
		}

		public IEnumerator<KeyValuePair<string, ResourceType>> GetEnumerator()
		{
			return _types.GetEnumerator();
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return _types.GetEnumerator();
		}

	}
}
