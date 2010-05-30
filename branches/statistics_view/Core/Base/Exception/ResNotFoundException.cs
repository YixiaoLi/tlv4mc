using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NU.OJL.MPRTOS.TLV.Core
{
    class ResNotFoundException : ConvertException
    {
        public ResNotFoundException(string name) :
            base(string.Format("指定されたリソース({0})が見つかりません", name)) { }
    }
}