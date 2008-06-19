using System.Collections.Generic;
using WeifenLuo.WinFormsUI.Docking;

namespace NagoyaUniv.OjlMpRtos.TraceLogVisualizer.Models
{
    abstract public class ApplicationModel
    {
        private Dictionary<string, DockContent> dockForms;

        public ApplicationModel()
        {
            
        }
    }
}
