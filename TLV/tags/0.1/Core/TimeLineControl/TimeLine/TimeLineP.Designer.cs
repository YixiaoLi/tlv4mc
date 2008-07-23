using System;
using System.ComponentModel;
using WinForms = System.Windows.Forms;
using System.Collections.Generic;
using NU.OJL.MPRTOS.TLV.Architecture.PAC;

namespace NU.OJL.MPRTOS.TLV.Core.TimeLineControl.TimeLine
{
    public partial class TimeLineP
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public void Add(IPresentation presentation)
        {

        }

        protected void NotifyPropertyChanged(string info)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(info));
            }
        }
    }

}
