using System;
using System.Drawing;
using System.Windows.Forms;
using NU.OJL.MPRTOS.TLV.Core.TimeLineControl;

namespace NU.OJL.MPRTOS.TLV.Core.TimeLineControl.TimeLineGrid
{
    public class TimeLineCell : DataGridViewCell
    {
        public TimeLineCell()
            : base()
        {
            this.Style.BackColor = Color.White;
        }

        public override Type FormattedValueType
        {
            get
            {
                return typeof(Log);
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
            Rectangle drawRect = new Rectangle(cellBounds.Width / 20, 5, cellBounds.Width / 3, cellBounds.Height - 6);

            Bitmap tmpBmp = new Bitmap(backRect.Width, backRect.Height);
            using (Graphics tmpBmpGraphics = Graphics.FromImage(tmpBmp))
            {

                tmpBmpGraphics.FillRectangle(Brushes.LightGray, backRect);
                tmpBmpGraphics.FillRectangle(new SolidBrush(this.Style.BackColor), foreRect);
                tmpBmpGraphics.FillRectangle(new SolidBrush(Color.FromArgb(10, 255, 0, 0)), foreRect);
                tmpBmpGraphics.FillRectangle(new SolidBrush(Color.FromArgb(50, 0, 255, 0)), drawRect);
                tmpBmpGraphics.DrawRectangle(new Pen(Color.FromArgb(200, 0, 255, 0)), drawRect);
                tmpBmpGraphics.DrawString(value.ToString(), new Font(FontFamily.GenericMonospace, 8), Brushes.Black, new PointF(5, 5));
            }
            graphics.DrawImage(tmpBmp, cellBounds.X, cellBounds.Y);
        }
    }
}
