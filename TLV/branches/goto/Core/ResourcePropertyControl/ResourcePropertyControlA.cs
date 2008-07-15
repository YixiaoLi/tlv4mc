using System;
using System.Collections.Generic;
using NU.OJL.MPRTOS.TLV.Architecture.PAC;
using NU.OJL.MPRTOS.TLV.Core.Base;

namespace NU.OJL.MPRTOS.TLV.Core.ResourcePropertyControl
{
    public class ResourcePropertyControlA : Abstraction
    {
        public object selectedObject;

        public object SelectedObject
        {
            get { return selectedObject; }
            set
            {
                if (selectedObject != value)
                {
                    selectedObject = value;
                    NotifyPropertyChanged("SelectedObject");
                }
            }
        }

        public ResourcePropertyControlA(string name)
            : base(name)
        {

        }
    }
}
