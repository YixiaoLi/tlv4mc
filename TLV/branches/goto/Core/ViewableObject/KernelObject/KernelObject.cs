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
        [PropertyDisplayName("ID", 10 * 3, true)]
        public int Id { get; protected set; }
        [PropertyDisplayName("属性", 10 * 4, false)]
        public string Atr { get; protected set; }

        public KernelObject(int id, string name, TimeLineViewableObjectType type, string atr, TimeLineEvents timeLineEvents)
            : base(type, timeLineEvents)
        {
            this.Name = name;
            this.Id = id;
            this.Atr = Atr;
        }

        public KernelObject(string resourceFileLine, TimeLineEvents timeLineEvents)
            : base(resourceFileLine, timeLineEvents)
        {

        }

        public override string ToString()
        {
            return Name;
        }
    }
}
