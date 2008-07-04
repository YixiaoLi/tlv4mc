using System;
using System.Drawing;
using System.Collections.Generic;
using WinForms = System.Windows.Forms;
using NU.OJL.MPRTOS.TLV.Architecture.PAC;

namespace NU.OJL.MPRTOS.TLV.Core.TimeLineControl.TimeLine
{
    public partial class TimeLineP : WinForms.Control, IPresentation
    {
        public TimeLineP(string name)
        {
            this.Name = name;
            this.BackColor = Color.Black;
        }

    }
}
