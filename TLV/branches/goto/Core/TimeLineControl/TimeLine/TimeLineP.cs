using System;
using System.Drawing;
using System.Collections.Generic;
using WinForms = System.Windows.Forms;
using System.Drawing.Drawing2D;
using NU.OJL.MPRTOS.TLV.Architecture.PAC;
using NU.OJL.MPRTOS.TLV.Base;

namespace NU.OJL.MPRTOS.TLV.Core.TimeLineControl.TimeLine
{
    public enum ScaleMarkDirection
    {
        Top,
        Bottom
    }

    public partial class TimeLineP : WinForms.UserControl, IPresentation
    {
        private Point mouseDownPoint = new Point();
        private WinForms.Cursor resizeCursor = WinForms.Cursors.SizeWE;
        private bool isResizing = false;
        private int timeLineX;
        private int timeLineMinimumX = 0;
        private ulong minimumTime = 0;
        private ulong maximumTime = 0;
        private ulong beginTime = 0;
        private ulong endTime = 0;
        private ulong displayTimeLength = 0;
        private ulong nsPerScaleMark = 1;
        private int pixelPerScaleMark = 5;
        private Font timeMarkLabelFont;
        private ulong scaleMarkStartTime;
        private int timeLineMarkLabelInterval;
        private float scaleMarkHeight = 5;
        private ulong nowMarkerTime = 0;

        public ScaleMarkDirection ScaleMarkDirection { get; set; }
        public bool IsDisplayNowMarkTime { get; set; }
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
                    NotifyPropertyChanged("MinimumTime");
                    this.Refresh();
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
                    this.Refresh();
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
                if(endTime != value)
                {
                    endTime = value;
                    int endTimeOrder = (int)Math.Ceiling(Math.Log10((double)EndTime + 1D));
                    double timeLineMarkLabelWidth = getTimeMarkLabelWidth((ulong)Math.Pow(10, endTimeOrder)).Width;
                    timeLineMarkLabelInterval = (int)Math.Ceiling(timeLineMarkLabelWidth / (double)pixelPerScaleMark);
                    timeLineMarkLabelInterval += timeLineMarkLabelInterval % 2;
                    timeLineMarkLabelInterval = (int)Math.Ceiling((double)timeLineMarkLabelInterval / 10D) * 10;
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

                    int order = (int)Math.Ceiling(Math.Log10((double)nsPerScaleMark + 1D));
                    scaleMarkStartTime = (ulong)(Math.Floor((double)MinimumTime / Math.Pow(10D, order - 1)) * Math.Pow(10D, order - 1)) + nsPerScaleMark;

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

                    int endTimeOrder = (int)Math.Ceiling(Math.Log10((double)EndTime + 1D));
                    double timeLineMarkLabelWidth = getTimeMarkLabelWidth((ulong)Math.Pow(10, endTimeOrder)).Width;
                    timeLineMarkLabelInterval = (int)Math.Ceiling(timeLineMarkLabelWidth / (double)pixelPerScaleMark);
                    timeLineMarkLabelInterval += timeLineMarkLabelInterval % 2;
                    timeLineMarkLabelInterval = (int)Math.Ceiling((double)timeLineMarkLabelInterval / 10D) * 10;

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
                    drawNowMarker(nowMarkerTime);
                    NotifyPropertyChanged("NowMarkerTime");
                }
            }
        }
        public Color NowMarkerColor
        {
            get;
            set;
        }

        public event WinForms.MouseEventHandler TimeLineXResizing = null;
        public event WinForms.MouseEventHandler TimeLineXResized = null;

        public TimeLineP(string name)
            : this(name, ScaleMarkDirection.Bottom) { }

        public TimeLineP(string name, ScaleMarkDirection scaleMarkDirection)
        {
            this.Name = name;
            this.Height = 30;
            this.ScaleMarkDirection = scaleMarkDirection;
            this.IsDisplayNowMarkTime = true;
            this.timeMarkLabelFont = new Font(FontFamily.GenericMonospace, 8);
            this.SetStyle(WinForms.ControlStyles.ResizeRedraw | WinForms.ControlStyles.Opaque, true);
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
            this.timeMarkLabelFont.Dispose();
        }

        protected override void OnPaint(WinForms.PaintEventArgs e)
        {
            Rectangle backRect = new Rectangle(0, 0, this.Width - TimeLineX, this.Height);
            if (backRect.Width > 0 && backRect.Height > 0)
            {
                Bitmap tmpBmp = new Bitmap(backRect.Width, backRect.Height);
                using (Graphics tmpBmpGraphics = Graphics.FromImage(tmpBmp))
                {
                    drawTimeLine(tmpBmpGraphics, backRect.Width, backRect.Height);
                }
                e.Graphics.FillRectangle(new SolidBrush(this.BackColor), new Rectangle(0, 0, timeLineX, Height));
                e.Graphics.DrawImage(tmpBmp, TimeLineX, 0);
            }
        }

        protected override void OnSizeChanged(EventArgs e)
        {
            base.OnSizeChanged(e);
            this.Refresh();
        }

        protected override void OnMouseDown(WinForms.MouseEventArgs e)
        {
            if (this.Cursor == resizeCursor)
            {
                mouseDownPoint.X = timeLineX;
                isResizing = true;
            }
            base.OnMouseDown(e);
        }

        protected override void OnMouseUp(WinForms.MouseEventArgs e)
        {
            base.OnMouseUp(e);
            if (isResizing)
            {
                int x = (e.X > this.ClientSize.Width) ? this.ClientSize.Width : (e.X < timeLineMinimumX) ? timeLineMinimumX : e.X;

                int delta = x - mouseDownPoint.X;
                this.TimeLineX += delta;
                isResizing = false;
                TimeLineXResized(this, e);
            }
        }

        protected override void OnMouseMove(WinForms.MouseEventArgs e)
        {
            base.OnMouseMove(e);

            if (e.X < timeLineX + 5 && e.X > timeLineX - 5)
            {
                if (this.Cursor != resizeCursor)
                {
                    this.Cursor = resizeCursor;
                }
            }
            else if (this.Cursor == resizeCursor && !isResizing)
            {
                this.Cursor = DefaultCursor;
            }

            if (isResizing && TimeLineXResizing != null)
            {
                TimeLineXResizing(this, e);
            }
        }

        private void drawTimeLine(Graphics graphics, int width, int height)
        {
            Rectangle backRect = new Rectangle(0, 0, width, height);
            graphics.FillRectangle(Brushes.Black, backRect);

            float scaleMarkY = height - scaleMarkHeight;
            if (ScaleMarkDirection == ScaleMarkDirection.Top)
            {
                scaleMarkY = 0f;
            }
            int i = 1;
            for (ulong time = scaleMarkStartTime; time < EndTime; time += nsPerScaleMark, i++)
            {
                float x = (((float)time - (float)BeginTime) / (float)nsPerScaleMark) * pixelPerScaleMark;
                float y = scaleMarkY;
                float h = scaleMarkHeight;
                if (i % timeLineMarkLabelInterval == 0)
                {
                    drawTimeMarkLabel(graphics, time);
                }
                if (i % 10 == 0 || i == 1)
                {
                    if (ScaleMarkDirection == ScaleMarkDirection.Bottom)
                    {
                        y -= h;
                    }
                    h *= 2f;
                }
                else if (i % 5 == 0)
                {
                    if (ScaleMarkDirection == ScaleMarkDirection.Bottom)
                    {
                        y -= 0.5f * h;
                    }
                    h *= 1.5f;
                }

                if (x > 0 && x < width)
                {
                    graphics.DrawLine(Pens.White, x, y, x, y + h);
                }
            }

        }

        private ulong drawTimeMarkLabel(Graphics graphics, ulong time)
        {
            ulong nextTime = 0;

            SizeF timeSize = getTimeMarkLabelWidth(time);

            float labelY = 1;

            if (ScaleMarkDirection == ScaleMarkDirection.Top)
            {
                labelY = this.Height - timeSize.Height - labelY;
            }

            float x = (((float)time - (float)BeginTime) / (float)nsPerScaleMark) * pixelPerScaleMark;

            nextTime = time + (ulong)(((timeSize.Width / 2f) + 5f) * (float)nsPerScaleMark);

            graphics.DrawString(time.ToString(), timeMarkLabelFont, Brushes.White, x - (timeSize.Width / 2f), labelY);

            return nextTime;
        }

        private void drawNowMarker(ulong time)
        {
            Rectangle backRect = new Rectangle(
                this.TimeLineX,
                0,
                this.Width,
                this.Height
                );

            Bitmap tmpBmp = new Bitmap(backRect.Width, backRect.Height);
            using (Graphics tmpBmpGraphics = Graphics.FromImage(tmpBmp))
            {
                float x = timeToX(time);
                using (Pen pen = new Pen(NowMarkerColor))
                {
                    //pen.DashStyle = DashStyle.Dash;
                    tmpBmpGraphics.DrawLine(pen, x, 0, x, backRect.Height);
                }
                if (IsDisplayNowMarkTime)
                {
                    SizeF timeSize = getTimeMarkLabelWidth(time);

                    float labelY = 1;
                    float nowLabelBottomY = Height - (scaleMarkHeight * 2);
                    float nowLabelMiddleY = labelY + timeSize.Height;
                    float nowLabelTopY = labelY;

                    if (ScaleMarkDirection == ScaleMarkDirection.Top)
                    {
                        labelY = this.Height - timeSize.Height - labelY;
                        nowLabelBottomY = scaleMarkHeight * 2;
                        nowLabelTopY = labelY + timeSize.Height;
                        nowLabelMiddleY = labelY;
                    };

                    int margin = 2;

                    PointF[] points = new[] {
                        new PointF(x - (timeSize.Width / 2) - margin, nowLabelTopY),
                        new PointF(x + (timeSize.Width / 2) + margin, nowLabelTopY),
                        new PointF(x + (timeSize.Width / 2) + margin, nowLabelMiddleY),
                        new PointF(x, nowLabelBottomY),
                        new PointF(x - (timeSize.Width / 2) - margin, nowLabelMiddleY),
                    };

                    tmpBmpGraphics.SmoothingMode = SmoothingMode.HighQuality;
                    using (SolidBrush brush = new SolidBrush(Color.White))
                    {
                        tmpBmpGraphics.FillPolygon(brush, points);
                    }
                    using (Pen pen = new Pen(NowMarkerColor))
                    {
                        tmpBmpGraphics.DrawPolygon(pen, points);
                    }
                    tmpBmpGraphics.SmoothingMode = SmoothingMode.Default;
                    tmpBmpGraphics.DrawString(time.ToString(), timeMarkLabelFont, Brushes.Black, x - (timeSize.Width / 2), labelY);
                }

                using (Graphics graphics = this.CreateGraphics())
                {
                    this.Refresh();
                    graphics.DrawImage(tmpBmp, backRect.X, backRect.Y);
                }
            }
        }

        private ulong xToTime(int x)
        {
            return (ulong)(((decimal)x * ((decimal)nsPerScaleMark / (decimal)pixelPerScaleMark)) + (decimal)beginTime);
        }

        private float timeToX(ulong t)
        {
            return (float)(((decimal)t - (decimal)beginTime) / ((decimal)nsPerScaleMark / (decimal)pixelPerScaleMark));
        }

        private SizeF getTimeMarkLabelWidth(ulong time)
        {
            SizeF size;
            using (Graphics graphics = this.CreateGraphics())
            {
                size = graphics.MeasureString(time.ToString(), timeMarkLabelFont);
            }
            return size;
        }

        public void MouseEnterInTimeLineGrid(object sender, WinForms.MouseEventArgs e)
        {
            throw new NotImplementedException();
        }

    }
}
