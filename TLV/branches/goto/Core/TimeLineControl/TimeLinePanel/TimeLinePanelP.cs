using System;
using System.Windows.Forms;
using System.Collections.Generic;
using NU.OJL.MPRTOS.TLV.Architecture.PAC;

namespace NU.OJL.MPRTOS.TLV.Core.TimeLineControl.TimeLinePanel
{
    public class TimeLinePanelP : Panel, IPresentation
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
