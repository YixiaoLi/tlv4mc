using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Collections.Generic;
using NU.OJL.MPRTOS.TLV.Architecture.PAC;
using NU.OJL.MPRTOS.TLV.Core.Base;

namespace NU.OJL.MPRTOS.TLV.Core.TimeLineControl.TimeLineGrid
{
    public class TimeLineGridA : Abstraction
    {
        private RowSizeMode rowSizeMode;
        private int timeLineX;
        private int timeLineMinimumX = 0;
        private ulong minimumTime = ulong.MaxValue;
        private ulong maximumTime = ulong.MinValue;
        private ulong beginTime = 0;
        private ulong displayTimeLength = 0;
        private ulong nsPerScaleMark = 0;
        private ulong maximumNsPerScaleMark = 1;
        private int pixelPerScaleMark = 5;
        private ulong nowMarkerTime = 0;
        private Color nowMarkerColor = Color.FromArgb(255, Color.Red);
        private int maxRowHeight = 100;
        private int minRowHeight = 15;
        private int rowHeight = 25;
        private Object viewableObjectDataSource;

        public RowSizeMode RowSizeMode
        {
            get { return rowSizeMode; }
            set
            {
                if (rowSizeMode != value)
                {
                    rowSizeMode = value;
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
                    NotifyPropertyChanged("PixelPerScaleMark");
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
        public int NowRowHeight
        {
            get { return rowHeight; }
            set
            {
                if (rowHeight != value)
                {
                    rowHeight = value;
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
        public Object ViewableObjectDataSource
        {
            get { return viewableObjectDataSource; }
            set { viewableObjectDataSource = value; }
        }

        public TimeLineGridA(string name)
            : base(name)
        {

        }
    }
}
