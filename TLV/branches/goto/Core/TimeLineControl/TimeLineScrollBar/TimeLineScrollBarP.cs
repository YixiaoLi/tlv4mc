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
        private int smallChangePerLargeChange = 20;

        public int X
        {
            get { return this.Location.X; }
            set { this.Location = new Point(value, this.Location.Y); }
        }
        public ulong MinimumTime
        {
            get { return minimumTime; }
            set
            {
                if (minimumTime != value)
                {
                    minimumTime = value;
                    minimumReCalc();
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
                    minimumReCalc();
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
            this.Minimum = 0;
            this.Maximum = int.MaxValue;
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
            ulong bt = (ulong)(((decimal)Value * (decimal)maximumTime) / (decimal)int.MaxValue);
            BeginTime = bt < minimumTime ? minimumTime : bt;
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
            decimal t = (decimal)minimumTime / (decimal)Math.Max(maximumTime, 1);
            int min =(decimal)Maximum * t > (decimal)(int.MaxValue) ? int.MaxValue : (int)((decimal)Maximum * t);
            Minimum = min > Maximum ? Maximum : min < 0 ? 0 : min;
            this.Value = Minimum;
        }

        protected void displayTimeLengthReCalc()
        {
            decimal t = (decimal)displayTimeLength / (decimal)Math.Max(maximumTime, 1);
            int lc = (decimal)Maximum * t > (decimal)(int.MaxValue) ? int.MaxValue : (int)((decimal)Maximum * t);
            LargeChange = lc > Maximum ? Maximum : lc < 1 ? 1 : lc;
            SmallChange = LargeChange / smallChangePerLargeChange > Maximum ? Maximum : LargeChange / smallChangePerLargeChange < 1 ? 1 : LargeChange / smallChangePerLargeChange;
        }

    }
}
