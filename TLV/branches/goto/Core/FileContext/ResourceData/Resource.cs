using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NU.OJL.MPRTOS.TLV.Base;

namespace NU.OJL.MPRTOS.TLV.Core
{
	public class Resource : GeneralKeyedJsonableCollection<string, Json, Resource>, INamed
	{
		public string Name { get; set; }
		public new Json this[string attributeName] { get { return base[attributeName]; } }

		public IEnumerable<string> AttributeNames
		{
			get { return this.Keys; }
		}

		public IEnumerable<Json> Attributes
		{
			get { return this.Values; }
		}
	}
}
