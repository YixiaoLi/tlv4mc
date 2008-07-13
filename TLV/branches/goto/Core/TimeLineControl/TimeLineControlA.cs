using System;
using System.Drawing;
using NU.OJL.MPRTOS.TLV.Architecture.PAC;
using NU.OJL.MPRTOS.TLV.Core.TimeLineControl.TimeLineGrid;
using NU.OJL.MPRTOS.TLV.Core.Base;
using NU.OJL.MPRTOS.TLV.Base;

namespace NU.OJL.MPRTOS.TLV.Core.TimeLineControl
{
    public class TimeLineControlA : Abstraction
    {
        private CursorMode cursorMode = CursorMode.Default;
        private Object viewableObjectDataSource;

        public CursorMode CursorMode
        {
            get { return cursorMode; }
            set
            {
                if (!value.Equals(cursorMode))
                {
                    cursorMode = value;

                    NotifyPropertyChanged("CursorMode");
                }
            }
        }
        public Object ViewableObjectDataSource
        {
            get { return viewableObjectDataSource; }
            set { viewableObjectDataSource = value; }
        }

        public TimeLineControlA(string name)
            : base(name)
        {

        }
    }
}
