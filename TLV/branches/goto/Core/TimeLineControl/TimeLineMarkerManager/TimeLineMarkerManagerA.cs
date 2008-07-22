using System;
using System.Collections.Generic;
using NU.OJL.MPRTOS.TLV.Architecture.PAC;

namespace NU.OJL.MPRTOS.TLV.Core.TimeLineControl.TimeLineMarkerManager
{
    public class TimeLineMarkerManagerA : Abstraction
    {
        private List<TimeLineMarker> timeLineMarkerList = new List<TimeLineMarker>();

        public List<TimeLineMarker> TimeLineMarkerList
        {
            get { return timeLineMarkerList; }
            set
            {
                if (! timeLineMarkerList.Equals(value))
                {
                    timeLineMarkerList = value;
                    NotifyPropertyChanged("TimeLineMarkerList");
                }
            }
        }

        public TimeLineMarkerManagerA(string name)
            : base(name)
        {

        }
    }
}
