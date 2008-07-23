using System;
using System.Collections.Generic;
using System.ComponentModel;
using NU.OJL.MPRTOS.TLV.Core.Base;
using NU.OJL.MPRTOS.TLV.Base;
using NU.OJL.MPRTOS.TLV.Core.ViewableObject.KernelObject;

namespace NU.OJL.MPRTOS.TLV.Core.ViewableObject.KernelObject
{
    [Serializable]
    [TypeConverter(typeof(PropertyDisplayConverter))]
    public class KernelObject : TimeLineViewableObject
    {
        [PropertyDisplayName("名前", 10 * 2, true)]
        public string Name { get; protected set; }
        [PropertyDisplayName("ID", 10 * 3, false)]
        public int Id { get; protected set; }
        [PropertyDisplayName("属性", 10 * 4, false)]
        public string Atr { get; protected set; }

        public KernelObject(string resourceFileLine)
            : base(resourceFileLine)
        {

        }

        public override string ToString()
        {
            return Name;
        }
    }
}
