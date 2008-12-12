using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace NU.OJL.MPRTOS.TLV.Core.Controls
{
	public class TimeLineCell : DataGridViewTextBoxCell
	{
		public override Type EditType
		{
			get
			{
				return typeof(ITimeLine);
			}
		}
		public override Type FormattedValueType
		{
			get
			{
				return typeof(ITimeLine);
			}
		}

		protected override void Paint(Graphics graphics, Rectangle clipBounds, Rectangle cellBounds, int rowIndex, DataGridViewElementStates cellState, object value, object formattedValue, string errorText, DataGridViewCellStyle cellStyle, DataGridViewAdvancedBorderStyle advancedBorderStyle, DataGridViewPaintParts paintParts)
		{
			paintParts &= ~DataGridViewPaintParts.Focus & ~DataGridViewPaintParts.SelectionBackground;
			base.Paint(graphics, clipBounds, cellBounds, rowIndex, cellState, value, formattedValue, errorText, cellStyle, advancedBorderStyle, paintParts);
			((ITimeLine)value).Draw(graphics, new Rectangle(cellBounds.X, cellBounds.Y, cellBounds.Width - 1, cellBounds.Height - 1));
		}
	}
}
