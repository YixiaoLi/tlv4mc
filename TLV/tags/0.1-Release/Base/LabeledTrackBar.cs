using System;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace NU.OJL.MPRTOS.TLV.Base
{
    public partial class LabeledTrackBar : UserControl
    {
        public LabeledTrackBar()
        {
            InitializeComponent();
            this.DoubleBuffered = true;
        }

        public TickStyle TickStyle
        {
            get { return trackBar.TickStyle; }
            set { trackBar.TickStyle = value; }
        }

        public int TickFrequency
        {
            get { return trackBar.TickFrequency; }
            set { trackBar.TickFrequency = value; }
        }

        public string MinLabel
        {
            get { return label_min.Text; }
            set { label_min.Text = value; }
        }

        public string NowLabel
        {
            get { return label_now.Text; }
            set { label_now.Text = value; }
        }

        public string MaxLabel
        {
            get { return label_max.Text; }
            set { label_max.Text = value; }
        }

        public int Value
        {
            get { return trackBar.Value; }
            set { trackBar.Value = value; }
        }

        public int Maximum
        {
            get { return trackBar.Maximum; }
            set { trackBar.Maximum = value; }
        }

        public int Minimum
        {
            get { return trackBar.Minimum; }
            set { trackBar.Minimum = value; }
        }

        public int LargeChange
        {
            get { return trackBar.LargeChange; }
            set { trackBar.LargeChange = value; }
        }

        public int SmallChange
        {
            get { return trackBar.SmallChange; }
            set { trackBar.SmallChange = value; }
        }

        public Size TrackBarSize
        {
            get { return this.Size; }
            set { this.Size = value; }
        }

        public event EventHandler ValueChanged;
        public event EventHandler TrackBarScroll;

        private void trackBar_ValueChanged(object sender, EventArgs e)
        {
            if (ValueChanged != null)
            {
                ValueChanged(this, e);
            }
        }

        private void trackBar_Scroll(object sender, EventArgs e)
        {
            if (TrackBarScroll != null)
            {
                TrackBarScroll(this, e);
            }
        }


    }
}
