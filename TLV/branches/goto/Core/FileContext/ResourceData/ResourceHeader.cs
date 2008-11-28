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
	public class ResourceHeader : IJsonable<ResourceHeader>, IEnumerable<ResourceType>
	{
		private GeneralNamedCollection<ResourceType> _types = new GeneralNamedCollection<ResourceType>();
		public string Name { get; set; }
		public ResourceType this[string typeName] { get { return _types[typeName]; } }

		public ResourceHeader()
			:base()
		{
		}

		public ResourceHeader(string name, GeneralNamedCollection<ResourceType> types)
			:this()
		{
			Name = name;
			_types = types;
		}

		public string ToJson()
		{
			return ApplicationFactory.JsonSerializer.Serialize(this);
		}

		public ResourceHeader Parse(string data)
		{
			return ApplicationFactory.JsonSerializer.Deserialize<ResourceHeader>(data);
		}

		public IEnumerator<ResourceType> GetEnumerator()
		{
			return _types.GetEnumerator();
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return _types.GetEnumerator();
		}

		public IEnumerable<string> TypeNames
		{
			get { return _types.Keys; }
		}

		public IEnumerable<ResourceType> ResourceTypes
		{
			get { return _types.Values; }
		}
	}
}
