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
				object obj = value;
				if (obj == null)
				{
					Type = JsonValueType.Null;
					_value = null;
				}
				else if (obj.GetType() == typeof(List<Json>))
				{
					Type = JsonValueType.Array;
					_value = obj;
				}
				else if (obj.GetType() == typeof(Dictionary<string, Json>))
				{
					Type = JsonValueType.Object;
					_value = obj;
				}
				else if (obj.GetType() == typeof(string) || obj.GetType() == typeof(char))
				{
					Type = JsonValueType.String;
					_value = obj.ToString();
				}
				else if (obj.GetType() == typeof(sbyte)
					|| obj.GetType() == typeof(byte)
					|| obj.GetType() == typeof(short)
					|| obj.GetType() == typeof(ushort)
					|| obj.GetType() == typeof(int)
					|| obj.GetType() == typeof(uint)
					|| obj.GetType() == typeof(long)
					|| obj.GetType() == typeof(ulong)
					|| obj.GetType() == typeof(decimal)
					|| obj.GetType() == typeof(double)
					|| obj.GetType() == typeof(float))
				{
					Type = JsonValueType.Decimal;
					_value = Convert.ToDecimal(obj);
				}
				else if (obj.GetType() == typeof(bool))
				{
					Type = JsonValueType.Boolean;
					_value = obj;
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
			return Type == JsonValueType.Object ? ((Dictionary<string, Json>)Value).ContainsKey(name) : false;
		}
		public bool ContainsKey(int index)
		{
			return Type == JsonValueType.Array ? ((List<Json>)Value).Contains(this[index]) : false;
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
			if (Type == JsonValueType.Object)
			{
				((Dictionary<string, Json>)Value).Add(name, new Json(new List<Json>()));
			}
		}

		public void AddObject(string name)
		{
			if (Type == JsonValueType.Object)
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
			return jsonValue.Value.ToString();
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
