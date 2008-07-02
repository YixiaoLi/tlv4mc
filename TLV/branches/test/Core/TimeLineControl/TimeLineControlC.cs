using NU.OJL.MPRTOS.TLV.Architecture.PAC;

namespace NU.OJL.MPRTOS.TLV.Core.TimeLineControl
{
    public class TimeLineControlC : Control<TimeLineControlP, TimeLineControlA>
    {
        public TimeLineControlC(string name, TimeLineControlP presentation, TimeLineControlA abstraction)
            : base(name, presentation, abstraction)
        {

        }
    }
}
