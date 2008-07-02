using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace NU.OJL.MPRTOS.TLV.Core.TimeLineControl
{
    public partial class TimeLine : UserControl
    {
        private bool isResizeble = false;
        private Cursor resizeCursor = Cursors.SizeWE;
        private Point mouseDownPoint = new Point();
        private int timeLinePositionX = 0;
        private int timeLinePositionMinimumX = 0;
        private Rectangle resizingCursorClip = Rectangle.Empty;

        public int TimeLinePisitionX
        {
            get { return timeLinePositionX; }
            set
            {
                timeLinePositionX = value;
                this.Refresh();
            }
        }
        public int TimeLinePositionMinimumX
        {
            get { return timeLinePositionMinimumX; }
            set { timeLinePositionMinimumX = value; }
        }
        public Rectangle ResizingCursorClip
        {
            get { return resizingCursorClip; }
            set { resizingCursorClip = value; }
        }

        public event EventHandler Resized;
        public event MouseEventHandler Resizing;

        public TimeLine()
        {
            InitializeComponent();
            this.DoubleBuffered = true;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            Rectangle backRect = new Rectangle(0, 0, this.Width - timeLinePositionX, this.Height);
            if (backRect.Width > 0 && backRect.Height > 0)
            {
                Bitmap tmpBmp = new Bitmap(backRect.Width, backRect.Height);
                using (Graphics tmpBmpGraphics = Graphics.FromImage(tmpBmp))
                {
                    drawTimeLine(tmpBmpGraphics, backRect.Width, backRect.Height);
                }
                e.Graphics.DrawImage(tmpBmp, timeLinePositionX, 0);
            }
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            if (this.Cursor == resizeCursor)
            {
                if (! resizingCursorClip.IsEmpty)
                {
                    Cursor.Clip = new Rectangle(this.PointToScreen(new Point(resizingCursorClip.Location.X, resizingCursorClip.Location.Y)), resizingCursorClip.Size);
                }
                mouseDownPoint.X = e.X;
                mouseDownPoint.Y = e.Y;
                isResizeble = true;
            }
            base.OnMouseDown(e);
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);
            if(isResizeble)
            {
                int x = e.X >= this.Width ? this.Width : e.X < timeLinePositionMinimumX ? timeLinePositionMinimumX : e.X;

                int delta = x - mouseDownPoint.X;
                this.timeLinePositionX += delta;
                Resized(this, EventArgs.Empty);
                isResizeble = false;

                Cursor.Clip = Rectangle.Empty;
            }
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);

            if (e.X < timeLinePositionX + 5 && e.X > timeLinePositionX - 5)
            {
                if (this.Cursor != resizeCursor)
                {
                    this.Cursor = resizeCursor;
                }
            }
            else if (this.Cursor == resizeCursor && !isResizeble)
            {
                this.Cursor = DefaultCursor;
            }

            if (isResizeble)
            {
                Resizing(this, e);
            }

        }

        private void drawTimeLine(Graphics graphics, int width, int height)
        {
            Rectangle backRect = new Rectangle(0, 0, width, height);

            Bitmap tmpBmp = new Bitmap(backRect.Width, backRect.Height);
            using (Graphics tmpBmpGraphics = Graphics.FromImage(tmpBmp))
            {
                tmpBmpGraphics.FillRectangle(Brushes.Black, backRect);
            }
            graphics.DrawImage(tmpBmp, 0, 0);
        }

    }
}
