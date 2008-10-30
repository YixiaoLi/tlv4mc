using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NU.OJL.MPRTOS.TLV.Base
{
	public class Json : IEnumerable<Json>
	{
		public object Value{get; private set;}
		public Json this[int i] { get { return Value is List<Json> ? ((List<Json>)Value)[i] : null; } }
		public Json this[string name] { get { return Value is Dictionary<string, Json> ? ((Dictionary<string, Json>)Value)[name] : null; } }
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

		public IEnumerator<Json> GetEnumerator()
		{
			if (Value is IEnumerable<Json>)
				return ((IEnumerable<Json>)Value).GetEnumerator();
			else
				return null;
		}

		public IEnumerable<KeyValuePair<string, Json>> GetKeyValuePaierEnumerator()
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

		public static implicit operator int(Json jsonValue)
		{
			return (int)jsonValue.Value;
		}

		public static implicit operator float(Json jsonValue)
		{
			return (float)jsonValue.Value;
		}

		public static implicit operator DateTime(Json jsonValue)
		{
			return (DateTime)jsonValue.Value;
		}

		public static implicit operator List<string>(Json jsonValue)
		{
			List<Json> jl = (List<Json>)jsonValue.Value;
			return new List<string>(jl.Select<Json, string>((j, s) => { return j; }));
		}

		public static implicit operator List<bool>(Json jsonValue)
		{
			List<Json> jl = (List<Json>)jsonValue.Value;
			return new List<bool>(jl.Select<Json, bool>((j, s) => { return j; }));
		}

		public static implicit operator List<int>(Json jsonValue)
		{
			List<Json> jl = (List<Json>)jsonValue.Value;
			return new List<int>(jl.Select<Json, int>((j, s) => { return j; }));
		}

		public static implicit operator List<float>(Json jsonValue)
		{
			List<Json> jl = (List<Json>)jsonValue.Value;
			return new List<float>(jl.Select<Json, float>((j, s) => { return j; }));
		}

		public static implicit operator List<DateTime>(Json jsonValue)
		{
			List<Json> jl = (List<Json>)jsonValue.Value;
			return new List<DateTime>(jl.Select<Json, DateTime>((j, s) => { return j; }));
		}

		public static implicit operator string[](Json jsonValue)
		{
			if (jsonValue.Value is List<Json>)
				return ((List<string>)jsonValue).ToArray<string>();
			if (jsonValue.Value is string)
				return new string[] { jsonValue };

			return new string[] { };
		}

		public static implicit operator bool[](Json jsonValue)
		{
			if (jsonValue.Value is List<Json>)
				return ((List<bool>)jsonValue).ToArray<bool>();
			if (jsonValue.Value is bool)
				return new bool[] { jsonValue };

			return new bool[] { };
		}

		public static implicit operator int[](Json jsonValue)
		{
			if (jsonValue.Value is List<Json>)
				return ((List<int>)jsonValue).ToArray<int>();
			if (jsonValue.Value is int)
				return new int[] { jsonValue };

			return new int[] { };
		}

		public static implicit operator float[](Json jsonValue)
		{
			if (jsonValue.Value is List<Json>)
				return ((List<float>)jsonValue).ToArray<float>();
			if (jsonValue.Value is float)
				return new float[] { jsonValue };

			return new float[] { };
		}

		public static implicit operator DateTime[](Json jsonValue)
		{
			if (jsonValue.Value is List<Json>)
				return ((List<DateTime>)jsonValue).ToArray<DateTime>();
			if (jsonValue.Value is DateTime)
				return new DateTime[] { jsonValue };

			return new DateTime[] { };
		}

		public static implicit operator Dictionary<string, string>(Json jsonValue)
		{
			return (Dictionary<string, string>)jsonValue.Value;
		}

		public static implicit operator Dictionary<string, bool>(Json jsonValue)
		{
			return (Dictionary<string, bool>)jsonValue.Value;
		}

		public static implicit operator Dictionary<string, int>(Json jsonValue)
		{
			return (Dictionary<string, int>)jsonValue.Value;
		}

		public static implicit operator Dictionary<string, float>(Json jsonValue)
		{
			return (Dictionary<string, float>)jsonValue.Value;
		}

		public static implicit operator Dictionary<string, DateTime>(Json jsonValue)
		{
			return (Dictionary<string, DateTime>)jsonValue.Value;
		}

	}
}
