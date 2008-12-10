using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Reflection;

namespace NU.OJL.MPRTOS.TLV.Base
{
	public static class ControlExtension
	{
		public static void ApplyNativeScroll(this Control control)
		{
			List<ScrollBar> scrollBars = new List<ScrollBar>();

			foreach (PropertyInfo pi in control.GetType().GetProperties(BindingFlags.Public| BindingFlags.NonPublic|BindingFlags.Instance))
			{
				if (typeof(ScrollBar).IsAssignableFrom(pi.PropertyType))
				{
					scrollBars.Add((ScrollBar)pi.GetValue(control, null));
				}
			}
			if (scrollBars.Count != 0)
			{
				NativeScrollBarApplier.Apply(control, scrollBars.ToArray());
			}
		}
	}
}
