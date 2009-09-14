
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
