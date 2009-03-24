using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NU.OJL.MPRTOS.TLV.Core
{
	[Flags]
	public enum EventTypes
	{
		None = 0x0000,
		When = 0x0001,
		Between = 0x002,
		WhenAttributeChange = 0x0010,
		WhenBehaviorHappen = 0x0020,
		FromAttributeChange = 0x0100,
		ToAttributeChange = 0x0200,
		FromBehaviorHappen = 0x0400,
		ToBehaviorHappen = 0x0800,
		Error = 0x1000
	}
}
