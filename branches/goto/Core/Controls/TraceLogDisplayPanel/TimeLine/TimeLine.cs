using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Design;
using System.Drawing.Drawing2D;

namespace NU.OJL.MPRTOS.TLV.Core.Controls
{
	partial class TimeLine : UserControl, ITimeLine
	{
		public TimeLine()
		{
			InitializeComponent();
		}

		public virtual void Draw(Graphics graphics, Rectangle clipBounds)
		{
			
		}
	}
}
