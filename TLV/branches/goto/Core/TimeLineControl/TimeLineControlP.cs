using System;
using System.IO;
using System.Drawing;
using System.ComponentModel;
using System.Windows.Forms;
using System.Runtime.Serialization.Formatters.Binary;
using System.Collections.Generic;
using WeifenLuo.WinFormsUI.Docking;
using NU.OJL.MPRTOS.TLV.Architecture.PAC;
using NU.OJL.MPRTOS.TLV.Base;
using NU.OJL.MPRTOS.TLV.Architecture.PAC.Bace;
using NU.OJL.MPRTOS.TLV.Core.TimeLineControl.TimeLineGrid;
using NU.OJL.MPRTOS.TLV.Core.Properties;
using NU.OJL.MPRTOS.TLV.Core.Base;
using NU.OJL.MPRTOS.TLV.Core.ViewableObject;
using NU.OJL.MPRTOS.TLV.Core.ViewableObject.KernelObject.TaskInfo;

namespace NU.OJL.MPRTOS.TLV.Core.TimeLineControl
{
    public partial class TimeLineControlP : DockContent, IPresentation
    {
        private RowSizeMode rowSizeMode;
        private ulong nsPerScaleMark = 1;
        private ulong maximumNsPerScaleMark = 1;
        private int pixelPerScaleMark;
        private int maxRowHeight = 0;
        private int minRowHeight = 0;
        private int rowHeight = 0;
        private Type viewableObjectType = typeof(TimeLineViewableObject);
        private object viewableObjectDataSource;
        private CursorMode cursorMode = CursorMode.Default;

        public event PropertyChangedEventHandler PropertyChanged = null;
        public event ViewableObjectAddHandler AddViewableObject = null;
        public event ViewableObjectRemoveAtHandler RemoveAtViewableObject = null;
        public event ViewableObjectInsertHandler InsertViewableObject = null;
        public event ViewableObjectGetHandler GetViewableObject = null;
        public event ViewableObjectIndexOfHandler IndexOfViewableObject = null;

        public ToolStripContentPanel ContentPanel { get { return this.toolStripContainer.ContentPanel; } }
        public RowSizeMode RowSizeMode
        {
            get { return rowSizeMode; }
            set
            {
                if(rowSizeMode != value)
                {
                    rowSizeMode = value;
                    fillFixRowButtonIconChange();
                    NotifyPropertyChanged("RowSizeMode");
                }
            }
        }
        public ulong NsPerScaleMark
        {
            get { return nsPerScaleMark; }
            set
            {
                if (nsPerScaleMark != value)
                {
                    nsPerScaleMark = value;

                    this.nsPerScaleMarkTrackBar.Value = nsPerScaleMark > (ulong)nsPerScaleMarkTrackBar.Maximum ? nsPerScaleMarkTrackBar.Maximum : nsPerScaleMark < (ulong)nsPerScaleMarkTrackBar.Minimum ? nsPerScaleMarkTrackBar.Minimum : (int)nsPerScaleMark;

                    this.nsPerScaleMarkButton.Text = this.nsPerScaleMarkTrackBar.PreFixText + value + this.nsPerScaleMarkTrackBar.PostFixText;

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

                    this.nsPerScaleMarkTrackBar.Maximum = maximumNsPerScaleMark > int.MaxValue ? int.MaxValue : maximumNsPerScaleMark < (ulong)nsPerScaleMarkTrackBar.Minimum ? nsPerScaleMarkTrackBar.Minimum : (int)maximumNsPerScaleMark;
                    
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

                    this.pixelPerScaleMarkButtonTrackBar.Value = pixelPerScaleMark;

                    this.pixelPerScaleMarkButton.Text = this.pixelPerScaleMarkButtonTrackBar.PreFixText + value + this.pixelPerScaleMarkButtonTrackBar.PostFixText;

                    NotifyPropertyChanged("PixelPerScaleMark");
                }
            }
        }
        public int RowHeight
        {
            get { return rowHeight; }
            set
            {
                if (rowHeight != value)
                {
                    rowHeight = value;

                    this.rowHeightTrackBar.Value = rowHeight;

                    this.rowHeightButton.Text = this.rowHeightTrackBar.PreFixText + value + this.rowHeightTrackBar.PostFixText;

                    NotifyPropertyChanged("RowHeight");
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

                    this.rowHeightTrackBar.Maximum = maxRowHeight;

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

                    this.rowHeightTrackBar.Minimum = minRowHeight;

                    NotifyPropertyChanged("MinRowHeight");
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
                    viewableObjectType = value;
                    NotifyPropertyChanged("ViewableObjectType");
                }
            }
        }
        public Object ViewableObjectDataSource
        {
            get { return viewableObjectDataSource; }
            set { viewableObjectDataSource = value; }
        }
        public CursorMode CursorMode
        {
            get { return cursorMode; }
            set
            {
                if (!value.Equals(cursorMode))
                {
                    cursorMode = value;

                    cursorButton.Checked = false;
                    zoomOutButton.Checked = false;
                    zoomInButton.Checked = false;
                    zoomSelectButton.Checked = false;
                    handButton.Checked = false;

                    switch (cursorMode)
                    {
                        case CursorMode.Default:
                            cursorButton.Checked = true;
                            break;

                        case CursorMode.ZoomIn:
                            zoomInButton.Checked = true;
                            break;

                        case CursorMode.ZoomOut:
                            zoomOutButton.Checked = true;
                            break;

                        case CursorMode.ZoomSelect:
                            zoomSelectButton.Checked = true;
                            break;

                        case CursorMode.Hand:
                            handButton.Checked = true;
                            break;
                    }

                    NotifyPropertyChanged("CursorMode");
                }
            }
        }

        public TimeLineControlP(string name)
        {
            InitializeComponent();
            this.Name = name;
            this.TabText = "タイムライン";
            this.RowSizeMode = RowSizeMode.Fill;
            this.PixelPerScaleMark = 5;
            this.nsPerScaleMarkTrackBar.Minimum = 1;
            this.nsPerScaleMarkTrackBar.PostFixText = " ns/目盛";
            this.pixelPerScaleMarkButtonTrackBar.Minimum = 2;
            this.pixelPerScaleMarkButtonTrackBar.Maximum = 100;
            this.pixelPerScaleMarkButtonTrackBar.PostFixText = " pixel/目盛";
            this.pixelPerScaleMarkButton.Text = this.PixelPerScaleMark + this.pixelPerScaleMarkButtonTrackBar.PostFixText;
            this.rowHeightTrackBar.PreFixText = "行サイズ : ";
            this.rowHeightTrackBar.PostFixText = " px";
            this.rowHeightTrackBar.Minimum = 15;
            this.rowHeightTrackBar.Maximum = 100;
            this.rowHeightTrackBar.Value = 25;
            this.rowHeightButton.Text = this.rowHeightTrackBar.PreFixText + this.RowHeight + this.rowHeightTrackBar.PostFixText;

            this.nsPerScaleMarkTrackBar.ValueChanged += new EventHandler(nsPerScaleMarkTrackBarTrackBarValueChanged);
            this.pixelPerScaleMarkButtonTrackBar.ValueChanged += new EventHandler(pixelPerScaleMarkButtonTrackBarValueChanged);
            this.rowHeightTrackBar.ValueChanged += new EventHandler(rowHeightTrackBarValueChanged);

            this.nsPerScaleMarkButton.ButtonClick += new EventHandler(nsPerScaleMarkButtonButtonClick);
            this.pixelPerScaleMarkButton.ButtonClick += new EventHandler(pixelPerScaleMarkButtonButtonClick);
            this.rowHeightButton.ButtonClick += new EventHandler(rowHeightButtonButtonClick);

            this.pixelPerScaleMarkAddButton.Click += new EventHandler(pixelPerScaleMarkAddButtonClick);
            this.pixelPerScaleMarkSubtractButton.Click += new EventHandler(pixelPerScaleMarkSubtractButtonClick);
            this.nsPerScaleMarkAddButton.Click += new EventHandler(nsPerScaleMarkAddButtonClick);
            this.nsPerScaleMarkSubtractButton.Click += new EventHandler(nsPerScaleMarkSubtractButtonClick);
            this.rowHeightAddButton.Click += new EventHandler(rowHeightAddButtonClick);
            this.rowHeightSubtractButton.Click += new EventHandler(rowHeightSubtractButtonClick);

            this.cursorButton.Click += new EventHandler(cursorButtonClick);
            this.handButton.Click += new EventHandler(handButtonClick);
            this.zoomInButton.Click += new EventHandler(zoomInButtonClick);
            this.zoomOutButton.Click += new EventHandler(zoomOutButtonClick);
            this.zoomSelectButton.Click += new EventHandler(zoomSelectButtonClick);
        }

        protected void zoomSelectButtonClick(object sender, EventArgs e)
        {
            CursorMode = CursorMode.ZoomSelect;
        }

        protected void handButtonClick(object sender, EventArgs e)
        {
            CursorMode = CursorMode.Hand;
        }

        protected void zoomOutButtonClick(object sender, EventArgs e)
        {
            CursorMode = CursorMode.ZoomOut;
        }

        protected void zoomInButtonClick(object sender, EventArgs e)
        {
            CursorMode = CursorMode.ZoomIn;
        }

        protected void cursorButtonClick(object sender, EventArgs e)
        {
            CursorMode = CursorMode.Default;
        }

        protected void rowHeightButtonButtonClick(object sender, EventArgs e)
        {
            this.rowHeightButton.ShowDropDown();
        }

        protected void rowHeightSubtractButtonClick(object sender, EventArgs e)
        {
            this.rowHeightTrackBar.Value -= this.rowHeightTrackBar.SmallChange;
        }

        protected void rowHeightAddButtonClick(object sender, EventArgs e)
        {
            this.rowHeightTrackBar.Value += this.rowHeightTrackBar.SmallChange;
        }

        protected void rowHeightTrackBarValueChanged(object sender, EventArgs e)
        {
            RowHeight = this.rowHeightTrackBar.Value;
        }

        protected void nsPerScaleMarkSubtractButtonClick(object sender, EventArgs e)
        {
            this.nsPerScaleMarkTrackBar.Value -= this.nsPerScaleMarkTrackBar.SmallChange;
        }

        protected void nsPerScaleMarkAddButtonClick(object sender, EventArgs e)
        {
            this.nsPerScaleMarkTrackBar.Value += this.nsPerScaleMarkTrackBar.SmallChange;
        }

        protected void pixelPerScaleMarkSubtractButtonClick(object sender, EventArgs e)
        {
            this.pixelPerScaleMarkButtonTrackBar.Value -= this.pixelPerScaleMarkButtonTrackBar.SmallChange;
        }

        protected void pixelPerScaleMarkAddButtonClick(object sender, EventArgs e)
        {
            this.pixelPerScaleMarkButtonTrackBar.Value += this.pixelPerScaleMarkButtonTrackBar.SmallChange;
        }

        protected void nsPerScaleMarkButtonButtonClick(object sender, EventArgs e)
        {
            this.nsPerScaleMarkButton.ShowDropDown();
        }

        protected void pixelPerScaleMarkButtonButtonClick(object sender, EventArgs e)
        {
            this.pixelPerScaleMarkButton.ShowDropDown();
        }

        protected void pixelPerScaleMarkButtonTrackBarValueChanged(object sender, EventArgs e)
        {
            PixelPerScaleMark = this.pixelPerScaleMarkButtonTrackBar.Value;
        }

        protected void nsPerScaleMarkTrackBarTrackBarValueChanged(object sender, EventArgs e)
        {
            NsPerScaleMark = (ulong)this.nsPerScaleMarkTrackBar.Value;
        }

        protected override void OnParentChanged(EventArgs e)
        {
            base.OnParentChanged(e);
        }

        public void Add(IPresentation presentation)
        {
            this.ContentPanel.Controls.Add((Control)presentation);
        }

        private void fillFixRowButtonClick(object sender, EventArgs e)
        {
            if (RowSizeMode == RowSizeMode.Fill)
            {
                RowSizeMode = RowSizeMode.Fix;
            }
            else
            {
                RowSizeMode = RowSizeMode.Fill;
            }
        }

        private void fillFixRowButtonIconChange()
        {
            if (RowSizeMode == RowSizeMode.Fill)
            {
                this.fillFixRowButton.Image = Resources.fixRowButton;
                this.fillFixRowButton.Text = "行サイズ固定";
                this.fillFixRowButton.ToolTipText = this.fillFixRowButton.Text;
                this.rowHeightButton.Visible = false;
                this.rowHeightAddButton.Visible = false;
                this.rowHeightSubtractButton.Visible = false;
            }
            else
            {
                this.fillFixRowButton.Image = Resources.fillRowButton;
                this.fillFixRowButton.Text = "行サイズ自動調整";
                this.fillFixRowButton.ToolTipText = this.fillFixRowButton.Text;
                this.rowHeightButton.Visible = true;
                this.rowHeightAddButton.Visible = true;
                this.rowHeightSubtractButton.Visible = true;
            }
        }

        protected void NotifyPropertyChanged(string info)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(info));
            }
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {

            TaskInfo ti = new TaskInfo("TSK, 1, MAIN_TASK, TA_NULL, 10, 1, 4096", new TimeLineEvents()
            {
                new TimeLineEvent(123987, Ta),
            });


            if (AddViewableObject != null)
            {
                AddViewableObject(ko, viewableObjectDataSource);
            }
        }

    }

}
