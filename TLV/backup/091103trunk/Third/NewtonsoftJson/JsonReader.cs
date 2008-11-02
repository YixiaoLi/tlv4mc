using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NU.OJL.MPRTOS.TLV.Base;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Converters;

namespace NU.OJL.MPRTOS.TLV.Third
{
	public class JsonReader : IJsonReader
	{
		private Newtonsoft.Json.JsonReader _reader;

		public JsonReader(Newtonsoft.Json.JsonReader reader)
		{
			_reader = reader;
		}

		public bool Read()
		{
			return _reader.Read();
		}

		public void Skip()
		{
			_reader.Skip();
		}

		public NU.OJL.MPRTOS.TLV.Base.JsonTokenType TokenType
		{
			get
			{
				switch(_reader.TokenType)
				{
					case JsonToken.StartObject:
						return NU.OJL.MPRTOS.TLV.Base.JsonTokenType.StartObject;
					case JsonToken.StartArray:
						return NU.OJL.MPRTOS.TLV.Base.JsonTokenType.StartArray;
					case JsonToken.PropertyName:
						return NU.OJL.MPRTOS.TLV.Base.JsonTokenType.PropertyName;
					case JsonToken.Integer:
						return NU.OJL.MPRTOS.TLV.Base.JsonTokenType.Integer;
					case JsonToken.Float:
						return NU.OJL.MPRTOS.TLV.Base.JsonTokenType.Float;
					case JsonToken.String:
						return NU.OJL.MPRTOS.TLV.Base.JsonTokenType.String;
					case JsonToken.Boolean:
						return NU.OJL.MPRTOS.TLV.Base.JsonTokenType.Boolean;
					case JsonToken.Null:
						return NU.OJL.MPRTOS.TLV.Base.JsonTokenType.Null;
					case JsonToken.EndObject:
						return NU.OJL.MPRTOS.TLV.Base.JsonTokenType.EndObject;
					case JsonToken.EndArray:
						return NU.OJL.MPRTOS.TLV.Base.JsonTokenType.EndArray;
					case JsonToken.Date:
						return NU.OJL.MPRTOS.TLV.Base.JsonTokenType.Date;
					default:
						return NU.OJL.MPRTOS.TLV.Base.JsonTokenType.Undefined;
				}
			}
		}

		public object Value
		{
			get { return _reader.Value; }
		}
	}
}
