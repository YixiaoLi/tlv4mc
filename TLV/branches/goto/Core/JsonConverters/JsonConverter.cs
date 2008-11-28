using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NU.OJL.MPRTOS.TLV.Base;

namespace NU.OJL.MPRTOS.TLV.Core
{
	public class JsonConverter : GeneralConverter<Json>
	{
		public override object ReadJson(IJsonReader reader)
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

			if (objectNest != 0)
				throw new Exception("Jsonの記述に誤りがあります。\n{と}の対応が取れていません。");

			if (arrayNest != 0)
				throw new Exception("Jsonの記述に誤りがあります。\n[と]の対応が取れていません。");

			return result;
		}

		protected override void WriteJson(IJsonWriter writer, Json json)
		{
			if (json.Value is Json)
			{
				WriteJson(writer, (Json)json.Value);
			}
			else if (json.Value is List<Json>)
			{
				writer.WriteArray(w =>
					{
						foreach (Json j in json)
						{
							WriteJson(w, j);
						}
					});
			}
			else if (json.Value is Dictionary<string, Json>)
			{
				writer.WriteObject(w =>
				{
					foreach (KeyValuePair<string, Json> sj in json.GetKeyValuePairEnumerator())
					{
						w.WriteProperty( sj.Key);
						WriteJson(w, sj.Value);
					}
				});
			}
			else
			{
				writer.WriteValue(json.Value);
			}
		}
	}
}

