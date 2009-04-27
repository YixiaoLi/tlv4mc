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
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Collections;
using System.Collections.Specialized;

namespace NU.OJL.MPRTOS.TLV.Base
{
	public class ObservableDictionary<K, T> : ObservableCollection<KeyValuePair<K, T>>, IDictionary, IDictionary<K,T>
	{
		public void Add(K key, T value)
		{
			if (!ContainsKey(key))
				this.Add(new KeyValuePair<K, T>(key, value));
			else
				throw new Exception("����" + key + "�Ϥ��Ǥ˥ǥ�������ʥ����¸�ߤ��ޤ���");
		}

		public int IndexOf(K key)
		{
			if(! ContainsKey(key))
				throw new Exception(key.ToString() + "�Ȥ��������ϥǥ�������ʥ����¸�ߤ��ޤ���");

			return base.IndexOf(this.Single<KeyValuePair<K, T>>(kvp => kvp.Key.Equals(key)));
		}

		public bool ContainsKey(K key)
		{
			return this.Any<KeyValuePair<K, T>>(kvp => kvp.Key.Equals(key));
		}

		public ICollection<K> Keys
		{
			get { return this.Select<KeyValuePair<K, T>, K>(kvp => kvp.Key).ToList<K>(); }
		}

		public bool Remove(K key)
		{
			if (ContainsKey(key))
			{
				this.RemoveItem(this.IndexOf(key));
				return true;
			}
			else
			{
				return false;
			}
		}

		public bool TryGetValue(K key, out T value)
		{
			if (ContainsKey(key))
			{
				value = this.Single<KeyValuePair<K, T>>(kvp => kvp.Key.Equals(key)).Value;
				return true;
			}
			else
			{
				value = default(T);
				return false;
			}
		}

		public ICollection<T> Values
		{
			get { return this.Select<KeyValuePair<K, T>, T>(kvp => kvp.Value).ToList<T>(); }
		}

		public T this[K key]
		{
			get
			{
				return this.Single<KeyValuePair<K, T>>(kvp => kvp.Key.Equals(key)).Value;
			}
			set
			{
				this.Remove(key);
				this.Add(key, value);
			}
		}

		public void Add(object key, object value)
		{
			if (!Contains(value))
				this.Add(new KeyValuePair<K, T>((K)key, (T)value));
			else
				throw new Exception("����" + key + "�Ϥ��Ǥ˥ǥ�������ʥ����¸�ߤ��ޤ���");
		}

		public bool Contains(object key)
		{
			return this.Any<KeyValuePair<K, T>>(kvp => kvp.Key.Equals(key));
		}

		public bool IsFixedSize
		{
			get { return false; }
		}

		public bool IsReadOnly
		{
			get { return false; }
		}

		public void Remove(object key)
		{
			this.RemoveItem(this.IndexOf(this.Single<KeyValuePair<K, T>>(kvp => kvp.Key.Equals(key))));
		}

		public object this[object key]
		{
			get
			{
				return this.Single<KeyValuePair<K, T>>(kvp => kvp.Key.Equals(key)).Value;
			}
			set
			{
				this.Remove(key);
				this.Add(key, value);
			}
		}

		ICollection IDictionary.Keys
		{
			get { return this.Select<KeyValuePair<K, T>, T>(kvp => kvp.Value).ToList<T>(); }
		}

		ICollection IDictionary.Values
		{
			get { return this.Select<KeyValuePair<K, T>, T>(kvp => kvp.Value).ToList<T>(); }
		}

		IDictionaryEnumerator IDictionary.GetEnumerator()
		{
			ListDictionary ld = new ListDictionary();
			IEnumerable<DictionaryEntry> e = this.Select<KeyValuePair<K, T>, DictionaryEntry>(kvp =>
				{
					return new DictionaryEntry(kvp.Key, kvp.Value);
				});
			foreach(DictionaryEntry de in e)
			{
				ld.Add(de.Key, de.Value);
			}
			return ld.GetEnumerator();
		}

	}
}
