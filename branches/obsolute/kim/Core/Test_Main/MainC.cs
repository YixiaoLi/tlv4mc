using System;
using System.Windows.Forms;
using NU.OJL.MPRTOS.TLV.Architecture.PAC;
using NU.OJL.MPRTOS.TLV.Architecture.PAC.Bace;

namespace NU.OJL.MPRTOS.TLV.Core.Test_Main
{
    public class MainC : Control<MainP, MainA>
    {
        public MainC(string name, MainP presentation, MainA absrtaction)
            : base(name, presentation, absrtaction)
        {
        }

    }

}
