using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace NU.OJL.MPRTOS.TLV.Core.Controls
{
	public interface ITimeLine
	{
		void Draw(Graphics graphics, Rectangle clipBounds);
	}
}
