using System;
using System.Collections.Generic;
using NU.OJL.MPRTOS.TLV.Architecture.PAC;

namespace NU.OJL.MPRTOS.TLV.Core.TraceLogListControl
{
    public class TraceLogListControlAgent : Agent<TraceLogListControlP, TraceLogListControlA, TraceLogListControlC>
    {
        public TraceLogListControlAgent(string name)
            : base(name, new TraceLogListControlC(name, new TraceLogListControlP(name), new TraceLogListControlA(name)))
        {

        }
    }
}
