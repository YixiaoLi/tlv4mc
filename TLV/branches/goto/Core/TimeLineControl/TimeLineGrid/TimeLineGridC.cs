using System;
using System.Windows.Forms;
using System.Collections.Generic;
using NU.OJL.MPRTOS.TLV.Architecture.PAC;
using NU.OJL.MPRTOS.TLV.Architecture.PAC.Bace;

namespace NU.OJL.MPRTOS.TLV.Core.TimeLineControl.TimeLineGrid
{
    public class TimeLineGridC : Control<TimeLineGridP, TimeLineGridA>
    {
        public TimeLineGridC(string name, TimeLineGridP presentation, TimeLineGridA abstraction)
            : base(name, presentation, abstraction)
        {
            P.DataBindings.Add("RowSizeMode", A, "RowSizeMode", false, DataSourceUpdateMode.OnPropertyChanged);
        }

    }
}
