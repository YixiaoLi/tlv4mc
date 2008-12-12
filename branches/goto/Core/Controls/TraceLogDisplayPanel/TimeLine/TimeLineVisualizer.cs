using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace NU.OJL.MPRTOS.TLV.Core.Controls
{
	partial class TimeLineVisualizer : TimeLine
	{
		public TimeLineVisualizer()
		{
			InitializeComponent();
		}

		public override void Draw(Graphics graphics, Rectangle clipBounds)
		{
			base.Draw(graphics, clipBounds);
			graphics.DrawRectangle(new System.Drawing.Pen(Color.Red), clipBounds);
		}
	}
}
