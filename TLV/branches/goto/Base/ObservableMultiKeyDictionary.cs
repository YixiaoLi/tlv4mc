using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NU.OJL.MPRTOS.TLV.Base
{
	public class ObservableMultiKeyDictionary<T> : ObservableDictionary<string, T>
	{
		public void SetValue(T value, params string[] name)
		{
			if (!ContainsKey(name))
				this.Add(arrayToString(name), value);
			else if (!this[arrayToString(name)].Equals(value))
				this[arrayToString(name)] = value;
		}
		public T GetValue(params string[] name)
		{
			return this[arrayToString(name)];
		}
		public bool ContainsKey(params string[] name)
		{
			return base.ContainsKey(arrayToString(name));
		}
		public IEnumerable<KeyValuePair<string[], T>> GetMultiKeyEnumeralor()
		{
			return this.Select(kvp =>
			{
				if (kvp.Key.Contains(':'))
				{
					string[] data = kvp.Key.Split(':');
					return new KeyValuePair<string[], T>(data, kvp.Value);
				}
				else
				{
					return new KeyValuePair<string[], T>(new string[] { kvp.Key }, kvp.Value);
				}
			});
		}
		private string arrayToString(params string[] name)
		{
			StringBuilder result = new StringBuilder();
			foreach (string str in name)
			{
				result.Append(str);
				result.Append(":");
			}
			result.Remove(result.Length - 1, 1);
			return result.ToString();
		}
	}
}
