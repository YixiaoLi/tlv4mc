using NU.OJL.MPRTOS.TLV.Architecture.PAC;

namespace NU.OJL.MPRTOS.TLV.Core.ResourceProperty
{
    public class ResourcePropertyC : Control<ResourcePropertyP, ResourcePropertyA>
    {
        public ResourcePropertyC(string name, ResourcePropertyP presentation, ResourcePropertyA abstraction)
            : base(name, presentation, abstraction)
        {

        }

    }
}
