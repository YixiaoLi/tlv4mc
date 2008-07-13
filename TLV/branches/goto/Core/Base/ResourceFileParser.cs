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

        public ResourceFileLineParser(List<string> list)
        {

        }
    }
}
