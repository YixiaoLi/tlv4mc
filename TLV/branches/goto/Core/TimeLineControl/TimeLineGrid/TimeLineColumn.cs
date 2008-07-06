using System;
using System.Drawing;
using System.Windows.Forms;

namespace NU.OJL.MPRTOS.TLV.Core.TimeLineControl.TimeLineGrid
{
    public class TimeLineColumn : DataGridViewColumn
    {
        public TimeLineColumn()
            : base(new TimeLineCell())
        {
            this.SortMode = DataGridViewColumnSortMode.NotSortable;
            this.DataPropertyName = "TimeLineEvents";
            this.Name = "TimeLine";
        }

        public override DataGridViewCell CellTemplate
        {
            get
            {
                return base.CellTemplate;
            }
            set
            {
                if (value != null && !value.GetType().IsAssignableFrom(typeof(TimeLineCell)))
                {
                    throw new InvalidCastException("Must be a TimeLineCell");
                }
                base.CellTemplate = value;
            }
        }
    }
}
