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
			return color.ToArgb().ToString("x8");
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
