/*
 *  TLV - Trace Log Visualizer
 *
 *  Copyright (C) 2008,2009 by Embedded and Real-Time Systems Laboratory
 *              Graduate School of Information Science, Nagoya Univ., JAPAN
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
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace NU.OJL.MPRTOS.TLV.Base.Controls
{
	public partial class MiniTrackBarForm : Form
	{
		
		public event EventHandler ValueChanged;

		private int _mouseDownX = -1;
		private int _WidthWhenMouseDown = -1;

		public int Value { get { return miniTrackBar.Value; } set { if (Value != value) miniTrackBar.Value = value; } }
		public int Maximum { get { return miniTrackBar.Maximum; } set { if (Maximum != value) miniTrackBar.Maximum = value; } }
		public int Minimum { get { return miniTrackBar.Minimum; } set { if (Minimum != value) miniTrackBar.Minimum = value; } }
		public int TickFrequency { get { return miniTrackBar.TickFrequency; } set { if (TickFrequency != value) miniTrackBar.TickFrequency = value; } }
		public TickStyle TickStyle { get { return miniTrackBar.TickStyle; } set { if (TickStyle != value) miniTrackBar.TickStyle = value; } }

		public MiniTrackBar MiniTrackBar { get { return miniTrackBar; } }

		public MiniTrackBarForm()
		{
			InitializeComponent();
			miniTrackBar.ValueChanged += (o, e) => { if (ValueChanged != null) ValueChanged(o, e); };
		}

		protected override void OnLoad(EventArgs e)
		{
			base.OnLoad(e);

			resizeBar.MouseEnter += (o, _e) =>
				{
					if (Cursor != Cursors.SizeWE && _mouseDownX == -1 && _WidthWhenMouseDown == -1)
					{
						Cursor = Cursors.SizeWE;
					}
				};
			resizeBar.MouseLeave += (o, _e) =>
				{
					if (Cursor != Cursors.Default && _mouseDownX == -1 && _WidthWhenMouseDown == -1)
					{
						Cursor = Cursors.Default;
					}
				};
			resizeBar.MouseDown += (o, _e) =>
				{
					_mouseDownX = PointToScreen(_e.Location).X;
					_WidthWhenMouseDown = Width;
				};
			resizeBar.MouseUp += (o, _e) =>
				{
					_mouseDownX = -1;
					_WidthWhenMouseDown = -1;
				};
			resizeBar.MouseMove += (o, _e) =>
				{
					if (Cursor == Cursors.SizeWE && _WidthWhenMouseDown != -1 && _mouseDownX != -1)
					{
						int w = _WidthWhenMouseDown + (PointToScreen(_e.Location).X - _mouseDownX);
						Width = w < 50 ? 50 : w;
						_WidthWhenMouseDown = Width;
					}
				};
		}
	}
}
