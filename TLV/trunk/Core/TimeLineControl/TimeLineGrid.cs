using System;
using System.Data;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using NU.OJL.MPRTOS.TLV.Base;

namespace NU.OJL.MPRTOS.TLV.Core.TimeLineControl
{
    public enum TimeLineGridRowSizeMode
    {
        Fix,
        Fill
    }

    public class TimeLineGrid : DataGridView
    {
        private TimeLineColumn timeLineColumn;
        private TimeLineGridRowSizeMode rowSizeMode = TimeLineGridRowSizeMode.Fill;
        private bool isShownCursor = true;
        private int allRowHeight = 0;
        private int rowMinimumHeight = 25;
        private int timeLinePositionX = 0;
        private int timeLinePositionMinimumX = 0;
        private int ownBeginGrabRowIndex = -1;
        private bool dropDestinationIsValid;
        private bool dropDestinationIsNextRow;
        private int dropDestinationRowIndex;
        private bool doesDrawLastIndex = false;
        private Bitmap firstDisplayBitmap;

        public int TimeLinePositionX
        {
            get { return timeLinePositionX; }
            set
            {
                int delta = value - timeLinePositionX;
                if (delta != 0)
                {
                    this.GetColumnFromDisplayIndex(this.Columns.Count - 2).Width += delta;
                }
            }
        }
        public int TimeLinePositionMinimumX
        {
            get { return timeLinePositionMinimumX; }
        }
        public int AllRowSize
        {
            get
            {
                return allRowHeight;
            }
        }
        public Rectangle TimeLineRowsRectangle
        {
            get
            {
                return new Rectangle(
                    new Point(timeLinePositionX, this.ColumnHeadersHeight),
                    new Size(this.ClientRectangle.Width - timeLinePositionX, this.ClientRectangle.Height - this.ColumnHeadersHeight)
                    );
            }
        }
        public TimeLineGridRowSizeMode RowSizeMode
        {
            get { return rowSizeMode; }
            set
            {
                if (rowSizeMode != value)
                {
                    rowSizeMode = value;
                    resizeRows();
                }
            }
        }
        public bool DoesDrawLastIndex
        {
            get { return doesDrawLastIndex; }
            set
            {
                doesDrawLastIndex = value;
                if (doesDrawLastIndex)
                {
                    firstDisplayBitmap = new Bitmap(this.Width, this.Rows[this.FirstDisplayedScrollingRowIndex].Height + this.ColumnHeadersHeight);
                    this.DrawToBitmap(firstDisplayBitmap, new Rectangle(0, 0, this.Width, this.Rows[this.FirstDisplayedScrollingRowIndex].Height + this.ColumnHeadersHeight));
                    this.Rows[this.FirstDisplayedScrollingRowIndex].Height = (this.Height - this.ColumnHeadersHeight) % this.RowTemplate.Height;
                }
                else
                {
                    resizeRows();
                }
                this.Refresh();
            }
        }

        public TimeLineGrid()
            : base()
        {
            this.BorderStyle = BorderStyle.None;
            this.RowHeadersVisible = false;
            this.RowTemplate.Height = 25;
            this.DefaultCellStyle.WrapMode = DataGridViewTriState.False;
            this.ColumnHeadersDefaultCellStyle.WrapMode = DataGridViewTriState.False;
            this.ColumnHeadersHeight = 30;
            this.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
            this.AllowUserToAddRows = false;
            this.AllowUserToDeleteRows = false;
            this.AllowUserToOrderColumns = true;
            this.AllowUserToResizeColumns = true;
            this.AllowUserToResizeRows = false;
            this.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            this.MultiSelect = false;
            this.AllowDrop = true;
            this.DoubleBuffered = true;

            this.timeLineColumn = new TimeLineColumn();
            this.timeLineColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            this.timeLineColumn.HeaderText = "タイムライン";
            this.timeLineColumn.Name = "TimeLine";
            this.timeLineColumn.ReadOnly = true;
            this.timeLineColumn.DataPropertyName = "Paths";
        }

        private bool decideDropDestinationRowIndex(DataGridView grid, DragEventArgs e, out int from, out int to, out bool next)
        {
            from = (int)e.Data.GetData(typeof(int));

            if (grid.NewRowIndex != -1 && grid.NewRowIndex == from)
            {
                to = 0;
                next = false;
                return false;
            }

            Point clientPoint = grid.PointToClient(new Point(e.X, e.Y));
            clientPoint.X = 1;
            DataGridView.HitTestInfo hit = grid.HitTest(clientPoint.X, clientPoint.Y);

            to = hit.RowIndex;
            if (to == -1)
            {
                int top = grid.ColumnHeadersVisible ? grid.ColumnHeadersHeight : 0;
                top += 1;

                if (top > clientPoint.Y)
                {
                    to = grid.FirstDisplayedCell.RowIndex;
                }
                else
                {
                    to = grid.Rows.Count - 1;
                }
            }

            if (to == grid.NewRowIndex)
            {
                to--;
            }

            next = (to > from);
            return (from != to);
        }

        private int moveDataValue(int from, int to, bool next)
        {
            SortableBindingList<TimeLineViewableObject> list = (SortableBindingList<TimeLineViewableObject>)this.DataSource;

            TimeLineViewableObject rowData = list[from].DeepClone();

            list.RemoveAt(from);

            if (to > from)
            {
                to--;
            }
            if (next)
            {
                to++;
            }
            if (to <= list.Count)
            {
                list.Insert(to, rowData);
            }
            else
            {
                list.Add(rowData);
            }

            return list.IndexOf(rowData);
        }

        protected override void OnRowPostPaint(DataGridViewRowPostPaintEventArgs e)
        {
            base.OnRowPostPaint(e);
            if (dropDestinationIsValid)
            {
                if (e.RowIndex == dropDestinationRowIndex)
                {
                    GraphicsPath path = new GraphicsPath();
                    path.StartFigure();
                    path.AddRectangle(new Rectangle(e.RowBounds.X + 1, e.RowBounds.Y, e.RowBounds.Width - 2, e.RowBounds.Height - 1));
                    path.AddRectangle(new Rectangle(e.RowBounds.X + 4, e.RowBounds.Y + 3, e.RowBounds.Width - 8, e.RowBounds.Height - 7));
                    path.CloseFigure();
                    e.Graphics.FillPath(new HatchBrush(HatchStyle.Percent50, Color.Black, Color.Transparent), path);
                }
            }
            if (e.IsFirstDisplayedRow && doesDrawLastIndex)
            {
                Bitmap tmpBmp = new Bitmap(this.Width, this.Rows[this.FirstDisplayedScrollingRowIndex].Height);
                using (Graphics tmpBmpGraphics = Graphics.FromImage(tmpBmp))
                {
                    tmpBmpGraphics.DrawImage(firstDisplayBitmap, 0, -1 * (this.ColumnHeadersHeight + (this.RowTemplate.Height - this.Rows[this.FirstDisplayedScrollingRowIndex].Height)));
                }
                e.Graphics.DrawImage(tmpBmp, e.RowBounds.X, e.RowBounds.Y);
            }
        }

        protected override void OnDragOver(DragEventArgs e)
        {
            base.OnDragOver(e);

            e.Effect = DragDropEffects.Move;

            int from, to;
            bool next;
            bool valid = decideDropDestinationRowIndex(this, e, out from, out to, out next);

            bool needRedraw = (valid != dropDestinationIsValid);

            if (valid)
            {
                needRedraw = needRedraw || (to != dropDestinationRowIndex) || (next != dropDestinationIsNextRow);
            }

            if (needRedraw)
            {
                if (dropDestinationIsValid)
                {
                    this.InvalidateRow(dropDestinationRowIndex);
                }
                if (valid)
                {
                    this.InvalidateRow(to);
                }
            }

            dropDestinationIsValid = valid;
            dropDestinationRowIndex = to;
            dropDestinationIsNextRow = next;

        }

        protected override void OnDragLeave(EventArgs e)
        {
            base.OnDragLeave(e);
            if (dropDestinationIsValid)
            {
                dropDestinationIsValid = false;
                this.InvalidateRow(dropDestinationRowIndex);
            }
        }

        protected override void OnDragDrop(DragEventArgs e)
        {
            base.OnDragDrop(e);

            int from, to;
            bool next;

            if (decideDropDestinationRowIndex(this, e, out from, out to, out next))
            {
                dropDestinationIsValid = false;

                to = moveDataValue(from, to, next);

                this.resizeRows();

                this.CurrentCell = this[this.CurrentCell.ColumnIndex, to];
            }

            if (this.doesDrawLastIndex)
            {
                this.FirstDisplayedScrollingRowIndex--;
                this.DoesDrawLastIndex = true;
                this.Refresh();
            }
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            if (!isShownCursor)
            {
                Cursor.Show();
                isShownCursor = true;
                this.Refresh();
            }
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);

            if ((e.Button & MouseButtons.Left) == MouseButtons.Left && ownBeginGrabRowIndex != -1)
            {
                this.DoDragDrop(ownBeginGrabRowIndex, DragDropEffects.Move);
            }
            else
            {
                if (e.X > this.TimeLinePositionX && e.Y > this.ColumnHeadersHeight)
                {
                    if (isShownCursor)
                    {
                        //Cursor.Hide();
                        isShownCursor = false;
                    }

                    drawNowMarker(e.X);
                }
                else if (!isShownCursor)
                {
                    Cursor.Show();
                    isShownCursor = true;
                    this.Refresh();
                }
            }
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);

            ownBeginGrabRowIndex = -1;

            if ((e.Button & MouseButtons.Left) == MouseButtons.Left)
            {
                DataGridView.HitTestInfo hit = this.HitTest(e.X, e.Y);

                if (hit.Type == DataGridViewHitTestType.Cell)
                {
                    CurrentCell = this[hit.ColumnIndex, hit.RowIndex];
                    ownBeginGrabRowIndex = hit.RowIndex;
                }
            }

        }

        protected override void OnDataSourceChanged(EventArgs e)
        {
            base.OnDataSourceChanged(e);

            if(this.Columns.Contains("Paths"))
            {
                this.Columns.Remove("Paths");
            }

            if(! this.Columns.Contains(this.timeLineColumn))
            {
                this.Columns.Add(this.timeLineColumn);
            }
            this.timeLineColumn.DisplayIndex = this.Columns.Count - 1;

            if (this.Columns.Count > 1)
            {
                this.GetColumnFromDisplayIndex(this.Columns.Count - 2).Frozen = true;
            }

            this.AutoResizeColumns();
        }

        protected override void OnRowsAdded(DataGridViewRowsAddedEventArgs e)
        {
            this.resizeRows();
            base.OnRowsAdded(e);
        }

        protected override void OnColumnWidthChanged(DataGridViewColumnEventArgs e)
        {
            int width = 0;
            foreach (DataGridViewColumn column in this.Columns)
            {
                if (column.DisplayIndex != this.Columns.Count - 1)
                {
                    width += column.Width;
                }
            }
            this.timeLinePositionX = width;
            if(this.Columns.Count > 1)
            {
                this.timeLinePositionMinimumX = this.timeLinePositionX - this.GetColumnFromDisplayIndex(this.Columns.Count - 2).Width + this.GetColumnFromDisplayIndex(this.Columns.Count - 2).MinimumWidth;
            }
            base.OnColumnWidthChanged(e);
        }

        protected override void OnSizeChanged(EventArgs e)
        {
            base.OnSizeChanged(e);
            if (rowSizeMode == TimeLineGridRowSizeMode.Fill)
            {
                this.resizeRows();
            }
        }

        private void resizeRows()
        {
            if(this.Rows.Count != 0)
            {
                switch(rowSizeMode)
                {
                    case TimeLineGridRowSizeMode.Fill:
                        int rowHeight = this.TimeLineRowsRectangle.Height / (this.Rows.Count != 0 ? this.Rows.Count : 1);
                        int rowHeightRemainder = this.TimeLineRowsRectangle.Height % (this.Rows.Count != 0 ? this.Rows.Count : 1);

                        rowHeight = (rowHeight < rowMinimumHeight) ? rowMinimumHeight : rowHeight;
                        rowHeightRemainder = (rowHeight * this.Rows.Count > this.TimeLineRowsRectangle.Height) ? 0 : rowHeightRemainder;

                        foreach (DataGridViewRow row in this.Rows)
                        {
                            row.Height = rowHeight;
                            if (rowHeightRemainder != 0)
                            {
                                row.Height++;
                                rowHeightRemainder--;
                            }
                        }
                        break;
                    case TimeLineGridRowSizeMode.Fix:
                        foreach (DataGridViewRow row in this.Rows)
                        {
                            row.Height = this.RowTemplate.Height;
                        }
                        this.Height = (this.RowTemplate.Height * this.Rows.Count) + this.ColumnHeadersHeight;
                        break;
                }

                int height = 0;
                foreach (DataGridViewRow row in this.Rows)
                {
                    height += row.Height;
                }
                allRowHeight = height;
            }
        }

        private void drawNowMarker(int x)
        {
            Rectangle backRect = new Rectangle(
                this.TimeLinePositionX,
                this.ColumnHeadersHeight,
                this.ClientSize.Width - this.TimeLinePositionX,
                this.ClientSize.Height - this.ColumnHeadersHeight
                );

            Bitmap tmpBmp = new Bitmap(backRect.Width, backRect.Height);
            using (Graphics tmpBmpGraphics = Graphics.FromImage(tmpBmp))
            {
                x -= this.TimeLinePositionX;
                using(Pen pen = new Pen(Color.FromArgb(50,0,0,0)))
                {
                    pen.DashStyle = DashStyle.Dash;
                    tmpBmpGraphics.DrawLine(pen, x, 0, x, backRect.Height);
                }
                using (Graphics graphics = this.CreateGraphics())
                {
                    this.DoubleBuffered = true;
                    this.Refresh();
                    graphics.DrawImage(tmpBmp, backRect.X, backRect.Y);
                    this.DoubleBuffered = false;
                }
            }
        }

        public DataGridViewColumn GetColumnFromDisplayIndex(int displayIndex)
        {
            foreach (DataGridViewColumn column in this.Columns)
            {
                if (column.DisplayIndex == displayIndex)
                {
                    return column;
                }
            }
            throw new Exception(String.Format("Display Index = {0} のカラムはありません", displayIndex));
        }

        public void DrawTimeLineResizeBar(int x)
        {
            int drawX = x;
            Rectangle backRect = this.ClientRectangle;
            Rectangle drawRect = new Rectangle(drawX - 1, backRect.Y + this.ColumnHeadersHeight, 3, backRect.Height - this.ColumnHeadersHeight);

            Bitmap tmpBmp = new Bitmap(backRect.Width, backRect.Height);
            using (Graphics tmpBmpGraphics = Graphics.FromImage(tmpBmp))
            {
                tmpBmpGraphics.FillRectangle(new HatchBrush(HatchStyle.Percent50, Color.Black, Color.Transparent), drawRect);

                using (Graphics graphics = this.CreateGraphics())
                {
                    this.DoubleBuffered = true;
                    this.Refresh();
                    graphics.DrawImage(tmpBmp, backRect.X, backRect.Y);
                    this.DoubleBuffered = false;
                }
            }
        }
    }
}
