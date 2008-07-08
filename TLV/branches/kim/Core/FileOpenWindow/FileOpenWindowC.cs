using NU.OJL.MPRTOS.TLV.Architecture.PAC;

namespace NU.OJL.MPRTOS.TLV.Core.FileOpenWindow
{
    public class FileOpenWindowC : Control< FileOpenWindowP,  FileOpenWindowA>
    {
        public FileOpenWindowC(string name, FileOpenWindowP presentation, FileOpenWindowA abstraction)
            : base(name, presentation, abstraction)
        {

        }

    }
}
