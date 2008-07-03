using System;
using System.Drawing;
using WinForms = System.Windows.Forms;
using System.Collections.Generic;
using NU.OJL.MPRTOS.TLV.Architecture.PAC;

namespace NU.OJL.MPRTOS.TLV.Core.TimeLineControl.TimeLinePanel
{
    public partial class TimeLinePanelP : WinForms.Panel, IPresentation
    {
        public TimeLinePanelP(string name)
        {
            this.Name = name;
        }

        public void Add(IPresentation presentation)
        {
            this.Controls.Add((WinForms.Control)presentation);
        }
    }
}
