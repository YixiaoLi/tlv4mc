
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NU.OJL.MPRTOS.TLV.Base;
using NU.OJL.MPRTOS.TLV.Core;

namespace NU.OJL.MPRTOS.TLV.Core
{
	public class Event : INamed
	{
		private string _ruleName = string.Empty;
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
		public TraceLog From { get; set; }
		public TraceLog To { get; set; }
		public TraceLog When { get; set; }
		public Figures Figures { get; set; }

		public void SetVisualizeRuleName(string name)
		{
			_ruleName = name;
		}

		public string GetVisualizeRuleName()
		{
			return _ruleName;
		}

		public string getImageKey()
		{
			string imgId = "warning";

			if (When != null)
			{
				if (When.Attribute != null && When.Behavior == null)
					imgId = "attribute";
				else if (When.Attribute == null && When.Behavior != null)
					imgId = "behavior";
			}
			else if (From != null && To != null)
			{
				string from = string.Empty;
				string to = string.Empty;

				if (From.Attribute != null && From.Behavior == null)
					from = "atr";
				else if (From.Attribute == null && From.Behavior != null)
					from = "bhr";


				if (To.Attribute != null && To.Behavior == null)
					to = "atr";
				else if (To.Attribute == null && To.Behavior != null)
					to = "bhr";

				imgId = from + "2" + to;
			}

			return imgId;

		}
	}
}
