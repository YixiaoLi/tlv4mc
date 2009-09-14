
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NU.OJL.MPRTOS.TLV.Base
{
	public static class CharExtension
	{
		public static decimal ToDecimal(this char ch)
		{
			ch = char.ToLower(ch);
			switch (ch)
			{
				case '0':
				case '1':
				case '2':
				case '3':
				case '4':
				case '5':
				case '6':
				case '7':
				case '8':
				case '9':
					return Convert.ToDecimal(ch - '0');
				default:
					return Convert.ToDecimal(ch - 'a') + 10m;
			}
		}
	}
}
