using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using NU.OJL.MPRTOS.TLV.Base;
using NU.OJL.MPRTOS.TLV.Base.Controls;
using System.Drawing;

namespace NU.OJL.MPRTOS.TLV.Third
{
	public class TreeGridView : Control, ITreeGridView
	{
		public event DataGridViewRowEventHandler RowHeightChanged = null;
		public event CollectionChangeEventHandler RowCountChanged = null;
		public DataGridView DataGridView { get { return treeGridView; } }

		DoubleBufferedTreeGridView treeGridView = new DoubleBufferedTreeGridView();

		private Dictionary<string, ITreeGirdViewNode> _nodes = new Dictionary<string, ITreeGirdViewNode>();

		public Dictionary<string, ITreeGirdViewNode> Nodes
		{
			get { return _nodes; }
		}

		public void Add(string name, params object[] values)
		{
			TreeGridViewNode node = new TreeGridViewNode(name, treeGridView, values);
			_nodes.Add(name, node);
			treeGridView.Nodes.Add(node);
		}

		public TreeGridView()
		{
			this.DoubleBuffered = true;
			treeGridView.ApplyNativeScroll();
			treeGridView.RowHeightChanged += (o,e) => RowHeightChanged(o,e);
			treeGridView.Rows.CollectionChanged += (o, e) => RowCountChanged(o, e);
			treeGridView.ShowLines = false;
			treeGridView.Dock = DockStyle.Fill;
			treeGridView.AllowUserToAddRows = false;
			treeGridView.AllowUserToDeleteRows = false;
			treeGridView.AllowUserToResizeRows = false;
			treeGridView.BorderStyle = System.Windows.Forms.BorderStyle.None;
			treeGridView.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.Disable;
			treeGridView.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
			dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
			dataGridViewCellStyle1.Font = new System.Drawing.Font("MS UI Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
			dataGridViewCellStyle1.Padding = new System.Windows.Forms.Padding(2);
			dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
			dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
			treeGridView.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
			treeGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
			treeGridView.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
			treeGridView.ReadOnly = true;
			treeGridView.RowHeadersVisible = false;
			treeGridView.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
			treeGridView.RowTemplate.Height = 21;
			treeGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
			Controls.Add(treeGridView);
		}

		public void AddColumn(DataGridViewColumn treeGridViewColumn)
		{
			treeGridView.Columns.Add(treeGridViewColumn);
		}

		public void Clear()
		{
			treeGridView.Nodes.Clear();
			_nodes.Clear();
		}

		public int VisibleRowsCount
		{
			get
			{
				int num = 0;

				foreach (DataGridViewRow row in treeGridView.Rows)
				{
					if (row.Visible)
						num++;
				}

				return num;
			}
		}

		public int RowHeight
		{
			get { return treeGridView.RowTemplate.Height; }
		}

		public int ColumnHeadersHeight
		{
			get { return treeGridView.ColumnHeadersHeight; }
		}
	}
}
