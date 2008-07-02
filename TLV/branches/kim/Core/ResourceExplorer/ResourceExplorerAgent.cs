using NU.OJL.MPRTOS.TLV.Architecture.PAC;
using WeifenLuo.WinFormsUI.Docking;

namespace NU.OJL.MPRTOS.TLV.Core.ResourceExplorer
{
    public class ResourceExplorerAgent : Agent<ResourceExplorerP, ResourceExplorerA, ResourceExplorerC>
    {
        public ResourceExplorerAgent(string name)
            : base(name, new ResourceExplorerC(name, new ResourceExplorerP(name), new ResourceExplorerA(name)))
        {
            ((ResourceExplorerP)this.Control.Presentation).DockAreas = DockAreas.Document;
        }
    }
}
