using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using NU.OJL.MPRTOS.TLV.Base;
using NU.OJL.MPRTOS.TLV.Base.Controls;

namespace NU.OJL.MPRTOS.TLV.Third
{
	public class TreeGridViewColumn : DataGridViewColumn, ITreeGridViewColumn
	{
		AdvancedDataGridView.TreeGridColumn _column = new AdvancedDataGridView.TreeGridColumn();

		public override DataGridViewCell CellTemplate
		{
			get
			{
				return _column.CellTemplate;
			}
			set
			{
				_column.CellTemplate = value;
			}
		}
	}
}
