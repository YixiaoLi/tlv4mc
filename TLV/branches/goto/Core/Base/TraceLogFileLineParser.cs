using System;
using System.ComponentModel;
using System.Text;
using System.Linq;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using NU.OJL.MPRTOS.TLV.Core.Base;
using NU.OJL.MPRTOS.TLV.Core.ViewableObject.KernelObject.TaskInfo;

namespace NU.OJL.MPRTOS.TLV.Core.Base
{
    public class TraceLogFileLineParser<T>
        where T : TimeLineViewableObject
    {
        public TraceLogFileLineParser()
        {

        }

        public Log Parse(string logLine, TimeLineViewableObjectList<T> tlvol)
        {
            Match timeMatch = new Regex(@"\[(?<Time>\d+)\]").Match(logLine);
            ulong time = ulong.Parse(timeMatch.Result("${Time}"));

            Match taskMatch = new Regex(@"task (?<Id>\d+)").Match(logLine);
            int taskId = int.Parse(taskMatch.Result("${Id}"));

            string verb = "";
            if (logLine.Contains("becomes"))
            {
                Match verbMatch = new Regex(@"becomes (?<Verb>\w+)\.").Match(logLine);
                verb = verbMatch.Result("${Verb}");
            }
            else if (logLine.Contains("dispatch to"))
            {
                verb = "RUN";
            }

            int metaId = tlvol.GetMetaIdFrom("Id", (object)taskId);

            if (metaId != -1)
            {
                return new Log(time, metaId, verb);
            }

            return null;
        }
    }
}
