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

		public object this[string columnName] { get { return base.Cells[columnName].Value; } }

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

			// ���ˤ���Ρ��ɤ������ͥ��Ȥ��������
			if (Level < bottomLevel)
			{
				Color bc = new Color().FromHsv((int)(DataGridView.GridColor.GetHue()), (int)(DataGridView.GridColor.GetSaturation() * 100f * (1f - (0.15f * bottomLevel))), (int)(DataGridView.GridColor.GetBrightness() * 100f * (1f + (0.15f * bottomLevel))));
				graphics.DrawLine(new Pen(DataGridView.DefaultCellStyle.BackColor), rowBounds.X + 1, rowBounds.Y + rowBounds.Height - 1, rowBounds.X + (16 * Level) - 2, rowBounds.Y + rowBounds.Height - 1);
				graphics.DrawLine(new Pen(bc), rowBounds.X + (16 * (Level)) - 1, rowBounds.Y + rowBounds.Height - 1, rowBounds.X + rowBounds.Width - 1, rowBounds.Y + rowBounds.Height - 1);
			}
			// �ȥåץ�٥�Ǥʤ������ˤ���Ρ��ɤȥͥ��Ȥο�����Ʊ���Ǥ�����
			else if (Level == bottomLevel && Level > 1)
			{
				Color bc = new Color().FromHsv((int)(DataGridView.GridColor.GetHue()), (int)(DataGridView.GridColor.GetSaturation() * 100f * (1f - (0.15f * Level))), (int)(DataGridView.GridColor.GetBrightness() * 100f * (1f + (0.15f * Level))));
				graphics.DrawLine(new Pen(DataGridView.DefaultCellStyle.BackColor), rowBounds.X + 1, rowBounds.Y + rowBounds.Height - 1, rowBounds.X + (16 * (Level - 1)) - 2, rowBounds.Y + rowBounds.Height - 1);
				graphics.DrawLine(new Pen(bc), rowBounds.X + (16 * (Level - 1)) - 1, rowBounds.Y + rowBounds.Height - 1, rowBounds.X + rowBounds.Width - 1, rowBounds.Y + rowBounds.Height - 1);
			}
			// �ȥåץ�٥�Ǥʤ������ˤ���Ρ��ɤ������ͥ��Ȥ��������
			else if (Level > bottomLevel && bottomLevel > 1)
			{
				Color bc = new Color().FromHsv((int)(DataGridView.GridColor.GetHue()), (int)(DataGridView.GridColor.GetSaturation() * 100f * (1f - (0.15f * bottomLevel))), (int)(DataGridView.GridColor.GetBrightness() * 100f * (1f + (0.15f * bottomLevel))));
				graphics.DrawLine(new Pen(DataGridView.DefaultCellStyle.BackColor), rowBounds.X + 1, rowBounds.Y + rowBounds.Height - 1, rowBounds.X + (16 * (bottomLevel - 1)) - 2, rowBounds.Y + rowBounds.Height - 1);
				graphics.DrawLine(new Pen(bc), rowBounds.X + (16 * (bottomLevel - 1)) - 1, rowBounds.Y + rowBounds.Height - 1, rowBounds.X + rowBounds.Width - 1, rowBounds.Y + rowBounds.Height - 1);
			}

			if (Level > 1)
			{
				for (int j = 1; j <= Level; j++)
				{
					Color bc = new Color().FromHsv((int)(DataGridView.GridColor.GetHue()), (int)(DataGridView.GridColor.GetSaturation() * 100f * (1f - (0.15f * j))), (int)(DataGridView.GridColor.GetBrightness() * 100f * (1f + (0.15f * j))));
					graphics.DrawLine(new Pen(bc), rowBounds.X + (16 * (j - 1)) - 1, rowBounds.Y - 1, rowBounds.X + (16 * (j - 1)) - 1, rowBounds.Y + rowBounds.Height - 1 - ((Level > bottomLevel) ? 1 : 0));
				}
			}
		}
	}
}
