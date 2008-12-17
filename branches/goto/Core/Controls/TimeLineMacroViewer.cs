using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace NU.OJL.MPRTOS.TLV.Core.Controls
{
	public class TimeLineMacroViewer : TimeLine
	{
		public TimeLineMacroViewer()
		{
			DynamicTimeRangeChange = false;
			BackColor = Color.Black;
		}

		public override void SetData(TraceLogVisualizerData data)
		{
			base.SetData(data);

			FromTime = _data.TraceLogData.MinTime;
			ToTime = _data.TraceLogData.MaxTime;
		}

		public override void Draw(PaintEventArgs e)
		{
			base.Draw(e);

		}
	}
}
