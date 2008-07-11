using System;
using System.Drawing;
using NU.OJL.MPRTOS.TLV.Architecture.PAC;
using NU.OJL.MPRTOS.TLV.Core.TimeLineControl.TimeLineGrid;
using NU.OJL.MPRTOS.TLV.Core.Base;
using NU.OJL.MPRTOS.TLV.Base;

namespace NU.OJL.MPRTOS.TLV.Core.TimeLineControl
{
    public delegate void ViewableObjectAddHandler(TimeLineViewableObject tlvo, object source);
    public delegate void ViewableObjectRemoveAtHandler(object source, int index);
    public delegate void ViewableObjectInsertHandler(TimeLineViewableObject tlvo, object source, int index);
    public delegate TimeLineViewableObject ViewableObjectGetHandler(object source, int index);
    public delegate int ViewableObjectIndexOfHandler(TimeLineViewableObject tlvo, object source);

    public class TimeLineControlA : Abstraction
    {
        private Type viewableObjectType = typeof(TimeLineViewableObject);
        private object viewableObjectDataSource;
        private CursorMode cursorMode = CursorMode.Default;

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
        public CursorMode CursorMode
        {
            get { return cursorMode; }
            set
            {
                if (!value.Equals(cursorMode))
                {
                    cursorMode = value;

                    NotifyPropertyChanged("CursorMode");
                }
            }
        }

        public TimeLineControlA(string name)
            : base(name)
        {

        }
    }
}
