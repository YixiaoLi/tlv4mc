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
        public new static void Add(TimeLineViewableObject ko, object source)
        {
            Add<KernelObject>(ko, source);
        }

        [PropertyDisplayName("ID")]
        public int Id { get; protected set; }
        [PropertyDisplayName("タイプ")]
        public ObjectType ObjectType { get; protected set; }
        [PropertyDisplayName("属性")]
        public string Atr { get; protected set; }
        [PropertyDisplayName("クラス")]
        public int Class { get; protected set; }

        public KernelObject(int id, string name, ObjectType type, string atr, int cls, TimeLineEvents timeLineEvents)
            :base(name, timeLineEvents)
        {
            this.Id = id;
            this.ObjectType = type;
            this.Class = cls;
            this.Atr = Atr;
        }
    }
}
