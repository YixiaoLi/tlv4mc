
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NU.OJL.MPRTOS.TLV.Base
{
	public static class StringArrayExtension
	{
		public static string ToCSVString(this string[] args)
		{
			StringBuilder sb = new StringBuilder();

			for (int i = 0; i < args.Length; i++)
			{
				sb.Append(args[i]);
				if (i != args.Length - 1)
					sb.Append(",");
			}

			return sb.ToString();
		}
	}
}
