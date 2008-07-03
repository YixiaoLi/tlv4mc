using System.Windows.Forms;
using NU.OJL.MPRTOS.TLV.Architecture.PAC.Bace;

namespace NU.OJL.MPRTOS.TLV.Architecture.PAC
{
    public interface IControl: IElement
    {
        IPresentation Presentation { get; }
    }

    public abstract class Control<Tp, Ta> : IControl
        where Tp : Control, IPresentation
        where Ta : IAbstraction
    {
        protected string name;
        protected Tp P;
        protected Ta A;

        public string Name
        {
            get { return name; }
        }
        public IPresentation Presentation { get { return P; } }


        protected Control(string name, Tp presentation, Ta abstraction)
        {
            this.name = name;
            this.P = presentation;
            this.A = abstraction;
        }

    }
}
