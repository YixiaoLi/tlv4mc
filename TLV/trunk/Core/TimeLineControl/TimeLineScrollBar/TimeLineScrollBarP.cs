using System;
using System.Drawing;
using System.ComponentModel;
using System.Windows.Forms;
using System.Collections.Generic;
using NU.OJL.MPRTOS.TLV.Architecture.PAC;

namespace NU.OJL.MPRTOS.TLV.Core.TimeLineControl.TimeLineScrollBar
{
    public partial class TimeLineScrollBarP : HScrollBar, IPresentation
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public int X
        {
            get { return this.Location.X; }
            set { this.Location = new Point(value, this.Location.Y); }
        }

        public TimeLineScrollBarP(string name)
        {
            this.Name = name;
        }

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
