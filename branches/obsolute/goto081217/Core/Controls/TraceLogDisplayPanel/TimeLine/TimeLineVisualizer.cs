using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Windows.Forms;

namespace NU.OJL.MPRTOS.TLV.Core.Controls
{
	partial class TimeLineVisualizer : TimeLine
	{
		public TimeLineVisualizer(TraceLogVisualizerData data)
			:base(data)
		{
			InitializeComponent();
		}

		public override void Draw(PaintEventArgs e)
		{
			base.Draw(e);
		}
	}
}
