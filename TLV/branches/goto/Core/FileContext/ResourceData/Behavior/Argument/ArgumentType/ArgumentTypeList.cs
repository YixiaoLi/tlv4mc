using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace NU.OJL.MPRTOS.TLV.Core
{
	public class ArgumentTypeList : GeneralJsonableCollection<ArgumentType, ArgumentTypeList>
	{
		public override string ToString()
		{
			StringBuilder sb = new StringBuilder();

			for (int i = 0; i < Count; i++)
			{
				sb.Append(this[i].Type.ToString() + " " + this[i].Name.ToString());
				if (i != Count - 1)
					sb.Append(", ");
			}

			return sb.ToString();
		}
	}
}
