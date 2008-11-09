using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NU.OJL.MPRTOS.TLV.Base;

namespace NU.OJL.MPRTOS.TLV.Core
{
	public class ResourceList : GeneralKeyedJsonableCollection<string, GeneralNamedCollection<Resource>, ResourceList>
	{
		public new GeneralNamedCollection<Resource> this[string resourceTypeName] { get { return base[resourceTypeName]; } }

		public IEnumerable<string> ResourceNames
		{
			get { return this.Keys; }
		}

		public IEnumerable<GeneralNamedCollection<Resource>> Resources
		{
			get { return this.Values; }
		}
	}

}
