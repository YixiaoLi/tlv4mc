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
    public delegate void ViewableObjectAddHandler(TimeLineViewableObject tlvo, object source);
    public delegate void ViewableObjectRemoveAtHandler(object source, int index);
    public delegate void ViewableObjectInsertHandler(TimeLineViewableObject tlvo, object source, int index);
    public delegate TimeLineViewableObject ViewableObjectGetHandler(object source, int index);
    public delegate int ViewableObjectIndexOfHandler(TimeLineViewableObject tlvo, object source);

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
        protected static void RemoveAt<T>(object source, int index)
            where T : TimeLineViewableObject
        {
            SortableBindingList<T> koList = (SortableBindingList<T>)source;
            koList.RemoveAt(index);
        }
        protected static void Insert<T>(TimeLineViewableObject ko, object source, int index)
            where T : TimeLineViewableObject
        {
            SortableBindingList<T> koList = (SortableBindingList<T>)source;
            koList.Insert(index, (T)ko);
        }
        protected static TimeLineViewableObject Get<T>(object source, int index)
            where T : TimeLineViewableObject
        {
            SortableBindingList<T> koList = (SortableBindingList<T>)source;
            return koList[index];
        }
        public static int IndexOf<T>(TimeLineViewableObject ko, object source)
            where T : TimeLineViewableObject
        {
            SortableBindingList<T> koList = (SortableBindingList<T>)source;
            return koList.IndexOf((T)ko);
        }

        public static void Add(TimeLineViewableObject ko, object source)
        {
            Add<TimeLineViewableObject>(ko, source);
        }
        public static void RemoveAt(object source, int index)
        {
            RemoveAt<TimeLineViewableObject>(source, index);
        }
        public static void Insert(TimeLineViewableObject ko, object source, int index)
        {
            Insert<TimeLineViewableObject>(ko, source, index);
        }
        public static TimeLineViewableObject Get(object source, int index)
        {
            return Get<TimeLineViewableObject>(source, index);
        }
        public static int IndexOf(TimeLineViewableObject ko, object source)
        {
            return IndexOf<TimeLineViewableObject>(ko, source);
        }

        [PropertyDisplayName("ログ", 10 * 10, true)]
        public TimeLineEvents TimeLineEvents { get; protected set; }
        [PropertyDisplayName("名前", 10, true)]
        public string Name { get; protected set; }

        public string[] ResourceFileLineFormat
        {
            get { return new[]{"Name"}; }
        }

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
