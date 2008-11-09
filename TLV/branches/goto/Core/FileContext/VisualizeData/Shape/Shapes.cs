using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace NU.OJL.MPRTOS.TLV.Core
{
	public class Shapes : GeneralJsonableCollection<Shape, Shapes>, INamed
	{
		public string Name { get; set; }
	}
}
