using System;
using System.Drawing;
using NU.OJL.MPRTOS.TLV.Architecture.PAC;
using NU.OJL.MPRTOS.TLV.Core.TimeLineControl.TimeLineGrid;
using NU.OJL.MPRTOS.TLV.Core.Base;
using NU.OJL.MPRTOS.TLV.Base;

namespace NU.OJL.MPRTOS.TLV.Core.TimeLineControl
{
    public class TimeLineControlA : Abstraction
    {
        private Type viewableObjectType = typeof(TimeLineViewableObject);
        private object viewableObjectDataSource;

        public ViewableObjectAddHandler AddViewableObject = (TimeLineViewableObject tlvo, object source) =>
        {
            tlvo.GetType().GetMethod("Add").Invoke(null, new object[]{tlvo, source});
        };

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

        public TimeLineControlA(string name)
            : base(name)
        {

        }
    }
}
