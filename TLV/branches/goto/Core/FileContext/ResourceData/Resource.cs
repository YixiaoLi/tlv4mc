using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NU.OJL.MPRTOS.TLV.Base;

namespace NU.OJL.MPRTOS.TLV.Core
{
	public class Resource : GeneralKeyedJsonableCollection<string, Json, Resource>
	{
		public new Json this[string attributeName] { get { return base[attributeName]; } }
	}
}
