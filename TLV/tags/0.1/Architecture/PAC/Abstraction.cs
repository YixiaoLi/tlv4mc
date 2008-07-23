using System.ComponentModel;
using NU.OJL.MPRTOS.TLV.Architecture.PAC.Bace;

namespace NU.OJL.MPRTOS.TLV.Architecture.PAC
{
    public class Abstraction : IAbstraction
    {

        public event PropertyChangedEventHandler PropertyChanged;

        public string Name { get; protected set; }

        public Abstraction(string name)
        {
            this.Name = name;
        }

        protected void NotifyPropertyChanged(string info)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(info));
            }
        }

    }
}
