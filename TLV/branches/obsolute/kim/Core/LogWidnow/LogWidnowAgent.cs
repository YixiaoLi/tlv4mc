using NU.OJL.MPRTOS.TLV.Architecture.PAC;
using WeifenLuo.WinFormsUI.Docking;

namespace NU.OJL.MPRTOS.TLV.Core.LogWindow
{
    public class LogWindowAgent : Agent<LogWindowP, LogWindowA, LogWindowC>
    {
        public LogWindowAgent(string name)
            : base(name, new LogWindowC(name, new LogWindowP(name), new LogWindowA(name)))
        {
            ((LogWindowP)this.Control.Presentation).DockAreas = DockAreas.Document;
        }
    }
}
