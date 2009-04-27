/*
 *  TLV - Trace Log Visualizer
 *
 *  Copyright (C) 2008,2009 by Embedded and Real-Time Systems Laboratory
 *              Graduate School of Information Science, Nagoya Univ., JAPAN
 *
 *  �嵭����Ԥϡ��ʲ���(1)��(4)�ξ������������˸¤ꡤ�ܥ��եȥ���
 *  �����ܥ��եȥ���������Ѥ�����Τ�ޤࡥ�ʲ�Ʊ���ˤ���ѡ�ʣ������
 *  �ѡ������ۡʰʲ������ѤȸƤ֡ˤ��뤳�Ȥ�̵���ǵ������롥
 *  (1) �ܥ��եȥ������򥽡��������ɤη������Ѥ�����ˤϡ��嵭������
 *      ��ɽ�����������Ѿ�浪��Ӳ�����̵�ݾڵ��꤬�����Τޤޤη��ǥ���
 *      ����������˴ޤޤ�Ƥ��뤳�ȡ�
 *  (2) �ܥ��եȥ������򡤥饤�֥������ʤɡ�¾�Υ��եȥ�������ȯ�˻�
 *      �ѤǤ�����Ǻ����ۤ�����ˤϡ������ۤ�ȼ���ɥ�����ȡ�����
 *      �ԥޥ˥奢��ʤɡˤˡ��嵭�����ɽ�����������Ѿ�浪��Ӳ���
 *      ��̵�ݾڵ����Ǻܤ��뤳�ȡ�
 *  (3) �ܥ��եȥ������򡤵�����Ȥ߹���ʤɡ�¾�Υ��եȥ�������ȯ�˻�
 *      �ѤǤ��ʤ����Ǻ����ۤ�����ˤϡ����Τ����줫�ξ�����������
 *      �ȡ�
 *    (a) �����ۤ�ȼ���ɥ�����ȡ����Ѽԥޥ˥奢��ʤɡˤˡ��嵭����
 *        �ɽ�����������Ѿ�浪��Ӳ�����̵�ݾڵ����Ǻܤ��뤳�ȡ�
 *    (b) �����ۤη��֤��̤�������ˡ�ˤ�äơ�TOPPERS�ץ������Ȥ�
 *        ��𤹤뤳�ȡ�
 *  (4) �ܥ��եȥ����������Ѥˤ��ľ��Ū�ޤ��ϴ���Ū�������뤤���ʤ�»
 *      ������⡤�嵭����Ԥ����TOPPERS�ץ������Ȥ����դ��뤳�ȡ�
 *      �ޤ����ܥ��եȥ������Υ桼���ޤ��ϥ���ɥ桼������Τ����ʤ���
 *      ͳ�˴�Ť����ᤫ��⡤�嵭����Ԥ����TOPPERS�ץ������Ȥ�
 *      ���դ��뤳�ȡ�
 *
 *  �ܥ��եȥ������ϡ�̵�ݾڤ��󶡤���Ƥ����ΤǤ��롥�嵭����Ԥ�
 *  ���TOPPERS�ץ������Ȥϡ��ܥ��եȥ������˴ؤ��ơ�����λ�����Ū
 *  ���Ф���Ŭ������ޤ�ơ������ʤ��ݾڤ�Ԥ�ʤ����ޤ����ܥ��եȥ���
 *  �������Ѥˤ��ľ��Ū�ޤ��ϴ���Ū�������������ʤ�»���˴ؤ��Ƥ⡤��
 *  ����Ǥ�����ʤ���
 *
 *  @(#) $Id$
 */
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
		public string BasePropertyName { get; set; }
		public ListSortDirection BasePropertySortDirection { get; set; }
		private ListSortDirection _sortDirection = ListSortDirection.Ascending;
        private bool _isSorted = false;

		public SortableBindingList()
		{
			BasePropertySortDirection = ListSortDirection.Ascending;
		}
		public SortableBindingList(IList<T> list) : base(list)
		{
			BasePropertySortDirection = ListSortDirection.Ascending;
		}
		public Dictionary<string, Comparison<T>> Comparisoins { get { return _comparisoins; } }

		private Dictionary<string, Comparison<T>> _comparisoins = new Dictionary<string, Comparison<T>>();

        protected override void ApplySortCore(PropertyDescriptor property, ListSortDirection direction)
		{
			_sortProperty = property;
			_sortDirection = direction;

			Thread thread = new Thread(new ThreadStart(() =>
			{
				if (Sorting != null)
					Sorting(this, EventArgs.Empty);

				List<T> list = (List<T>)Items;
				if (list != null)
				{
					if (BasePropertyName != null)
					{
						PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(typeof(T));
						_secondSortProperty = properties.Find(BasePropertyName, false);
					}

					IComparer<T> firstComparer = PropertyComparerFactory.Factory<T>(_sortProperty, _sortDirection);
					IComparer<T> secondComparer = null;

					if (_secondSortProperty != null)
					{
						secondComparer = PropertyComparerFactory.Factory<T>(_secondSortProperty, BasePropertySortDirection);
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
								if (_comparisoins.ContainsKey(BasePropertyName))
								{
									s = _comparisoins[_secondSortProperty.Name].Invoke(t1, t2) * (BasePropertySortDirection == ListSortDirection.Ascending ? 1 : -1);
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
