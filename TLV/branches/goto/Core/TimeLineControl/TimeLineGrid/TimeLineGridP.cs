using System;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.ComponentModel;
using System.Collections.Generic;
using System.Collections;
using System.Data;
using System.Reflection;
using NU.OJL.MPRTOS.TLV.Base;
using NU.OJL.MPRTOS.TLV.Architecture.PAC;
using NU.OJL.MPRTOS.TLV.Core.Base;
using NU.OJL.MPRTOS.TLV.Core.ViewableObject;

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
        private ulong minimumTime = ulong.MaxValue;
        private ulong maximumTime = ulong.MinValue;
        private ulong beginTime = 0;
        private ulong endTime = 0;
        private ulong displayTimeLength = 0;
        private ulong nsPerScaleMark = 1;
        private ulong maximumNsPerScaleMark = 1;
        private int pixelPerScaleMark = 5;
        private bool isMouseEnter = false;
        private Color nowMarkerColor;
        private ulong nowMarkerTime = 0;
        private int maxRowHeight = 0;
        private int minRowHeight = 0;
        private int rowHeight = 0;
        private int ownBeginGrabRowIndex = -1;
        private bool dropDestinationIsValid;
        private bool dropDestinationIsNextRow;
        private int dropDestinationRowIndex;
        private Type viewableObjectType = typeof(TimeLineViewableObject);
        private bool isfirstAddedRow = true;
        private PropertyDescriptorCollection viewableObjectTypePdc = null;
        private CursorMode cursorMode = CursorMode.Default;
        private Point mouseDownPoint = new Point(0, 0);
        private Point mouseMoveLastPoint = new Point(0, 0);
        private ContextMenuStrip contextMenuStrip = new ContextMenuStrip();
            
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
                        for (int i = 2; i <= this.Columns.Count; i++ )
                        {
                            if (this.Columns[this.Columns.Count - i].Visible)
                            {
                                this.Columns[this.Columns.Count - i].Width += delta;
                                break;
                            }
                        }
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
                    timeLineWidth = value <= 0 ? 10 : value;
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
                    EndTime = value + DisplayTimeLength;

                }
            }
        }
        public ulong EndTime
        {
            get { return endTime; }
            set
            {
                if (endTime != value)
                {
                    endTime = value;
                }
            }
        }
        public ulong NowMarkerTime
        {
            get { return nowMarkerTime; }
            set
            {
                if (nowMarkerTime != value)
                {
                    nowMarkerTime = value;
                    drawNowMarker(nowMarkerTime);
                    NotifyPropertyChanged("NowMarkerTime");
                }
            }
        }
        public Color NowMarkerColor
        {
            get { return nowMarkerColor; }
            set
            {
                if (!nowMarkerColor.Equals(value))
                {
                    nowMarkerColor = value;
                    NotifyPropertyChanged("NowMarkerColor");
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
                    EndTime = value + BeginTime;
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

                    NotifyPropertyChanged("NsPerScaleMark");

                    dispalyTimeLengthReCalc();
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
                    NotifyPropertyChanged("PixelPerScaleMark");

                    dispalyTimeLengthReCalc();
                    MaximumNsPerScaleMark = (ulong)(((decimal)(MaximumTime - MinimumTime) / (decimal)TimeLineWidth) * (decimal)pixelPerScaleMark);

                }
            }
        }
        public int NowRowHeight
        {
            get { return rowHeight; }
            set
            {
                if (rowHeight != value && value != 0)
                {
                    rowHeight = value;
                    AllRowsHeight = rowHeight * this.Rows.Count + this.ColumnHeadersHeight;

                    NotifyPropertyChanged("NowRowHeight");
                }
            }
        }
        public int MaxRowHeight
        {
            get { return maxRowHeight; }
            set
            {
                if (maxRowHeight != value)
                {
                    maxRowHeight = value;
                    NotifyPropertyChanged("MaxRowHeight");
                }
            }
        }
        public int MinRowHeight
        {
            get { return minRowHeight; }
            set
            {
                if (minRowHeight != value)
                {
                    minRowHeight = value;

                    NotifyPropertyChanged("MinRowHeight");
                }
            }
        }
        public PropertyDescriptorCollection ViewableObjectTypePdc
        {
            get { return viewableObjectTypePdc; }
            set
            {
                if(value != null && ! value.Equals(viewableObjectTypePdc))
                {
                    viewableObjectTypePdc = value;
                }
            }
        }
        public Type ViewableObjectType
        {
            get { return viewableObjectType; }
            set
            {
                if (value != null && !value.Equals(viewableObjectType))
                {
                    if(value.BaseType != typeof(TimeLineViewableObject))
                    {
                        throw new Exception("ViewableObjectTypeはTimeLineViewableObjectのサブクラスでなければなりません");
                    }

                    viewableObjectType = value;

                    Type tlelType = typeof(SortableBindingList<>);

                    this.Columns.Clear();
                    contextMenuStrip.Items.Clear();

                    ViewableObjectTypePdc = PropertyDescriptorCollectionUtils.ConvertToPropertyDisplayPropertyDescriptor(TypeDescriptor.GetProperties(viewableObjectType));

                    List<DataGridViewColumn> columns = new List<DataGridViewColumn>();

                    foreach (PropertyDisplayPropertyDescriptor pd in viewableObjectTypePdc)
                    {
                        Type type = pd.PropertyType;
                        string name = pd.Name;
                        string headerText = pd.DisplayName;
                        bool browsable = pd.DefaultBrowsable;

                        if (type != typeof(TimeLineEvents))
                        {
                            DataGridViewTextBoxColumn column = new DataGridViewTextBoxColumn();
                            column.Name = name;
                            column.ValueType = type;
                            column.DataPropertyName = name;
                            column.HeaderText = headerText;
                            column.CellTemplate = new DataGridViewTextBoxCell();
                            columns.Add(column);

                            ToolStripMenuItem newcontitem = new ToolStripMenuItem();
                            newcontitem.Text = headerText;
                            newcontitem.Name = name + "ContextMenuStrip";
                            newcontitem.Checked = browsable;
                            contextMenuStrip.Items.Add(newcontitem);
                            column.Visible = browsable;
                            newcontitem.Click += delegate
                            {
                                if (Columns[column.Name].Visible)
                                {
                                    Columns[column.Name].Visible = false;
                                    newcontitem.Checked = false;
                                }
                                else
                                {
                                    Columns[column.Name].Visible = true;
                                    newcontitem.Checked = true;
                                }
                            };
                        }

                    }
                    columns.Add(timeLineColumn);

                    timeLineColumnPreviousColumn = columns[columns.Count - 2];

                    foreach (DataGridViewColumn column in columns)
                    {
                        try
                        {
                            this.Columns.Add(column);
                        }
                        catch (InvalidOperationException e)
                        {
                            System.Console.WriteLine(e.Message);
                        }
                    }

                    timeLineColumnPreviousColumn.Frozen = true;

                    this.AutoResizeColumns();
                    this.Width = parentSize.Width - this.Location.X * 2;
                }
            }
        }
        public Object ViewableObjectDataSource
        {
            get { return DataSource; }
            set { DataSource = value; }
        }
        public CursorMode CursorMode
        {
            get { return cursorMode; }
            set
            {
                if (!value.Equals(cursorMode))
                {
                    cursorMode = value;

                    NotifyPropertyChanged("CursorMode");
                }
            }
        }

        #endregion

        #region イベント

        public event PropertyChangedEventHandler PropertyChanged = null;
        public event ViewableObjectAddHandler AddViewableObject = null;
        public event ViewableObjectRemoveAtHandler RemoveAtViewableObject = null;
        public event ViewableObjectInsertHandler InsertViewableObject = null;
        public event ViewableObjectGetHandler GetViewableObject = null;
        public event ViewableObjectIndexOfHandler IndexOfViewableObject = null;

        #endregion

        #region コンストラクタ

        public TimeLineGridP(string name)
        {

            #region スーパークラスプロパティ初期化

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
            this.ColumnHeadersHeight = 20;
            this.RowTemplate.Height = 25;
            this.rowHeight = this.RowTemplate.Height;
            this.AllowDrop = true;
            this.AllowUserToOrderColumns = true;
            this.AllowUserToAddRows = false;
            this.AllowUserToDeleteRows = false;
            this.AllowUserToResizeColumns = true;
            this.AutoGenerateColumns = false;
            this.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            this.VirtualMode = true;
            #endregion

            #region プロパティ初期化

            this.Name = name;
            this.AllRowsHeight = this.ColumnHeadersHeight;
            this.ContextMenuStrip = contextMenuStrip;
            this.DefaultCellStyle.SelectionForeColor = this.DefaultCellStyle.ForeColor;

            #endregion

            #region timeLineColumnプロパティ初期化

            timeLineColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            timeLineColumn.HeaderText = "タイムライン";

            #endregion
        }

        #endregion

        #region メソッド

        #region オーバライドメソッド

        protected override void OnRowsAdded(DataGridViewRowsAddedEventArgs e)
        {
            base.OnRowsAdded(e);

            AllRowsHeight += e.RowCount * rowHeight;

            TimeLineEvents tes = (TimeLineEvents)this.Rows[e.RowIndex].Cells[timeLineColumn.Name].Value;
            ulong st = tes.StartTime;
            ulong et = tes.EndTime;
            if (MinimumTime > st)
            {
                MinimumTime = st;
            }
            if (MaximumTime < et)
            {
                MaximumTime = et;
            }

            if (this.Rows.Count == 1 && isfirstAddedRow)
            {
                beginTimeDisplayTimeLengthReCalc();
                isfirstAddedRow = false;
            }

        }

        protected override void OnRowsRemoved(DataGridViewRowsRemovedEventArgs e)
        {
            base.OnRowsRemoved(e);

            AllRowsHeight -= e.RowCount * rowHeight;

            ulong st = ulong.MaxValue;
            ulong et = ulong.MinValue;

            foreach (DataGridViewRow row in this.Rows)
            {
                TimeLineEvents te = (TimeLineEvents)row.Cells[timeLineColumn.Name].Value;
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

            this.MinimumTime = st == ulong.MaxValue ? this.MinimumTime : st;
            this.MaximumTime = et == ulong.MinValue ? this.MaximumTime : et;
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

            for (int i = 2; i <= this.Columns.Count; i++)
            {
                if (this.Columns[this.Columns.Count - i].Visible)
                {
                    timeLineColumnPreviousColumn = this.Columns[this.Columns.Count - i];
                    break;
                }
            }

            foreach (DataGridViewColumn column in this.Columns)
            {
                if (column.Visible)
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

        protected override void OnMouseLeave(EventArgs e)
        {
            if (isMouseEnter)
            {
                Cursor.Show();
                this.Cursor = Cursors.Default;
                isMouseEnter = false;
                this.Refresh();
                NowMarkerTime = 0;
            }
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);

            if ((e.Button & MouseButtons.Left) == MouseButtons.Left)
            {
                if(ownBeginGrabRowIndex != -1)
                {
                    this.DoDragDrop(ownBeginGrabRowIndex, DragDropEffects.Move);
                }

                if (e.X < this.Width && e.X > this.TimeLineX && e.Y > this.ColumnHeadersHeight && e.Y < this.Height)
                {
                    if (CursorMode != CursorMode.Hand)
                    {
                        NowMarkerTime = xToTime(e.X - timeLineX);
                    }
                    else
                    {
                        moveTimeToX(xToTime(mouseMoveLastPoint.X), e.X);
                        mouseMoveLastPoint = e.Location;
                    }
                }
                else
                {

                }
            }
            else
            {
                if (e.X < this.Width && e.X > this.TimeLineX && e.Y > this.ColumnHeadersHeight && e.Y < this.Height)
                {
                    NowMarkerTime = xToTime(e.X - timeLineX);
                }
            }

            if (e.X < this.Width && e.X > this.TimeLineX && e.Y > this.ColumnHeadersHeight && e.Y < this.Height)
            {
                if (!isMouseEnter)
                {
                    this.Cursor = CursorMode.Cursor();
                    isMouseEnter = true;
                }
            }
            else
            {
                NowMarkerTime = 0;
                OnMouseLeave(EventArgs.Empty);
            }

        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            ownBeginGrabRowIndex = -1;

            mouseDownPoint = e.Location;

            if ((e.Button & MouseButtons.Left) == MouseButtons.Left)
            {
                if (e.X < this.Width && e.X > this.TimeLineX && e.Y > this.ColumnHeadersHeight && e.Y < this.Height)
                {
                    switch (cursorMode)
                    {
                        case CursorMode.Hand:
                            this.Cursor = CursorMode.Cursor(true);
                            mouseMoveLastPoint = mouseDownPoint;
                            NowMarkerTime = 0;
                            break;

                        case CursorMode.ZoomIn:
                            onZoomIn(e.X - TimeLineX);
                            break;

                        case CursorMode.ZoomOut:
                            onZoomOut(e.X - TimeLineX);
                            break;

                        default:
                            break;
                    }
                }
                else
                {
                    DataGridView.HitTestInfo hit = this.HitTest(e.X, e.Y);

                    if (hit.Type == DataGridViewHitTestType.Cell)
                    {
                        CurrentCell = this[hit.ColumnIndex, hit.RowIndex];
                        ownBeginGrabRowIndex = hit.RowIndex;
                    }
                }
            }

            if (e.X < this.Width && e.X > this.TimeLineX && e.Y > this.ColumnHeadersHeight && e.Y < this.Height)
            {
            }
            else
            {
                base.OnMouseDown(e);
            }
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);
            if ((e.Button & MouseButtons.Left) == MouseButtons.Left)
            {
                if (e.X < this.Width && e.X > this.TimeLineX && e.Y > this.ColumnHeadersHeight && e.Y < this.Height)
                {
                    switch (cursorMode)
                    {
                        case CursorMode.Hand:
                            this.Cursor = CursorMode.Cursor(false);
                            break;

                        default:
                            break;
                    }
                }
                else
                {

                }
            }
        }

        protected override void OnScroll(ScrollEventArgs e)
        {
            base.OnScroll(e);
            Refresh();
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

                this.autoResizeRows();

                this.CurrentCell = this[this.CurrentCell.ColumnIndex, to];
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

        protected override void OnRowPrePaint(DataGridViewRowPrePaintEventArgs e)
        {
            e.PaintParts &= ~DataGridViewPaintParts.Focus;
            e.PaintParts &= ~DataGridViewPaintParts.SelectionBackground;
            e.PaintParts &= ~DataGridViewPaintParts.ContentBackground;

            base.OnRowPrePaint(e);
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
            int rh = rowHeight;
            int heightInParent = parentSize.Height - (this.Margin.Top + this.Margin.Bottom);
            if (heightInParent != 0)
            {
                switch(RowSizeMode)
                {
                    case RowSizeMode.Fix:
                        if (heightInParent < allRowsHeight)
                        {
                            maxRowsHeight = (int)((decimal)heightInParent - ((decimal)(heightInParent - this.ColumnHeadersHeight) % (decimal)rh));
                            this.Height = maxRowsHeight;
                        }
                        else if (this.Height != allRowsHeight)
                        {
                            this.Height = allRowsHeight;
                        }
                        break;

                    case RowSizeMode.Fill:
                        if (this.Rows.Count != 0)
                        {
                            rh = (int)Math.Floor((double)(heightInParent - this.ColumnHeadersHeight) / (double)this.Rows.Count);
                            rh = rh < MinRowHeight ? MinRowHeight : rh;
                            int num = (int)Math.Floor((double)(heightInParent - this.ColumnHeadersHeight) / (double)rh);
                            num = num > this.Rows.Count ? this.Rows.Count : num;
                            maxRowsHeight = rh * num + this.ColumnHeadersHeight;
                            NowRowHeight = rh;
                            this.Height = maxRowsHeight;
                        }
                        break;
                }
            }
            foreach (DataGridViewRow row in this.Rows)
            {
                if (row.Height != rh)
                {
                    row.Height = rh;
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
            if(this.Rows.Count != 0)
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
        }

        private void drawNowMarker(ulong time)
        {
            Rectangle backRect = new Rectangle(
                this.TimeLineX,
                this.ColumnHeadersHeight,
                this.ClientSize.Width - this.TimeLineX,
                this.ClientSize.Height - this.ColumnHeadersHeight
                );

            Bitmap tmpBmp = new Bitmap(backRect.Width, backRect.Height);
            using (Graphics tmpBmpGraphics = Graphics.FromImage(tmpBmp))
            {
                float x = timeToX(time);
                using (Pen pen = new Pen(NowMarkerColor))
                {
                    //pen.DashStyle = DashStyle.Dash;
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
                if (((decimal)beginTime - ((decimal)(beginTime + tmpDisplayTimeLength) - (decimal)MaximumTime)) > (decimal)minimumTime)
                {
                    BeginTime -= ((beginTime + tmpDisplayTimeLength) - MaximumTime);
                    DisplayTimeLength = tmpDisplayTimeLength;
                }
                else
                {
                    BeginTime = minimumTime;
                    DisplayTimeLength = MaximumTime - BeginTime;
                    nsPerScaleMark = (ulong)(((decimal)DisplayTimeLength / (decimal)TimeLineWidth) * (decimal)pixelPerScaleMark);
                    NotifyPropertyChanged("NsPerScaleMark");
                }
            }
            else
            {
                DisplayTimeLength = tmpDisplayTimeLength;
            }
        }

        private ulong xToTime(int x)
        {
            return (ulong)(((decimal)x * ((decimal)nsPerScaleMark / (decimal)pixelPerScaleMark)) + (decimal)beginTime);
        }

        private float timeToX(ulong t)
        {
            return (float)(((decimal)t - (decimal)beginTime) / ((decimal)nsPerScaleMark / (decimal)pixelPerScaleMark));
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

            TimeLineViewableObject tvo = GetViewableObject(ViewableObjectDataSource, from).DeepClone();

            RemoveAtViewableObject(ViewableObjectDataSource, from);

            if (to > from)
            {
                to--;
            }
            if (next)
            {
                to++;
            }
            if (to <= (this.Rows.Count))
            {
                InsertViewableObject(tvo, ViewableObjectDataSource, to);
            }
            else
            {
                AddViewableObject(tvo, ViewableObjectDataSource);
            }

            return IndexOfViewableObject(tvo, ViewableObjectDataSource);
 
        }

        private void onZoomIn(int x)
        {
            ulong time = xToTime(x);
            NsPerScaleMark = (ulong)((double)nsPerScaleMark * 0.75d);
            moveTimeToX(time, x);
        }

        private void onZoomOut(int x)
        {
            ulong time = xToTime(x);
            if (nsPerScaleMark == 1)
            {
                NsPerScaleMark = 2;
            }
            else
            {
                NsPerScaleMark = (ulong)((double)nsPerScaleMark * 1.5d);
            }
            moveTimeToX(time, x);
        }

        private void moveTimeToX(ulong time, int x)
        {
            ulong xTime = xToTime(x);
            ulong nowTime = 0;

            decimal delta = (decimal)time - (decimal)xTime;

            decimal tmp = (decimal)BeginTime + delta;

            if (tmp < (decimal)minimumTime)
            {
                BeginTime = minimumTime;
                nowTime = xToTime(x);
            }
            else if (tmp + DisplayTimeLength > (decimal)maximumTime)
            {
                BeginTime = maximumTime - DisplayTimeLength;
                nowTime = xToTime(x);
            }
            else
            {
                BeginTime = (ulong)tmp;
                nowTime = time;
            }

            if(CursorMode != CursorMode.Hand)
            {
                NowMarkerTime = 0;
                NowMarkerTime = nowTime;
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
