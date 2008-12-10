using NU.OJL.MPRTOS.TLV.Architecture.PAC;
using WeifenLuo.WinFormsUI.Docking;

namespace NU.OJL.MPRTOS.TLV.Core.FileOpenWindow
{
    public class FileOpenWindowAgent : Agent<FileOpenWindowP, FileOpenWindowA, FileOpenWindowC>
    {
        public FileOpenWindowAgent(string name)
            : base(name, new FileOpenWindowC(name, new FileOpenWindowP(name), new FileOpenWindowA(name)))
        {
            ((FileOpenWindowP)this.Control.Presentation).DockAreas = DockAreas.Document;
        }
    }
}
