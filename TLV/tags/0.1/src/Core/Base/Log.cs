using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NU.OJL.MPRTOS.TLV.Core.Base
{
    public class Log
    {
        public int MetaId { get; protected set; }
        public string Subject { get; set; }
        public ulong Time { get; protected set; }
        public string Verb { get; protected set; }

        public Log(ulong time, int s, string v)
        {
            MetaId = s;
            Verb = v;
            Time = time;
        }
    }

    public class LogList
    {
        public List<Log> List { get; protected set; }

        public TimeLineEvents this[int id]
        {
            get
            {
                List<TimeLineEvent> list = new List<TimeLineEvent>();
                foreach(Log log in List.FindAll( l => l.MetaId == id ))
                {
                    list.Add(new TimeLineEvent(log.Time, log.Verb));
                }
                return new TimeLineEvents(list);
            }
        }

        public LogList()
        {
            List = new List<Log>();
        }

        public void Add(Log log)
        {
            List.Add(log);
        }
    }
}
