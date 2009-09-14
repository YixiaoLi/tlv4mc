
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NU.OJL.MPRTOS.TLV.Base
{
	public class Json : IEnumerable<Json>
	{
		public static Json Empty { get { return new Json(); } }
		public bool IsEmpty { get { return Value == null; } }

		public static Json Object { get { return new Json(new Dictionary<string, Json>()); } }
		public static Json Array { get { return new Json(new List<Json>()); } }

		public object Value { get; set; }
		public Json this[int i]
		{
			get { return Value is List<Json> ? ((List<Json>)Value)[i] : null; }
			set
			{
				if (Value is List<Json>)
				{
					((List<Json>)Value)[i] = value;
				}
			}
		}
		public Json this[string name]
		{
			get { return Value is Dictionary<string, Json> ? ((Dictionary<string, Json>)Value)[name] : null; }
			set
			{
				if (Value is Dictionary<string, Json>)
				{
					((Dictionary<string, Json>)Value)[name] = value;
				}
			}
		}

		public Json()
		{
			Value = null;
		}
		
		public Json(object value)
		{
			Value = value;
		}

		public bool ContainsKey(string name)
		{
			return Value is Dictionary<string, Json> ? ((Dictionary<string, Json>)Value).ContainsKey(name) : false;
		}
		public bool ContainsKey(int index)
		{
			return Value is List<Json> ? ((List<Json>)Value).Contains(this[index]) : false;
		}

		public int IndexOf(Json value)
		{
			if (Value is List<Json>)
			{
				return ((List<Json>)Value).IndexOf(value);
			}
			else
			{
				return -1;
			}
		}
		public int Count
		{
			get
			{
				if (Value is List<Json>)
				{
					return ((List<Json>)Value).Count;
				}
				else
				{
					return 0;
				}
			}
		}

		public bool IsArray { get { return Value is List<Json>; } }
		public bool IsObject { get { return Value is Dictionary<string, Json>; } }

		public void AddArray(string name)
		{
			if (Value is Dictionary<string, Json>)
			{
				((Dictionary<string, Json>)Value).Add(name, new Json(new List<Json>()));
			}
		}

		public void AddObject(string name)
		{
			if (Value is Dictionary<string, Json>)
			{
				((Dictionary<string, Json>)Value).Add(name, new Json(new Dictionary<string, Json>()));
			}
		}

		public void Add(string name, object value)
		{
			if (Value is Dictionary<string, Json>)
			{
				((Dictionary<string, Json>)Value).Add(name, new Json(value));
			}
		}

		public void Add(object value)
		{
			if (Value is List<Json>)
			{
				((List<Json>)Value).Add(new Json(value));
			}
		}

		public IEnumerator<Json> GetEnumerator()
		{
			if (Value is IEnumerable<Json>)
				return ((IEnumerable<Json>)Value).GetEnumerator();
			else
				return null;
		}

		public IEnumerable<KeyValuePair<string, Json>> GetKeyValuePairEnumerator()
		{
			if (Value is IEnumerable<KeyValuePair<string, Json>>)
				return (IEnumerable<KeyValuePair<string, Json>>)Value;
			else
				return null;
		}

		System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
		{
			if (Value is IEnumerable<Json>)
				return ((IEnumerable<Json>)Value).GetEnumerator();
			else if (Value is IEnumerable<KeyValuePair<string, Json>>)
				return ((IEnumerable<KeyValuePair<string, Json>>)Value).GetEnumerator();
			else
				return null;
		}

		public static implicit operator string(Json jsonValue)
		{
			return jsonValue.ToString();
		}
		public static implicit operator bool(Json jsonValue)
		{
			return (bool)jsonValue.Value;
		}
		public static implicit operator decimal(Json jsonValue)
		{
			return (decimal)jsonValue.Value;
		}
		public static implicit operator List<Json>(Json jsonValue)
		{
			return (List<Json>)jsonValue.Value;
		}
		public static implicit operator Dictionary<string,Json>(Json jsonValue)
		{
			return (Dictionary<string, Json>)jsonValue.Value;
		}

		public override string ToString()
		{
			if (Value == null)
				return "null";

			return Value.ToString();
		}

	}
}
