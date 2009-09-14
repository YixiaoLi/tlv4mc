
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace NU.OJL.MPRTOS.TLV.Core
{
	public class TimeLineMarker : INamed
	{
		public event EventHandler SelectedChanged = null;

		public string Name { get; set; }
		public string Text { get; set; }
		public Color Color { get; set; }
		public Time Time { get; set; }
		public bool Selected { get { return _selected; } }
		protected bool _selected = false;

		public TimeLineMarker() { }

		public TimeLineMarker(string name, Color color, Time time)
		{
			Name = name;
			Color = color;
			Time = time;
		}

		public TimeLineMarker(string name, Time time)
			: this(name, ApplicationFactory.ColorFactory.RamdomColor(), time)
		{

		}

		public void SelectToggle()
		{
			if (Selected)
				Unselect();
			else
				Select();
		}

		public void Select()
		{
			_selected = true;
			if (SelectedChanged != null)
				SelectedChanged(this, EventArgs.Empty);
		}

		public void Unselect()
		{
			_selected = false;
			if (SelectedChanged != null)
				SelectedChanged(this, EventArgs.Empty);
		}

	}
}
