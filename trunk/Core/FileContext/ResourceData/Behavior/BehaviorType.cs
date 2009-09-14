
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace NU.OJL.MPRTOS.TLV.Core
{
	public class BehaviorType : INamed
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
		/// <summary>
		/// 色
		/// </summary>
		public Color? Color { get; set; }

		public BehaviorType()
		{
			Arguments = new ArgumentTypeList();
			Color = ApplicationFactory.ColorFactory.RamdomColor();
		}
	}
}
