using System;
using System.Collections.Generic;
using NU.OJL.MPRTOS.TLV.Architecture.PAC;
using NU.OJL.MPRTOS.TLV.Architecture.PAC.Bace;

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
            BindPToA("ViewableObjectType", typeof(Type), "ViewableObjectType", SearchAFlags.Ancestors);
        }
    }
}
