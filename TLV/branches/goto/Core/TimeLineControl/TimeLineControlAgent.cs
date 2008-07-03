using WinForms = System.Windows.Forms;
using NU.OJL.MPRTOS.TLV.Architecture.PAC;
using NU.OJL.MPRTOS.TLV.Core.TimeLineControl.TimeLinePanel;

namespace NU.OJL.MPRTOS.TLV.Core.TimeLineControl
{
    public class TimeLineControlAgent : Agent<TimeLineControlP, TimeLineControlA, TimeLineControlC>
    {
        private TimeLinePanelAgent timeLinePanelAgent = new TimeLinePanelAgent("TimeLinePanel");

        public TimeLineControlAgent(string name)
            : base(name, new TimeLineControlC(name, new TimeLineControlP(name), new TimeLineControlA(name)))
        {
            this.Add(timeLinePanelAgent);

            timeLinePanelAgent.P.Dock = WinForms.DockStyle.Fill;
        }
    }
}
