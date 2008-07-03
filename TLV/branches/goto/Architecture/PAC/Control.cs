using System.Windows.Forms;
using NU.OJL.MPRTOS.TLV.Architecture.PAC.Bace;

namespace NU.OJL.MPRTOS.TLV.Architecture.PAC
{
    public abstract class Control<Tp, Ta> : IControl
        where Tp : Control, IPresentation
        where Ta : Abstraction
    {
        public string Name { get; protected set; }
        public Tp P { get; protected set; }
        public Ta A { get; protected set; }
        Abstraction IControl.Abstraction
        {
            get { return (Abstraction)A; }
        }
        IPresentation IControl.Presentation
        {
            get { return (IPresentation)P; }
        }

        protected Control(string name, Tp presentation, Ta abstraction)
        {
            this.Name = name;
            this.P = presentation;
            this.A = abstraction;
        }

    }
}
