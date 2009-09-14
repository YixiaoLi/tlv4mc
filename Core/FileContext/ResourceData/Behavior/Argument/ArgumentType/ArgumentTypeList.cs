
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace NU.OJL.MPRTOS.TLV.Core
{
	public class ArgumentTypeList : GeneralNamedCollection<ArgumentType>
	{
		public override string ToString()
		{
			StringBuilder sb = new StringBuilder();

			int i = 0;
			foreach(ArgumentType argType in this)
			{
				sb.Append(argType.Type.ToString() + " " + argType.Name.ToString());

				if (i != Count - 1)
					sb.Append(", ");

				i++;
			}

			return sb.ToString();
		}
	}
}
