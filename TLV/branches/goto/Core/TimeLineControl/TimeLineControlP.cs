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

namespace NU.OJL.MPRTOS.TLV.Core.TimeLineControl
{

    public partial class TimeLineControlP : DockContent, IPresentation
    {
        private RowSizeMode rowSizeMode;
        private ulong nsPerScaleMark = 1;
        private ulong maximumNsPerScaleMark = 1;
        private int pixelPerScaleMark;

        public event PropertyChangedEventHandler PropertyChanged;

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

                    this.nsPerScaleMarkButton.Text = value + this.nsPerScaleMarkTrackBar.PostFixText;

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

                    this.nsPerScaleMarkTrackBar.Minimum = pixelPerScaleMark;

                    this.pixelPerScaleMarkButton.Text = value + this.pixelPerScaleMarkButtonTrackBar.PostFixText;

                    NotifyPropertyChanged("PixelPerScaleMark");
                }
            }
        }

        public TimeLineControlP(string name)
        {
            InitializeComponent();

            this.Name = name;
            this.TabText = "タイムライン";
            this.RowSizeMode = RowSizeMode.Fill;
            this.nsPerScaleMarkTrackBar.Minimum = pixelPerScaleMark;
            this.pixelPerScaleMarkButtonTrackBar.Minimum = 2;
            this.pixelPerScaleMarkButtonTrackBar.Maximum = 100;
            this.PixelPerScaleMark = 5;
            this.nsPerScaleMarkTrackBar.PostFixText = " ns/目盛";
            this.pixelPerScaleMarkButtonTrackBar.PostFixText = " pixel/目盛";
            this.pixelPerScaleMarkButton.Text = this.PixelPerScaleMark + this.pixelPerScaleMarkButtonTrackBar.PostFixText;
            this.nsPerScaleMarkTrackBar.ValueChanged += new EventHandler(nsPerScaleMarkTrackBarTrackBarValueChanged);
            this.pixelPerScaleMarkButtonTrackBar.ValueChanged += new EventHandler(pixelPerScaleMarkButtonTrackBarValueChanged);
            this.pixelPerScaleMarkButton.ButtonClick += new EventHandler(pixelPerScaleMarkButtonButtonClick);
            this.nsPerScaleMarkButton.ButtonClick += new EventHandler(nsPerScaleMarkButtonButtonClick);
            this.pixelPerScaleMarkAddButton.Click += new EventHandler(pixelPerScaleMarkAddButtonClick);
            this.pixelPerScaleMarkSubtractButton.Click += new EventHandler(pixelPerScaleMarkSubtractButtonClick);
            this.nsPerScaleMarkAddButton.Click += new EventHandler(nsPerScaleMarkAddButtonClick);
            this.nsPerScaleMarkSubtractButton.Click += new EventHandler(nsPerScaleMarkSubtractButtonClick);
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
                this.fillFixRowButton.Text = "行サイズを固定にする";
                this.fillFixRowButton.ToolTipText = this.fillFixRowButton.Text;
            }
            else
            {
                this.fillFixRowButton.Image = Resources.fillRowButton;
                this.fillFixRowButton.Text = "行サイズを可変にする";
                this.fillFixRowButton.ToolTipText = this.fillFixRowButton.Text;
            }
        }

        protected void NotifyPropertyChanged(string info)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(info));
            }
        }

    }

}
