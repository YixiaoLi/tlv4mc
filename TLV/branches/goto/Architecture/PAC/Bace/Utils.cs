using System;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NU.OJL.MPRTOS.TLV.Architecture.PAC.Bace
{
    public interface IOaP
    {
        IAbstraction Object { get; set; }
        string Property { get; set; }
    }

    public class OaP<T> : IOaP
    {
        public IAbstraction Object { get; set; }
        public string Property { get; set; }

        public OaP(IAbstraction o, string propertyName)
        {
            this.Object = o;
            this.Property = propertyName;
        }
    }

    public class PACUtils
    {
        public static void SetBinding(Control control, string propertyName, IOaP value)
        {
            control.DataBindings.Add(propertyName, value.Object, value.Property); 
        }
    }
}
