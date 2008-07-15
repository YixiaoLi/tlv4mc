using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;

namespace NU.OJL.MPRTOS.TLV.Core.Base
{
    public interface ITimeLineEvent
    {
        ulong Time { get; }
        string Verb { get; }
    }

    [Serializable]
    public class TimeLineEvent : ITimeLineEvent
    {
        public ulong Time { get; protected set; }
        public string Verb { get; protected set; }

        public TimeLineEvent(ulong time, string verb)
        {
            this.Time = time;
            this.Verb = verb;
        }
    }

    [Serializable]
    public class TimeLineEvents : IEnumerable
    {
        public List<TimeLineEvent> List { get; protected set; }
        public ulong StartTime { get; protected set; }
        public ulong EndTime { get; protected set; }

        public TimeLineEvent this[int index] { get { return this.List[index]; } }
        public TimeLineEvents this[ulong time]
        {
            get
            {
                return new TimeLineEvents(this.List.FindAll(tle => tle.Time == time));
            }
        }

        public TimeLineEvents GetTimeLineEvents(ulong from, ulong to)
        {
            List<TimeLineEvent> l = List.FindAll(tlv => tlv.Time >= from && tlv.Time <= to);
            if (l.Count != 0)
            {
                TimeLineEvents tles = new TimeLineEvents(l);
                if (List.IndexOf(tles[0]) > 0)
                {
                    tles.List.Insert(0, List[List.IndexOf(tles[0]) - 1]);
                }
                if (List.IndexOf(tles[tles.List.Count - 1]) < List.Count - 1)
                {
                    tles.List.Add(List[List.IndexOf(tles[tles.List.Count - 1]) + 1]);
                }
                return tles;
            }
            else
            {
                return new TimeLineEvents();
            }
        }

        public TimeLineEvents()
            : base()
        {
            this.List = new List<TimeLineEvent>();
        }

        public TimeLineEvents(List<TimeLineEvent> list)
            :base()
        {
            this.List = list;
            this.StartTime = List.Min(te => te.Time);
            this.EndTime = List.Max(te => te.Time);
        }

        public void Add(TimeLineEvent timeLineEvent)
        {
            this.List.Add(timeLineEvent);
            this.StartTime = List.Min(te => te.Time);
            this.EndTime = List.Max(te => te.Time);
        }

        public IEnumerator<TimeLineEvent> GetEnumerator()
        {
            foreach (TimeLineEvent te in List)
            {
                yield return te;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            foreach (TimeLineEvent te in List)
            {
                yield return te;
            }
        }

    }
}
