using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;

namespace NU.OJL.MPRTOS.TLV.Core.Base
{
    public class TimeLineEvent
    {
        public ulong Time { get; protected set; }
        public Verb Verb { get; protected set; }

        public TimeLineEvent(ulong time, Verb verb)
        {
            this.Time = time;
            this.Verb = verb;
        }
    }

    public class TimeLineEvents
    {
        public List<TimeLineEvent> List { get; protected set; }
        public ulong StartTime { get; protected set; }
        public ulong EndTime { get; protected set; }

        public TimeLineEvent this[int index] { get { return this.List[index]; } }

        public TimeLineEvents(List<TimeLineEvent> list)
            :base()
        {
            this.List = list;
            this.StartTime = List.Min(te => te.Time);
            this.EndTime = List.Max(te => te.Time);
        }

        public IEnumerator<TimeLineEvent> GetEnumerator()
        {
            foreach(TimeLineEvent te in List)
            {
                yield return te;
            }
        }

    }
}
