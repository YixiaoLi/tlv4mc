using System;
using System.Text;

namespace NU.OJL.MPRTOS.TLV.Core.Base
{
    public class ResourceFileParser
    {
        public static string DefaultCommentStr = "#";
        public static string DefaultSplitStr = ",";

        public string CommentStr { get; protected set; }
        public string SplitStr { get; protected set; }


    }
}
