/*
 *  TLV - Trace Log Visualizer
 *
 *  Copyright (C) 2008,2009 by Nagoya Univ., JAPAN
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
