using WinForms = System.Windows.Forms;
using NU.OJL.MPRTOS.TLV.Architecture.PAC;
using NU.OJL.MPRTOS.TLV.Architecture.PAC.Bace;

namespace NU.OJL.MPRTOS.TLV.Architecture.PAC
{
    public interface IPresentation : IElement
    {
        void Show();
        void Add(IPresentation presentation, object args);
    }

    public static class IPresentationExtension
    {
        public static void Add(this IPresentation self, IPresentation presentation)
        {
            self.Add(presentation, null);
        }
    }

}
