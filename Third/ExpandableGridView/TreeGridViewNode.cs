/*
 *  TLV - Trace Log Visualizer
 *
 *  Copyright (C) 2008,2009 by Nagoya Univ., JAPAN
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

			// 下にあるノードの方がネストが深い場合
			if (Level < bottomLevel)
			{
				Color bc = new Color().FromHsv((int)(DataGridView.GridColor.GetHue()), (int)(DataGridView.GridColor.GetSaturation() * 100f * (1f - (0.15f * bottomLevel))), (int)(DataGridView.GridColor.GetBrightness() * 100f * (1f + (0.15f * bottomLevel))));
				graphics.DrawLine(new Pen(DataGridView.DefaultCellStyle.BackColor), rowBounds.X + 1, rowBounds.Y + rowBounds.Height - 1, rowBounds.X + (16 * Level) - 2, rowBounds.Y + rowBounds.Height - 1);
				graphics.DrawLine(new Pen(bc), rowBounds.X + (16 * (Level)) - 1, rowBounds.Y + rowBounds.Height - 1, rowBounds.X + rowBounds.Width - 1, rowBounds.Y + rowBounds.Height - 1);
			}
			// トップレベルでなく、下にあるノードとネストの深さが同じである場合
			else if (Level == bottomLevel && Level > 1)
			{
				Color bc = new Color().FromHsv((int)(DataGridView.GridColor.GetHue()), (int)(DataGridView.GridColor.GetSaturation() * 100f * (1f - (0.15f * Level))), (int)(DataGridView.GridColor.GetBrightness() * 100f * (1f + (0.15f * Level))));
				graphics.DrawLine(new Pen(DataGridView.DefaultCellStyle.BackColor), rowBounds.X + 1, rowBounds.Y + rowBounds.Height - 1, rowBounds.X + (16 * (Level - 1)) - 2, rowBounds.Y + rowBounds.Height - 1);
				graphics.DrawLine(new Pen(bc), rowBounds.X + (16 * (Level - 1)) - 1, rowBounds.Y + rowBounds.Height - 1, rowBounds.X + rowBounds.Width - 1, rowBounds.Y + rowBounds.Height - 1);
			}
			// トップレベルでなく、下にあるノードの方がネストが浅い場合
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
