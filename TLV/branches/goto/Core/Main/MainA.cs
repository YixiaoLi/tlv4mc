using System;
using System.Collections.Generic;
using NU.OJL.MPRTOS.TLV.Architecture.PAC;
using NU.OJL.MPRTOS.TLV.Base;
using NU.OJL.MPRTOS.TLV.Core.Base;

namespace NU.OJL.MPRTOS.TLV.Core.Main
{
    public class MainA : Abstraction
    {
        private string resourceFilePath = String.Empty;
        private string traceLogFilePath = String.Empty;
        private Type viewableObjectType = typeof(TimeLineViewableObject);
        private object viewableObjectDataSource;
        private Dictionary<TimeLineViewableObjectType, List<TimeLineViewableObject>> viewableObjectList = new Dictionary<TimeLineViewableObjectType, List<TimeLineViewableObject>>();

        public Type ViewableObjectType
        {
            get { return viewableObjectType; }
            set
            {
                if (value != null && !value.Equals(viewableObjectType))
                {
                    viewableObjectType = value;

                    Type sblType = typeof(SortableBindingList<>);

                    this.viewableObjectDataSource = Activator.CreateInstance(sblType.MakeGenericType(viewableObjectType));

                    NotifyPropertyChanged("ViewableObjectType");
                }
            }
        }
        public Object ViewableObjectDataSource
        {
            get { return viewableObjectDataSource; }
            set { viewableObjectDataSource = value; }
        }
        public string ResourceFilePath
        {
            get { return resourceFilePath; }
            set
            {
                if (value != resourceFilePath)
                {
                    resourceFilePath = value;
                    NotifyPropertyChanged("ResourceFilePath");
                }
            }
        }
        public string TraceLogFilePath
        {
            get { return traceLogFilePath; }
            set
            {
                if (value != traceLogFilePath)
                {
                    traceLogFilePath = value;
                    NotifyPropertyChanged("TraceLogFilePath");
                }
            }
        }
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

        public void AddViewableObject(TimeLineViewableObject tlvo, object source)
        {
            viewableObjectType.GetMethod("Add").Invoke(null, new object[] { tlvo, source });
        }
        public void RemoveAtViewableObject(object source, int index)
        {
            viewableObjectType.GetMethod("RemoveAt").Invoke(null, new object[] { source, index });
        }
        public void InsertViewableObject(TimeLineViewableObject tlvo, object source, int index)
        {
            viewableObjectType.GetMethod("Insert").Invoke(null, new object[] { tlvo, source, index });
        }
        public TimeLineViewableObject GetViewableObject(object source, int index)
        {
            return (TimeLineViewableObject)(viewableObjectType.GetMethod("Get").Invoke(null, new object[] { source, index }));
        }
        public int IndexOfViewableObject(TimeLineViewableObject tlvo, object source)
        {
            return (int)(viewableObjectType.GetMethod("IndexOf").Invoke(null, new object[] { tlvo, source }));
        }


        public MainA(string name)
            :base(name)
        {
        }
    }
}
