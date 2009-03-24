using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NU.OJL.MPRTOS.TLV.Base;

namespace NU.OJL.MPRTOS.TLV.Core
{
	public class ApplicationMethod
	{
		public static void SetValue<T>(ref T nowValue, T newValue, EventHandler<GeneralChangedEventArgs<T>> changedEvent, object sender)
		{
			if (!nowValue.Equals(newValue))
			{
				T old = nowValue;
				nowValue = newValue;
				if (changedEvent != null)
					changedEvent(sender, new GeneralChangedEventArgs<T>(old, nowValue));
			}
		}
	}
}
