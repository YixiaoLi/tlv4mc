using System;
using System.Drawing;
using System.ComponentModel;
using System.Windows.Forms;
using System.Collections.Generic;
using NU.OJL.MPRTOS.TLV.Architecture.PAC;

namespace NU.OJL.MPRTOS.TLV.Core.TimeLineControl.TimeLineScrollBar
{
    public partial class TimeLineScrollBarP : HScrollBar, IPresentation
    {
        private ulong minimumTime = 0;
        private ulong maximumTime = 0;
        private ulong beginTime = 0;
        private ulong displayTimeLength = 0;
        private int smallChangePerLargeChange = 100;

        public int X
        {
            get { return this.Location.X; }
            set
            {
                if (this.Location.X != value)
                {
                    int delta = value - this.Location.X;
                    this.Width -= delta;
                    this.Location = new Point(value, this.Location.Y);
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
                    displayTimeLengthReCalc();
                    NotifyPropertyChanged("DisplayTimeLength");
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public TimeLineScrollBarP(string name)
        {
            this.Name = name;
            this.Minimum = 1;
            this.Maximum = int.MaxValue;
            this.Value = 1;
        }

        public void Add(IPresentation presentation)
        {

        }

        protected override void OnScroll(ScrollEventArgs se)
        {
            if(se.NewValue <= int.MaxValue && se.NewValue >= 0)
            {
                base.OnScroll(se);
            }
        }

        protected override void OnValueChanged(EventArgs e)
        {
            base.OnValueChanged(e);
            BeginTime = valueToTime(Value);
        }

        protected void NotifyPropertyChanged(string info)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(info));
            }
        }

        protected void minimumReCalc()
        {
            Minimum = timeToValue(minimumTime);
            if(Value == 0)
            {
                Value = Minimum;
            }
        }

        protected void displayTimeLengthReCalc()
        {
            LargeChange = timeToValue(beginTime + displayTimeLength) - timeToValue(beginTime);
            SmallChange = LargeChange / smallChangePerLargeChange < 1 ? 1 : LargeChange / smallChangePerLargeChange;

            Value = timeToValue(beginTime);
        }

        protected int timeToValue(ulong time)
        {
            if (maximumTime - minimumTime != 0)
            {
                decimal i = (decimal)(maximumTime - minimumTime) / (decimal)(Maximum - Minimum);
                decimal v = ((decimal)time - ((decimal)minimumTime - i)) / i;
                return (int)(v > (decimal)(int.MaxValue) ? int.MaxValue : v < 1 ? 1 : v);
            }
            else
            {
                return 1;
            }
        }

        protected ulong valueToTime(int value)
        {
            decimal i = (decimal)(maximumTime - minimumTime) / (decimal)(Maximum - Minimum);
            decimal t = i * (decimal)value + ((decimal)minimumTime - i);
            return (ulong)(t > (decimal)(maximumTime) ? maximumTime : t < (decimal)minimumTime ? minimumTime : t);
        }

    }
}
