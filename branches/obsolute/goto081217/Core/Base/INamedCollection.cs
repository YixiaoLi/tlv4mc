using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NU.OJL.MPRTOS.TLV.Core
{
	public interface INamedCollection
	{
		IEnumerable<INamed> GetINameEnumerator();
		void Add(string name, INamed namedObject);
	}
}
