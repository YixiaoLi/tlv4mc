using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NU.OJL.MPRTOS.TLV.Core
{
	public class Coordinate
	{
		public string X { get; set; }
		public string Y { get; set; }

		public Coordinate(string x, string y)
		{
			X = x;
			Y = y;
		}

		public Coordinate(string coordinate)
		{
			string[] c = coordinate.Replace(" ", "").Replace("\t", "").Split(',');
			X = c[0];
			Y = c[1];
		}
	}
}
