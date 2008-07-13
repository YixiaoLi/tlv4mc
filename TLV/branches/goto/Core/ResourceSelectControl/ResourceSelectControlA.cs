using System;
using System.Collections.Generic;
using NU.OJL.MPRTOS.TLV.Architecture.PAC;
using NU.OJL.MPRTOS.TLV.Core.Base;

namespace NU.OJL.MPRTOS.TLV.Core.ResourceSelectControl
{
    public class ResourceSelectControlA : Abstraction
    {
        private Dictionary<TimeLineViewableObjectType, List<TimeLineViewableObject>> viewableObjectList = new Dictionary<TimeLineViewableObjectType, List<TimeLineViewableObject>>();

        public Dictionary<TimeLineViewableObjectType, List<TimeLineViewableObject>> ViewableObjectList
        {
            get { return viewableObjectList; }
            set
            {
                if (value != null && !value.Equals(viewableObjectList))
                {
                    viewableObjectList = value;
                    NotifyPropertyChanged("ViewableObjectList");
                }
            }
        }

        public ResourceSelectControlA(string name)
            : base(name)
        {

        }
    }
}
