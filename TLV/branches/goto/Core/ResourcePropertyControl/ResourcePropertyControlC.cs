using System;
using System.Collections.Generic;
using NU.OJL.MPRTOS.TLV.Architecture.PAC;

namespace NU.OJL.MPRTOS.TLV.Core.ResourcePropertyControl
{
    public class ResourcePropertyControlC : Control<ResourcePropertyControlP, ResourcePropertyControlA>
    {
        public ResourcePropertyControlC(string name, ResourcePropertyControlP presentation, ResourcePropertyControlA abstraction)
            : base(name, presentation, abstraction)
        {
        }
    }
}
