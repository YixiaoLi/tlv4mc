using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace NU.OJL.MPRTOS.TLV.Core.Controls
{
	public class TimeLineColumn : DataGridViewColumn
	{
		public override DataGridViewCell CellTemplate
		{
			get
			{
				return new TimeLineCell();
			}
			set
			{
				if (value != null && !value.GetType().IsAssignableFrom(typeof(TimeLineCell)))
				{
					throw new InvalidCastException("CellTemplateはTimeLineCellでなくてはなりません。");
				}
				base.CellTemplate = value;
			}
		}
	}
}
