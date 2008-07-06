using System;
using System.Drawing;
using System.Collections.Generic;
using WinForms = System.Windows.Forms;
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
        private ulong displayTimeLength = 0;
        private ulong nsPerPixel = 1;

        public ScaleMarkDirection ScaleMarkDirection { get; set; }
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
                    NotifyPropertyChanged("BeginTime");
                    this.Refresh();
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
                    this.Refresh();
                }
            }
        }
        public ulong NsPerPixel
        {
            get { return nsPerPixel; }
            set
            {
                if (nsPerPixel != value)
                {
                    nsPerPixel = value;
                    NotifyPropertyChanged("NsPerPixel");
                    this.Refresh();
                }
            }
        }

        public event WinForms.MouseEventHandler TimeLineXResizing = null;
        public event WinForms.MouseEventHandler TimeLineXResized = null;

        public TimeLineP(string name)
            : this(name, ScaleMarkDirection.Bottom) { }

        public TimeLineP(string name, ScaleMarkDirection scaleMarkDirection)
        {
            this.Name = name;
            this.BackColor = Color.Transparent;
            this.Height = 30;
            this.DoubleBuffered = true;
            this.ScaleMarkDirection = scaleMarkDirection;
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

            ulong endTime = this.BeginTime + this.DisplayTimeLength;

            int scaleMarkIntervalPixel = 5;
            ulong scaleMarkIntervalTime = (ulong)scaleMarkIntervalPixel * nsPerPixel;
            int order = (int)Math.Ceiling(Math.Log10((double)scaleMarkIntervalTime + 1D));
            ulong scaleMarkStartTime = (ulong)(Math.Floor((double)BeginTime / Math.Pow(10D, order - 1)) * Math.Pow(10D, order - 1)) + scaleMarkIntervalTime;

            float scaleMarkHeight = 5;
            float scaleMarkY = height - scaleMarkHeight;
            if (ScaleMarkDirection == ScaleMarkDirection.Top)
            {
                scaleMarkY = 0f;
            }
            int i = 1;
            for (ulong time = scaleMarkStartTime; time < endTime; time += scaleMarkIntervalTime, i++)
            {
                float x = ((float)time - (float)BeginTime) / (float)nsPerPixel;
                float y = scaleMarkY;
                float h = scaleMarkHeight;
                if(i % 10 == 0)
                {
                    if (ScaleMarkDirection == ScaleMarkDirection.Bottom)
                    {
                        y -= h;
                    }
                    h *= 2f;
                    drawTimeMarkLabel(graphics, time);
                }
                else if (i % 5 == 0)
                {
                    if (ScaleMarkDirection == ScaleMarkDirection.Bottom)
                    {
                        y -= 0.5f * h;
                    }
                    h *= 1.5f;
                }

                graphics.DrawLine(Pens.White, x, y, x, y + h);
            }

        }

        private void drawTimeMarkLabel(Graphics graphics, ulong time)
        {
            using (Font font = new Font(FontFamily.GenericMonospace, 8))
            {
                SizeF timeSize = graphics.MeasureString(time.ToString(), font);

                float labelY = 1;

                if (ScaleMarkDirection == ScaleMarkDirection.Top)
                {
                    labelY = this.Height - timeSize.Height - labelY;
                }

                float x = ((float)time - (float)BeginTime) / (float)nsPerPixel;

                graphics.DrawString(time.ToString(), font, Brushes.White, x - (timeSize.Width / 2f), labelY);
            }
        }

    }
}
