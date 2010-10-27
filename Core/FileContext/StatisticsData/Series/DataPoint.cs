using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NU.OJL.MPRTOS.TLV.Base;
using System.Drawing;

namespace NU.OJL.MPRTOS.TLV.Core
{
    public class DataPoint
    {
        public string XLabel { get; set; }
        public string XValue { get; set; }
        public string YValue { get; set; }
        public Color? Color { get; set; }

        public DataPoint()
        {
        }
    }
}
