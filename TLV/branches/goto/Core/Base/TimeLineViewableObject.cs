using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.ComponentModel;
using System.Reflection;
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
        [PropertyDisplayName("タイプ", 10 * 1, true, true)]
        public TimeLineViewableObjectType ObjectType { get; protected set; }
        [PropertyDisplayName("ログ", 10 * 10, true)]
        public TimeLineEvents TimeLineEvents { get; protected set; }

        public TimeLineViewableObject(TimeLineViewableObjectType objectType, TimeLineEvents timeLineEvents)
        {
            this.ObjectType = objectType;
            this.TimeLineEvents = timeLineEvents;
        }

        public TimeLineViewableObject(string resourceFileLine, TimeLineEvents timeLineEvents)
        {
            this.TimeLineEvents = timeLineEvents;

            ResourceFileLineParser resFormatter = new ResourceFileLineParser();
            this.ObjectType = resFormatter.GetObjectType(resourceFileLine);
            List<string> resFormat = this.ObjectType.GetResourceFormat();

            Dictionary<string, string> dic = resFormatter.Parse(resFormat, resourceFileLine);
            if (dic == null)
            {
                throw new Exception("リソースファイル行のフォーマットが異常です。:" + resourceFileLine);
            }
            else
            {
                foreach(KeyValuePair<string, string> prop in dic)
                {
                    PropertyInfo pi = this.GetType().GetProperty(prop.Key);
                    if(pi != null)
                    {
                        object o = TypeDescriptor.GetConverter(pi.PropertyType).ConvertFromString(prop.Value);
                        pi.SetValue(this, o, null);
                    }
                }
            }
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

        public override string ToString()
        {
            return ObjectType.ToString();
        }
    }

}
