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
				this.RemoveItem(this.IndexOf(this.Single<KeyValuePair<K, T>>(kvp => kvp.Key.Equals(key))));
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
