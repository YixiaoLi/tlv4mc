using NU.OJL.MPRTOS.TLV.Architecture.PAC;

namespace NU.OJL.MPRTOS.TLV.Core.LogWindow
{
    public class LogWindowC : Control< LogWindowP,  LogWindowA>
    {
        public LogWindowC(string name, LogWindowP presentation, LogWindowA abstraction)
            : base(name, presentation, abstraction)
        {

        }

    }
}
