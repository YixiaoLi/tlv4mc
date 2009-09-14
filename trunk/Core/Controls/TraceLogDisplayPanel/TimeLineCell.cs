
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

		protected override bool SetValue(int rowIndex, object value)
		{
			return base.SetValue(rowIndex, value);
			
		}

		protected override void Paint(Graphics graphics, Rectangle clipBounds, Rectangle cellBounds, int rowIndex, DataGridViewElementStates cellState, object value, object formattedValue, string errorText, DataGridViewCellStyle cellStyle, DataGridViewAdvancedBorderStyle advancedBorderStyle, DataGridViewPaintParts paintParts)
		{
			paintParts &= ~DataGridViewPaintParts.Focus & ~DataGridViewPaintParts.SelectionBackground;
			base.Paint(graphics, clipBounds, cellBounds, rowIndex, cellState, value, formattedValue, errorText, cellStyle, advancedBorderStyle, paintParts);

			graphics.SetClip(new Rectangle(cellBounds.X, DataGridView.Location.Y, cellBounds.Width - 1, DataGridView.Height));
			((ITimeLineControl)value).Draw(graphics, new Rectangle(cellBounds.X + 1, cellBounds.Y, cellBounds.Width - 1, cellBounds.Height - 1));
			graphics.ResetClip();
		}
	}
}
