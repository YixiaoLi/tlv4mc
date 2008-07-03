using System;
using System.Windows.Forms;
using NU.OJL.MPRTOS.TLV.Architecture.PAC;
using NU.OJL.MPRTOS.TLV.Architecture.PAC.Bace;
using NU.OJL.MPRTOS.TLV.Base;

namespace NU.OJL.MPRTOS.TLV.Core.Main
{
    public partial class MainP : Form, IPresentation
    {
        //public OaP<string> Text1
        //{
        //    set { PACUtils.SetBinding(textBox1, "Text", value); }
        //}
        //public OaP<string> Text2
        //{
        //    set { PACUtils.SetBinding(textBox2, "Text", value); }
        //}
        //public event EventHandler Button1Click;

        public MainP(string name)
        {
            InitializeComponent();

            this.Name = name;

            //this.button1.Click += (object o, EventArgs e) =>
            //    {
            //        Button1Click(this, EventArgs.Empty);
            //    };
        }

        public void Add(IPresentation presentation, object args)
        {
            this.Controls.Add((Control)presentation);
        }

    }

}
