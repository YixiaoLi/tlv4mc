
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NU.OJL.MPRTOS.TLV.Base;
using System.Reflection;

namespace NU.OJL.MPRTOS.TLV.Core
{
	public class ClassHavingNullablePropertyConverter : GeneralConverter<IHavingNullableProperty>
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

		public override object ReadJson(IJsonReader reader)
		{
			Type type = _types.Pop();
			object obj = Activator.CreateInstance(type);

			while (reader.TokenType != JsonTokenType.EndObject)
			{
				if (reader.TokenType == JsonTokenType.PropertyName)
				{
					string key = (string)reader.Value;

					PropertyInfo pi = type.GetProperties(BindingFlags.Instance | BindingFlags.Public).Single<PropertyInfo>(p => p.Name == key);

					object o = ApplicationFactory.JsonSerializer.Deserialize(reader, pi.PropertyType);

					pi.SetValue(obj, o, null);
				}
				reader.Read();
			}

			return obj;
		}

		protected override void WriteJson(IJsonWriter writer, IHavingNullableProperty obj)
		{
			writer.WriteObject(w =>
			{
				foreach (PropertyInfo pi in obj.GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public))
				{
					object value = pi.GetValue(obj, null);

					if (value != null)
					{
						w.WriteProperty(pi.Name);
						w.WriteValue(value, ApplicationFactory.JsonSerializer);
					}
				}
			});
		}
	}
}
