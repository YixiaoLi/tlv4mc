using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace NU.OJL.MPRTOS.TLV.Base.Controls
{
	public interface ITreeGirdViewNode
	{
		ITreeGirdViewNode Parent { get; }
		bool HasChildren { get; }
		bool HasChild(string name);
		bool Collapse();
		bool Expand();
		string Name { get; }
		int Level { get; }
		bool Visible { get; set; }
		Dictionary<string, ITreeGirdViewNode> Nodes { get; }
		void Add(string name, params object[] values);
		Image Image { get; set; }
	}
}
