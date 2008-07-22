using System;
using System.ComponentModel;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Collections.Generic;
using NU.OJL.MPRTOS.TLV.Architecture.PAC;

namespace NU.OJL.MPRTOS.TLV.Core.TimeLineControl.TimeLineMarkerManager
{
    public partial class TimeLineMarkerManagerP : UserControl, IPresentation
    {
        private int timeLineX = 0;
        private int timeLineMinimumX = 0;
        private ulong minimumTime = 0;
        private ulong maximumTime = 100;
        private ulong beginTime = 0;
        private ulong endTime = 100;
        private ulong displayTimeLength = 100;
        private ulong nsPerScaleMark = 0;
        private int pixelPerScaleMark = 5;
        private Font timeMarkLabelFont = new Font(FontFamily.GenericSansSerif, 8);
        private ulong nowMarkerTime = 0;
        private ulong selectRectStartTime = 0;
        private int labelMargin = 2;
        private List<TimeLineMarker> timeLineMarkerList = new List<TimeLineMarker>();

        public int TimeLineX
        {
            get { return timeLineX; }
            set
            {
                if (timeLineX != value)
                {
                    timeLineX = value;
                    NotifyPropertyChanged("TimeLineX");
                    this.Refresh();
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

                    if (beginTime < minimumTime)
                    {
                        BeginTime = minimumTime;
                    }
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
                    EndTime = value + DisplayTimeLength;
                    NotifyPropertyChanged("BeginTime");
                    this.Refresh();
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
        public ulong DisplayTimeLength
        {
            get { return displayTimeLength; }
            set
            {
                if (displayTimeLength != value)
                {
                    displayTimeLength = value;
                    EndTime = BeginTime + value;
                    NotifyPropertyChanged("DisplayTimeLength");
                    this.Refresh();
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

                    NotifyPropertyChanged("NsPerScaleMark");
                    this.Refresh();
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
                    this.Refresh();
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
                    Refresh();
                }
            }
        }
        public ulong TmpMarkerTime
        {
            get;
            set;
        }
        public Color NowMarkerColor
        {
            get;
            set;
        }
        public Color TmpMarkerColor
        {
            get;
            set;
        }
        public ulong SelectRectStartTime
        {
            get { return selectRectStartTime; }
            set
            {
                if (selectRectStartTime != value)
                {
                    selectRectStartTime = value;
                    NotifyPropertyChanged("SelectRectStartTime");
                    if (selectRectStartTime == 0)
                    {
                        Refresh();
                    }
                }
            }
        }
        public List<TimeLineMarker> TimeLineMarkerList
        {
            get { return timeLineMarkerList; }
            set
            {
                if (!timeLineMarkerList.Equals(value))
                {
                    timeLineMarkerList = value;
                    NotifyPropertyChanged("TimeLineMarkerList");
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public TimeLineMarkerManagerP(string name)
        {
            InitializeComponent();
            this.Name = name;
            this.DoubleBuffered = true;
        }

        public void Add(IPresentation presentation)
        {

        }

        protected void NotifyPropertyChanged(string info)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(info));
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            if (TimeLineMarkerList.Count != 0)
            {
                foreach (TimeLineMarker tlm in TimeLineMarkerList)
                {
                    drawMarker(tlm, e.Graphics);
                }
            }
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);
            ulong time = xToTime(e.X);
            if (displayTimeLength != 0 && !TimeLineMarkerList.Exists(tlm => tlm.Time == time))
            {
                TimeLineMarkerList.Add(new TimeLineMarker(time));
            }
            Refresh();
        }

        private void drawMarker(TimeLineMarker tlm, Graphics graphics)
        {

            float x = timeToX(tlm.Time);
            float labelY = 0;

            SizeF labelSize = getMarkLabelWidth(tlm.Name);

            float nowLabelTopY = labelY;
            float nowLabelMiddleY = nowLabelTopY + 5;
            float nowLabelBottomY = nowLabelMiddleY + labelSize.Height;

            PointF[] points = new[] {
                new PointF(x - (labelSize.Width / 2) - labelMargin, nowLabelBottomY),
                new PointF(x + (labelSize.Width / 2) + labelMargin, nowLabelBottomY),
                new PointF(x + (labelSize.Width / 2) + labelMargin, nowLabelMiddleY),
                new PointF(x, nowLabelTopY),
                new PointF(x - (labelSize.Width / 2) - labelMargin, nowLabelMiddleY),
            };

            graphics.SmoothingMode = SmoothingMode.HighQuality;
            using (SolidBrush brush = new SolidBrush(Color.FromArgb(250, Color.White)))
            {
                graphics.FillPolygon(brush, points);
            }
            using (Pen pen = new Pen(tlm.Color))
            {
                graphics.DrawPolygon(pen, points);
            }
            graphics.SmoothingMode = SmoothingMode.Default;
            graphics.DrawString(tlm.Name.ToString(), timeMarkLabelFont, Brushes.Black, x - (labelSize.Width / 2), nowLabelMiddleY);
        }

        private ulong xToTime(int x)
        {
            return (ulong)(((decimal)x * ((decimal)nsPerScaleMark / (decimal)pixelPerScaleMark)) + (decimal)beginTime);
        }

        private float timeToX(ulong t)
        {
            return (float)(((decimal)t - (decimal)beginTime) / ((decimal)nsPerScaleMark / (decimal)pixelPerScaleMark));
        }

        private SizeF getMarkLabelWidth(string label)
        {
            SizeF size;
            using (Graphics graphics = this.CreateGraphics())
            {
                size = graphics.MeasureString(label, timeMarkLabelFont);
            }
            return size;
        }
    }
}
