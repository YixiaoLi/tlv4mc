using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NU.OJL.MPRTOS.TLV.Base;

namespace NU.OJL.MPRTOS.TLV.Core
{
	public class ArgumentList : GeneralJsonableCollection<Json, ArgumentList>
	{
		public override string ToString()
		{
			StringBuilder sb = new StringBuilder();

			for (int i = 0; i < Count; i++ )
			{
				sb.Append(this[i].Value.ToString());
				if(i != Count-1)
					sb.Append(", ");
			}

			return sb.ToString();
		}
	}
}
