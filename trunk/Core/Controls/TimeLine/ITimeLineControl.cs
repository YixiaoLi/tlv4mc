
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using NU.OJL.MPRTOS.TLV.Base;

namespace NU.OJL.MPRTOS.TLV.Core.Controls
{
	public interface ITimeLineControl : ITraceLogVisualizerControl
	{
		void Draw(Graphics g, Rectangle rect);
		TimeLine TimeLine { get; }
	}
}
