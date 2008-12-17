using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NU.OJL.MPRTOS.TLV.Core
{
	public class Behavior : INamed
	{
		private string _name = string.Empty;

		public string Name
		{
			get { return _name; }
			set
			{
				_name = value;
				if (DisplayName == null)
					DisplayName = value;
			}
		}
		public string DisplayName { get; set; }
		public ArgumentTypeList Arguments { get; set; }

		public Behavior()
		{
			Arguments = new ArgumentTypeList();
		}
	}
}
