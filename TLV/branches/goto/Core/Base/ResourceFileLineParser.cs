using System;
using System.Text;
using System.Collections.Generic;

namespace NU.OJL.MPRTOS.TLV.Core.Base
{
    public class ResourceFileLineParser
    {
        public static string DefaultCommentStr = "#";
        public static string DefaultSplitStr = ",";

        public string CommentStr { get; protected set; }
        public string SplitStr { get; protected set; }
        public List<string> FormatList { get; protected set; }

        public ResourceFileLineParser(List<string> formatList)
            : this(formatList, DefaultCommentStr, DefaultSplitStr)
        { }

        public ResourceFileLineParser(List<string> formatList, string commentStr, string splitStr)
        {
            CommentStr = commentStr;
            SplitStr = splitStr;
            FormatList = formatList;
        }

        public Dictionary<string, string> Parse(string resourceFileLine)
        {
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
