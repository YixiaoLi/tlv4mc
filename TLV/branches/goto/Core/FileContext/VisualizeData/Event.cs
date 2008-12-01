using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NU.OJL.MPRTOS.TLV.Base;
using NU.OJL.MPRTOS.TLV.Core;

namespace NU.OJL.MPRTOS.TLV.Core.FileContext.VisualizeData
{
	public class Event
	{
		private EventTypes _type = EventTypes.None;

		public string DisplayName { get; set; }
		public TimeEvent From { get; set; }
		public TimeEvent To { get; set; }
		public TimeEvent When { get; set; }
		public ShapesList Shapes { get; set; }

		public EventTypes Type
		{
			get
			{
				if (_type == EventTypes.None)
				{
					if (When == null && From != null && To != null)
						_type |= EventTypes.Between;
					else if (When != null && From == null && To == null)
						_type |= EventTypes.When;
					else
						_type |= EventTypes.Error;

					if ((_type & EventTypes.Between) == EventTypes.Between)
					{
						if (From.Attribute != null && From.Behavior == null)
							_type |= EventTypes.FromAttributeChange;
						else if (From.Attribute == null && From.Behavior != null)
							_type |= EventTypes.FromBehaviorHappen;
						else
							_type |= EventTypes.Error;

						if (To.Attribute != null && To.Behavior == null)
							_type |= EventTypes.ToAttributeChange;
						else if (To.Attribute == null && To.Behavior != null)
							_type |= EventTypes.ToBehaviorHappen;
						else
							_type |= EventTypes.Error;
					}
					else if ((_type & EventTypes.When) == EventTypes.When)
					{
						if (When.Attribute != null && When.Behavior == null)
							_type |= EventTypes.WhenAttributeChange;
						else if (When.Attribute == null && When.Behavior != null)
							_type |= EventTypes.WhenBehaviorHappen;
						else
							_type |= EventTypes.Error;
					}
					else
					{
						_type |= EventTypes.Error;
					}

					return _type;
				}
				else
				{
					return _type;
				}
			}
		}
	}

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

	public class TimeEvent
	{
		public string Behavior { get; set; }
		public string Attribute { get; set; }
		public string Value { get; set; }
		public string[] Arguments { get; set; }
	}

	public class ShapeArgPair: INamed
	{
		public string Name { get; set; }
		public Json[] Args { get; set; }
	}

	public class ShapesList : GeneralKeyedJsonableCollection<string,GeneralNamedCollection<ShapeArgPair>,ShapesList>
	{

	}
}
