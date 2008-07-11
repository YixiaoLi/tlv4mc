using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.ComponentModel;
using NU.OJL.MPRTOS.TLV.Core.Base;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using NU.OJL.MPRTOS.TLV.Base;

namespace NU.OJL.MPRTOS.TLV.Core.Base
{
    [Serializable]
    [TypeConverter(typeof(PropertyDisplayConverter))]
    public class TimeLineViewableObject : ITimeLineViewable
    {
        protected static void Add<T>(TimeLineViewableObject ko, object source)
            where T : TimeLineViewableObject
        {
            SortableBindingList<T> koList = (SortableBindingList<T>)source;
            koList.Add((T)ko);
        }

        public static void Add(TimeLineViewableObject ko, object source)
        {
            Add<TimeLineViewableObject>(ko, source);
        }

        [PropertyDisplayName("ログ")]
        public TimeLineEvents TimeLineEvents { get; protected set; }
        [PropertyDisplayName("名前")]
        public string Name { get; protected set; }

        public TimeLineViewableObject(string name, TimeLineEvents timeLineEvents)
        {
            this.Name = name;
            this.TimeLineEvents = timeLineEvents;
        }

        public TimeLineViewableObject DeepClone()
        {
            TimeLineViewableObject target = null;
            using (MemoryStream stream = new MemoryStream())
            {
                BinaryFormatter formatter = new BinaryFormatter();
                formatter.Serialize(stream, this);
                stream.Position = 0;

                target = (TimeLineViewableObject)formatter.Deserialize(stream);
            }

            return target;
        }
    }

}
