using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NU.OJL.MPRTOS.TLV.Core
{
    class ConvertException: System.Exception
    {
        public string rule { get; set; }

        public ConvertException(string message):base(message)
        {
            
            this.rule = "<empty>";
        }

        override public string ToString()
        {
            return string.Format("変換中にエラーが発生しました。\n"+
            "   詳細:{0}\n\n"+ 
            "   問題のある変換ルール:{1}", this.Message, this.rule);
        }
    }
}
