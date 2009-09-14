
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NU.OJL.MPRTOS.TLV.Base;

namespace NU.OJL.MPRTOS.TLV.Core
{
	public class GeneralNamedCollectionConverter : IJsonConverter
	{
		public Type Type { get { return typeof(GeneralNamedCollection<>); } }
		
		private Stack<Type> _types = new Stack<Type>();

		public bool CanConvert(Type type)
		{
			if ((type.IsGenericType && type.GetGenericTypeDefinition() == Type)
				|| (type.BaseType.IsGenericType && type.BaseType.GetGenericTypeDefinition() == Type))
			{
				_types.Push(type);
				return true;
			}
			else
			{
				return false;
			}
		}

		public void WriteJson(IJsonWriter writer, object obj)
		{
			writer.WriteObject(w =>
				{
					foreach (INamed t in ((INamedCollection)obj).GetINameEnumerator())
					{
						w.WriteProperty(t.Name);
						w.WriteValue(t, ApplicationFactory.JsonSerializer);
					}
				});
		}

		public object ReadJson(IJsonReader reader)
		{
			Type type = _types.Pop();

			INamedCollection gnc = (INamedCollection)Activator.CreateInstance(type);

			if (type.BaseType.IsGenericType && type.BaseType.GetGenericTypeDefinition() == Type)
				type = type.BaseType;

			while (reader.TokenType != JsonTokenType.EndObject)
			{
				if (reader.TokenType == JsonTokenType.PropertyName)
				{
					string key = (string)reader.Value;

					INamed obj = (INamed)ApplicationFactory.JsonSerializer.Deserialize(reader, type.GetGenericArguments()[0]);
					obj.Name = key;
					gnc.Add(obj);
				}

				reader.Read();
			}

			return gnc;
		}
	}
}
