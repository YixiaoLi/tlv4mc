using System;
using WinForms = System.Windows.Forms;
using System.Collections.Generic;
using NU.OJL.MPRTOS.TLV.Architecture.PAC;

namespace NU.OJL.MPRTOS.TLV.Core.TimeLineControl.TimeLine
{
    //TODO : Windows.Forms.Controlのサブクラスを継承してください
    public partial class TimeLineP : WinForms.Control, IPresentation
    {
        public TimeLineP(string name)
        {
            this.Name = name;
        }

        public void Add(IPresentation presentation, object args)
        {

        }

    }
}
