using System;
using System.Collections.Generic;
using System.ComponentModel;
using NU.OJL.MPRTOS.TLV.Core.Base;
using NU.OJL.MPRTOS.TLV.Base;

namespace NU.OJL.MPRTOS.TLV.Core.ViewableObject.KernelObject.TaskInfo
{
    [Serializable]
    [TypeConverter(typeof(PropertyDisplayConverter))]
    public class TaskInfo : KernelObject
    {
        public enum Verb
        {
            RUN,
            RUNNABLE,
            DORMANT,
            WAITING,
            SUSPENDED,
            WAITING_SUSPENDED
        }

        public static Dictionary<int, string> VerbFormats
        {
            get
            {
                return new Dictionary<int, string>()
                {
                    {(int)Verb.RUN, "dispatch to"},
                    {(int)Verb.RUNNABLE, "becomes RUNNABLE"},
                    {(int)Verb.DORMANT, "becomes DORMANT"},
                    {(int)Verb.WAITING, "becomes WAITING"},
                    {(int)Verb.SUSPENDED, "becomes SUSPENDED"},
                    {(int)Verb.WAITING_SUSPENDED, "becomes WAITING_SUSPENDED"},
                };
            }
        }

        [PropertyDisplayName("優先度", (int)(10D * 3.5D), true)]
        public int Pri { get; protected set; }
        [PropertyDisplayName("拡張情報", 10 * 5, false)]
        public string Exinf { get; protected set; }
        [PropertyDisplayName("スタックサイズ", 10 * 6, false)]
        public int Stksize { get; protected set; }
        [PropertyDisplayName("起動関数", 10 * 7, false)]
        public string Task { get; protected set; }

        public TaskInfo(int id, string name, string atr, int pri, string exinf, int stksize, string task, TimeLineEvents timeLineEvents)
            : base(id, name, TimeLineViewableObjectType.TSK, atr, timeLineEvents)
        {
            this.Pri = pri;
            this.Exinf = exinf;
            this.Stksize = stksize;
            this.Task = task;
        }

        /// <summary>
        /// リソースファイル１行のフォーマットの文字列を入力してTaskInfoを生成
        /// </summary>
        /// <param name="resourceFileLine">"TYPE, ID, NAME, ATR, PRI, EXINF, STKSIZE, TASK"</param>
        /// <param name="timeLineEvents">タイムラインイベント</param>
        public TaskInfo(string resourceFileLine, TimeLineEvents timeLineEvents)
            : base(resourceFileLine, timeLineEvents)
        {

        }
    }
}
