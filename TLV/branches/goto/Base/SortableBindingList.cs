using System;
using System.Reflection;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading;

namespace NU.OJL.MPRTOS.TLV.Base
{
    public class SortableBindingList<T> : BindingList<T>
    {
		public event EventHandler Sorting = null;
		public event EventHandler Sorted = null;
		private PropertyDescriptor _sortProperty = null;
		private PropertyDescriptor _secondSortProperty = null;
		public string SecondSortPropertyName { get; set; }
		private ListSortDirection _sortDirection = ListSortDirection.Ascending;
		private ListSortDirection _secondSortDirection = ListSortDirection.Ascending;
        private bool _isSorted = false;

		public SortableBindingList() { }
        public SortableBindingList(IList<T> list) : base(list) { }
		public Dictionary<string, Comparison<T>> Comparisoins { get { return _comparisoins; } }

		private Dictionary<string, Comparison<T>> _comparisoins = new Dictionary<string, Comparison<T>>();

        protected override void ApplySortCore(PropertyDescriptor property, ListSortDirection direction)
        {

			Thread thread = new Thread(new ThreadStart(() =>
			{
				if (Sorting != null)
					Sorting(this, EventArgs.Empty);

					List<T> list = (List<T>)Items;
					if (list != null)
					{
						if(SecondSortPropertyName != null)
						{
							PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(typeof(T));
							_secondSortProperty = properties.Find(SecondSortPropertyName, false);
							if (property.Name == _secondSortProperty.Name)
								_secondSortDirection = direction;
						}
						else if (_secondSortProperty != _sortProperty && property != _sortProperty)
						{
							_secondSortProperty = _sortProperty;
							_secondSortDirection = _sortDirection;
						}
						_sortProperty = property;
						_sortDirection = direction;

						IComparer<T> firstComparer = PropertyComparerFactory.Factory<T>(_sortProperty, _sortDirection);
						IComparer<T> secondComparer = null;

						if (_secondSortProperty != null)
						{
							secondComparer = PropertyComparerFactory.Factory<T>(_secondSortProperty, _secondSortDirection);
						}

						list.Sort((t1,t2) =>
							{
								int f = 0;
								if (_comparisoins.ContainsKey(_sortProperty.Name))
								{
									f = _comparisoins[_sortProperty.Name].Invoke(t1, t2) * (_sortDirection == ListSortDirection.Ascending ? 1 : -1);
								}
								else
								{
									f = firstComparer.Compare(t1, t2);
								}
								int s = 0;
								if (secondComparer != null)
								{
									if (_comparisoins.ContainsKey(_secondSortProperty.Name))
									{
										s = _comparisoins[_secondSortProperty.Name].Invoke(t1, t2) * (_secondSortDirection == ListSortDirection.Ascending ? 1 : -1);
									}
									else
									{
										s = secondComparer.Compare(t1, t2);
									}
								}
								return f == 0 ? s : f;
							});

						_isSorted = true;

						if (Sorted != null)
							Sorted(this, EventArgs.Empty);
					}
				}));
			thread.IsBackground = true;
			thread.Start();

			//OnListChanged(new ListChangedEventArgs(ListChangedType.Reset, -1));
        }

        protected override bool SupportsSortingCore { get { return true; } }
        protected override void RemoveSortCore() { }
        protected override bool IsSortedCore { get { return _isSorted; } }
		protected override PropertyDescriptor SortPropertyCore { get { return _sortProperty; } }
		protected override ListSortDirection SortDirectionCore { get { return _sortDirection; } }

    }

    public static class PropertyComparerFactory
    {
        public static IComparer<T> Factory<T>(PropertyDescriptor property, ListSortDirection direction)
        {
			Type pcType = typeof(PropertyComparer<,>).MakeGenericType(new Type[] { typeof(T), property.PropertyType });
			return (IComparer<T>)Activator.CreateInstance(pcType, new object[] { property, direction });
        }
    }

    public sealed class PropertyComparer<T, U> : IComparer<T>
    {
        private PropertyDescriptor _property;
        private ListSortDirection _direction;
        private Comparer<U> _comparer;

        public PropertyComparer(PropertyDescriptor property, ListSortDirection direction)
        {
            _property = property;
            _direction = direction;
            _comparer = Comparer<U>.Default;
        }

        public int Compare(T x, T y)
        {
            U xValue = (U)_property.GetValue(x);
            U yValue = (U)_property.GetValue(y);

			if (typeof(IComparable).IsAssignableFrom(typeof(U)) || typeof(U).GetInterfaces().Any(t => t.IsGenericType && t.GetGenericTypeDefinition() == typeof(IComparable<>)))
			{
				if (_direction == ListSortDirection.Ascending)
					return _comparer.Compare(xValue, yValue);
				else
					return _comparer.Compare(yValue, xValue);
			}
			else
			{
				return 0;
			}
        }
    }

}
