using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NU.OJL.MPRTOS.TLV.Base;

namespace NU.OJL.MPRTOS.TLV.Core
{
	public class ResourceList : GeneralKeyedJsonableCollection<string, List<Resource>, ResourceList>
	{
		public new List<Resource> this[string resourceTypeName] { get { return base[resourceTypeName]; } }
	}

}
