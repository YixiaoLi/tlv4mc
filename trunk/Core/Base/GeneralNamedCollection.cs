
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NU.OJL.MPRTOS.TLV.Core
{
	public class GeneralNamedCollection<T> : GeneralKeyedJsonableCollection<string, T, GeneralNamedCollection<T>>, IEnumerable<T>, INamedCollection
		where T:class, INamed
	{
		public new IEnumerator<T> GetEnumerator()
		{
			return this.Values.GetEnumerator();
		}

		public IEnumerable<INamed> GetINameEnumerator()
		{
			foreach (T t in this.Values)
			{
				yield return (INamed)t;
			}
		}

		public void Add(INamed namedObject)
		{
			if (namedObject.Name == null)
				throw new Exception("namedObject.Nameがnullです");
			if (namedObject.Name == string.Empty)
				throw new Exception("namedObject.NameがEmptyです");

			base.Add(namedObject.Name, (T)namedObject);
		}

	}
}
