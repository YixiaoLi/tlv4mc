
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace NU.OJL.MPRTOS.TLV.Base.Controls
{
	public class ToolStripTextNumericUpDown : ToolStripControlHost
	{
		public decimal Value
		{
			get { return TextNumericUpDownTrackBarControl.Value; }
			set { TextNumericUpDownTrackBarControl.Value = value; }
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
			AutoSize = true;
		}

		public TextNumericUpDownTrackBarControl TextNumericUpDownTrackBarControl
		{
			get
			{
				return (TextNumericUpDownTrackBarControl)Control;
			}
		}

		public new bool AutoSize
		{
			get { return TextNumericUpDownTrackBarControl.AutoSize; }
			set { TextNumericUpDownTrackBarControl.AutoSize = value; }
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
