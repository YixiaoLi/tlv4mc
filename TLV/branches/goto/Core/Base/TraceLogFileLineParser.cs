using System;
using System.ComponentModel;
using System.Text;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using NU.OJL.MPRTOS.TLV.Core.Base;

namespace NU.OJL.MPRTOS.TLV.Core.Base
{
    public class TraceLogFileLineParser
    {
        public bool ContainLog(string logLine, string resLine)
        {
            ResourceFileLineParser resParser = new ResourceFileLineParser();
            TimeLineViewableObjectType type = resParser.GetObjectType(resLine);
            Dictionary<string, string> resDict = resParser.Parse(type.GetResourceFormat(), resLine);
            Dictionary<int, string> VerbFormats = (Dictionary<int, string>)type.GetObjectType().GetProperty("VerbFormats").GetValue(null, null);
            
            foreach(KeyValuePair<int, string> kvp in  VerbFormats)
            {
                if(! Regex.IsMatch(logLine, @"task (?<id>\d+)"))
                {
                    return false;
                }
                Regex regex = new Regex(@"task (?<id>\d+)");
                Match match = regex.Match(logLine); 
                int logId = int.Parse(match.Result("${id}"));
                int resId = int.Parse(resDict["Id"]);
                if (logLine.Contains(kvp.Value) && logId == resId)
                {
                    return true;
                }
            }
            return false;
        }

        public TimeLineEvents GetTimeLineEvent(string logLine, string resLine)
        {
            if (ContainLog(logLine, resLine))
            {
                ResourceFileLineParser resParser = new ResourceFileLineParser();
                TimeLineViewableObjectType type = resParser.GetObjectType(resLine);
                Dictionary<string, string> resDict = resParser.Parse(type.GetResourceFormat(), resLine);
                Dictionary<int, string> VerbFormats = (Dictionary<int, string>)type.GetObjectType().GetProperty("VerbFormats").GetValue(null, null);
                TimeLineEvents tles = new TimeLineEvents();

                foreach (KeyValuePair<int, string> kvp in VerbFormats)
                {
                    Regex regex = new Regex(@"task (?<id>\d+)");
                    Match match = regex.Match(logLine);
                    int logId = int.Parse(match.Result("${id}"));
                    int resId = int.Parse(resDict["Id"]);
                    if (logLine.Contains(kvp.Value) && logId == resId)
                    {
                        Regex timeRegex = new Regex(@"\[(?<time>\d+)\]");
                        Match timeMatch = timeRegex.Match(logLine);
                        ulong time = ulong.Parse(timeMatch.Result("${time}"));
                        tles.Add(new TimeLineEvent(time, kvp.Key));
                    }
                }
                return tles;
            }
            else
            {
                return null;
            }
        }
    }
}
