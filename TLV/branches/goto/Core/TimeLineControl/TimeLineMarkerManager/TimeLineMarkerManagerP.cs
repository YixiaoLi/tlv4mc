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
        private Font timeMarkFont = new Font(FontFamily.GenericMonospace, 8);
        private ulong nowMarkerTime = 0;
        private ulong selectRectStartTime = 0;
        private int labelMargin = 2;
        private float verticalMargin = 5;
        private TimeLineMarkers timeLineMarkerList = new TimeLineMarkers();

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
        public TimeLineMarkers TimeLineMarkerList
        {
            get { return timeLineMarkerList; }
            set
            {
                if (!timeLineMarkerList.Equals(value))
                {
                    timeLineMarkerList = value;
                    NotifyPropertyChanged("TimeLineMarkerList");

                    timeLineMarkerList.SelectChanged += delegate
                    {
                        Refresh();
                    };
                    timeLineMarkerList.SelectCleared += delegate
                    {
                        Refresh();
                    };
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

        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);

            timeLineMarkerList.SelectClear();

            foreach(TimeLineMarker tlm in timeLineMarkerList.GetBetween(beginTime, endTime))
            {
                SizeF labelSize = getMarkLabelWidth(tlm.Name, timeMarkLabelFont);
                float x = timeToX(tlm.Time);
                float from = x - labelSize.Width / 2 - labelMargin;
                float to = x + labelSize.Width / 2 + labelMargin;
                if(e.X >= from && e.X <= to)
                {
                    tlm.Selected = true;
                }
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            if (TimeLineMarkerList.Count != 0)
            {
                TimeLineMarker preTlm = null;
                foreach (TimeLineMarker tlm in TimeLineMarkerList.GetBetweenExtended(beginTime, endTime))
                {
                    if (preTlm != null)
                    {
                        drawFromToLabel(preTlm, tlm, e.Graphics);
                    }
                    preTlm = tlm;
                }
                foreach (TimeLineMarker tlm in TimeLineMarkerList.GetBetween(beginTime, endTime))
                {
                    drawMarker(tlm, e.Graphics);
                }
            }
        }

        private void drawMarker(TimeLineMarker tlm, Graphics graphics)
        {

            float x = timeToX(tlm.Time);
            float labelY = 0;
            Font font = new Font(timeMarkLabelFont, FontStyle.Regular);

            if (tlm.Selected)
            {
                font = new Font(font, FontStyle.Bold);
            }

            SizeF labelSize = getMarkLabelWidth(tlm.Name, font);

            float nowLabelTopY = labelY;
            float nowLabelMiddleY = nowLabelTopY + verticalMargin;
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
            if(tlm.Selected)
            {
                using (Pen pen = new Pen(Color.FromArgb(200, tlm.Color)))
                {
                    pen.Width = 3;
                    graphics.DrawPolygon(pen, points);
                }
            }
            using (Pen pen = new Pen(tlm.Color))
            {
                graphics.DrawPolygon(pen, points);
            }
            graphics.SmoothingMode = SmoothingMode.Default;
            graphics.DrawString(tlm.Name.ToString(), font, Brushes.Black, x - (labelSize.Width / 2), nowLabelMiddleY);
        }

        private void drawFromToLabel(TimeLineMarker f, TimeLineMarker t, Graphics graphics)
        {
            TimeLineMarker from = t.Time > f.Time ? f : t;
            TimeLineMarker to = t.Time > f.Time ? t : f;
            SizeF fromTimeSize = getMarkLabelWidth(from.Name, timeMarkLabelFont);
            SizeF toTimeSize = getMarkLabelWidth(to.Name, timeMarkLabelFont);
            float fromX = timeToX(from.Time) + fromTimeSize.Width / 2;
            float toX = timeToX(to.Time) - toTimeSize.Width / 2; ;
            int arrowSize = 8;
            ulong time = to.Time - from.Time;
            float timeX = timeToX(from.Time + (time / 2));
            SizeF timeSize = getMarkLabelWidth(time.ToString(), timeMarkFont);
            float timeLength = timeToX(to.Time) - timeToX(from.Time);
            float lineY = verticalMargin + (timeSize.Height / 2);

            if (fromTimeSize.Width / 2 + toTimeSize.Width / 2 + (labelMargin * 2) < timeLength)
            {
                using (Pen pen1 = new Pen(from.Color))
                using(Pen pen2 = new Pen(to.Color))
                {
                    graphics.DrawLine(pen1, fromX + labelMargin, lineY, (fromX + toX) / 2, lineY);
                    graphics.DrawLine(pen2, (fromX + toX) / 2, lineY, toX - labelMargin, lineY);

                    graphics.DrawLine(pen1, fromX + labelMargin, lineY, fromX + arrowSize + labelMargin, lineY + 3);
                    graphics.DrawLine(pen1, fromX + labelMargin, lineY, fromX + arrowSize + labelMargin, lineY - 3);
                    graphics.DrawLine(pen2, toX - labelMargin, lineY, toX - arrowSize - labelMargin, lineY + 3);
                    graphics.DrawLine(pen2, toX - labelMargin, lineY, toX - arrowSize - labelMargin, lineY - 3);
                }
            }

            PointF[] points = new[] {
                new PointF(timeX - timeSize.Width / 2,                  lineY - timeSize.Height / 2),
                new PointF(timeX - timeSize.Width / 2 - verticalMargin, lineY),
                new PointF(timeX - timeSize.Width / 2,                  lineY + timeSize.Height / 2),
                new PointF(timeX + timeSize.Width / 2,                  lineY + timeSize.Height / 2),
                new PointF(timeX + timeSize.Width / 2 + verticalMargin, lineY),
                new PointF(timeX + timeSize.Width / 2,                  lineY - timeSize.Height / 2),
            };

            using (SolidBrush brush = new SolidBrush(Color.FromArgb(250, Color.White)))
            {
                graphics.FillPolygon(brush, points);
            }
            using (Pen pen = new Pen(Color.DarkGray))
            {
                graphics.DrawPolygon(pen, points);
            }
            graphics.DrawString(time.ToString(), timeMarkFont, Brushes.Black, timeX - (timeSize.Width / 2), lineY - (timeSize.Height / 2));
        }

        private ulong xToTime(int x)
        {
            return (ulong)(((decimal)x * ((decimal)nsPerScaleMark / (decimal)pixelPerScaleMark)) + (decimal)beginTime);
        }

        private float timeToX(ulong t)
        {
            return (float)(((decimal)t - (decimal)beginTime) / ((decimal)nsPerScaleMark / (decimal)pixelPerScaleMark));
        }

        private SizeF getMarkLabelWidth(string label, Font font)
        {
            SizeF size;
            using (Graphics graphics = this.CreateGraphics())
            {
                size = graphics.MeasureString(label, font);
            }
            return size;
        }
    }
}
