using System;
using System.Collections.Generic;
using NU.OJL.MPRTOS.TLV.Architecture.PAC;
using NU.OJL.MPRTOS.TLV.Core.Base;

namespace NU.OJL.MPRTOS.TLV.Core.TimeLineControl.TimeLineGrid
{
    public class TimeLineGridAgent<T> : Agent<TimeLineGridP<T>, TimeLineGridA<T>, TimeLineGridC<T>>
        where T : TimeLineViewableObject
    {
        public TimeLineGridAgent(string name)
            : base(name, new TimeLineGridC<T>(name, new TimeLineGridP<T>(name), new TimeLineGridA<T>(name)))
        {

        }
    }
}
