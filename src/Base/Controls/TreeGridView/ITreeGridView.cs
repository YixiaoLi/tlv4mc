using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace NU.OJL.MPRTOS.TLV.Base.Controls
{
	public interface ITreeGridView
	{
		Dictionary<string, ITreeGirdViewNode> Nodes { get; }
		void Add(string name, params object[] values);
	}
}
