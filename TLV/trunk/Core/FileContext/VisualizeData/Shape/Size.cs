using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NU.OJL.MPRTOS.TLV.Core
{
	public class Size
	{
		public string Width { get; set; }
		public string Height { get; set; }

		public Size(string width, string height)
		{
			Width = width;
			Height = height;
		}
		public Size(string sizes)
		{
			string[] c = sizes.Replace(" ", "").Replace("\t", "").Split(',');
			Width = c[0];
			Height = c[1];
		}

		public override string ToString()
		{
			return Width + "," + Height;
		}
	}
}
