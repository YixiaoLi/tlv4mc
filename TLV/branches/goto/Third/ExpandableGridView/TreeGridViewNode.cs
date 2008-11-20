using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using NU.OJL.MPRTOS.TLV.Base;
using NU.OJL.MPRTOS.TLV.Base.Controls;

namespace NU.OJL.MPRTOS.TLV.Third
{
	public class TreeGridViewNode : AdvancedDataGridView.TreeGridNode, ITreeGirdViewNode
	{
		private Dictionary<string, ITreeGirdViewNode> _nodes = new Dictionary<string, ITreeGirdViewNode>();

		public bool HasChild(string name)
		{
			return Nodes.Values.Any<ITreeGirdViewNode>(n => n.Name == name);
		}

		public string Name { get; private set; }

		public TreeGridViewNode(string name, DataGridView dataGridView, params object[] values)
			:base()
		{
			Name = name;
			DataGridView = dataGridView;
			CreateCells(DataGridView, values);
		}

		public new ITreeGirdViewNode Parent
		{
			get { return (TreeGridViewNode)base.Parent; }
		}

		public new Dictionary<string, ITreeGirdViewNode> Nodes
		{
			get { return _nodes; }
		}

		public void Add(string name, params object[] values)
		{
			TreeGridViewNode node = new TreeGridViewNode(name, DataGridView, values);
			_nodes.Add(name, node);
			base.Nodes.Add(node);
		}

		public new DataGridView DataGridView { get; private set; }
	}
}
