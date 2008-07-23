using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NU.OJL.MPRTOS.TLV.Core.Base
{
    public class TimeLineViewableObjectList<T>
        where T : TimeLineViewableObject
    {
        public List<T> List { get; protected set; }
        public List<Type> Types { get; protected set; }

        public List<T> this[Type type]
        {
            get
            {
                return List.FindAll(o => o.ObjectType.GetObjectType() == type);
            }
        }
        public T this[int metaId]
        {
            get
            {
                return List.Find(o => o.MetaId == metaId);
            }
        }
        public int GetMetaIdFrom(string prop, object obj)
        {
            foreach (T tlvo in List)
            {
                object o = tlvo.GetType().GetProperty(prop).GetValue(tlvo, null);
                if (obj.Equals(o))
                {
                    return tlvo.MetaId;
                }
            }
            return -1;
        }

        public TimeLineViewableObjectList()
        {
            List = new List<T>();
            Types = new List<Type>();
        }

        public void Add(T tlvo)
        {
            List.Add(tlvo); 
            if(!Types.Contains(tlvo.ObjectType.GetObjectType()))
            {
                Types.Add(tlvo.ObjectType.GetObjectType());
            }
        }

    }
}
