using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NU.OJL.MPRTOS.TLV.Base;

namespace NU.OJL.MPRTOS.TLV.Core
{
	public class JsonConverter : IJsonConverter
	{
		public Type Type { get { return typeof(Json); } }

		public object ReadJson(IJsonReader reader)
		{
			Stack<Json> stack = new Stack<Json>();
			Stack<string> keyStack = new Stack<string>();
			Json result = null;
			int objectNest = 0;
			int arrayNest = 0;

			Action<object> jsonValueSet = (object value) =>
			{
				if (result != null)
				{
					if (result.Value is List<Json>)
					{
						((List<Json>)result.Value).Add(new Json(value));
					}
					else if (result.Value is Dictionary<string, Json>)
					{
						((Dictionary<string, Json>)result.Value).Add(keyStack.Pop(), new Json(value));
					}
				}
				else
				{
					result = new Json(value);
				}
			};

			do
			{
				switch (reader.TokenType)
				{
					case JsonTokenType.StartArray:
						if (result != null)
							stack.Push(result);
						result = new Json(new List<Json>());
						arrayNest++;
						break;
					case JsonTokenType.StartObject:
						if (result != null)
							stack.Push(result);
						result = new Json(new Dictionary<string, Json>());
						objectNest++;
						break;
					case JsonTokenType.PropertyName:
						keyStack.Push(reader.Value as string);
						break;
					case JsonTokenType.EndArray:
						if (stack.Count != 0)
						{
							Json a = result;
							result = stack.Pop();
							jsonValueSet(a.Value);
						}
						arrayNest--;
						break;
					case JsonTokenType.EndObject:
						if (stack.Count != 0)
						{
							Json o = result;
							result = stack.Pop();
							jsonValueSet(o.Value);
						}
						objectNest--;
						break;
					default:
						jsonValueSet(reader.Value);
						break;
				}
			} while (!(objectNest == 0 && arrayNest == 0) && reader.Read());
			return result;
		}

		public void WriteJson(IJsonWriter writer, object obj)
		{
			writeJson(writer, (Json)obj);
		}

		private static void writeJson(IJsonWriter writer, Json json)
		{
			if (json.Value is Json)
			{
				writeJson(writer, json.Value as Json);
			}
			else if (json.Value is List<Json>)
			{
				writeJsonArray(writer, json);
			}
			else if (json.Value is Dictionary<string, Json>)
			{
				writeJsonObject(writer, json);
			}
			else if (json.Value is string)
			{
				writer.Write(JsonTokenType.String, (string)json.Value);
			}
			else if (json.Value is char)
			{
				writer.Write(JsonTokenType.String, ((char)json.Value).ToString());
			}
			else if (json.Value is sbyte)
			{
				writer.Write(JsonTokenType.Integer, Convert.ToDecimal(json.Value));
			}
			else if (json.Value is byte)
			{
				writer.Write(JsonTokenType.Integer, Convert.ToDecimal(json.Value));
			}
			else if (json.Value is short)
			{
				writer.Write(JsonTokenType.Integer, Convert.ToDecimal(json.Value));
			}
			else if (json.Value is ushort)
			{
				writer.Write(JsonTokenType.Integer, Convert.ToDecimal(json.Value));
			}
			else if (json.Value is int)
			{
				writer.Write(JsonTokenType.Integer, Convert.ToDecimal(json.Value));
			}
			else if (json.Value is uint)
			{
				writer.Write(JsonTokenType.Integer, Convert.ToDecimal(json.Value));
			}
			else if (json.Value is long)
			{
				writer.Write(JsonTokenType.Integer, Convert.ToDecimal(json.Value));
			}
			else if (json.Value is ulong)
			{
				writer.Write(JsonTokenType.Integer, Convert.ToDecimal(json.Value));
			}
			else if (json.Value is decimal)
			{
				writer.Write(JsonTokenType.Integer, Convert.ToDecimal(json.Value));
			}
			else if (json.Value is double)
			{
				writer.Write(JsonTokenType.Float, Convert.ToDecimal(json.Value));
			}
			else if (json.Value is float)
			{
				writer.Write(JsonTokenType.Float, Convert.ToDecimal(json.Value));
			}
			else if (json.Value is bool)
			{
				writer.Write(JsonTokenType.Boolean, (bool)json.Value);
			}
			else if (json.Value == null)
			{
				writer.Write(JsonTokenType.Null);
			}
			else
			{
				ApplicationFactory.JsonSerializer.Serialize(writer, json.Value);
			}
		}

		private static void writeJsonArray(IJsonWriter writer, Json json)
		{
			writer.Write(JsonTokenType.StartArray);
			foreach (Json j in json)
			{
				writeJson(writer, j);
			}
			writer.Write(JsonTokenType.EndArray);
		}

		private static void writeJsonObject(IJsonWriter writer, Json json)
		{
			writer.Write(JsonTokenType.StartObject);
			foreach (KeyValuePair<string, Json> sj in json.GetKeyValuePairEnumerator())
			{
				writer.Write(JsonTokenType.PropertyName, sj.Key);
				writeJson(writer, sj.Value);
			}
			writer.Write(JsonTokenType.EndObject);
		}

	}
}
