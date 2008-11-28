using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NU.OJL.MPRTOS.TLV.Base;
using System.Reflection;
using System.Collections;

namespace NU.OJL.MPRTOS.TLV.Core
{
	public class INamedConverter : GeneralConverter<INamed>
	{
		private Stack<Type> _types = new Stack<Type>();

		public override bool CanConvert(Type type)
		{
			if (base.CanConvert(type))
			{
				_types.Push(type);
				return true;
			}
			else
			{
				return false;
			}
		}

		protected override void WriteJson(IJsonWriter writer, INamed obj)
		{
			if (isCollection(obj.GetType()))
			{
				writer.WriteArray(w =>
					{
						foreach (object o in (IList)obj)
						{
							w.WriteValue(o, ApplicationFactory.JsonSerializer);
						}
					});
			}
			else
			{
				writer.WriteObject(w =>
					{
						foreach (PropertyInfo pi in obj.GetType().GetProperties())
						{
							if (pi.Name != "Name")
							{
								w.WriteProperty(pi.Name);
								w.WriteValue(pi.GetValue(obj, null), ApplicationFactory.JsonSerializer);
							}
						}
					});
			}
		}

		public override object ReadJson(IJsonReader reader)
		{
			Type type = _types.Pop();
			object obj;

			if (isCollection(type))
			{
				List<object> list = new List<object>();
				while (reader.TokenType != JsonTokenType.EndArray)
				{
					Type t = getCollectionType(type).GetGenericArguments()[0];
					list.Add(ApplicationFactory.JsonSerializer.Deserialize(reader, t));
					reader.Read();
				}
				obj = Activator.CreateInstance(type);
				foreach(object o in list)
				{
					((IList)obj).Add(o);
				}
			}
			else
			{
				obj = Activator.CreateInstance(type);
				while (reader.TokenType != JsonTokenType.EndObject)
				{
					if (reader.TokenType == JsonTokenType.PropertyName)
					{
						string key = (string)reader.Value;

						PropertyInfo pi = type.GetProperties().Single<PropertyInfo>(p => p.Name == key);

						object o = ApplicationFactory.JsonSerializer.Deserialize(reader, pi.PropertyType);

						pi.SetValue(obj, o, null);
					}
					reader.Read();
				}
			}

			return obj;
		}

		private bool isCollection(Type type)
		{
			if (type.IsGenericType && typeof(ICollection).IsAssignableFrom(type))
			{
				return true;
			}
			else if (type != typeof(object))
			{
				return isCollection(type.BaseType);
			}
			else
			{
				return false;
			}
		}

		private Type getCollectionType(Type type)
		{
			if (type.IsGenericType && typeof(ICollection).IsAssignableFrom(type))
				return type;
			else
				return getCollectionType(type.BaseType);
		}
	}
}
