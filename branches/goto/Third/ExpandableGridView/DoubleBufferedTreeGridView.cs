using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using NU.OJL.MPRTOS.TLV.Base;
using NU.OJL.MPRTOS.TLV.Base.Controls;
using System.Drawing;

namespace NU.OJL.MPRTOS.TLV.Third
{
	public class DoubleBufferedTreeGridView : AdvancedDataGridView.TreeGridView
	{
		public DoubleBufferedTreeGridView()
		{
			DoubleBuffered = true;
		}
	}
}
