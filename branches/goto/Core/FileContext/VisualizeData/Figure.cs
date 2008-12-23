using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NU.OJL.MPRTOS.TLV.Core
{
	public class Figure
	{
		public string Condition { get; set; }
		public Figures Figures { get; set; }
		public string Shape { get; set; }
		public string[] Args { get; set; }
		public bool IsShape { get { return Shape != null; } }
		public bool IsFigures { get { return Figures != null; } }

		public Figure()
		{

		}
	}
}
