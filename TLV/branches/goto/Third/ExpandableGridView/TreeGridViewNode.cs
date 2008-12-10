using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using NU.OJL.MPRTOS.TLV.Base;
using NU.OJL.MPRTOS.TLV.Base.Controls;
using System.Drawing;

namespace NU.OJL.MPRTOS.TLV.Third
{
	public class TreeGridViewNode : AdvancedDataGridView.TreeGridNode, ITreeGirdViewNode
	{
		private Dictionary<string, ITreeGirdViewNode> _nodes = new Dictionary<string, ITreeGirdViewNode>();
		private string _imageKey = string.Empty;

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

		protected override void PaintCells(Graphics graphics, Rectangle clipBounds, Rectangle rowBounds, int rowIndex, DataGridViewElementStates rowState, bool isFirstDisplayedRow, bool isLastVisibleRow, DataGridViewPaintParts paintParts)
		{
			base.PaintCells(graphics, clipBounds, rowBounds, rowIndex, rowState, isFirstDisplayedRow, isLastVisibleRow, paintParts);

			int bottomLevel = 1;
			for (int i = 1; rowIndex + i < DataGridView.RowCount; i++)
			{
				if (((AdvancedDataGridView.TreeGridNode)(DataGridView.Rows[rowIndex + i])).Visible)
				{
					bottomLevel = ((AdvancedDataGridView.TreeGridNode)(DataGridView.Rows[rowIndex + i])).Level;
					break;
				}
			}

			// 下にあるノードの方がネストが深い場合
			if (Level < bottomLevel)
			{
				graphics.DrawLine(new Pen(DataGridView.DefaultCellStyle.BackColor), rowBounds.X + 1, rowBounds.Y + rowBounds.Height - 1, rowBounds.X + (16 * Level) - 2, rowBounds.Y + rowBounds.Height - 1);
			}
			// トップレベルでなく、下にあるノードとネストの深さが同じである場合
			else if (Level == bottomLevel && Level > 1)
			{
				graphics.DrawLine(new Pen(DataGridView.DefaultCellStyle.BackColor), rowBounds.X + 1, rowBounds.Y + rowBounds.Height - 1, rowBounds.X + (16 * (Level - 1)) - 2, rowBounds.Y + rowBounds.Height - 1);
			}
			// トップレベルでなく、下にあるノードの方がネストが浅い場合
			else if (Level > bottomLevel && bottomLevel > 1)
			{
				graphics.DrawLine(new Pen(DataGridView.DefaultCellStyle.BackColor), rowBounds.X + 1, rowBounds.Y + rowBounds.Height - 1, rowBounds.X + (16 * (bottomLevel - 1)) - 2, rowBounds.Y + rowBounds.Height - 1);
			}

			if (Level > 1)
			{
				for (int j = 1; j <= Level; j++)
				{
					graphics.DrawLine(new Pen(DataGridView.GridColor), rowBounds.X + (16 * (j - 1)) - 1, rowBounds.Y -1, rowBounds.X + (16 * (j - 1)) - 1, rowBounds.Y + rowBounds.Height);
				}
			}
		}
	}
}
