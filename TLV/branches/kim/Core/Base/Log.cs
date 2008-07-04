using System;
using System.Collections.Generic;

namespace NU.OJL.MPRTOS.TLV.Core.Base
{
    public class Log
    {
        private ulong time;
        private int prcid = 0;
        private Subject subject;
        private Verb verb;

        public ulong Time
        {
            get{ return time; }
        }
        public int PrcID
        {
            get { return prcid; }
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

        public Log(ulong time, int prcid, Subject subject, Verb verb)
        {
            this.time = time;
            this.prcid = prcid;
            this.subject = subject;
            this.verb = verb;
        }

        public Log(Log log)
        {
            this.time = log.time;
            this.prcid = log.prcid;
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

        public int GetRunTaskId(int prcId)
        {
            int runTaskId = 0;

            int runTaskNo = 0;
            int dormantTaskNo = 0;

            for (int i = (list.Count - 1); i > 0; i--)
            {
                if (list[i].PrcID== prcId && list[i].Verb == Verb.RUN)
                {
                    runTaskNo = i;
                    break;                    
                }                
            }

            for (int i = (list.Count - 1); i > 0; i--)
            {
                if (list[i].PrcID == prcId && list[i].Verb == Verb.DORMANT)
                {
                    dormantTaskNo = i;
                    break;
                }
            }

            if (runTaskNo > dormantTaskNo)
            {
                runTaskId = list[runTaskNo].Subject.Id;
            }

            return runTaskId;
        }
    }

    public class Subject
    {
        private ResourceType type;
        private int id;

        public ResourceType Type
        {
            get { return type; }
        }
        public int Id
        {
            get { return id; }
        }

        public Subject(ResourceType type, int id)
        {
            this.type = type;
            this.id = id;
        }
    }

}
