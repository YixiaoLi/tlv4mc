using System;
using System.Collections.Generic;
using NU.OJL.MPRTOS.TLV.Architecture.PAC;

namespace NU.OJL.MPRTOS.TLV.Core.ResourceSelectControl
{
    public class ResourceSelectControlAgent : Agent<ResourceSelectControlP, ResourceSelectControlA, ResourceSelectControlC>
    {
        public ResourceSelectControlAgent(string name)
            : base(name, new ResourceSelectControlC(name, new ResourceSelectControlP(name), new ResourceSelectControlA(name)))
        {

        }
    }
}
