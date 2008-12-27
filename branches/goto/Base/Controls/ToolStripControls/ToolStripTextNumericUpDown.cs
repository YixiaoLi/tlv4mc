using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace NU.OJL.MPRTOS.TLV.Base.Controls
{
	public class ToolStripTextNumericUpDown : ToolStripControlHost
	{
		public int TextBoxWidth
		{
			get { return Width - 28; }
			set { Width = value + 28; }
		}
		public decimal Maximum
		{
			get { return TextNumericUpDownTrackBarControl.Maximum; }
			set { TextNumericUpDownTrackBarControl.Maximum = value; }
		}
		public decimal Minimum
		{
			get { return TextNumericUpDownTrackBarControl.Minimum; }
			set { TextNumericUpDownTrackBarControl.Minimum = value; }
		}

		/// <summary>
		/// コンストラクタ
		/// </summary>
		public ToolStripTextNumericUpDown()
			: base(new TextNumericUpDownTrackBarControl())
		{

		}

		public TextNumericUpDownTrackBarControl TextNumericUpDownTrackBarControl
		{
			get
			{
				return (TextNumericUpDownTrackBarControl)Control;
			}
		}

		public int Radix
		{
			get
			{
				return TextNumericUpDownTrackBarControl.Radix;
			}
			set
			{
				TextNumericUpDownTrackBarControl.Radix = value;
			}
		}

	}
}
