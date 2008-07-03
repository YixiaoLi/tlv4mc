using System;
using WinForms = System.Windows.Forms;
using System.Collections.Generic;
using NU.OJL.MPRTOS.TLV.Architecture.PAC;

namespace NU.OJL.MPRTOS.TLV.Core.TimeLineControl.TimeLineGrid
{
    public partial class TimeLineGridP : WinForms.DataGridView, IPresentation
    {
        public TimeLineGridP(string name)
        {
            this.Name = name;
        }

        public void Add(IPresentation presentation)
        {

        }

    }
}
