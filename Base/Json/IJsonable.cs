
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NU.OJL.MPRTOS.TLV.Base
{
	public interface IJsonable<T>
	{
		string ToJson();
		T Parse(string data);
	}
}
