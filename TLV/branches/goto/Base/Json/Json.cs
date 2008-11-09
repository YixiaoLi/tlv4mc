using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NU.OJL.MPRTOS.TLV.Base
{
	public class Json : IEnumerable<Json>
	{
		private object _value;
		public JsonValueType Type { get; private set; }
		public object Value
		{
			get { return _value; }
			set
			{
				if (value is List<Json>)
				{
					Type = JsonValueType.Array;
					_value = value;
				}
				else if (value is Dictionary<string, Json>)
				{
					Type = JsonValueType.Object;
					_value = value;
				}
				else if (value is string || value is char)
				{
					Type = JsonValueType.String;
					_value = value.ToString();
				}
				else if (value is sbyte
					|| value is byte
					|| value is short
					|| value is ushort
					|| value is int
					|| value is uint
					|| value is long
					|| value is ulong
					|| value is decimal
					|| value is double
					|| value is float)
				{
					Type = JsonValueType.Decimal;
					_value = (decimal)value;
				}
				else if (value is bool)
				{
					Type = JsonValueType.Boolean;
					_value = value;
				}
				else if (value == null)
				{
					Type = JsonValueType.Null;
					_value = null;
				}
				else
				{
					Type = JsonValueType.Null;
					_value = null;
				}
			}
		}
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
			if (value.Type == JsonValueType.Array)
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
				if (Type == JsonValueType.Array)
				{
					return ((List<Json>)Value).Count;
				}
				else
				{
					return 0;
				}
			}
		}

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
			if (Type == JsonValueType.Object)
			{
				((Dictionary<string, Json>)Value).Add(name, new Json(value));
			}
		}

		public void Add(object value)
		{
			if (Type == JsonValueType.Array)
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
			return (string)jsonValue.Value;
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
			return Value.ToString();
		}
	}
}
