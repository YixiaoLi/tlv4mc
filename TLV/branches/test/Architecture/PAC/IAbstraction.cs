using System.ComponentModel;
using NU.OJL.MPRTOS.TLV.Architecture.PAC.Bace;

namespace NU.OJL.MPRTOS.TLV.Architecture.PAC
{
    public interface IAbstraction : IElement, INotifyPropertyChanged
    {

    }

    public class Abstraction : IAbstraction
    {
        protected string name;
        public string Name { get { return name; } }
        public Abstraction(string name)
        {
            this.name = name;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void NotifyPropertyChanged(string info)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(info));
            }
        }

    }
}
