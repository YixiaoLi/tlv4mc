using System;
using System.Reflection;
using System.Collections.Generic;
using System.ComponentModel;

namespace NU.OJL.MPRTOS.TLV.Base
{
    public class SortableBindingList<T> : BindingList<T>
    {
        private PropertyDescriptor _sortProp = null;
        private ListSortDirection _sortDir = ListSortDirection.Ascending;
        private bool _isSorted = false;

        public SortableBindingList() { }
        public SortableBindingList(IList<T> list) : base(list) { }

        protected override void ApplySortCore(PropertyDescriptor property, ListSortDirection direction)
        {
            List<T> list = this.Items as List<T>;
            if (list != null)
            {
                list.Sort(PropertyComparerFactory.Factory<T>(property, direction));

                this._isSorted = true;
                this._sortProp = property;
                this._sortDir = direction;

                this.OnListChanged(new ListChangedEventArgs(ListChangedType.Reset, -1));
            }
        }

        protected override bool SupportsSortingCore { get { return true; } }
        protected override void RemoveSortCore() { }
        protected override bool IsSortedCore { get { return this._isSorted; } }
        protected override PropertyDescriptor SortPropertyCore { get { return this._sortProp; } }
        protected override ListSortDirection SortDirectionCore { get { return this._sortDir; } }
    }

    public static class PropertyComparerFactory
    {
        public static IComparer<T> Factory<T>(PropertyDescriptor property, ListSortDirection direction)
        {
            Type seed = typeof(PropertyComparer<,>);
            Type[] typeArgs = { typeof(T), property.PropertyType };
            Type pcType = seed.MakeGenericType(typeArgs);

            IComparer<T> comparer = (IComparer<T>)Activator.CreateInstance(pcType, new object[] { property, direction });
            return comparer;
        }
    }

    public sealed class PropertyComparer<T, U> : IComparer<T>
    {
        private PropertyDescriptor _property;
        private ListSortDirection _direction;
        private Comparer<U> _comparer;

        public PropertyComparer(PropertyDescriptor property, ListSortDirection direction)
        {
            this._property = property;
            this._direction = direction;
            this._comparer = Comparer<U>.Default;
        }

        public int Compare(T x, T y)
        {
            U xValue = (U)this._property.GetValue(x);
            U yValue = (U)this._property.GetValue(y);

            if (this._direction == ListSortDirection.Ascending)
                return this._comparer.Compare(xValue, yValue);
            else
                return this._comparer.Compare(yValue, xValue);
        }
    }

}
