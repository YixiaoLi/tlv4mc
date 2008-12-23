using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Drawing2D;

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

		protected override void Paint(Graphics graphics, Rectangle clipBounds, Rectangle cellBounds, int rowIndex, DataGridViewElementStates cellState, object value, object formattedValue, string errorText, DataGridViewCellStyle cellStyle, DataGridViewAdvancedBorderStyle advancedBorderStyle, DataGridViewPaintParts paintParts)
		{
			paintParts &= ~DataGridViewPaintParts.Focus & ~DataGridViewPaintParts.SelectionBackground;
			base.Paint(graphics, clipBounds, cellBounds, rowIndex, cellState, value, formattedValue, errorText, cellStyle, advancedBorderStyle, paintParts);

			Region r = graphics.Clip;
			graphics.Clip = new Region(new Rectangle(cellBounds.X, cellBounds.Y, cellBounds.Width - 1, cellBounds.Height - 2));
			((ITimeLineControl)value).Draw(new PaintEventArgs(graphics, new Rectangle(cellBounds.X, cellBounds.Y, cellBounds.Width - 1, cellBounds.Height - 2)));
			graphics.Clip = r;
		}
	}
}
