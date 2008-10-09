using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NU.OJL.MPRTOS.TLV.Base;
using NU.OJL.MPRTOS.TLV.Third;

namespace NU.OJL.MPRTOS.TLV.Core
{
    public static class ApplicationFactory
    {
        public static IWindowManagerHandler WindowManagerHandler
        {
            // Thirdパーティ製のドッキングパネル使用
            get { return new WeifenLuoWindowManagerHandler(); }
        }
    }
}
