using System;
using System.IO;
using System.Drawing;
using System.Windows.Forms;
using System.Runtime.Serialization.Formatters.Binary;
using System.Collections.Generic;
using WeifenLuo.WinFormsUI.Docking;
using NU.OJL.MPRTOS.TLV.Architecture.PAC;
using NU.OJL.MPRTOS.TLV.Base;

namespace NU.OJL.MPRTOS.TLV.Core.TimeLineControl
{

    public partial class TimeLineControlP : DockContent, IPresentation
    {
        private TimeLineGridRowSizeMode timeLineGridRowSizeMode = TimeLineGridRowSizeMode.Fix;
        private SortableBindingList<TimeLineViewableObject> bindingList;
        private bool timeLineGridVScrollBarVisibility = false;
        private int timeLineGridVScrollBarMaximunValue = 0; 
        private int n = 0;

        public int ClientWidth
        {
            get { return this.ClientSize.Width - (this.Padding.Left + this.Padding.Right); }
        }
        private int TimeLineGridMinimumHeight
        {
            get { return this.timeLineGrid.AllRowSize + this.timeLineGrid.ColumnHeadersHeight + this.topTimeLine.Height + this.bottomTimeLine.Height + this.timeLineScrollBar.Height; }
        }
        private TimeLineGridRowSizeMode TimeLineGridRowSizeMode
        {
            get { return timeLineGridRowSizeMode; }
            set
            {
                if (timeLineGridRowSizeMode != value)
                {
                    timeLineGridRowSizeMode = value;
                    this.timeLineGrid.RowSizeMode = timeLineGridRowSizeMode;
                    checkTimeLineGridVScrollBarVisiblity();
                }
            }
        }
        private bool TimeLineGridVScrollBarVisibility
        {
            get { return timeLineGridVScrollBarVisibility; }
            set
            {
                timeLineGridVScrollBarVisibility = value;
                if (value)
                {
                    if (this.timeLineGridVScrollBar.Visible == false)
                    {
                        this.timeLineGridVScrollBar.Show();
                        this.timeLineGridVScrollBar.Value = 0;
                    }
                    this.timeLineGridVScrollBar.Minimum = 0;
                    this.timeLineGridVScrollBar.Maximum = this.timeLineGrid.AllRowSize;
                    this.timeLineGridVScrollBar.LargeChange = (this.timeLineGrid.Height - this.timeLineGrid.ColumnHeadersHeight) < 0 ? 0 : (this.timeLineGrid.Height - this.timeLineGrid.ColumnHeadersHeight);
                    this.timeLineGridVScrollBar.SmallChange = this.timeLineGrid.RowTemplate.Height;
                    this.timeLineGridVScrollBarMaximunValue = this.timeLineGridVScrollBar.Maximum - this.timeLineGridVScrollBar.LargeChange;
                    this.timeLineGrid.RowSizeMode = TimeLineGridRowSizeMode.Fill;
                    changeTableLayoutPanelByTimeLineGridRowSizeMode();
                }
                else
                {
                    this.timeLineGrid.DoesDrawLastRow = false;
                    if (this.timeLineGridVScrollBar.Visible)
                    {
                        this.timeLineGridVScrollBar.Hide();
                    }
                    this.timeLineGrid.RowSizeMode = timeLineGridRowSizeMode;
                    changeTableLayoutPanelByTimeLineGridRowSizeMode();
                }
            }
        }

        public TimeLineControlP(string name)
        {
            InitializeComponent();

            this.Name = name;
            this.TabText = "タイムライン";
            this.bindingList = new SortableBindingList<TimeLineViewableObject>();

            this.tableLayoutPanel.DataBindings.Add("Width", this, "ClientWidth");

            this.timeLineGrid.ColumnWidthChanged += new DataGridViewColumnEventHandler(timeLineGridColumnWidthChanged);
            this.timeLineGrid.ColumnHeadersHeightChanged += new EventHandler(timeLineGridColumnHeadersHeightChanged);
            this.timeLineGrid.SizeChanged += new EventHandler(timeLineGridSizeChanged);
            this.timeLineGrid.RowsAdded += new DataGridViewRowsAddedEventHandler(timeLineGridRowsAdded);
            this.timeLineGrid.MouseWheel += new MouseEventHandler(timeLineGridMouseWheel);
            this.topTimeLine.Resized    += new EventHandler(timeLineResized);
            this.topTimeLine.Resizing   += new MouseEventHandler(timeLineResizing);
            this.bottomTimeLine.Resizing += new MouseEventHandler(timeLineResizing);
            this.bottomTimeLine.Resized += new EventHandler(timeLineResized);
            this.panel1.SizeChanged += new EventHandler(panel1SizeChanged);
            this.timeLineGridVScrollBar.ValueChanged += new EventHandler(timeLineGridVScrollBarValueChanged);
            this.timeLineGrid.RowSizeMode = timeLineGridRowSizeMode;
            this.timeLineGrid.DataSource = bindingList;
            this.timeLineGrid.Height = this.timeLineGrid.ColumnHeadersHeight;
        }

        public void AddChild(Control control, object args)
        {

        }

        private void changeTableLayoutPanelByTimeLineGridRowSizeMode()
        {
            switch (this.timeLineGrid.RowSizeMode)
            {
                case TimeLineGridRowSizeMode.Fill:
                    this.tableLayoutPanel.RowStyles[1].Height = 100;
                    this.tableLayoutPanel.RowStyles[1].SizeType = SizeType.Percent;
                    break;
                case TimeLineGridRowSizeMode.Fix:
                    this.tableLayoutPanel.RowStyles[1].SizeType = SizeType.AutoSize;
                    break;
            }
        }

        private bool doesTimeLineGridNeedVScrollBar()
        {
            return (panel1.Height < TimeLineGridMinimumHeight);
        }

        private void checkTimeLineGridVScrollBarVisiblity()
        {
            this.TimeLineGridVScrollBarVisibility = doesTimeLineGridNeedVScrollBar();
            if (this.timeLineGrid.RowSizeMode == TimeLineGridRowSizeMode.Fill && this.timeLineGrid.DoesDrawLastRow)
            {
                this.timeLineGridVScrollBar.Value = this.timeLineGridVScrollBarMaximunValue;
            }
            else
            {
                this.timeLineGrid.DoesDrawLastRow = false;
            }
        }

        #region イベント

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            this.timeLineGridColumnHeadersHeightChanged(this, EventArgs.Empty);
        }

        private void timeLineResizing(object sender, MouseEventArgs e)
        {
            int x = e.X >= this.timeLineGrid.Width - 2 ? this.timeLineGrid.Width - 2 : e.X <= this.timeLineGrid.TimeLinePositionMinimumX + 1 ? this.timeLineGrid.TimeLinePositionMinimumX + 1 : e.X;

            this.timeLineGrid.DrawTimeLineResizeBar(x);
        }

        private void timeLineResized(object sender, EventArgs e)
        {
            this.timeLineGrid.TimeLinePositionX = ((TimeLine)sender).TimeLinePisitionX;
        }

        private void timeLineGridColumnWidthChanged(object sender, DataGridViewColumnEventArgs e)
        {
            this.topTimeLine.TimeLinePisitionX = this.timeLineGrid.TimeLinePositionX;
            this.bottomTimeLine.TimeLinePisitionX = this.timeLineGrid.TimeLinePositionX;
            this.topTimeLine.TimeLinePositionMinimumX = this.timeLineGrid.TimeLinePositionMinimumX;
            this.bottomTimeLine.TimeLinePositionMinimumX = this.timeLineGrid.TimeLinePositionMinimumX;
            this.timeLineScrollBar.Margin = new Padding(this.timeLineGrid.TimeLinePositionX, this.timeLineScrollBar.Margin.Top, this.timeLineScrollBar.Margin.Right, this.timeLineScrollBar.Margin.Bottom);
        }

        private void timeLineGridColumnHeadersHeightChanged(object sender, EventArgs e)
        {
            this.timeLineGridVScrollBar.Margin = new Padding(this.timeLineGridVScrollBar.Margin.Left, this.timeLineGrid.ColumnHeadersHeight, this.timeLineGridVScrollBar.Margin.Right, this.timeLineGridVScrollBar.Margin.Bottom);
        }

        private void timeLineGridSizeChanged(object sender, EventArgs e)
        {
            int height = this.topTimeLine.Height + this.timeLineGrid.Height + this.bottomTimeLine.Height;
            this.topTimeLine.ResizingCursorClip = new Rectangle(new Point(timeLineGrid.TimeLinePositionMinimumX, 0), new Size(this.topTimeLine.Width - timeLineGrid.TimeLinePositionMinimumX, height));
            this.bottomTimeLine.ResizingCursorClip = new Rectangle(new Point(timeLineGrid.TimeLinePositionMinimumX, -1 * ( this.topTimeLine.Height + this.timeLineGrid.Height)), new Size(this.topTimeLine.Width - timeLineGrid.TimeLinePositionMinimumX, height));
        }

        private void timeLineGridRowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            checkTimeLineGridVScrollBarVisiblity();
        }

        private void timeLineGridVScrollBarValueChanged(object sender, EventArgs e)
        {
            this.timeLineGrid.FirstDisplayedScrollingRowIndex = this.timeLineGridVScrollBar.Value / this.timeLineGrid.RowTemplate.Height;
            this.timeLineGrid.DoesDrawLastRow = (this.timeLineGridVScrollBar.Value >= timeLineGridVScrollBarMaximunValue);
        }

        private void timeLineGridMouseWheel(object sender, MouseEventArgs e)
        {
            if(this.timeLineGridVScrollBarVisibility)
            {
                int delta = -1 * e.Delta / 120 * this.timeLineGridVScrollBar.SmallChange;
                this.timeLineGridVScrollBar.Value = this.timeLineGridVScrollBar.Value + delta < this.timeLineGridVScrollBar.Minimum ?
                    0 : this.timeLineGridVScrollBar.Value + delta > this.timeLineGridVScrollBarMaximunValue ?
                    this.timeLineGridVScrollBarMaximunValue : this.timeLineGridVScrollBar.Value + delta;
            }
        }

        #endregion

        private void button1_Click(object sender, EventArgs e)
        {
            switch (timeLineGridRowSizeMode)
            {
                case TimeLineGridRowSizeMode.Fill:
                    this.TimeLineGridRowSizeMode = TimeLineGridRowSizeMode.Fix;
                    break;
                case TimeLineGridRowSizeMode.Fix:
                    this.TimeLineGridRowSizeMode = TimeLineGridRowSizeMode.Fill;
                    break;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {

            bindingList.Add(new TimeLineViewableObject("test" + (++n).ToString()));

        }

        void panel1SizeChanged(object sender, EventArgs e)
        {
            checkTimeLineGridVScrollBarVisiblity();
        }

    }

    [Serializable]
    public class TimeLineViewableObject
    {
        public string Name { get; set; }
        public int Value { get; set; }
        public string Paths { get; set; }
        public TimeLineViewableObject(string name)
        {
            this.Name = name;
            this.Value = 100;
            this.Paths = "test" + this.Name + this.Value.ToString();
        }

        public TimeLineViewableObject DeepClone()
        {
            TimeLineViewableObject target = null;
            using (MemoryStream stream = new MemoryStream())
            {
                BinaryFormatter formatter = new BinaryFormatter();
                formatter.Serialize(stream, this);
                stream.Position = 0;

                target = (TimeLineViewableObject)formatter.Deserialize(stream);
            }

            return target;
        }

    }

}
