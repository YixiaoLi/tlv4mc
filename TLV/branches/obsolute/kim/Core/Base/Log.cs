using System;
using System.Collections.Generic;

namespace NU.OJL.MPRTOS.TLV.Core.Base
{
    public class Log
    {
        // メンバ変数との不一致リスク低減のためメンバ変数をなるべくなくす
        // そのかわりプロパティにprotected setを使ってアクセス制限

        public ulong Time { get; protected set; }
        public int PrcID { get; protected set; }
        public Subject Subject { get; protected set; }
        public Verb Verb { get; protected set; }

        // コンストラクタはパラメータの一番複雑なものを再利用する

        public Log(ulong time, Subject subject, Verb verb)
            : this(time, 0, subject, verb)
        { }

        public Log(Log log)
            : this(log.Time, log.PrcID, log.Subject, log.Verb)
        { }

        public Log(ulong time, int prcid, Subject subject, Verb verb)
        {
            this.Time = time;
            this.PrcID = prcid;
            this.Subject = subject;
            this.Verb = verb;
        }
    }

    public class LogList
    {
        // メンバ変数との不一致リスク低減のためメンバ変数をなるべくなくす
        // そのかわりプロパティにprotected setを使ってアクセス制限

        public List<Log> List { get; protected set; }

        public int Count
        {
            get { return List.Count; }
        }

        public void Clear()
        {
            this.List.Clear();
        }

        public void Add(Log log)
        {
            List.Add(new Log(log));
        }

        public LogList(List<Log> logList)
        {
            List = new List<Log>(logList);
        }

        public LogList()
        {
            List = new List<Log>();
        }

    }
}
