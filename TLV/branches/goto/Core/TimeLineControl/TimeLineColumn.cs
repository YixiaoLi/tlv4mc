using System;
using System.Drawing;
using System.Windows.Forms;

namespace NU.OJL.MPRTOS.TLV.Core.TimeLineControl
{
    public class TimeLineColumn : DataGridViewColumn
    {
        public TimeLineColumn()
            : base(new TimeLineCell())
        {
            this.HeaderCell = new TimeLineColumnHeaderCell();
            this.SortMode = DataGridViewColumnSortMode.NotSortable;
        }

        public override DataGridViewCell CellTemplate
        {
            get
            {
                return base.CellTemplate;
            }
            set
            {
                if (value != null && !value.GetType().IsAssignableFrom(typeof(TimeLineCell)))
                {
                    throw new InvalidCastException("Must be a TimeLineCell");
                }
                base.CellTemplate = value;
            }
        }
    }

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
                return typeof(string);
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
                tmpBmpGraphics.DrawString((string)value, new Font(FontFamily.GenericMonospace, 8), Brushes.Black, new PointF(5, 5));
            }
            graphics.DrawImage(tmpBmp, cellBounds.X, cellBounds.Y);
        }
    }

    public class TimeLineColumnHeaderCell : DataGridViewColumnHeaderCell
    {
        public TimeLineColumnHeaderCell()
            : base()
        {
            this.Style.WrapMode = DataGridViewTriState.False;
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

            Bitmap tmpBmp = new Bitmap(backRect.Width, backRect.Height);
            using (Graphics tmpBmpGraphics = Graphics.FromImage(tmpBmp))
            {

                DataGridViewAdvancedBorderStyle borderStyle = new DataGridViewAdvancedBorderStyle();
                borderStyle.All = DataGridViewAdvancedCellBorderStyle.Single;

                base.Paint(tmpBmpGraphics, backRect, backRect, rowIndex, elementState, value, formattedValue, errorText, cellStyle, advancedBorderStyle, paintParts);
                base.PaintBorder(tmpBmpGraphics, backRect, backRect, cellStyle, advancedBorderStyle);
            }
            graphics.DrawImage(tmpBmp, cellBounds.X, cellBounds.Y);
        }

    }
}
