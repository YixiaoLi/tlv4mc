/*
 *  TLV - Trace Log Visualizer
 *
 *  Copyright (C) 2008-2010 by Nagoya Univ., JAPAN
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
				throw new Exception("キー" + key + "はすでにディクショナリ内に存在します。");
		}

		public int IndexOf(K key)
		{
			if(! ContainsKey(key))
				throw new Exception(key.ToString() + "というキーはディクショナリ内に存在しません");

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
				throw new Exception("キー" + key + "はすでにディクショナリ内に存在します。");
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
