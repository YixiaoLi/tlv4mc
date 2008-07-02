using System;
using System.Windows.Forms;
using NU.OJL.MPRTOS.TLV.Architecture.PAC;
using NU.OJL.MPRTOS.TLV.Architecture.PAC.Bace;

namespace NU.OJL.MPRTOS.TLV.Core.Main
{
    public class MainC : Control<MainP, MainA>
    {
        public MainC(string name, MainP presentation, MainA absrtaction)
            : base(name, presentation, absrtaction)
        {
            //P.Text1 = new OaP<string>(A, "Text1");
            //P.Text2 = new OaP<string>(A, "Text2");
            //P.Button1Click += (object o, EventArgs e) => { A.Text2 = A.Text1; };
        }

    }

}
