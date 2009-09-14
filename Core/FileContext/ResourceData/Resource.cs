
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NU.OJL.MPRTOS.TLV.Base;
using System.Drawing;

namespace NU.OJL.MPRTOS.TLV.Core
{
	public class Resource : INamed
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
		public string Type { get; set; }
		public AttributeList Attributes { get; set; }
		public Color? Color { get; set; }
		public bool? Visible { get; set; }

		public Resource()
		{
			Color = ApplicationFactory.ColorFactory.RamdomColor();
			Visible = ApplicationData.Setting.DefaultResourceVisible;
		}
	}
}
