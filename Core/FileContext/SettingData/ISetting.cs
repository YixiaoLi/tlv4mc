
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NU.OJL.MPRTOS.TLV.Base;

namespace NU.OJL.MPRTOS.TLV.Core
{
	public delegate void SettingChangeEventHandler(object sender, string propertyName);

	public interface ISetting
	{
		event SettingChangeEventHandler BecameDirty;
	}
}
