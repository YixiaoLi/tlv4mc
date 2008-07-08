using System;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.ComponentModel;
using NU.OJL.MPRTOS.TLV.Base;
using NU.OJL.MPRTOS.TLV.Architecture.PAC;
using NU.OJL.MPRTOS.TLV.Core.Base;

namespace NU.OJL.MPRTOS.TLV.Core.TimeLineControl.TimeLineGrid
{
    public partial class TimeLineGridP : DataGridView, IPresentation
    {
        #region メンバ

        private TimeLineColumn timeLineColumn = new TimeLineColumn();
        private DataGridViewColumn timeLineColumnPreviousColumn = new DataGridViewColumn();
        private RowSizeMode rowSizeMode = RowSizeMode.Fix;
        private Size parentSize = new Size(0,0);
        Rectangle resizingBarRect = Rectangle.Empty;
        private int allRowsHeight = 0;
        private int maxRowsHeight = 0;
        private int timeLineX = 0;
        private int timeLineMinimumX = 0;
        private int timeLineWidth = 0;
        private ulong minimumTime = 0;
        private ulong maximumTime = 0;
        private ulong beginTime = 0;
        private ulong displayTimeLength = 0;
        private ulong nsPerScaleMark = 1;
        private ulong maximumNsPerScaleMark = 1;
        private int pixelPerScaleMark = 5;
        public bool Edited = false;
            
        #endregion

        #region プロパティ

        public HScrollBar HScrollBar { get; set; }
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
        public int TimeLineWidth
        {
            get { return timeLineWidth; }
            set
            {
                if (timeLineWidth != value)
                {
                    timeLineWidth = value;
                    dispalyTimeLengthReCalc();
                    MaximumNsPerScaleMark = (ulong)(((decimal)(MaximumTime - MinimumTime) / (decimal)TimeLineWidth) * (decimal)pixelPerScaleMark);
                    NotifyPropertyChanged("TimeLineWidth");
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
        public ulong MinimumTime
        {
            get { return minimumTime; }
            set
            {
                if (minimumTime != value)
                {
                    minimumTime = value;
                    NotifyPropertyChanged("MinimumTime");
                }
            }
        }
        public ulong MaximumTime
        {
            get { return maximumTime; }
            set
            {
                if (maximumTime != value)
                {
                    maximumTime = value;
                    NotifyPropertyChanged("MaximumTime");
                }
            }
        }
        public ulong BeginTime
        {
            get { return beginTime; }
            set
            {
                if (beginTime != value)
                {
                    beginTime = value;
                    NotifyPropertyChanged("BeginTime");
                }
            }
        }
        public ulong DisplayTimeLength
        {
            get { return displayTimeLength; }
            set
            {
                if (displayTimeLength != value)
                {
                    displayTimeLength = value;
                    NotifyPropertyChanged("DisplayTimeLength");
                }
            }
        }
        public ulong NsPerScaleMark
        {
            get { return nsPerScaleMark; }
            set
            {
                if (nsPerScaleMark != value && value != 0)
                {
                    nsPerScaleMark = value;

                    dispalyTimeLengthReCalc();

                    NotifyPropertyChanged("NsPerScaleMark");
                }
            }
        }
        public ulong MaximumNsPerScaleMark
        {
            get { return maximumNsPerScaleMark; }
            set
            {
                if (maximumNsPerScaleMark != value)
                {
                    maximumNsPerScaleMark = value;
                    NotifyPropertyChanged("MaximumNsPerScaleMark");
                }
            }
        }
        public int PixelPerScaleMark
        {
            get { return pixelPerScaleMark; }
            set
            {
                if (pixelPerScaleMark != value)
                {
                    pixelPerScaleMark = value;

                    dispalyTimeLengthReCalc();
                    MaximumNsPerScaleMark = (ulong)(((decimal)(MaximumTime - MinimumTime) / (decimal)TimeLineWidth) * (decimal)pixelPerScaleMark);

                    NotifyPropertyChanged("PixelPerScaleMark");
                }
            }
        }

        #endregion

        #region イベント

        public event PropertyChangedEventHandler PropertyChanged = null;
        public event EventHandler RowChanged = null;

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
            this.RowChanged += onRowChanged;

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

            if (e.Column.ValueType == typeof(TimeLineEvents))
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

            RowChanged(this, EventArgs.Empty);
        }

        protected override void OnRowsRemoved(DataGridViewRowsRemovedEventArgs e)
        {
            base.OnRowsRemoved(e);
            AllRowsHeight -= e.RowCount * this.RowTemplate.Height;

            RowChanged(this, EventArgs.Empty);
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
            int minimumWidth = 0;
            foreach(DataGridViewColumn column in this.Columns)
            {
                if (column.Name != this.timeLineColumn.Name)
                {
                    width += column.Width;
                    if (column.Name == this.timeLineColumnPreviousColumn.Name)
                    {
                        minimumWidth += column.MinimumWidth;
                    }
                    else
                    {
                        minimumWidth += column.Width;
                    }
                }
            }
            if (timeLineX != width)
            {
                timeLineX = width;
                NotifyPropertyChanged("TimeLineX");
            }
            if (TimeLineMinimumX != minimumWidth)
            {
                TimeLineMinimumX = minimumWidth;
            }

            TimeLineWidth = this.Width - timeLineX;
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

        protected override void OnSorted(EventArgs e)
        {
            base.OnSorted(e);
            if (Edited == false)
            {
                Edited = true;
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
                        rowHeight = (heightInParent - this.ColumnHeadersHeight) / this.Rows.Count;
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

        private void onRowChanged(object sender, EventArgs e)
        {
            minTimeMaxTimeReCalc();
            if (Edited == false)
            {
                beginTimeDisplayTimeLengthReCalc();
            }
        }

        private void beginTimeDisplayTimeLengthReCalc()
        {
            if (this.Rows.Count > 0)
            {
                BeginTime = MinimumTime;
                if (MaximumTime > BeginTime)
                {
                    DisplayTimeLength = MaximumTime - BeginTime;
                    MaximumNsPerScaleMark = (ulong)(((decimal)(MaximumTime - MinimumTime) / (decimal)TimeLineWidth) * (decimal)pixelPerScaleMark);
                    nsPerScaleMark = (ulong)(((decimal)DisplayTimeLength / (decimal)TimeLineWidth) * (decimal)pixelPerScaleMark);
                    NotifyPropertyChanged("NsPerScaleMark");
                }
            }
        }

        private void dispalyTimeLengthReCalc()
        {
            ulong tmpDisplayTimeLength = (ulong)((decimal)nsPerScaleMark * ((decimal)TimeLineWidth / (decimal)pixelPerScaleMark));
            if (beginTime + tmpDisplayTimeLength > MaximumTime)
            {
                DisplayTimeLength = MaximumTime - BeginTime;
                nsPerScaleMark = (ulong)(((decimal)DisplayTimeLength / (decimal)TimeLineWidth) * (decimal)pixelPerScaleMark);
                NotifyPropertyChanged("NsPerScaleMark");
            }
            else
            {
                DisplayTimeLength = tmpDisplayTimeLength;
            }
        }

        private void minTimeMaxTimeReCalc()
        {
            if (this.Rows.Count > 0)
            {
                ulong st = ulong.MaxValue;
                ulong et = ulong.MinValue;

                for (int i = 0; i < this.Rows.Count; i++ )
                {
                    TimeLineEvents te = (TimeLineEvents)this[timeLineColumn.Name, i].Value;
                    if (te != null)
                    {
                        if (te.StartTime < st)
                        {
                            st = te.StartTime;
                        }
                        if (te.EndTime > et)
                        {
                            et = te.EndTime;
                        }
                    }
                }

                this.MinimumTime = st;
                this.MaximumTime = et;
            }
        }

        #endregion

        #endregion

        #region NativeScrollBar

        private const int WM_HSCROLL = 0x0114;
        private const int WM_VSCROLL = 0x0115;
        private const int SB_LINELEFT = 0;
        private const int SB_LINERIGHT = 1;
        private const int SB_PAGELEFT = 2;
        private const int SB_PAGERIGHT = 3;
        private const int SB_THUMBPOSITION = 4;
        private const int SB_THUMBTRACK = 5;
        private const int SB_LEFT = 6;
        private const int SB_RIGHT = 7;
        private const int SB_ENDSCROLL = 8;

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
                    if (m.LParam != VerticalScrollBar.Handle && VerticalScrollBar.Visible)
                    {
                        Control.ReflectMessage(this.VerticalScrollBar.Handle, ref m);
                    }
                    break;
                case WM_HSCROLL:
                    if (HScrollBar != null)
                    {
                        if (m.LParam != HScrollBar.Handle && HScrollBar.Visible)
                        {
                            int sb = m.WParam.ToInt32();

                            if ((decimal)HScrollBar.Value + (decimal)HScrollBar.SmallChange > (decimal)HScrollBar.Maximum && sb == SB_LINERIGHT)
                            {
                                HScrollBar.Value = HScrollBar.Maximum - HScrollBar.LargeChange;
                            }
                            else if ((decimal)HScrollBar.Value + (decimal)HScrollBar.LargeChange > (decimal)HScrollBar.Maximum && sb == SB_PAGERIGHT)
                            {
                                HScrollBar.Value = HScrollBar.Maximum - HScrollBar.LargeChange;
                            }
                            else if ((decimal)HScrollBar.Value - (decimal)HScrollBar.SmallChange < (decimal)HScrollBar.Minimum && sb == SB_LINELEFT)
                            {
                                HScrollBar.Value = HScrollBar.Minimum;
                            }
                            else if ((decimal)HScrollBar.Value - (decimal)HScrollBar.LargeChange < (decimal)HScrollBar.Minimum && sb == SB_PAGELEFT)
                            {
                                HScrollBar.Value = HScrollBar.Minimum;
                            }
                            else
                            {
                                Control.ReflectMessage(this.HScrollBar.Handle, ref m);
                            }
                        }
                    }
                    break;
            }
            base.WndProc(ref m);
        }

        #endregion
    }
}
