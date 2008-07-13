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
        public new static void Insert(TimeLineViewableObject ko, object source, int index)
        {
            Insert<KernelObject>(ko, source, index);
        }
        public new static void RemoveAt(object source, int index)
        {
            RemoveAt<KernelObject>(source, index);
        }
        public new static TimeLineViewableObject Get(object source, int index)
        {
            return Get<KernelObject>(source, index);
        }
        public new static int IndexOf(TimeLineViewableObject ko, object source)
        {
            return IndexOf<KernelObject>(ko, source);
        }

        [PropertyDisplayName("タイプ", 10 * 2, true, true)]
        public ObjectType ObjectType { get; protected set; }
        [PropertyDisplayName("ID", 10 * 3, true)]
        public int Id { get; protected set; }
        [PropertyDisplayName("属性", 10 * 4, false)]
        public string Atr { get; protected set; }

        public KernelObject(int id, string name, ObjectType type, string atr, TimeLineEvents timeLineEvents)
            :base(name, timeLineEvents)
        {
            this.Id = id;
            this.ObjectType = type;
            this.Atr = Atr;
        }
    }
}
