using System;
using System.Drawing;
using System.Collections.Generic;
using WinForms = System.Windows.Forms;
using NU.OJL.MPRTOS.TLV.Architecture.PAC;

namespace NU.OJL.MPRTOS.TLV.Core.TimeLineControl.TimeLine
{
    public partial class TimeLineP : WinForms.UserControl, IPresentation
    {
        private Point mouseDownPoint = new Point();
        private WinForms.Cursor resizeCursor = WinForms.Cursors.SizeWE;
        private bool isResizing = false;
        private int timeLineX;
        private int timeLineMinimumX = 0;
        public int TimeLineX
        {
            get { return timeLineX; }
            set
            {
                if (timeLineX != value)
                {
                    timeLineX = value;
                    NotifyPropertyChanged("TimeLineX");
                    onTimeLineXChanged();
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

        public event WinForms.MouseEventHandler TimeLineXResizing = null;
        public event WinForms.MouseEventHandler TimeLineXResized = null;

        public TimeLineP(string name)
        {
            this.Name = name;
            this.BackColor = Color.Transparent;
            this.Height = 30;
            this.DoubleBuffered = true;
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
        }

        private void onTimeLineXChanged()
        {
            this.Refresh();
        }

    }
}
