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
	public class JsonWriter : IJsonWriter
	{
		private Newtonsoft.Json.JsonWriter _writer;

		public Newtonsoft.Json.JsonWriter Writer { get { return _writer; } }

		public JsonWriter( Newtonsoft.Json.JsonWriter writer)
		{
			_writer = writer;
		}

		public void WriteValue(object obj, IJsonSerializer serializer)
		{
			serializer.Serialize(this, obj);
		}

		public void WriteValue(object obj)
		{
			_writer.WriteValue(obj);
		}

		public void WriteProperty(string name)
		{
			_writer.WritePropertyName(name);
		}

		public void WriteArray(Action<IJsonWriter> contents)
		{
			_writer.WriteStartArray();
			contents(this);
			_writer.WriteEndArray();
		}

		public void WriteObject(Action<IJsonWriter> contents)
		{
			_writer.WriteStartObject();
			contents(this);
			_writer.WriteEndObject();
		}

	}
}
