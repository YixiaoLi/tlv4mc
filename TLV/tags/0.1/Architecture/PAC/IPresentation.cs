using System;
using System.ComponentModel;
using WinForms = System.Windows.Forms;
using NU.OJL.MPRTOS.TLV.Architecture.PAC;
using NU.OJL.MPRTOS.TLV.Architecture.PAC.Bace;

namespace NU.OJL.MPRTOS.TLV.Architecture.PAC
{
    public interface IPresentation : IElement, INotifyPropertyChanged
    {
        void Show();
        void Add(IPresentation presentation);
    }
}
