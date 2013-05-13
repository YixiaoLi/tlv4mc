/*
 *  TLV - Trace Log Visualizer
 *
 *  Copyright (C) 2008-2013 by Nagoya Univ., JAPAN
 *
 *  上記著作権者は，以下の(1)〜(4)の条件を満たす場合に限り，本ソフトウェ
 *  ア（本ソフトウェアを改変したものを含む．以下同じ）を使用・複製・改
 *  変・再配布（以下，利用と呼ぶ）することを無償で許諾する．
 *  (1) 本ソフトウェアをソースコードの形で利用する場合には，上記の著作
 *      権表示，この利用条件および下記の無保証規定が，そのままの形でソー
 *      スコード中に含まれていること．
 *  (2) 本ソフトウェアを，ライブラリ形式など，他のソフトウェア開発に使
 *      用できる形で再配布する場合には，再配布に伴うドキュメント（利用
 *      者マニュアルなど）に，上記の著作権表示，この利用条件および下記
 *      の無保証規定を掲載すること．
 *  (3) 本ソフトウェアを，機器に組み込むなど，他のソフトウェア開発に使
 *      用できない形で再配布する場合には，次のいずれかの条件を満たすこ
 *      と．
 *    (a) 再配布に伴うドキュメント（利用者マニュアルなど）に，上記の著
 *        作権表示，この利用条件および下記の無保証規定を掲載すること．
 *    (b) 再配布の形態を，別に定める方法によって，TOPPERSプロジェクトに
 *        報告すること．
 *  (4) 本ソフトウェアの利用により直接的または間接的に生じるいかなる損
 *      害からも，上記著作権者およびTOPPERSプロジェクトを免責すること．
 *      また，本ソフトウェアのユーザまたはエンドユーザからのいかなる理
 *      由に基づく請求からも，上記著作権者およびTOPPERSプロジェクトを
 *      免責すること．
 *
 *  本ソフトウェアは，無保証で提供されているものである．上記著作権者お
 *  よびTOPPERSプロジェクトは，本ソフトウェアに関して，特定の使用目的
 *  に対する適合性も含めて，いかなる保証も行わない．また，本ソフトウェ
 *  アの利用により直接的または間接的に生じたいかなる損害に関しても，そ
 *  の責任を負わない．
 *
 *  @(#) $Id$
 */
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
