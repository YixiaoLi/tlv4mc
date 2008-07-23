﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms.Design;
using System.Drawing;

namespace NU.OJL.MPRTOS.TLV.Base
{
    [ToolStripItemDesignerAvailability(ToolStripItemDesignerAvailability.All)]
    public class ToolStripLabeledTrackBar : ToolStripControlHost
    {
        private string postFixText = "";
        private string preFixText = "";

        public ToolStripLabeledTrackBar() : base(new LabeledTrackBar())
        {
            ((LabeledTrackBar)Control).ValueChanged += new EventHandler(ToolStripLabeledTrackBar_ValueChanged);
            ((LabeledTrackBar)Control).TickFrequency = ((LabeledTrackBar)Control).Width / 2;
            ((LabeledTrackBar)Control).SmallChange = 1;
        }

        void ToolStripLabeledTrackBar_ValueChanged(object sender, EventArgs e)
        {
            ((LabeledTrackBar)Control).NowLabel = PreFixText + Value.ToString() + PostFixText;
        }

        public TickStyle TickStyle
        {
            get { return ((LabeledTrackBar)Control).TickStyle; }
            set { ((LabeledTrackBar)Control).TickStyle = value; }
        }

        public int TickFrequency
        {
            get { return ((LabeledTrackBar)Control).TickFrequency; }
            set { ((LabeledTrackBar)Control).TickFrequency = value; }
        }

        public string MinLabel
        {
            get { return ((LabeledTrackBar)Control).MinLabel; }
            set { ((LabeledTrackBar)Control).MinLabel = value; }
        }

        public string NowLabel
        {
            get { return ((LabeledTrackBar)Control).NowLabel; }
            set { ((LabeledTrackBar)Control).NowLabel = value; }
        }

        public string MaxLabel
        {
            get { return ((LabeledTrackBar)Control).MaxLabel; }
            set { ((LabeledTrackBar)Control).MaxLabel = value; }
        }

        public int Value
        {
            get { return ((LabeledTrackBar)Control).Value; }
            set
            {
                if (((LabeledTrackBar)Control).Value != value && ((LabeledTrackBar)Control).Maximum >= value && ((LabeledTrackBar)Control).Minimum <= value)
                {
                    ((LabeledTrackBar)Control).Value = value;
                    ((LabeledTrackBar)Control).NowLabel = PreFixText + ((LabeledTrackBar)Control).Value.ToString() + PostFixText;
                }
            }
        }

        public int Maximum
        {
            get { return ((LabeledTrackBar)Control).Maximum; }
            set
            {
                ((LabeledTrackBar)Control).Maximum = value;

                int lc = ((LabeledTrackBar)Control).Maximum / 100;

                ((LabeledTrackBar)Control).LargeChange = lc < 1 ? 1 : lc;

                ((LabeledTrackBar)Control).TickFrequency = ((LabeledTrackBar)Control).LargeChange;

                ((LabeledTrackBar)Control).MaxLabel = PreFixText + value.ToString() + PostFixText;
            }
        }

        public int Minimum
        {
            get { return ((LabeledTrackBar)Control).Minimum; }
            set
            {
                ((LabeledTrackBar)Control).Minimum = value;
                ((LabeledTrackBar)Control).MinLabel = PreFixText + value.ToString() + PostFixText;
            }
        }

        public int LargeChange
        {
            get { return ((LabeledTrackBar)Control).LargeChange; }
            set { ((LabeledTrackBar)Control).LargeChange = value; }
        }

        public int SmallChange
        {
            get { return ((LabeledTrackBar)Control).SmallChange; }
            set { ((LabeledTrackBar)Control).SmallChange = value; }
        }

        public Size TrackBarSize
        {
            get { return ((LabeledTrackBar)Control).TrackBarSize; }
            set { ((LabeledTrackBar)Control).TrackBarSize = value; }
        }

        public string PostFixText
        {
            get { return postFixText; }
            set
            {
                if (postFixText != value)
                {
                    postFixText = value;
                    ((LabeledTrackBar)Control).NowLabel = PreFixText + ((LabeledTrackBar)Control).NowLabel.ToString() + PostFixText;
                    ((LabeledTrackBar)Control).MinLabel = PreFixText + ((LabeledTrackBar)Control).MinLabel.ToString() + PostFixText;
                    ((LabeledTrackBar)Control).MaxLabel = PreFixText + ((LabeledTrackBar)Control).MaxLabel.ToString() + PostFixText;
                }
            }
        }

        public string PreFixText
        {
            get { return preFixText; }
            set
            {
                if (preFixText != value)
                {
                    preFixText = value;
                    ((LabeledTrackBar)Control).NowLabel = PreFixText + ((LabeledTrackBar)Control).NowLabel.ToString() + PostFixText;
                    ((LabeledTrackBar)Control).MinLabel = PreFixText + ((LabeledTrackBar)Control).MinLabel.ToString() + PostFixText;
                    ((LabeledTrackBar)Control).MaxLabel = PreFixText + ((LabeledTrackBar)Control).MaxLabel.ToString() + PostFixText;
                }
            }
        }

        protected override void OnSubscribeControlEvents(Control control)
        {
            base.OnSubscribeControlEvents(control);
            LabeledTrackBar trackBarControl = (LabeledTrackBar)control;
            trackBarControl.ValueChanged += new EventHandler(LabeledTrackBar_OnValueChanged);
            trackBarControl.TrackBarScroll += new EventHandler(LabeledTrackBar_OnScroll);
        }

        protected override void OnUnsubscribeControlEvents(Control control)
        {
            base.OnUnsubscribeControlEvents(control);
            LabeledTrackBar trackBarControl = (LabeledTrackBar)control;
            trackBarControl.ValueChanged -= new EventHandler(LabeledTrackBar_OnValueChanged);
            trackBarControl.TrackBarScroll -= new EventHandler(LabeledTrackBar_OnScroll);
        }

        public event EventHandler ValueChanged;
        public event EventHandler TrackBarScroll;

        private void LabeledTrackBar_OnValueChanged(object sender, EventArgs e)
        {
            if (ValueChanged != null)
            {
                ValueChanged(this, e);
            }
        }

        private void LabeledTrackBar_OnScroll(object sender, EventArgs e)
        {
            if (TrackBarScroll != null)
            {
                TrackBarScroll(this, e);
            }
        }

    }
}
