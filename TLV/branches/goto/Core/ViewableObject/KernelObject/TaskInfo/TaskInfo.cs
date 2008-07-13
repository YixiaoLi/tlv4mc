using System;
using System.Collections.Generic;
using System.ComponentModel;
using NU.OJL.MPRTOS.TLV.Core.Base;
using NU.OJL.MPRTOS.TLV.Base;

namespace NU.OJL.MPRTOS.TLV.Core.ViewableObject.KernelObject.TaskInfo
{
    public class TaskInfo : KernelObject
    {
        public new static void Add(TimeLineViewableObject ko, object source)
        {
            Add<TaskInfo>(ko, source);
        }
        public new static void Insert(TimeLineViewableObject ko, object source, int index)
        {
            Insert<TaskInfo>(ko, source, index);
        }
        public new static void RemoveAt(object source, int index)
        {
            RemoveAt<TaskInfo>(source, index);
        }
        public new static TimeLineViewableObject Get(object source, int index)
        {
            return Get<TaskInfo>(source, index);
        }
        public new static int IndexOf(TimeLineViewableObject ko, object source)
        {
            return IndexOf<TaskInfo>(ko, source);
        }

        [PropertyDisplayName("優先度", (int)(10D * 3.5D), true)]
        public int Pri { get; protected set; }
        [PropertyDisplayName("拡張情報", 10 * 5, false)]
        public string Exinf { get; protected set; }
        [PropertyDisplayName("スタックサイズ", 10 * 6, false)]
        public int Stksize { get; protected set; }
        [PropertyDisplayName("起動関数", 10 * 7, false)]
        public string Task { get; protected set; }

        public override List<string> ResourceFileLineFormat
        {
            get
            {
                List<string> list = base.ResourceFileLineFormat;
                list.AddRange(new List<string>() { "Pri", "Exinf", "Stksize", "Task" });
                return list;
            }
        }

        public TaskInfo(int id, string name, string atr, int pri, string exinf, int stksize, string task, TimeLineEvents timeLineEvents)
            :base(id, name, ObjectType.TSK, atr, timeLineEvents)
        {
            this.Pri = pri;
            this.Exinf = exinf;
            this.Stksize = stksize;
            this.Task = task;
        }

        public TaskInfo(string resourceFileLine, TimeLineEvents timeLineEvents)
            : base(resourceFileLine, timeLineEvents)
        {

        }
    }
}
