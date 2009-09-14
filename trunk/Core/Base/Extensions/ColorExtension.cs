
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace NU.OJL.MPRTOS.TLV.Core
{
	public static class ColorExtension
	{
		public static string ToHexString(this Color color)
		{
			return color.A.ToString("x2") + color.R.ToString("x2") + color.G.ToString("x2") + color.B.ToString("x2");
		}
		public static Color FromHexString(this Color color, string str)
		{
			try
			{
				return Color.FromArgb(Convert.ToInt32(str, 16));
			}
			catch
			{
				return Color.Empty;
			}
		}
	}
}
