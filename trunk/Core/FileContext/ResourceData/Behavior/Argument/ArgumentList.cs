
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
				if(this[i] != null)
					sb.Append(this[i].Value.ToString());
				
				if(i != Count-1)
					sb.Append(",");
			}

			return sb.ToString();
		}

		public bool checkArgs(string[] args)
		{
			bool[] results = new bool[this.Count];

			for (int i = 0; i < this.Count; i++)
			{
				if (args.Length - 1 < i || args[i] == string.Empty)
					results[i] = true;
				else
					results[i] = args[i] == this[i].ToString();
			}

			bool result = true;

			foreach (bool r in results)
			{
				result &= r;
			}

			return result;
		}
	}
}
