﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Converters;
using NU.OJL.MPRTOS.TLV.Base;

namespace NU.OJL.MPRTOS.TLV.Third
{
	public class JsonValueConverter : JsonConverter
	{
		public override bool CanConvert(Type objectType)
		{
			return objectType.IsAssignableFrom(typeof(Json));
		}

		public override object ReadJson(Newtonsoft.Json.JsonReader reader, Type objectType)
		{
			Stack<Json> stack = new Stack<Json>();
			Stack<string> keyStack = new Stack<string>();
			Json result = null;
			int objectNest = 0;
			int arrayNest = 0;

			Action<object> jsonValueSet = (object value) =>
				{
					if (result.Value is List<Json>)
					{
						((List<Json>)result.Value).Add(new Json(value));
					}
					else if (result.Value is Dictionary<string, Json>)
					{
						((Dictionary<string, Json>)result.Value).Add(keyStack.Pop(), new Json(value));
					}
				};

			do
			{
				switch (reader.TokenType)
				{
					case JsonToken.StartArray:
						if (result != null)
							stack.Push(result);
						result = new Json(new List<Json>());
						arrayNest++;
						break;
					case JsonToken.StartObject:
						if(result != null)
							stack.Push(result);
						result = new Json(new Dictionary<string, Json>());
						objectNest++;
						break;
					case JsonToken.PropertyName:
						keyStack.Push(reader.Value as string);
						break;
					case JsonToken.EndArray:
						if (stack.Count != 0)
						{
							Json a = result;
							result = stack.Pop();
							jsonValueSet(a.Value);
						}
						arrayNest--;
						break;
					case JsonToken.EndObject:
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

		public override void WriteJson(Newtonsoft.Json.JsonWriter writer, object value)
		{
			writeJson(writer, value as Json);
		}

		private static void writeJson(Newtonsoft.Json.JsonWriter writer, Json json)
		{
			if (json.Value is List<Json>)
			{
				writeJsonArray(writer, json);
			}

			if (json.Value is Dictionary<string, Json>)
			{
				writeJsonObject(writer, json);
			}

			if (json.Value is string)
			{
				writer.WriteValue((string)json.Value);
			}
			if (json.Value is char)
			{
				writer.WriteValue((char)json.Value);
			}
			if (json.Value is sbyte)
			{
				writer.WriteValue((sbyte)json.Value);
			}
			if (json.Value is byte)
			{
				writer.WriteValue((byte)json.Value);
			}
			if (json.Value is short)
			{
				writer.WriteValue((short)json.Value);
			}
			if (json.Value is ushort)
			{
				writer.WriteValue((ushort)json.Value);
			}
			if (json.Value is int)
			{
				writer.WriteValue((int)json.Value);
			}
			if (json.Value is uint)
			{
				writer.WriteValue((uint)json.Value);
			}
			if (json.Value is long)
			{
				writer.WriteValue((long)json.Value);
			}
			if (json.Value is ulong)
			{
				writer.WriteValue((ulong)json.Value);
			}
			if (json.Value is decimal)
			{
				writer.WriteValue((decimal)json.Value);
			}
			if (json.Value is double)
			{
				writer.WriteValue((double)json.Value);
			}
			if (json.Value is float)
			{
				writer.WriteValue((float)json.Value);
			}
			if (json.Value is DateTime)
			{
				writer.WriteValue((DateTime)json.Value);
			}
			if (json.Value is bool)
			{
				writer.WriteValue((bool)json.Value);
			}
			if (json.Value == null)
			{
				writer.WriteNull();
			}
		}

		private static void writeJsonArray(Newtonsoft.Json.JsonWriter writer, Json json)
		{
			writer.WriteStartArray();
			foreach (Json j in json)
			{
				writeJson(writer, j);
			}
			writer.WriteEndArray();
		}

		private static void writeJsonObject(Newtonsoft.Json.JsonWriter writer, Json json)
		{
			writer.WriteStartObject();
			foreach (KeyValuePair<string,Json> sj in json.GetKeyValuePaierEnumerator())
			{
				writer.WritePropertyName(sj.Key);
				writeJson(writer, sj.Value);
			}
			writer.WriteEndObject();
		}
	}
}