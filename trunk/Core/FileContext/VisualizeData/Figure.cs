
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NU.OJL.MPRTOS.TLV.Core
{
	public class Figure : ICloneable
	{
		public string Condition { get; set; }
		public Figures Figures { get; set; }
		public string Shape { get; set; }
		public string[] Args { get; set; }
		public bool IsShape { get { return Shape != null; } }
		public bool IsFigures { get { return Figures != null; } }

		public Figure()
		{

		}

		public object Clone()
		{
			Figure fg = new Figure();
			fg.Condition = this.Condition;
			fg.Shape = this.Shape;
			fg.Figures = this.Figures;
			if (this.Figures != null)
			{
				foreach(Figure f in this.Figures)
				{
					fg.Figures.Add((Figure)f.Clone());
				}
			}
			fg.Args = this.Args;

			return fg;
		}

	}
}
