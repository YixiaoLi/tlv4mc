using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace NU.OJL.MPRTOS.TLV.Core.Controls
{
	public class TimeLineCell : DataGridViewTextBoxCell
	{
		public override Type EditType
		{
			get
			{
				return typeof(ITimeLineControl);
			}
		}
		public override Type FormattedValueType
		{
			get
			{
				return typeof(ITimeLineControl);
			}
		}
	}
}
