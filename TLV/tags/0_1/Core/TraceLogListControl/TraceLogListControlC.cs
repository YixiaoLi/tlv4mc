using System;
using System.Collections.Generic;
using NU.OJL.MPRTOS.TLV.Architecture.PAC;
using NU.OJL.MPRTOS.TLV.Architecture.PAC.Bace;
using NU.OJL.MPRTOS.TLV.Core.Base;
using NU.OJL.MPRTOS.TLV.Core.ViewableObject.KernelObject.TaskInfo;

namespace NU.OJL.MPRTOS.TLV.Core.TraceLogListControl
{
    public class TraceLogListControlC : Control<TraceLogListControlP, TraceLogListControlA>
    {
        public TraceLogListControlC(string name, TraceLogListControlP presentation, TraceLogListControlA abstraction)
            : base(name, presentation, abstraction)
        {
        }

        public override void InitParentFirst()
        {
            BindPToA("ViewableObjectList", typeof(TimeLineViewableObjectList<TaskInfo>), "ViewableObjectList", SearchAFlags.Ancestors);
            BindPToA("LogList", typeof(LogList), "LogList", SearchAFlags.Ancestors);
        }
    }
}
