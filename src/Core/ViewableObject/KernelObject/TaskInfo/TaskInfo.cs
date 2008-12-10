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
        public static List<string> Verbs
        {
            get
            {
                return new List<string>()
                {
                    "RUN",
                    "RUNNABLE",
                    "DORMANT",
                    "WAITING",
                    "SUSPENDED",
                    "WAITING_SUSPENDED"
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

        /// <summary>
        /// リソースファイル１行のフォーマットの文字列を入力してTaskInfoを生成
        /// </summary>
        /// <param name="resourceFileLine">"TYPE, ID, NAME, ATR, PRI, EXINF, STKSIZE, TASK"</param>
        public TaskInfo(string resourceFileLine)
            : base(resourceFileLine)
        {

        }
    }
}
