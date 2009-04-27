/*
 *  TLV - Trace Log Visualizer
 *
 *  Copyright (C) 2008,2009 by Embedded and Real-Time Systems Laboratory
 *              Graduate School of Information Science, Nagoya Univ., JAPAN
 *
 *  �嵭����Ԥϡ��ʲ���(1)��(4)�ξ������������˸¤ꡤ�ܥ��եȥ���
 *  �����ܥ��եȥ���������Ѥ�����Τ�ޤࡥ�ʲ�Ʊ���ˤ���ѡ�ʣ������
 *  �ѡ������ۡʰʲ������ѤȸƤ֡ˤ��뤳�Ȥ�̵���ǵ������롥
 *  (1) �ܥ��եȥ������򥽡��������ɤη������Ѥ�����ˤϡ��嵭������
 *      ��ɽ�����������Ѿ�浪��Ӳ�����̵�ݾڵ��꤬�����Τޤޤη��ǥ���
 *      ����������˴ޤޤ�Ƥ��뤳�ȡ�
 *  (2) �ܥ��եȥ������򡤥饤�֥������ʤɡ�¾�Υ��եȥ�������ȯ�˻�
 *      �ѤǤ�����Ǻ����ۤ�����ˤϡ������ۤ�ȼ���ɥ�����ȡ�����
 *      �ԥޥ˥奢��ʤɡˤˡ��嵭�����ɽ�����������Ѿ�浪��Ӳ���
 *      ��̵�ݾڵ����Ǻܤ��뤳�ȡ�
 *  (3) �ܥ��եȥ������򡤵�����Ȥ߹���ʤɡ�¾�Υ��եȥ�������ȯ�˻�
 *      �ѤǤ��ʤ����Ǻ����ۤ�����ˤϡ����Τ����줫�ξ�����������
 *      �ȡ�
 *    (a) �����ۤ�ȼ���ɥ�����ȡ����Ѽԥޥ˥奢��ʤɡˤˡ��嵭����
 *        �ɽ�����������Ѿ�浪��Ӳ�����̵�ݾڵ����Ǻܤ��뤳�ȡ�
 *    (b) �����ۤη��֤��̤�������ˡ�ˤ�äơ�TOPPERS�ץ������Ȥ�
 *        ��𤹤뤳�ȡ�
 *  (4) �ܥ��եȥ����������Ѥˤ��ľ��Ū�ޤ��ϴ���Ū�������뤤���ʤ�»
 *      ������⡤�嵭����Ԥ����TOPPERS�ץ������Ȥ����դ��뤳�ȡ�
 *      �ޤ����ܥ��եȥ������Υ桼���ޤ��ϥ���ɥ桼������Τ����ʤ���
 *      ͳ�˴�Ť����ᤫ��⡤�嵭����Ԥ����TOPPERS�ץ������Ȥ�
 *      ���դ��뤳�ȡ�
 *
 *  �ܥ��եȥ������ϡ�̵�ݾڤ��󶡤���Ƥ����ΤǤ��롥�嵭����Ԥ�
 *  ���TOPPERS�ץ������Ȥϡ��ܥ��եȥ������˴ؤ��ơ�����λ�����Ū
 *  ���Ф���Ŭ������ޤ�ơ������ʤ��ݾڤ�Ԥ�ʤ����ޤ����ܥ��եȥ���
 *  �������Ѥˤ��ľ��Ū�ޤ��ϴ���Ū�������������ʤ�»���˴ؤ��Ƥ⡤��
 *  ����Ǥ�����ʤ���
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
