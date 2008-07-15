using System;
using System.Collections.Generic;
using NU.OJL.MPRTOS.TLV.Architecture.PAC;
using NU.OJL.MPRTOS.TLV.Architecture.PAC.Bace;
using NU.OJL.MPRTOS.TLV.Core.Base;

namespace NU.OJL.MPRTOS.TLV.Core.ResourcePropertyControl
{
    public class ResourcePropertyControlC : Control<ResourcePropertyControlP, ResourcePropertyControlA>
    {
        public ResourcePropertyControlC(string name, ResourcePropertyControlP presentation, ResourcePropertyControlA abstraction)
            : base(name, presentation, abstraction)
        {
        }

        public override void InitParentFirst()
        {
            BindPToA("SelectedObject", typeof(object), "SelectedObject", SearchAFlags.Self);
        }
    }
}
