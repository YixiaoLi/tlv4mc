using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Text;

namespace NU.OJL.MPRTOS.TLV.Base
{
	public static class NativeScrollBarApplier
	{
		private static List<NativeScrollBarHelper> _nativeScrollHelpers = new List<NativeScrollBarHelper>();

		public static void Apply(Control control, ScrollBar[] scrollBars)
		{
			NativeScrollBarHelper nativeScrollHelper = new NativeScrollBarHelper(control, scrollBars);
			_nativeScrollHelpers.Add(nativeScrollHelper);
		}
	}
}
