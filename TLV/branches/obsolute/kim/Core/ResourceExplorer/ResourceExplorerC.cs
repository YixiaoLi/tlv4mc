using NU.OJL.MPRTOS.TLV.Architecture.PAC;

namespace NU.OJL.MPRTOS.TLV.Core.ResourceExplorer
{
    public class ResourceExplorerC : Control<ResourceExplorerP, ResourceExplorerA>
    {
        public ResourceExplorerC(string name, ResourceExplorerP presentation, ResourceExplorerA abstraction)
            : base(name, presentation, abstraction)
        {

        }
    }
}
