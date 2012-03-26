/*
 *  TLV - Trace Log Visualizer
 *
 *  Copyright (C) 2008-2011 by Nagoya Univ., JAPAN
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
		class DoubleBufferdTreeGridView : AdvancedDataGridView.TreeGridView
		{
			public DoubleBufferdTreeGridView()
				:base()
			{
				DoubleBuffered = true;
			}
		}

		public event DataGridViewRowEventHandler RowHeightChanged = null;
		public event CollectionChangeEventHandler RowCountChanged = null;
		public DataGridView DataGridView { get { return treeGridView; } }

		DoubleBufferdTreeGridView treeGridView = new DoubleBufferdTreeGridView();

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
