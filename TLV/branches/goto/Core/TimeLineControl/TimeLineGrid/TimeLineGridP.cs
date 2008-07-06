using System;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.ComponentModel;
using System.Collections.Generic;
using NU.OJL.MPRTOS.TLV.Base;
using NU.OJL.MPRTOS.TLV.Architecture.PAC;
using NU.OJL.MPRTOS.TLV.Core.TimeLineControl;

namespace NU.OJL.MPRTOS.TLV.Core.TimeLineControl.TimeLineGrid
{
    public partial class TimeLineGridP : DataGridView, IPresentation
    {
        #region メンバ

        private TimeLineColumn timeLineColumn = new TimeLineColumn();
        private DataGridViewColumn timeLineColumnPreviousColumn = new DataGridViewColumn();
        private int allRowsHeight = 0;
        private int maxRowsHeight = 0;
        private int timeLineX = 0;
        private int timeLineMinimumX = 0;
        private RowSizeMode rowSizeMode = RowSizeMode.Fix;
        private Size parentSize = new Size(0,0);
        Rectangle resizingBarRect = Rectangle.Empty;
            
        #endregion

        #region イベント

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        #region プロパティ

        private int AllRowsHeight
        {
            get { return allRowsHeight; }
            set
            {
                if (allRowsHeight != value)
                {
                    allRowsHeight = value;
                    autoResizeRows();
                }
            }
        }
        public RowSizeMode RowSizeMode
        {
            get { return rowSizeMode; }
            set
            {
                if (rowSizeMode != value)
                {
                    rowSizeMode = value;
                    autoResizeRows();
                    NotifyPropertyChanged("RowSizeMode");
                }
            }
        }
        public int TimeLineX
        {
            get { return timeLineX; }
            set
            {
                if (timeLineX != value)
                {
                    int delta = value - timeLineX;
                    if (delta != 0 && this.Columns.Count > 1)
                    {
                        this.Columns[this.Columns.Count - 2].Width += delta;
                    }
                    timeLineX = value;
                    NotifyPropertyChanged("TimeLineX");
                }
            }
        }
        public int TimeLineMinimumX
        {
            get { return timeLineMinimumX; }
            set
            {
                if (timeLineMinimumX != value)
                {
                    timeLineMinimumX = value;
                    NotifyPropertyChanged("TimeLineMinimumX");
                }
            }
        }
        public int VerticalScrollBarWidth
        {
            get
            {
                if (this.Height < AllRowsHeight)
                {
                    return this.VerticalScrollBar.Width;
                }
                else
                {
                    return 0;
                }
            }
        }

        #endregion

        #region コンストラクタ

        public TimeLineGridP(string name)
        {

            #region スーパークラスプロパティ初期化

            this.AllowUserToOrderColumns = true;
            this.ReadOnly = true;
            this.RowHeadersVisible = false;
            this.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.RowTemplate.Resizable = DataGridViewTriState.False;
            this.BorderStyle = BorderStyle.None;
            this.DefaultCellStyle.WrapMode = DataGridViewTriState.False;
            this.ColumnHeadersDefaultCellStyle.WrapMode = DataGridViewTriState.False;
            this.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            this.MultiSelect = false;
            this.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
            this.ColumnHeadersHeight = 30;
            this.RowTemplate.Height = 25;

            #endregion

            #region プロパティ初期化

            this.Name = name;
            this.AllRowsHeight = this.ColumnHeadersHeight;

            #endregion

            #region timeLineColumnプロパティ初期化

            timeLineColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            timeLineColumn.HeaderText = "タイムライン";

            #endregion
        }

        #endregion

        #region メソッド

        #region オーバライドメソッド

        protected override void OnColumnAdded(DataGridViewColumnEventArgs e)
        {
            base.OnColumnAdded(e);

            if (e.Column.ValueType == typeof(Log))
            {
                this.Columns.Remove(e.Column);
                this.Columns.Add(timeLineColumn);
            }

            if(this.Columns.Contains(timeLineColumn))
            {
                if (timeLineColumn.DisplayIndex != this.Columns.Count - 1)
                {
                    timeLineColumn.DisplayIndex = this.Columns.Count - 1;
                }
                timeLineColumn = (TimeLineColumn)this.Columns[this.Columns.Count - 1];
                if (this.Columns.Count > 1)
                {
                    timeLineColumnPreviousColumn = this.Columns[this.Columns.Count - 2];
                    if (timeLineColumnPreviousColumn.Frozen == false)
                    {
                        timeLineColumnPreviousColumn.Frozen = true;
                    }
                }
            }

            this.AutoResizeColumns();
            this.Width = parentSize.Width - this.Location.X * 2;
        }

        protected override void OnRowsAdded(DataGridViewRowsAddedEventArgs e)
        {
            base.OnRowsAdded(e);
            AllRowsHeight += e.RowCount * this.RowTemplate.Height;
        }

        protected override void OnRowsRemoved(DataGridViewRowsRemovedEventArgs e)
        {
            base.OnRowsRemoved(e);
            AllRowsHeight -= e.RowCount * this.RowTemplate.Height;
        }

        protected override void OnParentChanged(EventArgs e)
        {
            base.OnParentChanged(e);
            this.Parent.ClientSizeChanged += ParentSizeChanged;
        }

        protected override void OnColumnWidthChanged(DataGridViewColumnEventArgs e)
        {
            base.OnColumnWidthChanged(e);
            int width = 0;
            int minimunWidth = 0;
            foreach(DataGridViewColumn column in this.Columns)
            {
                if (column.Name != this.timeLineColumn.Name)
                {
                    width += column.Width;
                    if (column.Name == this.timeLineColumnPreviousColumn.Name)
                    {
                        minimunWidth += column.MinimumWidth;
                    }
                    else
                    {
                        minimunWidth += column.Width;
                    }
                }
            }
            if (timeLineX != width)
            {
                timeLineX = width;
                NotifyPropertyChanged("TimeLineX");
            }
            if (TimeLineMinimumX != minimunWidth)
            {
                TimeLineMinimumX = minimunWidth;
            }
        }

        protected override void OnClientSizeChanged(EventArgs e)
        {
            base.OnClientSizeChanged(e);
            if (!resizingBarRect.IsEmpty)
            {
                resizingBarRect.Height = this.ClientRectangle.Height - this.ColumnHeadersHeight;
            }
        }

        protected override void OnColumnHeadersHeightChanged(EventArgs e)
        {
            base.OnColumnHeadersHeightChanged(e);
            if (! resizingBarRect.IsEmpty)
            {
                resizingBarRect.Y = this.ClientRectangle.Y + this.ColumnHeadersHeight;
                resizingBarRect.Height = this.ClientRectangle.Height - this.ColumnHeadersHeight;
            }
        }

        #endregion

        #region パブリックメソッド

        public void Add(IPresentation presentation)
        {

        }

        public void TimeLineXResizing(object sender, MouseEventArgs e)
        {
            int x = (e.X > this.Width - 3) ? this.Width - 3 : (e.X < this.timeLineMinimumX - 1) ? this.timeLineMinimumX - 1 : e.X;

            this.drawTimeLineResizeBar(x);
        }

        public void TimeLineXResized(object sender, MouseEventArgs e)
        {
            Refresh();
        }

        #endregion

        #region プライベートメソッド

        private void ParentSizeChanged(object sender, EventArgs e)
        {
            if (!parentSize.Equals(this.Parent.ClientSize))
            {
                if (parentSize.Height != this.Parent.ClientSize.Height)
                {
                    parentSize.Height = this.Parent.ClientSize.Height;
                    autoResizeRows();
                }
                if (parentSize.Width != this.Parent.ClientSize.Width)
                {
                    parentSize.Width = this.Parent.ClientSize.Width;
                    this.Width = parentSize.Width - (this.Margin.Left + this.Margin.Right);
                }
            }
        }

        private void autoResizeRows()
        {
            int rowHeight = 0;
            int heightInParent = parentSize.Height - (this.Margin.Top + this.Margin.Bottom);
            switch(RowSizeMode)
            {
                case RowSizeMode.Fix:
                    if (heightInParent < allRowsHeight)
                    {
                        maxRowsHeight = heightInParent - ((heightInParent - this.ColumnHeadersHeight) % this.RowTemplate.Height);
                        this.Height = maxRowsHeight;
                    }
                    else if (this.Height != allRowsHeight)
                    {
                        this.Height = allRowsHeight;
                    }
                    rowHeight = this.RowTemplate.Height;
                    break;

                case RowSizeMode.Fill:
                    if (heightInParent != 0 && this.Rows.Count != 0)
                    {
                        rowHeight = heightInParent / (this.Rows.Count + 1);
                        rowHeight = rowHeight < this.RowTemplate.Height ? this.RowTemplate.Height : rowHeight;
                        maxRowsHeight = heightInParent - ((heightInParent - this.ColumnHeadersHeight) % rowHeight);
                        this.Height = maxRowsHeight;
                    }
                    break;
            }
            foreach (DataGridViewRow row in this.Rows)
            {
                if (row.Height != rowHeight)
                {
                    row.Height = rowHeight;
                }
            }
        }

        protected void NotifyPropertyChanged(string info)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(info));
            }
        }

        private void drawTimeLineResizeBar(int x)
        {
            if (resizingBarRect.IsEmpty)
            {
                resizingBarRect = new Rectangle(x - 1, this.ClientRectangle.Y + this.ColumnHeadersHeight, 3, this.ClientRectangle.Height - this.ColumnHeadersHeight);
            }
            resizingBarRect.X = x;

            Bitmap tmpBmp = new Bitmap(resizingBarRect.Width, resizingBarRect.Height);

            using (Graphics graphics = this.CreateGraphics())
            {
                this.DoubleBuffered = true;
                this.Refresh();
                graphics.FillRectangle(new HatchBrush(HatchStyle.Percent50, Color.Black, Color.Transparent), resizingBarRect);
                this.DoubleBuffered = false;
            }
        }

        #endregion

        #endregion

        #region NativeScrollBar

        private const int WM_HSCROLL = 0x0114;
        private const int WM_VSCROLL = 0x0115;
        private NativeScrollBar nsb;

        protected override void OnHandleCreated(EventArgs e)
        {
            base.OnHandleCreated(e);
            if (this.Site == null)
            {
                this.nsb = new NativeScrollBar(this);
            }
        }
        protected override void OnHandleDestroyed(EventArgs e)
        {
            base.OnHandleDestroyed(e);
            if (this.nsb != null)
            {
                this.nsb.DestroyHandle();
            }
        }
        protected override void WndProc(ref Message m)
        {
            switch (m.Msg)
            {
                case WM_VSCROLL:
                    if (m.LParam != this.VerticalScrollBar.Handle && this.VerticalScrollBar.Visible)
                    {
                        Control.ReflectMessage(this.VerticalScrollBar.Handle, ref m);
                    }
                    break;
                case WM_HSCROLL:
                    if (m.LParam != this.HorizontalScrollBar.Handle && this.HorizontalScrollBar.Visible)
                    {
                        Control.ReflectMessage(this.HorizontalScrollBar.Handle, ref m);
                    }
                    break;
            }
            base.WndProc(ref m);
        }

        #endregion
    }
}
