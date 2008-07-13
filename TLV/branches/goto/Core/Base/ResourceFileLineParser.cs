using System;
using System.ComponentModel;
using System.Text;
using System.Collections.Generic;
using NU.OJL.MPRTOS.TLV.Core.Base;

namespace NU.OJL.MPRTOS.TLV.Core.Base
{
    public class ResourceFileLineParser
    {
        public static string DefaultCommentStr = "#";
        public static string DefaultSplitStr = ",";

        public string CommentStr { get; protected set; }
        public string SplitStr { get; protected set; }
        public List<string> FormatList { get; protected set; }

        public ResourceFileLineParser()
            : this(DefaultCommentStr, DefaultSplitStr)
        { }

        public ResourceFileLineParser(string commentStr, string splitStr)
        {
            CommentStr = commentStr;
            SplitStr = splitStr;
        }

        public TimeLineViewableObjectType GetObjectType(string resourceFileLine)
        {
            if (resourceFileLine == "")
            {
                return TimeLineViewableObjectType.NONE;
            }

            string[] res = resourceFileLine.Split(SplitStr.ToCharArray());

            TimeLineViewableObjectType ot = (TimeLineViewableObjectType)TypeDescriptor.GetConverter(typeof(TimeLineViewableObjectType)).ConvertFromString(res[0]);

            return ot;
        }

        public Dictionary<string, string> Parse(List<string> FormatList, string resourceFileLine)
        {
            if(FormatList == null)
            {
                return null;
            }

            if (resourceFileLine == "")
            {
                return null;
            }

            string[] res = resourceFileLine.Split(SplitStr.ToCharArray());

            if (res.Length != FormatList.Count)
            {
                return null;
            }

            Dictionary<string, string> dic = new Dictionary<string, string>();

            for (int i = 0; i < FormatList.Count; i++ )
            {
                dic.Add(FormatList[i], res[i]);
            }

            return dic;
        }
    }
}
