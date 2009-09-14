
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
	public partial class TextNumericUpDownTrackBarControl : UserControl
	{
		private MiniTrackBarForm trackBar = new MiniTrackBarForm();
		private decimal _value = 0.0m;
		private int _radix = 10;
		private decimal _tick = 1.0m;

		public decimal Value
		{
			get { return _value; }
			set
			{
				if (_value != value && value <= Maximum && value >= Minimum)
				{
					_value = value;
					Text = Value.ToString(Radix);
					OnValidated(EventArgs.Empty);
				}
			}
		}
		public decimal Maximum { get; set; }
		public decimal Minimum { get; set; }
		public int Radix
		{
			get { return _radix; }
			set
			{
				if (_radix != value)
				{
					_radix = value;
					
					if (Text != _value.ToString(Radix))
						Text = _value.ToString(Radix);
				}
			}
		}
		public decimal Tick
		{
			get { return _tick; }
			set
			{
				if (_tick != value)
				{
					_tick = value;
				}
			}
		}
		public override string Text
		{
			get
			{
				return textBox.Text;
			}
			set
			{
				if (textBox.Text != value && value != null && value != string.Empty)
				{
					if (IsHandleCreated)
					{
						Invoke(new MethodInvoker(() =>
						{
							textBox.Text = value;
							Value = value.ToDecimal(Radix);
						}));
					}
				}
			}
		}

		private readonly int buttonWidths = 0;

		public TextNumericUpDownTrackBarControl()
		{
			InitializeComponent();
			buttonWidths = vScrollBar.Width + trackBarButton.Width;
			MinimumSize = new Size(buttonWidths + 50, 14);
			trackBar.Minimum = 0;
			trackBar.Maximum = int.MaxValue;
			trackBar.TickFrequency = (trackBar.Maximum - trackBar.Minimum) / 4;
			trackBar.StartPosition = FormStartPosition.Manual;
			trackBar.Deactivate += (o, e) =>
			{
				trackBarButton.CheckState = CheckState.Unchecked;
			};
		}

		protected override void OnLoad(EventArgs e)
		{
			base.OnLoad(e);
			vScrollBar.ValueChanged += vScrollBarValueChanged;
			textBox.Validated += (o, _e) => { OnValidated(_e); };
			textBox.TextChanged += (o, _e) =>
			{
				if (AutoSize)
				{
					int w = TextRenderer.MeasureText(Text, Font).Width + buttonWidths;
					Width = w < MinimumSize.Width ? MinimumSize.Width : w;
				}
				OnTextChanged(_e);
			};
			textBox.KeyPress += textBoxKeyPress;
			textBox.KeyUp += textBoxKeyUp;
			trackBar.ValueChanged += trackBarValueChanged;
			trackBar.MiniTrackBar.TrackBar.KeyDown += (o, _e) =>
			{
				if ((_e.KeyCode & Keys.Enter) == Keys.Enter)
					trackBarButton.CheckState = CheckState.Unchecked;
			};

			trackBar.MiniTrackBar.TrackBar.PreviewKeyDown += (o, _e) =>
			{
				decimal v = (trackBar.Maximum - trackBar.Minimum) / 100;

				if ((_e.KeyData & Keys.Right) == Keys.Left)
				{
					if (Maximum - Minimum != 0)
					{
						decimal r = (Value - (Maximum - Minimum)/100 - Minimum) / (Maximum - Minimum) * (decimal)(trackBar.Maximum - trackBar.Minimum);
						trackBar.Value = r > trackBar.Maximum ? trackBar.Maximum - 1 : r < trackBar.Minimum ? trackBar.Minimum : (int)r;
					}
				}
				if ((_e.KeyData & Keys.Right) == Keys.Right)
				{
					if (Maximum - Minimum != 0)
					{
						decimal r = (Value + (Maximum - Minimum) / 100 - Minimum) / (Maximum - Minimum) * (decimal)(trackBar.Maximum - trackBar.Minimum);
						trackBar.Value = r > trackBar.Maximum ? trackBar.Maximum - 1 : r < trackBar.Minimum ? trackBar.Minimum : (int)r;
					}
				}
			};
			trackBarButton.CheckStateChanged += trackBarButtonCheckStateChanged;
		}

		private void trackBarValueChanged(object sender, EventArgs e)
		{
			Value = Math.Round(Minimum + (Maximum - Minimum) * trackBar.Value / (trackBar.Maximum - trackBar.Minimum));
		}

		private void trackBarButtonCheckStateChanged(object sender, EventArgs e)
		{
			if (trackBarButton.CheckState == CheckState.Checked)
			{
				if (Maximum - Minimum != 0)
				{
					decimal r = (Value - Minimum) / (Maximum - Minimum) * (decimal)(trackBar.Maximum - trackBar.Minimum);
					trackBar.Value = r > trackBar.Maximum ? trackBar.Maximum : r < trackBar.Minimum ? trackBar.Minimum : (int)r;
				}
				Point sp = PointToScreen(trackBarButton.Location);
				trackBar.Location = new Point(sp.X, sp.Y + trackBarButton.Height);
				trackBar.Show();
			}
			else if (trackBarButton.CheckState == CheckState.Unchecked)
			{
				trackBar.Hide();
			}
		}

		private void textBoxKeyPress(object sender, KeyPressEventArgs e)
		{
			if (!char.IsControl(e.KeyChar) && !e.KeyChar.ToString().IsValid(Radix) && e.KeyChar != '-' && e.KeyChar != '.')
			{
				e.Handled = true;
			}
		}

		private void textBoxKeyUp(object sender, KeyEventArgs e)
		{
			base.OnKeyUp(e);

			if ((e.KeyCode & Keys.Enter) == Keys.Enter)
			{
				try
				{
					decimal v = textBox.Text.ToDecimal(Radix);
					Value = v;
					textBox.Text = Value.ToString(Radix);
				}
				catch { }
				Text = textBox.Text;

				OnValidated(EventArgs.Empty);
			}
		}

		private void vScrollBarValueChanged(object sender, EventArgs e)
		{
			if (vScrollBar.Value > 50)
			{
				downButton();
			}
			else if (vScrollBar.Value < 50)
			{
				upButton();
			}
			else
			{
				return;
			}
			vScrollBar.Value = 50;
		}

		private void downButton()
		{
			Value -= Tick;
		}

		private void upButton()
		{
			Value += Tick;
		}

	}
}
