using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NU.OJL.MPRTOS.TLV.Core
{
    public interface INullObjectOfParser : IParser
    {
        /// <summary>
        /// 既にパースが成功しているかどうか。
        /// 具体的には、ParserCombinatorsクラスのORメソッドを通った場合true。
        /// </summary>
        bool Success { get; set; }
    }
}
