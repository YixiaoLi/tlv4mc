using System.ComponentModel;
using NU.OJL.MPRTOS.TLV.Architecture.PAC.Bace;

namespace NU.OJL.MPRTOS.TLV.Architecture.PAC
{
    public class Abstraction : IAbstraction
    {
        public string Name { get; protected set; }
        public Abstraction(string name)
        {
            this.Name = name;
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
