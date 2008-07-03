using System;
using WinForms = System.Windows.Forms;
using System.Collections.Generic;
using NU.OJL.MPRTOS.TLV.Architecture.PAC;

namespace NU.OJL.MPRTOS.TLV.Core.TimeLineControl.TimeLinePanel
{
    //TODO : Windows.Forms.Controlのサブクラスを継承してください
    public partial class TimeLinePanelP : WinForms.Control, IPresentation
    {
        public TimeLinePanelP(string name)
        {
            this.Name = name;
        }

        public void Add(IPresentation presentation, object args)
        {

        }

    }
}
