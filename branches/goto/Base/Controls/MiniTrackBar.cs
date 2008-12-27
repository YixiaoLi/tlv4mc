using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace NU.OJL.MPRTOS.TLV.Base.Controls
{
	public partial class MiniTrackBar : UserControl
	{
		public event EventHandler ValueChanged;

		public int Value { get { return trackBar.Value; } set { if (Value != value) trackBar.Value = value; } }
		public int Maximum { get { return trackBar.Maximum; } set { if (Maximum != value) trackBar.Maximum = value; } }
		public int Minimum { get { return trackBar.Minimum; } set { if (Minimum != value) trackBar.Minimum = value; } }
		public int TickFrequency { get { return trackBar.TickFrequency; } set { if (TickFrequency != value) trackBar.TickFrequency = value; } }
		public TickStyle TickStyle { get { return trackBar.TickStyle; } set { if (TickStyle != value) trackBar.TickStyle = value; } }

		public MiniTrackBar()
		{
			InitializeComponent();

			trackBar.ValueChanged += (o, e) => { if (ValueChanged != null) ValueChanged(o, e); };
		}
	}
}
