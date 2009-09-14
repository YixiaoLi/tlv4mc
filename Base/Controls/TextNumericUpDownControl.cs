
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace NU.OJL.MPRTOS.TLV.Base.Controls
{
	public class TextNumericUpDownControl : UpDownBase
	{

		private decimal _value = 0.0m;
		private int _radix = 10;
		private decimal _tick = 1.0m;

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
				if(_tick != value)
				{
					_tick = value;
				}
			}
		}

		public TextNumericUpDownControl()
		{
			ImeMode = ImeMode.Disable;
			TextAlign = HorizontalAlignment.Right;
			BorderStyle = BorderStyle.None;
		}

		public override void DownButton()
		{
			_value = Text.ToDecimal(Radix);
			_value -= Tick;
			UpdateEditText();
			OnValidated(EventArgs.Empty);
		}

		public override void UpButton()
		{
			_value = Text.ToDecimal(Radix);
			_value += Tick;
			UpdateEditText();
			OnValidated(EventArgs.Empty);
		}

		protected override void OnKeyUp(KeyEventArgs e)
		{
			base.OnKeyUp(e);

			if ((e.KeyCode & Keys.Enter) == Keys.Enter)
				OnValidated(EventArgs.Empty);
		}

		protected override void OnKeyPress(KeyPressEventArgs e)
		{
			if (!char.IsControl(e.KeyChar) && !e.KeyChar.ToString().IsValid(Radix) && e.KeyChar != '-' && e.KeyChar != '.')
			{
				e.Handled = true;
			}
		}

		protected override void UpdateEditText()
		{
			Text = _value.ToString(Radix);
		}

		protected override void ValidateEditText()
		{
			try
			{
				_value = Text.ToDecimal(Radix);
			}
			catch
			{
				Text = _value.ToString(Radix);
			}
		}
	}
}
