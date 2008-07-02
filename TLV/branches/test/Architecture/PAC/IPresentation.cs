using System.Windows.Forms;
using NU.OJL.MPRTOS.TLV.Architecture.PAC.Bace;

namespace NU.OJL.MPRTOS.TLV.Architecture.PAC
{
    public interface IPresentation : IElement
    {
        void Show();
        void AddChild(Control control, object args);
    }
}
