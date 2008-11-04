using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NU.OJL.MPRTOS.TLV.Base;

namespace NU.OJL.MPRTOS.TLV.Core
{
	public class VisualizeRule : IEnumerable<KeyValuePair<string,string>>
	{
		private object _data;

		public bool IsMapped { get { return _data is Dictionary<string, string>; } }
		public string this[string name] { get { return IsMapped ? ((Dictionary<string, string>)_data)[name] : null;}}

		public VisualizeRule()
		{
			_data = null;
		}

		public VisualizeRule(string data)
		{
			_data = data;
		}

		public VisualizeRule(Dictionary<string, string> data)
		{
			_data = data;
		}

		public IEnumerator<KeyValuePair<string, string>> GetEnumerator()
		{
			if (IsMapped)
			{
				return ((Dictionary<string, string>)_data).GetEnumerator();
			}
			else
			{
				return null;
			}
		}

		System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
		{
			if (IsMapped)
			{
				return ((Dictionary<string, string>)_data).GetEnumerator();
			}
			else
			{
				return null;
			}
		}

		public void Add(string name, string value)
		{
			if (IsMapped)
			{
				((Dictionary<string, string>)_data).Add(name, value);
			}
		}

		public static implicit operator string(VisualizeRule data)
		{
			return (string)data._data;
		}
		public static implicit operator Dictionary<string, string>(VisualizeRule data)
		{
			return (Dictionary<string, string>)data._data;
		}

		public override string ToString()
		{
			return _data.ToString();
		}
	}
}
