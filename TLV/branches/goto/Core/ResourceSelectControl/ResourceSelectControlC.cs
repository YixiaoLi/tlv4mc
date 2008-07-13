using System;
using System.Collections.Generic;
using NU.OJL.MPRTOS.TLV.Architecture.PAC;
using NU.OJL.MPRTOS.TLV.Architecture.PAC.Bace;
using NU.OJL.MPRTOS.TLV.Core.Base;

namespace NU.OJL.MPRTOS.TLV.Core.ResourceSelectControl
{
    public class ResourceSelectControlC : Control<ResourceSelectControlP, ResourceSelectControlA>
    {
        public ResourceSelectControlC(string name, ResourceSelectControlP presentation, ResourceSelectControlA abstraction)
            : base(name, presentation, abstraction)
        {
        }

        public override void InitParentFirst()
        {
            BindPToA("ViewableObjectList", typeof(Dictionary<TimeLineViewableObjectType, List<TimeLineViewableObject>>), "ViewableObjectList", SearchAFlags.Ancestors);
        }
    }
}
