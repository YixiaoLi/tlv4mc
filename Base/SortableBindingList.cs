/*
 *  TLV - Trace Log Visualizer
 *
 *  Copyright (C) 2008,2009 by Embedded and Real-Time Systems Laboratory
 *              Graduate School of Information Science, Nagoya Univ., JAPAN
 *
 *  上記著作権者は，以下の(1)〜(4)の条件を満たす場合に限り，本ソフトウェ
 *  ア（本ソフトウェアを改変したものを含む．以下同じ）を使用・複製・改
 *  変・再配布（以下，利用と呼ぶ）することを無償で許諾する．
 *  (1) 本ソフトウェアをソースコードの形で利用する場合には，上記の著作
 *      権表示，この利用条件および下記の無保証規定が，そのままの形でソー
 *      スコード中に含まれていること．
 *  (2) 本ソフトウェアを，ライブラリ形式など，他のソフトウェア開発に使
 *      用できる形で再配布する場合には，再配布に伴うドキュメント（利用
 *      者マニュアルなど）に，上記の著作権表示，この利用条件および下記
 *      の無保証規定を掲載すること．
 *  (3) 本ソフトウェアを，機器に組み込むなど，他のソフトウェア開発に使
 *      用できない形で再配布する場合には，次のいずれかの条件を満たすこ
 *      と．
 *    (a) 再配布に伴うドキュメント（利用者マニュアルなど）に，上記の著
 *        作権表示，この利用条件および下記の無保証規定を掲載すること．
 *    (b) 再配布の形態を，別に定める方法によって，TOPPERSプロジェクトに
 *        報告すること．
 *  (4) 本ソフトウェアの利用により直接的または間接的に生じるいかなる損
 *      害からも，上記著作権者およびTOPPERSプロジェクトを免責すること．
 *      また，本ソフトウェアのユーザまたはエンドユーザからのいかなる理
 *      由に基づく請求からも，上記著作権者およびTOPPERSプロジェクトを
 *      免責すること．
 *
 *  本ソフトウェアは，無保証で提供されているものである．上記著作権者お
 *  よびTOPPERSプロジェクトは，本ソフトウェアに関して，特定の使用目的
 *  に対する適合性も含めて，いかなる保証も行わない．また，本ソフトウェ
 *  アの利用により直接的または間接的に生じたいかなる損害に関しても，そ
 *  の責任を負わない．
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
