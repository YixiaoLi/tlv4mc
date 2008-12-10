using System;
using System.Drawing;
using System.Collections.Generic;
using System.Windows.Forms;
using NU.OJL.MPRTOS.TLV.Core.TimeLineControl;
using NU.OJL.MPRTOS.TLV.Core.Base;

namespace NU.OJL.MPRTOS.TLV.Core.TimeLineControl.TimeLineGrid
{
    public class TimeLineCell : DataGridViewCell
    {
        public TimeLineCell()
            : base()
        {
            this.Style.BackColor = Color.White;
            this.ValueType = typeof(TimeLineEvents);
        }

        public override Type FormattedValueType
        {
            get
            {
                return this.ValueType;
            }
        }

        protected override void Paint(
            Graphics graphics,
            Rectangle clipBounds,
            Rectangle cellBounds,
            int rowIndex,
            DataGridViewElementStates elementState,
            object value,
            object formattedValue,
            string errorText,
            DataGridViewCellStyle cellStyle,
            DataGridViewAdvancedBorderStyle advancedBorderStyle,
            DataGridViewPaintParts paintParts)
        {
            Rectangle backRect = new Rectangle(0, 0, cellBounds.Width, cellBounds.Height);
            Rectangle foreRect = new Rectangle(0, 0, cellBounds.Width - 1, cellBounds.Height - 1);

            Bitmap tmpBmp = new Bitmap(backRect.Width, backRect.Height);
            using (Graphics tmpBmpGraphics = Graphics.FromImage(tmpBmp))
            {
                tmpBmpGraphics.FillRectangle(Brushes.LightGray, backRect);
                tmpBmpGraphics.FillRectangle(new SolidBrush(this.Style.BackColor), foreRect);
            }
            graphics.DrawImage(tmpBmp, cellBounds.X, cellBounds.Y);
        }
    }
}
