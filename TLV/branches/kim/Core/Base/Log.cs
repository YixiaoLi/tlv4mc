using System;
using System.Collections.Generic;

namespace NU.OJL.MPRTOS.TLV.Core.Base
{
    public class Log
    {
        private ulong time;
        private Subject subject;
        private Verb verb;

        public ulong Time
        {
            get{ return time; }
        }
        public Subject Subject
        {
            get { return subject; }
        }
        public Verb Verb
        {
            get { return verb; }
        }

        public Log(ulong time, Subject subject, Verb verb)
        {
            this.time = time;
            this.subject = subject;
            this.verb = verb;
        }

        public Log(Log log)
        {
            this.time = log.time;
            this.subject = log.subject;
            this.verb = log.verb;
        }

    }

    public class LogList
    {
        private List<Log> list;
        public List<Log> List
        {
            get { return list; }
        }

        public int Count
        {
            get { return list.Count; }
        }

        public void Clear()
        {
            this.list.Clear();
        }

        public void Add(Log log)
        {
            list.Add(new Log(log));
        }

        public LogList(List<Log> logList)
        {
            list = new List<Log>(logList);
        }

        public LogList()
        {
            list = new List<Log>();
        }
    }

    public class Subject
    {
        private ResourceType type;
        private int id;

        public ResourceType Type
        {
            get { return type; }
            set { type = value; }
        }
        public int Id
        {
            get { return id; }
            set { id = value; }
        }
    }

}
