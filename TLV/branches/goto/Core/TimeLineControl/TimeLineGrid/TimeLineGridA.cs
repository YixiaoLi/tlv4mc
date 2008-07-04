using System;
using System.Collections.Generic;
using NU.OJL.MPRTOS.TLV.Architecture.PAC;

namespace NU.OJL.MPRTOS.TLV.Core.TimeLineControl.TimeLineGrid
{
    public class TimeLineGridA : Abstraction
    {
        private RowSizeMode rowSizeMode;

        public RowSizeMode RowSizeMode
        {
            get { return rowSizeMode; }
            set
            {
                if (rowSizeMode != value)
                {
                    rowSizeMode = value;
                    NotifyPropertyChanged("RowSizeMode");
                }
            }
        }

        public TimeLineGridA(string name)
            : base(name)
        {

        }
    }
}
