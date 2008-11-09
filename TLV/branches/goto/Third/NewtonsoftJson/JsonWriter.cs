﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NU.OJL.MPRTOS.TLV.Base;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Converters;

namespace NU.OJL.MPRTOS.TLV.Third
{
	public class JsonWriter : IJsonWriter
	{
		private Newtonsoft.Json.JsonTextWriter _writer;

		public JsonWriter(Newtonsoft.Json.JsonTextWriter writer)
		{
			_writer = writer;
			_writer.Formatting = Formatting.Indented;
		}

		public static implicit operator Newtonsoft.Json.JsonTextWriter(JsonWriter writer)
		{
			return writer._writer;
		}

		public void Write(object obj)
		{
			_writer.WriteValue(obj);
		}

		public void Write(NU.OJL.MPRTOS.TLV.Base.JsonTokenType type)
		{
			Write(type, null);
		}

		public void  Write(NU.OJL.MPRTOS.TLV.Base.JsonTokenType type, object value)
		{
			switch (type)
			{
				case NU.OJL.MPRTOS.TLV.Base.JsonTokenType.StartObject:
					_writer.WriteStartObject();
					break;
				case NU.OJL.MPRTOS.TLV.Base.JsonTokenType.StartArray:
					_writer.WriteStartArray();
					break;
				case NU.OJL.MPRTOS.TLV.Base.JsonTokenType.PropertyName:
					_writer.WritePropertyName((string)value);
					break;
				case NU.OJL.MPRTOS.TLV.Base.JsonTokenType.Decimal:
					_writer.WriteValue((decimal)value);
					break;
				case NU.OJL.MPRTOS.TLV.Base.JsonTokenType.String:
					_writer.WriteValue((string)value);
					break;
				case NU.OJL.MPRTOS.TLV.Base.JsonTokenType.Boolean:
					_writer.WriteValue((bool)value);
					break;
				case NU.OJL.MPRTOS.TLV.Base.JsonTokenType.Null:
					_writer.WriteNull();
					break;
				case NU.OJL.MPRTOS.TLV.Base.JsonTokenType.EndObject:
					_writer.WriteEndObject();
					break;
				case NU.OJL.MPRTOS.TLV.Base.JsonTokenType.EndArray:
					_writer.WriteEndArray();
					break;
				case NU.OJL.MPRTOS.TLV.Base.JsonTokenType.Raw:
					_writer.WriteRawValue((string)value);
					break;
				default:
					break;
			}
		}
	}
}
