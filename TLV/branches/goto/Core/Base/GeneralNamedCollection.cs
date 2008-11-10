using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NU.OJL.MPRTOS.TLV.Core
{
	public class GeneralNamedCollection<T> : GeneralKeyedJsonableCollection<string, T, GeneralNamedCollection<T>>, IEnumerable<T>
		where T : class, INamed
	{
		public new IEnumerator<T> GetEnumerator()
		{
			return this.Values.GetEnumerator();
		}

	}
}
