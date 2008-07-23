using System;
using System.Collections.Generic;
using NU.OJL.MPRTOS.TLV.Architecture.PAC;

namespace NU.OJL.MPRTOS.TLV.Core.ResourcePropertyControl
{
    public class ResourcePropertyControlAgent : Agent<ResourcePropertyControlP, ResourcePropertyControlA, ResourcePropertyControlC>
    {
        public ResourcePropertyControlAgent(string name)
            : base(name, new ResourcePropertyControlC(name, new ResourcePropertyControlP(name), new ResourcePropertyControlA(name)))
        {

        }
    }
}
