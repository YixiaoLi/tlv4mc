using NU.OJL.MPRTOS.TLV.Architecture.PAC;
using WeifenLuo.WinFormsUI.Docking;

namespace NU.OJL.MPRTOS.TLV.Core.ResourceProperty
{
    public class ResourcePropertyAgent : Agent<ResourcePropertyP, ResourcePropertyA, ResourcePropertyC>
    {
        public ResourcePropertyAgent(string name)
            : base(name, new ResourcePropertyC(name, new ResourcePropertyP(name), new ResourcePropertyA(name)))
        {
            ((ResourcePropertyP)this.Control.Presentation).DockAreas = DockAreas.Document;
        }
    }
}
